var vColumnNames_DJ = [];
var vColumnModel_DJ = [];
//分单信息
var itemFD = [];
//分单数据明细
var itemGZSD = [];
//分单规则明细
var itemGZITEM = [];
var fdindex = 0;
//var curEditcell;
var gzdialog = "";
var bYXD = IsNullValue(GetUrlParam("yx"), "0");
var lx = 0;
var djlx = 0;

$(document).ready(function () {
    $("#B_UnExec").css("width", "96px");
    addPanel();
    //生成左边列表
    MakeLeftPanel();
    $("#YHQPanel").hide();
    //生成右边输入框
    MakeRightPanel();
    $("#status-bar").before('<div class="gztoolbar"></div>');
    $("#status-bar").before('<div id="tab-tools"></div>');
    $("#status-bar").before('<table id="list"></table>');
    $(".gztoolbar").append('<button id="AddItem" type="button" class="item_addtoolbar">添加</button>');
    $(".gztoolbar").append('<button id="DelItem" type="button" class="item_deltoolbar">删除</button>');
    $("#tab-tools").append('<a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:\'icon-add\'" onclick="addPanel()">添加分单</a>');
    $.parser.parse("#tab-tools");
    FillSH($("#S_SH"));
    //没有也不会报错
    BFButtonClick("TB_HYKNAME", function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", true);
    })
    BFButtonClick("TB_CXZT", function () {
        SelectCXHD("TB_CXZT", "HF_CXID", "zHF_CXID", false);
    });
    //BFButtonClick("TB_YHQMC", function () {
    //    var conData = new Object();
    //    //conData.iBJ_TS = lx;
    //    SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", false, conData);
    //});

    var winHeight = $(window).height();
    var bodyHeight = $(document.body).outerHeight(true);
    DrawGrid("list", vColumnNames, vColumnModel, false, 40 + (winHeight - (bodyHeight + 50)));
    //$("#TreePanel").height(winHeight - 93);
    $("#list").datagrid("loadData", itemGZSD.filter(function (item) { return item.iINX == 0; }));
    RefreshButtonSep();

    vColumnNames_DJ = ["记录编号", "开始时间", "结束时间"];
    vColumnModel_DJ = [
        { name: "iJLBH", width: 40, },
        { name: "dRQ1", width: 45, },
        { name: "dRQ2", width: 70, },
    ];

    DrawGrid("list_dj", vColumnNames_DJ, vColumnModel_DJ, true, 250 + (winHeight - (bodyHeight + 128)));

    $("#LB_CXSDSTR").click(function () {
        var dialogUrl = "../../CrmArt/TimeSet/CrmArt_TimeSet.aspx";
        $.dialog.data('SetDialogTimeEnable', vProcStatus != cPS_BROWSE)
        $.dialog.data('DialogTimeData', $("#HF_CXSD").val());
        //dialogUrl = dialogUrl.replace("../..", sCRMPATH + "/CRMWEB");
        $.dialog.open(dialogUrl, {
            lock: true, width: 810, height: 300, cancel: false,
            drag: true, fixed: false,
            close: function () {
                if ($.dialog.data('DialogTimeData')) {
                    $("#LB_CXSDSTR").text($.dialog.data('DialogTimeDataStr'));
                    $("#HF_CXSD").val($.dialog.data('DialogTimeData'));
                }
                $.dialog.data('DialogTimeData', "");
            }
        }, false);
    });
    $('#tt').tabs({
        onSelect: function (title, tab_index) {
            //保存前分单数据明细
            var preTabData = $("#list").datagrid("getData").rows;
            if (preTabData.length) {
                removeRecord(preTabData[0].iINX);
                addRecord(preTabData);
            }
            $("#list").datagrid("loadData",
               itemGZSD.filter(function (item) {
                   return item.iINX == tab_index;
               }));
            var curTabInfo = itemFD.filter(function (item) {
                return item.iINX == tab_index;
            })
            //分单没日期的也不会报错
            $("#TB_FDRQ1").val("");
            $("#TB_FDRQ2").val("");
            $("#HF_CXSD").val("");
            $("#LB_CXSDSTR").text("");
            if (curTabInfo.length) {
                $("#TB_FDRQ1").val(curTabInfo[0].dKSRQ);
                $("#TB_FDRQ2").val(curTabInfo[0].dJSRQ);
                $("#HF_CXSD").val(curTabInfo[0].sSD);
                if (curTabInfo[0].sSD.length > 0) {
                    $("#LB_CXSDSTR").text(DatasToChinese(curTabInfo[0].sSD))
                }
            }
        },
        onClose: function (title, index) {
            removeRecord(index);
            itemGZITEM = itemGZITEM.filter(function (item) {
                return item.iINX != index;
            })
        }
    });
    $("#AddItem").click(function () {
        var tab = $('#tt').tabs('getSelected');
        var tab_index = $('#tt').tabs('getTabIndex', tab);
        var rowDatas = $("#list").datagrid("getData").rows;
        var rowIndex = rowDatas.length;
        var tp_max = 0;
        if (rowIndex > 0) {
            for (var i = 0; i < rowIndex; i++) {
                var rowdata = rowDatas[i];
                if (parseInt(rowdata.iGZBH) >= tp_max) {
                    tp_max = rowdata.iGZBH;
                }
            }
        }
        AddNewFDPanel(tab_index, rowIndex, tp_max);
    });
    $("#DelItem").click(function () {
        var rows = $('#list').datagrid("getSelections");
        var tab = $('#tt').tabs('getSelected');
        var tab_index = $('#tt').tabs('getTabIndex', tab);
        var copyRows = [];
        for (var j = 0; j < rows.length; j++) {
            copyRows.push(rows[j]);
        }
        for (var i = 0; i < copyRows.length; i++) {
            var index = $('#list').datagrid('getRowIndex', copyRows[i]);
            var row_gzbh = copyRows[i].iGZBH;
            if (row_gzbh) {
                itemGZITEM = itemGZITEM.filter(function (item) {
                    return !(item.iINX == tab_index && item.iGZBH == row_gzbh);
                })
            }
            $('#list').datagrid('deleteRow', index);
        }
        $('#list').datagrid('clearSelections');
        //修改优先序列
        var rowDatas = $("#list").datagrid("getData").rows;
        if (rowDatas.length > 0) {
            for (var i = 0; i < rowDatas.length; i++) {
                var rowIndex = $("#list").datagrid("getRowIndex", rowDatas[i]);
                rowDatas[i].iXH = rowIndex + 1;
                $('#list').datagrid('refreshRow', rowIndex);
            }
        }
    });
    SetControlBaseState();
});

