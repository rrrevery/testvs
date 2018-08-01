var vColumnNames = [];
var vColumnModel = [];
var vAllColumns = [];
var vColumns = [];
var vFrozenColumns = [];
var GridWidth = 1000;
var GridHeight = 700;
var bDrawExport = false;
var bDropDown = false;
var ExportMode = false;
var OtherSearchCondition = "";
// T--CurrentPath
// 添加、修改指向地址包含特殊参数，用键值方式保存
var ConbinDataArry = new Array();
var iWXPID = 1;
var DBQ = GetUrlParam("PLDBQ");

//MakeNewTab放到commonfunc里
function SetControlBaseState() {
    //var rowid = $("#list").jqGrid("getGridParam", "selrow");
    var rowData = $("#list").datagrid("getSelected");
    var bExecuted = rowData == undefined || rowData == null || ((rowData.iZXR !== "") && (rowData.iZXR != undefined) && (rowData.iZXR != "0"));//$("#LB_ZXRMC").text() != "";//已审核
    var bHasData = $("#list").datagrid("getData").rows.length > 0;//$("#TB_JLBH").text() != "";//有数据
    if (rowData != null)
        vJLBH = rowData.iJLBH;

    document.getElementById("B_Insert").disabled = !bCanEdit;
    document.getElementById("B_Update").disabled = !(bHasData && !bExecuted) || !bCanEdit;
    document.getElementById("B_Delete").disabled = !(bHasData && !bExecuted) || !bCanEdit;
    document.getElementById("B_Exec").disabled = !(bHasData && !bExecuted) || !bCanExec;
    document.getElementById("B_Search").disabled = !bCanSrch;
    SetControlState();
};

function onHYZLXClick(e, treeId, treeNode) {
    $("#TB_HYZLXMC").val(treeNode.name);
    $("#HF_HYZLXID").val(treeNode.data);
    hideMenu("menuContentHYZLX");
};

$(document).ready(function () {
    GridWidth = $(document).width() - 2;
    GridHeight = $(document).height() - 144;
    //T-- 此处注意菜单命名必须符合规则，不能有误
    //获取当前菜单页面路径

    //初始化按钮、状态
    AddToolButtons("查询", "B_Search");
    AddToolButtons("添加", "B_Insert");
    AddToolButtons("修改", "B_Update");
    AddToolButtons("删除", "B_Delete");
    AddToolButtons("审核", "B_Exec");
    AddToolButtons("导出组", "B_ExportMember");
    AddToolButtons("导出", "B_Export");
    AddToolButtons("打印", "B_Print");
    AddCustomerButton();
    //默认隐藏按钮
    $("#B_ExportMember").css("width", "86px");
    $("#B_ExportMember").hide();
    //暂时去掉删除和审核，很多菜单没处理可能会报错
    $("#B_Delete").hide();
    $("#B_Exec").hide();
    //动态添加分隔线
    AddButtonSep();
    //判断在线人员
    if (sDJRMC == "") {
        alert("您已离线，请重新登录！");
    }
    BindKey();
    RefreshButtonSep();
    //收缩查询条件
    //$("#MainPanel .common_menu_tit").click(function () {
    //    ToggleHiddenPanel();
    //});
    $("#SearchPanel_Hidden").slideUp();
    $(".btnsep:visible:last").hide();
    DrawGrid();

    //$("[class='fa fa-list-ul fa-lg']").click(function () {
    //    ToggleNavigationMen();
    //});

    SetControlBaseState();
    //日期控件的默认日期，DJSJ等是当天，RQ等是昨天
    //for (i = 0; i < $(".bffld_right input[type=text]").length; i++) {
    //    if ($(".bffld_right input[type=text]")[i].id.indexOf("TB_DJSJ") >= 0 //||
    //        //$(".bffld_right input[type=text]")[i].id.indexOf("TB_ZXRQ") >= 0 ||
    //        //$(".bffld_right input[type=text]")[i].id.indexOf("TB_QDSJ") >= 0 ||
    //        //$(".bffld_right input[type=text]")[i].id.indexOf("TB_QDRQ") >= 0 ||
    //        //$(".bffld_right input[type=text]")[i].id.indexOf("TB_ZZSJ") >= 0 ||
    //        //$(".bffld_right input[type=text]")[i].id.indexOf("TB_ZZRQ") >= 0
    //        )
    //        $(".bffld_right input[type=text]")[i].value = GetDateStr(0);
    //    if ($(".bffld_right input[type=text]")[i].id.indexOf("TB_RQ") >= 0 ||
    //        $(".bffld_right input[type=text]")[i].id.indexOf("TB_TJRQ") >= 0
    //        )
    //        $(".bffld_right input[type=text]")[i].value = GetDateStr(-1);
    //}
    //ZSel_MoreCondition_Load(v_ZSel_rownum);


    //if (DBQ != 1) {
    //    SearchClick();
    //}


});

function BindKey() {
    //处理按钮事件    
    document.getElementById("B_Search").onclick = SearchClick;
    //document.getElementById("B_Delete").onclick = DeleteClick;
    //document.getElementById("B_Exec").onclick = ExecClick;
    document.getElementById("B_Export").onclick = ExportClick;
    document.getElementById("B_Print").onclick = PrintClick;
    document.getElementById("B_ExportMember").onclick = ExportMemberClick;
    document.getElementById("B_Insert").onclick = InsertClick;
    document.getElementById("B_Update").onclick = UpdateClick;
}

function DrawGrid(listName, vColName, vColModel, vSingle) {
    //为简化查询模板开发流程，统一Grid格式，新的查询可以使用InitGrid函数初始化vColumnNames和vColumnModel
    InitGrid();
    if (listName == undefined) { listName = "list"; }
    if (vSingle == undefined) { vSingle = true; }
    if (vColName == undefined) { vColName = vColumnNames; }
    if (vColModel == undefined) { vColModel = vColumnModel; }
    if (vColumns.length == 0 || vColName != vColumnNames) {
        vColumns = InitColumns(undefined, vColModel, vColName);
        vAllColumns = vColumns;
    }
    $("#" + listName + "").datagrid({
        //url: vUrl + "?mode=Search&func=" + vPageMsgID,
        //method: 'post',
        width: GridWidth,
        height: GridHeight,//674,
        autoRowHeight: false,
        striped: true,
        columns: [vColumns],
        frozenColumns: [vFrozenColumns],
        sortName: vColumns[0].field,
        singleSelect: vSingle,
        sortOrder: 'desc',
        //remoteSort: false,
        //fitColumns: true,
        //scrollbarSize: 0,
        showHeader: true,
        showFooter: true,
        pagePosition: 'bottom',
        rownumbers: true, //添加一列显示行号
        pagination: true,  //启用分页
        pageNumber: 1,
        pageSize: 10,
        pageList: [10, 50, 100],
        onSortColumn: function (sort, order) {
            SearchData(listName, undefined, undefined, sort, order);
        },
        onClickRow: OnClickRow,
        onDblClickRow: DBClickRow,
        onLoadSuccess: OnLoadSuccess,
    });
    var pager = $('#' + listName + '').datagrid("getPager");
    pager.pagination({
        onSelectPage: function (pageNum, pageSize) {
            SearchData(listName, pageNum, pageSize);
        },
        buttons: [{
            iconCls: 'fa fa-cog datagrid_setting',
            handler: ListSet
        }]
    });
}

function OnLoadSuccess() {
    ;
}

function ListSet() {
    var bSel = false;
    $.dialog.data('Columns', vColumns);
    $.dialog.data('allColumns', vAllColumns);
    $.dialog.data('frozenColumns', vFrozenColumns);
    $.dialog.data('dialogSelected', bSel);
    var pturl = window.location.pathname.toUpperCase();
    var dialogUrl = "../../CrmArt/ListSet/CrmArt_ListSet.aspx";
    pturl = pturl.substr(0, pturl.indexOf("CRMWEB"));
    dialogUrl = dialogUrl.replace("../..", pturl + "CRMWEB");
    $.dialog.open(dialogUrl,
    {
        lock: true, width: 480, height: 500, cancel: false,
        close: function () {
            bSel = $.dialog.data('dialogSelected');
            if (bSel) {
                vColumns = $.dialog.data('Columns');
                vAllColumns = $.dialog.data('allColumns');
                vFrozenColumns = $.dialog.data('frozenColumns');
                DrawGrid();
                //$("#list").datagrid({
                //    columns: [vColumns],
                //    frozenColumns: [vFrozenColumns],
                //});
                $('#list').datagrid('loadData', JSON.parse(vSearchData), "json");
            }
        }
    }, false);
}

function ExportMemberClick() {
    DrawView();
    SetControlBaseState();
};

function GetDateStr(days) {
    var now = new Date();
    now = now.valueOf();
    now = now + days * 24 * 60 * 60 * 1000;
    now = new Date(now);
    y = now.getFullYear();
    m = now.getMonth() + 1;
    d = now.getDate();
    m = m < 10 ? "0" + m : m;
    d = d < 10 ? "0" + d : d;
    return y + "-" + m + "-" + d;
}

function IsValidSearch() {
    return true;
};

function InsertClick() {
    //T --添加事件
    var sInsertCurrentPath = "";
    sInsertCurrentPath = sCurrentPath + "?action=add";
    sInsertCurrentPath = ConbinPath(sInsertCurrentPath);
    MakeNewTab(sInsertCurrentPath, vCaption, vPageMsgID);
};

function UpdateClick() {
    //T-- 修改事件
    var sUpdateCurrentPath = "";
    sUpdateCurrentPath = sCurrentPath + "?action=edit&jlbh=" + vJLBH;
    sUpdateCurrentPath = ConbinPath(sUpdateCurrentPath);
    MakeNewTab(sUpdateCurrentPath, vCaption, vPageMsgID);
};