function AddNewFDPanel(tab_index, rowIndex, tp_max) {

}

function MakeLeftPanel() {
    //商户
    $("#TreePanel").append('<div class="bfrow"><div class="bffld_l4"><div class="bffld_right"><select id="S_SH" class="easyui-combobox" style="color: #a9a9a9"></select></div></div></div>');
    //部门
    $("#TreePanel").append('<div class="bfrow"><div class="bffld_l4"><div class="bffld_right"><input id="TB_BM" type="text" placeholder="选择部门" /><input id="HF_SHDM" type="hidden" /><input id="HF_BMDM" type="hidden" /><input id="HF_SHBMID" type="hidden" /></div></div></div>');
    //优惠券，只在发券用券显示
    $("#TreePanel").append('<div class="bfrow" id="YHQPanel"><div class="bffld_l4"><div class="bffld_right"><input id="TB_YHQMC" type="text" placeholder="选择优惠券" /><input id="HF_YHQID" type="hidden" /><input id="zHF_YHQID" type="hidden" /></div></div></div>');
    //优先单
    $("#TreePanel").append('<div class="bfrow"><div class="bffld_l4"><div class="bffld_right">'
        + '<input class="magic-radio" type="radio" name="YXD" id="CK_PT" value="0" onclick="SearchDJList()" checked="checked"/><label for="CK_PT">普通单</label>'
        + '<input class="magic-radio" type="radio" name="YXD" id="CK_YX" value="1" onclick="SearchDJList()" /><label for="CK_YX">优先单</label></div></div></div>');
    //列表
    $("#TreePanel").append('<div class="bfrow"><div class="bffld_l4"><div class="bffld_left">部门已有单据</div></div></div><table id="list_dj"></table>');
    if (bYXD == "1")
        $("#CK_YX")[0].checked = true;
}