function DBClickRow(rowIndex, rowData) {
    //T--表格双击事件
    if ($("#B_Insert").css("display") != "none" || $("#B_Update").css("display") != "none") {
        var sDbCurrentPath = "";
        //var rowData = $("#list").getRowData(rowid);
        sDbCurrentPath = sCurrentPath + "?jlbh=" + rowData.iJLBH + "&iLoginPUBLICID=" + iWXPID;

        //sDbCurrentPath = sCurrentPath + "?jlbh=" + rowData.iJLBH;
        sDbCurrentPath = ConbinPath(sDbCurrentPath);
        MakeNewTab(sDbCurrentPath, vCaption, vPageMsgID);
    }
};

function ConbinPath(path) {
    //T--添加、修改指向地址包含特殊参数拼接
    for (var key in ConbinDataArry) {
        path += "&" + key + "=" + ConbinDataArry[key];
    }
    return path;
};

function PrintClick() {
    var obj = MakeSearchJSON();
    $.ajax({
        type: "post",
        url: vUrl + "?mode=Print&func=" + vPageMsgID,
        async: false,
        data: { json: JSON.stringify(obj), titles: 'cybillprint' },
    }).done(function (data) {
        try {
            PrintData(data);
        }
        catch (error) {
            alert(error.message);
        }
    });
};

function SearchClick() {
    if (!IsValidSearch())
        return;
    //location.hash = "tab2-tab";    
    //$("#list").jqGrid('setGridParam', {
    //    url: vUrl + "?mode=Search&func=" + vPageMsgID,
    //    postData: { json: JSON.stringify(obj), titles: "cybillsearch" },
    //    page: 1,
    //    loadError: function (xhr, status, error) {
    //        ShowMessage(xhr.responseText);
    //    }
    //}).trigger("reloadGrid");
    //$('#list').datagrid({
    //    url: vUrl + "?mode=Search&func=" + vPageMsgID,
    //    queryParams: { json: JSON.stringify(obj) },
    //});

    //$('#list').datagrid('load', {});
    $("#SearchPanel_Hidden").slideUp();
    SearchData();
    SetControlBaseState();
};

//function DeleteClick() {
//    art.dialog({
//        title: "删除",
//        lock: true,
//        content: "是否删除？",
//        ok: function () {
//            if (posttosever(SaveData(), vUrl, "Delete") == true) {
//                DoSearch();
//                SetControlBaseState();
//            }
//        },
//        okVal: '是',
//        cancelVal: '否',
//        cancel: true
//    });
//};

//function ExecClick() {
//    art.dialog({
//        title: "审核",
//        lock: true,
//        content: "审核后不可修改，是否审核？",
//        ok: function () {
//            if (posttosever(SaveData(), vUrl, "Exec") == true) {
//                //ShowDataBase(vJLBH);
//                DoSearch();
//                SetControlBaseState();
//            }
//        },
//        okVal: '是',
//        cancelVal: '否',
//        cancel: true
//    });
//};

function ExportClick() {
    var obj = MakeSearchJSON();
    var zUrl = vUrl + "?mode=Export&func=" + vPageMsgID;
    var array = new Object();
    array.json = JSON.stringify(obj);
    array = $.sign(array);
    PostToExport(zUrl, array);
    SetControlBaseState();
};