function MakeRightPanel() {
    //商户部门
    $("#jlbh").after('<div class="bffld"><div class="bffld_left">商户部门</div><div class="bffld_right"><label id="LB_SHBM" style="text-align: left;"/></div></div>');
    //日期范围，MainPanel的第一个bfrow的后边加   //.bfrow:first
    $("#MainPanel .bfrow:eq(1)").append('<div class="bfrow"><div class="bffld"><div class="bffld_left">开始日期</div><div class="bffld_right"><input id="TB_RQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" /></div></div>'
        + '<div class="bffld"><div class="bffld_left">结束日期</div><div class="bffld_right"><input id="TB_RQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" /></div></div></div>');
}

function TreeNodeClickCustom(e, treeId, treeNode) {
    if (treeId == "TreeSHBM") {
        $("#TB_BM").val(treeNode.sBMMC);
        $("#LB_SHBM").text(treeNode.sBMMC);
        $("#HF_BMDM").val(treeNode.sBMDM);
        $("#HF_SHBMID").val(treeNode.iSHBMID);
        $("#HF_SHDM").val($('#S_SH').combobox('getValue'));
        SearchDJList();
    }
}

function SearchDJList() {
    if ($("#HF_SHDM").val() == "")
        return;
    if ($("#HF_BMDM").val() == "")
        return;
    if ($("#YHQPanel")[0].style.display != "none" && $("#HF_YHQID").val() == "")
        return;
    SearchData();
}

function selComSH(record) {
    if (record.value != "")
        FillSHBMTreeBase("TreeSHBM", "TB_BM", record.value, 4);
}

function OnClickRow(rowIndex, rowData) {
    if (rowData.iJLBH != "" && rowData.iJLBH != undefined && vProcStatus == cPS_BROWSE) {
        ShowDataBase(rowData.iJLBH);
    }
}

//查询现有单据
function SearchData(page, rows, sort, order) {
    var obj = MakeSearchJSON();
    //page页码,rows每页行数,sort排序字段,order排序方式
    page = page || $('#list_dj').datagrid("options").pageNumber;
    rows = rows || $('#list_dj').datagrid("options").pageSize;
    sort = sort || $('#list_dj').datagrid("options").sortName;
    order = order || $('#list_dj').datagrid("options").sortOrder;
    $('#list_dj').datagrid("loading");
    $.ajax({
        type: "post",
        url: vUrl + "?mode=Search&func=" + vPageMsgID,
        async: true,
        data: {
            json: JSON.stringify(obj),
            titles: 'cybillsearch',
            page: page,
            rows: rows,
            sort: sort,
            order: order,
        },
        success: function (data) {
            if (data.indexOf('错误') >= 0 || data.indexOf('error') >= 0) {
                ShowMessage(data);
            }
            $('#list_dj').datagrid('loadData', JSON.parse(data), "json");
            $('#list_dj').datagrid("loaded");
            vSearchData = data;
        },
        error: function (data) {
            ShowMessage(data);
        }
    });

}

function MakeSearchJSON() {
    var cond = MakeSearchCondition();
    if (cond == null)
        return;
    var Obj = new Object();
    Obj.SearchConditions = cond;
    Obj.iLoginRYID = iDJR;
    return Obj;
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_SHDM", "sSHDM", "=", true);
    MakeSrchCondition(arrayObj, "HF_BMDM", "sBMDM", "like", true);
    MakeSrchCondition(arrayObj, "HF_YHQID", "iYHQID", "=", false);//没有优惠券的也无所谓，因为查询条件也没有iYHQID
    MakeSrchCondition2(arrayObj, $("#CK_YX")[0].checked ? 1 : 0, "iYXCLBJ", "=", false);
    return arrayObj;
}