function PrintData(data) {
    //T 已实现打印功能、遗憾的是由于列宽不确定翻页无法显示表头   2016-12-10 
    date = new Date();
    var userAgent = window.navigator.userAgent;
    var dataList = JSON.parse(data);

    LODOP = getLodop();
    //  DataArray 最终要打印的数据包括表头
    var DataArray = new Array();
    var colNumber = 0;
    var totalwidth = 0;
    //var cols = $("#list").jqGrid("getGridParam", "colModel");
    //var names = $("#list").jqGrid("getGridParam", "colNames");
    var cols = $("#list").data('datagrid').options.columns[0];
    var tmpArray = new Object();
    var prt = new Array();
    for (i = 0; i < 3; i++) {
        prt[i] = new Array();
    }
    for (i = 0; i < cols.length; i++) {
        if (!cols[i].hidden) {
            tmpArray[cols[i].field] = cols[i].title;
            colNumber += 1;
            totalwidth += cols[i].width;
            prt[0].push(cols[i].title);
            prt[1].push(cols[i].width);
            prt[2].push(cols[i].field);
        }
    }

    //DataArray.push(tmpArray);

    // 筛选所需数据
    for (var i = 0; i < dataList.length; i++) {
        //var dataObject = new Object();
        //for (var key in tmpArray) {
        //    dataObject[key] = dataList[i][key];
        //}
        //DataArray.push(dataObject);
        prt[i + 3] = new Array();
        for (var j = 0; j < prt[2].length; j++)
            prt[i + 3].push(dataList[i][prt[2][j]]);
    }

    var tmpTableStr = "<table style='font-size:12px; table-layout:fixed; empty-cells:show; border-collapse: collapse; margin:0 auto; border:1px solid #333333; color:#666; width:90%'>";

    //for (var i = 0; i < DataArray.length; i++) {
    //    tmpTableStr += "<tr style='background-color:#f5fafe; '>";
    //    for (var key in tmpArray) {
    //        if (i == 0)
    //            tmpTableStr += "<th style='border:1px solid #333333; height:35px;width:auto;' ><b>" + DataArray[i][key] + "</b></th>";
    //        else {
    //            tmpTableStr += "<td style='border:1px solid #333333; height:30px;' >" + DataArray[i][key] + "</td>";
    //        }
    //    }
    //    tmpTableStr += "</tr>";
    //}
    for (var i = 0; i < prt.length; i++) {
        tmpTableStr += "<tr style='background-color:#f5fafe; '>";
        for (var j = 0; j < prt[2].length; j++) {
            if (i == 0)
                tmpTableStr += "<th style='border:1px solid #333333; height:35px;width:" + 100 * prt[1][j] / totalwidth + "%;' ><b>" + prt[i][j] + "</b></th>";
            else if (i > 2) {
                tmpTableStr += "<td style='border:1px solid #333333; height:30px;' >" + prt[i][j] + "</td>";
            }
        }
        tmpTableStr += "</tr>";
    }

    tmpTableStr += "</table>";

    //此处不同项目取值方式可能不一样
    //var caption = vCaption; //$("#mainTabs .active", window.parent.frames["subTabs"].document)[0].innerText;
    // var caption = $(".external.selected", parent.document)[0].firstChild.innerText;

    tmpHeadStr = "<div style='background-color:white'> <center>";
    tmpHeadStr += "<h1 style='width:50%' align='center'>" + vCaption + "打印单</h1>"
    tmpHeadStr += "打印人：" + sDJRMC + " &nbsp 打印时间:" + FormatDate(date, 'yyyy-MM-dd HH:mm:ss');
    tmpHeadStr += "</center></div>";

    //LODOP.SET_LICENSES("北京长京益康信息科技有限公司", "653556581728688787994958093190", "", "");
    LODOP.PRINT_INIT("打印任务名");
    if (userAgent.indexOf("Firefox") < 0) {
        LODOP.ADD_PRINT_TABLE(128, "5%", "90%", 314, tmpTableStr);

    } else {
        LODOP.ADD_PRINT_TABLE(128, "5%", "100%", 314, tmpTableStr);
    }
    if (colNumber > 7)
        LODOP.SET_PRINT_PAGESIZE(2, 0, 0, "A4");

    LODOP.SET_PRINT_STYLEA(0, "Vorient", 3);
    LODOP.ADD_PRINT_HTM(26, "5%", "90%", 109, tmpHeadStr);
    LODOP.SET_PRINT_STYLEA(0, "ItemType", 1);
    LODOP.SET_PRINT_STYLEA(0, "LinkedItem", 1);
    LODOP.ADD_PRINT_HTM(444, "5%", "90%", 54, "<center><span tdata='pageNO'>第##页</span></center>");
    LODOP.SET_PRINT_STYLEA(0, "ItemType", 1);
    LODOP.SET_PRINT_STYLEA(0, "LinkedItem", 1);
    LODOP.ADD_PRINT_HTM(1, 600, 300, 100, "总页号：<font color='#0000ff'><span tdata='pageNO'>第##页</span>/<span tdata='pageCount'>共##页</span></font>");
    LODOP.SET_PRINT_STYLEA(0, "ItemType", 1);
    LODOP.SET_PRINT_STYLEA(0, "Horient", 1);
    LODOP.PREVIEW();
    //LODOP.PRINT_DESIGN();
};

function PostToExport(URL, PARAMS) {
    // T 解决导出出URL长度大于限制长度的问题   2016-12-10 
    var temp = document.createElement("form");
    temp.style.display = "none";
    for (var x in PARAMS) {
        var opt = document.createElement("textarea");
        opt.name = x;
        opt.value = PARAMS[x];
        temp.appendChild(opt);
    }
    document.body.appendChild(temp);
    temp.action = URL;
    temp.method = "post";
    temp.submit();

    return temp;
}

function ReturnQX() {
    ;
}