function removeRecord(tabIndex) {
    itemGZSD = itemGZSD.filter(function (item) {
        return item.iINX != tabIndex;
    });
    itemFD = itemFD.filter(function (item) {
        return item.iINX != tabIndex;
    })
}

function addRecord(tabData) {
    itemGZSD = itemGZSD.concat(tabData);
    var values = "111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111";
    itemFD.push(
        {
            iINX: tabData[0].iINX,
            dKSRQ: $("#TB_FDRQ1").val(),
            dJSRQ: $("#TB_FDRQ2").val(),
            sSD: $("#HF_CXSD").val() == "" ? values : $("#HF_CXSD").val(),
        });
}

function addPanel() {
    fdindex++;
    $('#tt').tabs('add', {
        title: '分单' + fdindex,
        content: '<div style="padding:10px">Content' + fdindex + '</div>',
        closable: true,
    });
}

function removePanel() {
    var tab = $('#tt').tabs('getSelected');
    if (tab) {
        var index = $('#tt').tabs('getTabIndex', tab);
        $('#tt').tabs('close', index);
    }
}

function SearchListData() {
    setTimeout('SearchData()', 100);
}

function InsertClickCustom() {
    if ($("#TB_BM").val() == "") {
        ShowMessage("请选择商户部门");
        vProcStatus = cPS_BROWSE;
        SetControlBaseState();
    }
    else if ($("#YHQPanel")[0].style.display != "none" && $("#HF_YHQID").val() == "") {
        ShowMessage("请选择优惠券");
        vProcStatus = cPS_BROWSE;
        SetControlBaseState();
    }
    else {
        $("#LB_SHBM").text($("#TB_BM").val());
        $("#LB_CXSDSTR").text("请选择促销时段");
        itemFD = [];
        itemGZITEM = [];
        itemGZSD = [];
        var tabs = $('#tt').tabs('tabs');
        while (tabs.length != 1) {
            var delTabInx = $('#tt').tabs('getTabIndex', tabs[tabs.length - 1]);
            $('#tt').tabs('close', delTabInx);
        }
        fdindex = 1;
    }
}

//function IsValidData() {
//    if ($("#TB_RQ1").val() == "" || $("#TB_RQ2").val() == "") {
//        ShowMessage("请输入日期范围");
//        return false;
//    }
//    return IsValidData2();
//}

//function IsValidData2() {
//    return true;
//}

function SetControlState() {
    $("#B_UnExec").show();
    $("#B_Start").show();
    $("#B_Stop").show();
    $("#QDR").show();
    $("#QDSJ").show();
    $("#ZZR").show();
    $("#ZZSJ").show();
    var Element = document.getElementById("TreePanel");
    DisableCtrls(Element, vProcStatus != cPS_BROWSE);
    $("#tab-tools a").prop("disabled", vProcStatus == cPS_BROWSE);
    var tabs = $('#tt').tabs('tabs');
    for (var i = 0; i < tabs.length; i++) {
        $('#tt').tabs('update', {
            tab: tabs[i],
            options: {
                closable: vProcStatus != cPS_BROWSE,
            }
        });
    }
};

function SaveDataBase() {
    var Obj = new Object();
    Obj = SaveData(Obj);
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sSHDM = $("#HF_SHDM").val();
    Obj.sSHBMDM = $("#HF_BMDM").val();
    Obj.iSHBMID = $("#HF_SHBMID").val();
    Obj.dRQ1 = $("#TB_RQ1").val();
    Obj.dRQ2 = $("#TB_RQ2").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    Obj.iLoginPUBLICID = iPID;
    return Obj;
};