function DrawView() {
    if (!bDrawExport) {
        $("#btn-toolbar").append("<div id='Import_message' style='width:800px;height:100px;display:none;'></div>");
        var sHtml = "<div class='form'>"
        sHtml += "<div class='bfrow'><div class='bffld'><div class='bffld_left'>组名称</div><div class='bffld_right'><input id='TB_HYZMC' type='text'></div></div>";
        sHtml += "<div class='bffld'><div class='bffld_left'>客群组类型</div><div class='bffld_right'> <input type='text' id='TB_HYZLXMC'/><input type='hidden' id='HF_HYZLXID'/></div></div></div>";
        sHtml += "<div class='bfrow'><div class='bffld'><div class='bffld_left'>开始日期</div><div class='bffld_right'><input id='TB_KSSJ' type='text' class='Wdate' onfocus='WdatePicker({isShowWeek:true})'></div></div>";
        sHtml += "<div class='bffld'><div class='bffld_left'>结束日期</div><div class='bffld_right'> <input type='text' id='TB_JSSJ' class='Wdate' onfocus='WdatePicker({isShowWeek:true})'/></div></div></div>";
        sHtml += "<div class='bfrow'><div class='bffld'><div class='bffld_left'>客群组级别</div><div class='bffld_right'> <select id='S_JB'> <option value='0'>总部</option><option value='2'>门店</option></select></div></div>";//<option value='1'>事业部</option>
        sHtml += "<div class='bffld'><div class='bffld_left'>客群门店</div><div class='bffld_right'> <input type='text' id='TB_MDMC'/><input type='hidden' id='HF_MDID' /><input type='hidden' id='zHF_MDID' /></div></div></div>";
        sHtml += "<div class='bfrow'><div class='bffld'><button id='B_SaveMemberGrp' class='btn-dynamic'>保存</button></div></div>";
        sHtml += "</div>";
        document.getElementById("Import_message").innerHTML = sHtml;
        $("#panelwrap").append("<div id='dv_hyzlx'></div>");
        var val = " <div><div id='menuContentHYZLX' class='menuContent' style='display: none; position: absolute; background-color: white' />";
        val += " <ul id='TreeHYZLX' class='ztree' style='margin-top: 0; width: 200px; height: 200px'></ul></div>";
        document.getElementById("dv_hyzlx").innerHTML = val;
        FillHYZLXTree("TreeHYZLX", "TB_HYZLXMC", "menuContentHYZLX", iDJR, false);
        bDrawExport = true;
    }
    if (!bDropDown)
        $("#Import_message").slideDown("slow");
    else
        $("#Import_message").slideUp("slow");
    bDropDown = !bDropDown;
    $("#B_SaveMemberGrp").click(function () {
        if ($("#TB_HYZMC").val() != "" && $("#HF_HYZLXID").val() != "" && $("#TB_KSSJ").val() != "" && $("#TB_JSSJ").val() != "" && $("#S_JB").val() != "") {
            ExportMember($("#TB_HYZMC").val(), $("#HF_HYZLXID").val(), $("#TB_KSSJ").val(), $("#TB_JSSJ").val(), $("#S_JB").val());
        }
        else {
            art.dialog({ lock: true, content: "请补充导出信息,再导出", time: 2 });
        }
    });
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true, "", "", "", 1);
    });

}

function ExportMember(sGRPMC, iGRPLX, dKSRQ, dJSRQ, iGRPJB) {
    var obj = MakeSearchJSON();
    var export_data = new Object();
    export_data.sGRPMC = sGRPMC;
    export_data.iGRPLX = iGRPLX;
    export_data.dKSRQ = dKSRQ;
    export_data.dJSRQ = dJSRQ;
    export_data.iGRPJB = iGRPJB;
    obj.HYZXX = export_data;
    try {
        $.ajax({
            url: vUrl + "?mode=ExportMember&func=" + vPageMsgID + "&RYID=" + iDJR,
            dataType: "json",
            async: false,
            data: { json: JSON.stringify(obj), titles: 'cybillprint' },
            success: function (data) {
                result = JSON.stringify(data);
                ShowMessage("导出成功");
            },
            error: function (data) {
                ShowMessage("导出失败");
            }
        });
    } catch (e) {
        alert(e.message);
    }
};

function MakeMoreSrchCondition(arrayObj) {
    for (var i = 0; i < ZSel_Array.length; i++) {
        var tp_ZSel = new Object();
        tp_ZSel.i = ZSel_Array[i].i;
        tp_ZSel.type = ZSel_Array[i].type;
        tp_ZSel.ElementName = ZSel_Array[i].id;
        tp_ZSel.idname = ZSel_Array[i].idname;
        tp_ZSel.ComparisonSign = ZSel_Array[i].cd;
        tp_ZSel.cdname = ZSel_Array[i].cdname;
        tp_ZSel.Value1 = ZSel_Array[i].tb;
        switch (tp_ZSel.ElementName.substr(0, 1)) {
            case "i":
            case "f":
                tp_ZSel.InQuotationMarks = false;
                break;
            case "s":
            case "d":
                tp_ZSel.InQuotationMarks = true;
                break;
        }
        if (tp_ZSel.ComparisonSign == "like") {
            tp_ZSel.InQuotationMarks = true;
            tp_ZSel.Value1 = "%" + tp_ZSel.Value1 + "%";
        }
        arrayObj.push(tp_ZSel);
    }
}

//zy
var ZSel_Array = new Array();
var v_ZSel_rownum = 1;
//base you kyuu
function ZSel_OptionCondition() {
    var tp_str = "";
    tp_str += " <option value=\"=\">等于</option>";
    tp_str += " <option value=\">\">大于</option>";
    tp_str += " <option value=\"<\" >小于</option>";
    tp_str += " <option value=\">=\" >不小于</option>";
    tp_str += " <option value=\"<=\" >不大于</option>";
    tp_str += " <option value=\">=\" >不等于</option>";
    //tp_str += " <option value=\"<=\" >匹配</option>";
    tp_str += " <option value=\"like\" >包含</option>";
    return tp_str;
}
//set 
function ZSel_SetDate_CheckAdd() {

    var tp_msg = "";
    for (var i = 0; i <= v_ZSel_rownum - 1; i++) {
        //
        if ($("#ZSel_Type_" + i + "").find("option:selected").text() == "") {
            tp_msg = "请完整输入更多条件内容！";
            return tp_msg;
        }
        //
        if ($("#ZSel_Id_" + i + "").find("option:selected").text() == "") {

            tp_msg = "请完整输入更多条件内容！";
            return tp_msg;
        }
        //
        if ($("#ZSel_Cd_" + i + "").find("option:selected").text() == "") {

            tp_msg = "请完整输入更多条件内容！";
            return tp_msg;
        }
        //
        if ($("#ZSel_Tb_" + i + "").find("option:selected").val() == "") {

            tp_msg = "请完整输入更多条件内容！";
            return tp_msg;
        }
    }
    return tp_msg;
}
function ZSel_SetDate_CheckAddInx(i) {

    var tp_msg = "";

    //
    if ($("#ZSel_Type_" + i + "").find("option:selected").text() == "") {
        tp_msg = "请完整输入更多条件内容！";
        return tp_msg;
    }
    //
    if ($("#ZSel_Id_" + i + "").find("option:selected").text() == "") {

        tp_msg = "请完整输入更多条件内容！";
        return tp_msg;
    }
    //
    if ($("#ZSel_Cd_" + i + "").find("option:selected").text() == "") {

        tp_msg = "请完整输入更多条件内容！";
        return tp_msg;
    }
    //
    if ($("#ZSel_Tb_" + i + "").find("option:selected").val() == "") {

        tp_msg = "请完整输入更多条件内容！";
        return tp_msg;
    }

    return tp_msg;
}
function ZSel_SetDate_Add() {

    ZSel_Array = new Array();
    for (var i = 0; i <= v_ZSel_rownum - 1; i++) {
        if ($("#ZSel_Id_" + i + "").find("option:selected").text() != "") {
            var tp_Zsel = new Object();
            tp_Zsel.i = i;
            tp_Zsel.type = $("#ZSel_Type_" + i + "").find("option:selected").val();
            tp_Zsel.id = $("#ZSel_Id_" + i + "").find("option:selected").val();
            tp_Zsel.idname = $("#ZSel_Id_" + i + "").find("option:selected").text();
            tp_Zsel.cd = $("#ZSel_Cd_" + i + "").find("option:selected").val();
            tp_Zsel.cdname = $("#ZSel_Cd_" + i + "").find("option:selected").text();
            tp_Zsel.tb = $("#ZSel_Tb_" + i + "").val();
            ZSel_Array.push(tp_Zsel);
        }
    }
    return ZSel_Array
}
function ZSel_SetDate_Minu(inx) {
    ZSel_Array = new Array();
    var tp_minu = 0;
    for (var i = 0; i <= v_ZSel_rownum - 1; i++) {
        if (i != inx) {
            var tp_Zsel = new Object();
            tp_Zsel.i = i - tp_minu;
            tp_Zsel.type = $("#ZSel_Type_" + i + "").find("option:selected").val();
            tp_Zsel.id = $("#ZSel_Id_" + i + "").find("option:selected").val();
            tp_Zsel.idname = $("#ZSel_Id_" + i + "").find("option:selected").text();
            tp_Zsel.cd = $("#ZSel_Cd_" + i + "").find("option:selected").val();
            tp_Zsel.cdname = $("#ZSel_Cd_" + i + "").find("option:selected").text();
            tp_Zsel.tb = $("#ZSel_Tb_" + i + "").val();
            ZSel_Array.push(tp_Zsel);
        }
        else {
            tp_minu++;
        }
    }
    v_ZSel_rownum = v_ZSel_rownum - 1;
    return ZSel_Array
}
//get
function ZSel_GetDate_Add() {
    for (var i = 0; i <= ZSel_Array.length - 1; i++) {
        //
        if (ZSel_Array[i].id != "") {
            $("#ZSel_Id_" + ZSel_Array[i].i + "").val(ZSel_Array[i].id);
        }
        //
        if (ZSel_Array[i].cd != "") {
            $("#ZSel_Cd_" + ZSel_Array[i].i + "").val(ZSel_Array[i].cd);
        }
        //
        if (ZSel_Array[i].tb != "") {
            $("#ZSel_Tb_" + ZSel_Array[i].i + "").val(ZSel_Array[i].tb);
        }
        //
        if (ZSel_Array[i].type != "") {
            $("#ZSel_Type_" + ZSel_Array[i].i + "").val(ZSel_Array[i].type);

        }
    }
}
function ZSel_GetDate_Minu() {
    for (var i = 0; i <= ZSel_Array.length - 1; i++) {
        //
        if (ZSel_Array[i].id != "") {
            $("#ZSel_Id_" + ZSel_Array[i].i + "").val(ZSel_Array[i].id);
        }
        //
        if (ZSel_Array[i].cd != "") {
            $("#ZSel_Cd_" + ZSel_Array[i].i + "").val(ZSel_Array[i].cd);
        }
        //
        if (ZSel_Array[i].tb != "") {
            $("#ZSel_Tb_" + ZSel_Array[i].i + "").val(ZSel_Array[i].tb);
        }
        //
        if (ZSel_Array[i].type != "") {
            $("#ZSel_Type_" + ZSel_Array[i].i + "").val(ZSel_Array[i].type);

        }
    }
}
function ZSel_WriteToPage() {
    var tp_str = "";
    var tp_colNames = $("#list").jqGrid('getGridParam', 'colNames')
    var tp_colNames2 = $("#list").jqGrid('getGridParam', 'colModel')
    //for (var i = 0; i <= v_ZSel_rownum - 1; i++) {
    i = v_ZSel_rownum - 1;
    tp_str += " <div class=\"bfrow\">";
    tp_str += " <select id=\"ZSel_Type_" + i + "\"  class=\"form_select\" name=\"\" style=\"width:100px;\">";
    tp_str += " <option value=\"and\">并且</option>";
    tp_str += " <option value=\"or\">或者</option>";
    tp_str += " </select>";
    tp_str += " <select id=\"ZSel_Id_" + i + "\" class=\"form_select\" name=\"\" style=\"width:100px;\">";
    for (var j = 0; j <= tp_colNames.length - 1; j++) {
        var tp_name = tp_colNames[j];
        var tp_val = tp_colNames2[j]["name"];
        if (tp_colNames2[j].hidden == false) {
            tp_str += "  <option value=" + tp_val + ">" + tp_name + "</option>";
        }
    }
    tp_str += " </select>";
    tp_str += " <label style=\"width:5px;\"></label>";
    tp_str += " <select id=\"ZSel_Cd_" + i + "\" value='<' class=\"form_select\" name=\"\" style=\"width:100px;\">";
    tp_str += ZSel_OptionCondition();
    tp_str += " </select>";
    tp_str += " <label  style=\"width:5px;\"></label>";
    tp_str += " <input id=\"ZSel_Tb_" + i + "\" type=\"text\" class=\"form_input\" name=\"\" style=\"width:201px;\"/>";
    tp_str += " <input type=\"button\" class=\"form_button\" value=\"-\" onclick=\"ZSel_MoreCondition_Minu(" + i + ");\" />";
    tp_str += " </div>";
    //}
    return tp_str;

}