function onClickCell(index, field) {
    var tab = $('#tt').tabs('getSelected');
    var tab_index = $('#tt').tabs('getTabIndex', tab);
    if (endEditing() && vProcStatus != cPS_BROWSE) {
        $('#list').datagrid('selectRow', index)
        .datagrid('editCell', { index: index, field: field });
        editIndex = index;
        var ed = $(this).datagrid('getEditor', { index: index, field: field });
        if (ed) {
            $(ed.target).bind("keypress", function (event) {
                if (event.keyCode == 13) {
                    if ($('#list').datagrid('validateRow', editIndex)) {
                        $('#list').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return true;
                    }
                }
            })
        }
        //if (field == "sCLDX") {
        //    var rowData = $("#list").datagrid("getSelected");
        //    if (rowData.iGZBH) {
        //        //curEditcell = ed.target;
        //        //SelectSPSB("", "", "", false);
        //        var conData = new Object();
        //        conData["SHDM"] = $("#HF_SHDM").val();
        //        conData["SearchEnable"] = true;
        //        var data = itemGZITEM.filter(function (item) {
        //            return (item.iINX == tab_index && item.iGZBH == rowData.iGZBH);
        //        })
        //        SelectCLDX("", "", data, false, conData);
        //    }
        //}
        if (field == "sGZMC") {
            var rowData = $("#list").datagrid("getSelected");
            if (rowData.iGZBH) {
                //curEditcell = ed.target;
                //SelectSPSB("", "", "", false);
                var conData = new Object();
                //conData["SHDM"] = $("#HF_SHDM").val();
                //conData["SearchEnable"] = true;
                conData["Mode"] = gzdialog;
                conData["iBJ_TS"] = lx;
                SelectDYDGZ("", "", "", false, conData);
            }
        }
    }
    if (field == "sCLDX") {
        var rowData = $("#list").datagrid("getRows")[index];
        if (rowData.iGZBH) {
            var conData = new Object();
            conData["SHDM"] = $("#HF_SHDM").val();
            conData["BMDM"] = $("#HF_BMDM").val();
            conData["SearchEnable"] = vProcStatus != cPS_BROWSE;
            var data = itemGZITEM.filter(function (item) {
                return (item.iINX == tab_index && item.iGZBH == rowData.iGZBH);
            })
            SelectCLDX("", "", data, false, conData);
        }
    }
    onClickCellCustom(index, field);
}