function ZSel_MoreCondition_Load() {
    var tp_str = ZSel_WriteToPage();
    $("#Label1").html(tp_str);
}
function ZSel_MoreCondition_Add() {
    if (v_ZSel_rownum > 10) { return false; }
    //
    //var tp_msg = ZSel_SetDate_CheckAdd();
    var tp_msg = "";
    if (tp_msg == "") {
        //ZSel_SetDate_Add();
        v_ZSel_rownum = v_ZSel_rownum + 1;
    }
    else {
        alert(tp_msg);
        return;
    }
    //
    var tp_str = ZSel_WriteToPage();
    $("#Label1").append(tp_str);
    //
    //  ZSel_GetDate_Add();
    //
    //  ZSel_LineCondition_Load();
}
function ZSel_MoreCondition_Minu(inx) {
    //
    //ZSel_SetDate_Minu(inx);
    //
    // var tp_str = ZSel_WriteToPage();
    //$("#Label1").html(tp_str);
    $("#ZSel_Type_" + inx).parent().remove();
    //
    //ZSel_GetDate_Add();
}
function ZSel_LineCondition_Load() {

    //
    var tpx_1 = "查询条件:";
    //var obj1 = ZSel_Array;
    for (var i = 0; i < v_ZSel_rownum; i++) {
        var tp_msg = ZSel_SetDate_CheckAddInx(i);
        if (tp_msg == "") {
            tpx_1 += " <label  style=\"width:50px;\">" + $("#ZSel_Id_" + i + "").find("option:selected").text() + $("#ZSel_Cd_" + i + "").find("option:selected").text() + $("#ZSel_Tb_" + i + "").val() + "</label>";
            tpx_1 += " <input type=\"button\" class=\"form_button\" value=\"X\" onclick=\"ZSel_LineMinu(" + i + ");\" />";
        }
    }
    $("#tab2 h3").html(tpx_1);//浏览器兼容性问题 2014.11.20 ys
}
function ZSel_LineMinu(inx) {
    ZSel_MoreCondition_Minu(inx);
    $("#B_Search").click();
}