function onClickCellCustom(index, field) {

}
function MoseDialogCustomerReturn(dialogName, lstData, showField) {
    if (dialogName == "ArtCLDX") {
        var tab = $('#tt').tabs('getSelected');
        var tab_index = $('#tt').tabs('getTabIndex', tab);
        var row_gzbh = $("#list").datagrid("getData").rows[editIndex].iGZBH;
        itemGZITEM = itemGZITEM.filter(function (item) {
            return !(item.iINX == tab_index && item.iGZBH == row_gzbh);
        })
        var item_sjlx = new Array();
        for (var i = 0; i < lstData.length; i++) {
            itemGZITEM.push({
                iINX: tab_index,
                iGZBH: row_gzbh,
                iSJLX: lstData[i].iSJLX,
                iSJNR: lstData[i].iSJNR,
                iXH: i
            })
            if ((item_sjlx.indexOf(lstData[i].iSJLX) === -1)) {
                item_sjlx.push(lstData[i].iSJLX);
            }
        }
        var sCLDX = "指定";
        var clfs = new Object();
        if (item_sjlx.length > 0) {
            for (var i = 0; i < item_sjlx.length; i++) {
                switch (item_sjlx[i]) {
                    case 1: sCLDX += "部门、"; clfs.iCLFS_BM = 1; break;
                    case 2: sCLDX += "合同、"; clfs.iCLFS_HT = 1; break;
                    case 3: sCLDX += "分类、"; clfs.iCLFS_SPFL = 1; break;
                    case 4: sCLDX += "品牌、"; clfs.iCLFS_SPSB = 1; break;
                    case 5: sCLDX += "会员组、"; clfs.iCLFS_HYZ = 1; break;
                    case 6: sCLDX += "商品、"; clfs.iCLFS_SP = 1; break;
                }
            }
        }
        var updateIndex;
        //$(curEditcell).val(sCLDX.substr(0, sCLDX.length - 1));
        //$("#list").datagrid("getRows")[editIndex].sCLDX = sCLDX.substr(0, sCLDX.length - 1);
        if ($('#list').datagrid('validateRow', editIndex)) {
            $('#list').datagrid('endEdit', editIndex);
            updateIndex = editIndex;
            editIndex = undefined;
        }
        var rows = $("#list").datagrid("getData").rows;
        var rowData = rows[updateIndex];
        rows[updateIndex].iCLFS_BM = clfs.iCLFS_BM == undefined ? 0 : clfs.iCLFS_BM;
        rows[updateIndex].iCLFS_HT = clfs.iCLFS_HT == undefined ? 0 : clfs.iCLFS_HT;
        rows[updateIndex].iCLFS_SPFL = clfs.iCLFS_SPFL == undefined ? 0 : clfs.iCLFS_SPFL;
        rows[updateIndex].iCLFS_SPSB = clfs.iCLFS_SPSB == undefined ? 0 : clfs.iCLFS_SPSB;
        rows[updateIndex].iCLFS_HYZ = clfs.iCLFS_HYZ == undefined ? 0 : clfs.iCLFS_HYZ;
        rows[updateIndex].iCLFS_SP = clfs.iCLFS_SP == undefined ? 0 : clfs.iCLFS_SP;
        rows[updateIndex].sCLDX = sCLDX.substr(0, sCLDX.length - 1);
        $("#list").datagrid("updateRow", rows[updateIndex]);
        $("#list").datagrid('refreshRow', updateIndex);
    }
    if (dialogName == "ListDYDGZ") {
        var updateIndex;
        //$(curEditcell).val(lstData[0].sGZMC);
        //$("#list").datagrid("getRows")[editIndex].sGZMC = lstData[0].sGZMC;
        if ($('#list').datagrid('validateRow', editIndex)) {
            $('#list').datagrid('endEdit', editIndex);
            updateIndex = editIndex;
            editIndex = undefined;
        }
        var rows = $("#list").datagrid("getData").rows;
        var rowData = rows[updateIndex];
        rows[updateIndex].iGZID = lstData[0].iJLBH;
        rows[updateIndex].sGZMC = lstData[0].sGZMC;
        $("#list").datagrid("updateRow", rows[updateIndex]);
        $("#list").datagrid('refreshRow', updateIndex);
    }
    if (dialogName == "ListCXHD") {
        $("#TB_RQ1").val(lstData[0].dKSSJ);
        $("#TB_RQ2").val(lstData[0].dJSSJ);
        CheckCXHDYHQ(lstData[0].iCXID, $("#HF_YHQID").val());
    }
};

function CheckCXHDYHQ(cxid, yhqid) {
    //其实只有发券有
}

//时段处理
function DatasToChinese(sstr) {
    tp_val = new Array();
    if (sstr == "") { return "请选择促销时段!"; }
    var tp_array = new Array();
    var values0 = "000000000000000000000000000000000000000000000000;000000000000000000000000000000000000000000000000;000000000000000000000000000000000000000000000000;000000000000000000000000000000000000000000000000;000000000000000000000000000000000000000000000000;000000000000000000000000000000000000000000000000;000000000000000000000000000000000000000000000000";
    if (sstr == "00") { sstr = values0; $("#HF_CXSD").val(sstr); }
    if (sstr == values0) { return "请选择促销时段!"; }
    var values = "111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111";
    if (sstr == values) {
        return "本周";
    }
    else {
        var tp_target = sstr.split(";");
        for (var i = 0; i <= tp_target.length - 1; i++) {
            //tp_array.push("day:"+(i+1));
            //星期几 时段
            for (var j = 0; j <= 48 - 1; j++) {

                if (tp_target[i].substring(j, j + 1) == "1") {
                    if (j % 2 == 0) {
                        //tp_array.push( j / 2 + ":00~" + j / 2 + ":30");
                        tp_array.push("day:" + (i + 1) + "~day:" + (i + 2) + "," + j / 2 + ":00~" + j / 2 + ":30");
                    }
                    else {
                        //tp_array.push( (j - 1) / 2 + ":30~" + ((j - 1) / 2 + 1) + ":00");
                        if (i + 2 == 8) {
                            tp_array.push("day:" + (i + 1) + "~day:" + 8 + "," + (j - 1) / 2 + ":30~" + ((j - 1) / 2 + 1) + ":00");
                        }
                        else {
                            tp_array.push("day:" + (i + 1) + "~day:" + (i + 2) + "," + (j - 1) / 2 + ":30~" + ((j - 1) / 2 + 1) + ":00");
                        }
                    }
                }
            }
        }
    }

    var tp_st = tp_array[0].split(',')[1].split('~')[0];
    var tp_ed = tp_array[0].split(',')[1].split('~')[1];
    var tp_array2 = new Array();
    for (var i = 1; i <= tp_array.length - 1; i++) {
        if (tp_ed == tp_array[i].split(',')[1].split('~')[0]) {
            tp_ed = tp_array[i].split(',')[1].split('~')[1];
        }
        else {
            tp_array2.push(tp_array[i - 1].split(',')[0] + "," + tp_st + "~" + tp_ed);

            tp_st = tp_array[i].split(',')[1].split('~')[0];
            tp_ed = tp_array[i].split(',')[1].split('~')[1];
        }
    }
    var tp_zz2 = tp_array[tp_array.length - 1].split(',')[0];

    tp_array2.push(tp_zz2 + "," + tp_st + "~" + tp_ed);

    //
    var tpz1 = tp_array2.length;
    //day:1,2:00~9:00
    var tp_array3 = new Array();
    var tpzy = new Array();
    for (var i = 0; i <= tp_array2.length - 1; i++) {
        var tp_31 = tp_array2[i].split(',')[1];
        tpzy.splice(0, tpzy.length);
        tpzy = isCon(tp_array2, tp_31);
        if (tpzy.length > 0 && tpzy.length == tpz1) {
            var tp_zst = tpzy[0].split('~')[0];
            var tp_zed = tpzy[0].split('~')[1];
            for (var j = 1; j <= tpzy.length - 1; j++) {
                if (tp_zed == tpzy[j].split('~')[0]) {
                    tp_zed = tpzy[j].split('~')[1];
                }
                else {
                    if (tp_zst != "" && tp_zed != "") {
                        tp_array3.push(tp_zst + "~" + tp_zed + "," + tp_31);
                    }
                    tp_zst = tpzy[j].split('~')[0];
                    tp_zed = tpzy[j].split('~')[1];
                }
            }
            tp_array3.push(tp_zst + "~" + tp_zed + "," + tp_31);
            i = 0;
        }
        else {
            return "请查看明细!";
        }
    }



    //day:1~day:2,12:00~19:00
    var tp_array4 = "";
    for (var i = 0; i <= tp_array3.length - 1; i++) {
        tp_array4 += zreplace(tp_array3[i]);
    }
    if (tp_array4.length > 24) { return "请查看明细!"; }

    return tp_array4;
};

function isCon(arr, val) {

    for (var i = 0; i < arr.length; i++) {
        if (arr[i].split(',')[1] == val) {
            tp_val.push(arr[i].split(',')[0]);
            arr.splice(i, 1);
            isCon(arr, val);
        }
    }
    return tp_val;
}

function zreplace(val) {
    val = val.replace("day:1~", "星期一~");
    val = val.replace("day:2~", "星期二~");
    val = val.replace("day:3~", "星期三~");
    val = val.replace("day:4~", "星期四~");
    val = val.replace("day:5~", "星期五~");
    val = val.replace("day:6~", "星期六~");
    val = val.replace("day:7~", "星期日~");
    //
    val = val.replace("~day:2", "~星期一");
    val = val.replace("~day:3", "~星期二");
    val = val.replace("~day:4", "~星期三");
    val = val.replace("~day:5", "~星期四");
    val = val.replace("~day:6", "~星期五");
    val = val.replace("~day:7", "~星期六");
    val = val.replace("~day:8", "~星期日");
    //
    return val;
}