//用来保存选择框dialog返回的数据,参数分别是input[type='text'] 的控件名  
//type=hidden的控件名 type=hidden的控件名
function WUC_Dialog_Return(viewDataIn, trueDataIn, jsonStringIn) {
    var tp_return = $.dialog.data('IpValuesReturn');//跨框架数据共享读取接口。指定name即返回数据，任何引用了artDialog的页面都有效，如果还指定值，就是设置其值

    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + viewDataIn + "").val(tp_mc);//首先清空数据
            var jsonInput = JSON.parse(jsonString);//将jsonString字符串，解析转换成json数据对象类型
            var contractValues = new Array();
            contractValues = jsonInput.Depts;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += "'" + contractValues[i].Code + "',";
            }
            $("#" + viewDataIn + "").val(tp_mc);
            $("#" + trueDataIn + "").val(tp_hf.substr(0, tp_hf.length - 1));//不取最后一个 单引号' 防止sql查询出错
            $("#" + jsonStringIn + "").val(jsonString);//记录返回的数据
        }
    }
}

//会员组
var setting = {
    view: {
        showIcon: false
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    callback: {
        beforeClick: beforeClick,
        onClick: onHYZLXClick
    }
};
var menu;
function beforeClick(treeId, treeNode) {
    //var check = treeNode && (treeNode.bj_lx == "2"); //BJ_LX？？
    var check = treeNode && (treeNode.bj_mj == "1");
    return check;
}

function hideMenu(menuid) {
    $("#" + menuid).fadeOut("fast");
    $("body").unbind("mousedown", onBodyDown);
}

function onBodyDown(event) {
    if (!(event.target.id == "menuBtn" || event.target.id == menu || $(event.target).parents("#" + menu).length > 0)) {
        hideMenu(menu);
    }
}

function FillHYZLXTree(treename, editbgdd, menuid, ryid, view) {
    menu = menuid;
    $("#" + editbgdd).attr("readonly", true);
    $("#" + editbgdd).click(function () {
        menu = menuid;
        var Obj = $("#" + editbgdd);
        var Offset = $("#" + editbgdd).offset();
        $("#" + menuid).css({ left: Offset.left + "px", top: Offset.top + Obj.outerHeight() + "px" }).slideDown("fast");
        $("body").bind("mousedown", onBodyDown);
    });
    if (!ryid) {
        ryid = "";
    }
    var url = "../../CrmLib/CrmLib.ashx?func=FillHYZLXTree&RYID=" + ryid;
    if (view == true) {
        url += "&VIEW=true";
    }
    $.ajax({
        url: url,
        type: 'post',
        dataType: "json",
        success: function (data) {
            var zNodes = "[";
            for (var i = 0; i < data.length; i++) {
                zNodes = zNodes + "{id:'" + data[i].sHYZLXDM +
                    "',pId:'" + ((data[i].sPHYZLXDM == "") ? "0" : data[i].sPHYZLXDM) +
                    "',name:'" + data[i].sHYZLXMC +
                    "',data:'" + data[i].iHYZLXID +
                    "',bj_mj:'" + data[i].iBJ_MJ +
                    "',bj_lx:'" + data[i].iBJ_LX +
                     "'}";
                if (i < data.length - 1)
                    zNodes = zNodes + ",";
            }
            zNodes = zNodes + "]";
            //zNodes = "[{id:1,pID:0,name:'aaaa'}]";
            var treeObj = $.fn.zTree.init($("#" + treename), setting, eval(zNodes));
            var nodes = treeObj.getNodes();
            //使用递归算法，删除所有的 bj_lx！=2 且为叶节点的节点
        },
        error: function (data) {
            art.dialog({ content: "会员组类型加载失败", lock: true });
        },
    });
}

function DeleteHYZLXNode(parentNode) {
    if (parentNode.isParent)
    { }
}

//function SearchData(page, rows, sort, order, listName) {
//    if (listName == undefined) { listName = "list"; }
//    var obj = MakeSearchJSON(listName);
//    //page页码,rows每页行数,sort排序字段,order排序方式
//    page = page || $('#' + listName + '').datagrid("options").pageNumber;
//    rows = rows || $('#' + listName + '').datagrid("options").pageSize;
//    sort = sort || $('#' + listName + '').datagrid("options").sortName;
//    order = order || $('#' + listName + '').datagrid("options").sortOrder;
//    $('#' + listName + '').datagrid("loading");
//    $.ajax({
//        type: "post",
//        url: vUrl + "?mode=Search&func=" + vPageMsgID,
//        async: true,
//        data: {
//            json: JSON.stringify(obj),
//            titles: 'cybillsearch',
//            page: page,
//            rows: rows,
//            sort: sort,
//            order: order,
//        },
//        success: function (data) {
//            //if (data.indexOf('错误') >= 0 || data.indexOf('error') >= 0) {
//            //    ShowErrMessage(data);
//            //    $('#' + listName + '').datagrid("loaded");
//            //    return;
//            //}
//            $('#' + listName + '').datagrid('loadData', JSON.parse(data), "json");
//            $('#' + listName + '').datagrid("loaded");
//            vSearchData = data;
//        },
//        error: function (data) {
//            ShowErrMessage(data);
//        }
//    });
//}

function OnClickRow(rowIndex, rowData) {
    SetControlBaseState();
}

