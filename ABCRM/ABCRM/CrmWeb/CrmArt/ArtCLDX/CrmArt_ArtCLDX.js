vCaption = "处理对象信息";
vUrl = "../../CRMGL/CRMGL.ashx";
var vDialogName = "ArtCLDX";
var vSingleSelect = false;
var vReturnData = []; //{iSJLX:iSJLX,iSJNR:iSJNR}
//var vTabTitle = "部门";
var vTabIndex = 0;
var vTabIdField;
var vTabDialogName = "TreeSHBM";
var vData = $.dialog.data("DialogCondition");
if (vData)
    vData = JSON.parse(vData);
var vAlreadyData = $.dialog.data("dialogInput");
if (vAlreadyData)
    vAlreadyData = JSON.parse(vAlreadyData);
else
    vAlreadyData = [];
var vAlreadyTabData = [];
var vSB = [], vHT = [], vSP = [];
var vSearchEnable = vData.SearchEnable && true;
var vSearchAlreadyEnable = true;
var vTmpReturnData = new Array();
var vSrchCount = 0;
var setting = {
    check: {
        enable: true
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    view: {
        showIcon: false,
    }
};

function InitGrid() {
    vBrandColumns = [
        { field: 'iSHSBID', title: '商户商标ID', hidden: true },
        { field: 'sSHDM', title: '商户代码', width: 100, hidden: true },
        { field: 'sSBDM', title: '商标代码', width: 100 },
        { field: 'sSBMC', title: '商标名称', width: 100 },
    ];
    //vBrandIdField = "iSHSBID";
    vContractColumns = [
        { field: 'iSHHTID', title: '商户合同ID', hidden: true },
        { field: 'sSHDM', title: '商户代码', width: 100, hidden: true },
        { field: 'sHTH', title: '合同号', width: 100 },
        { field: 'sGHSMC', title: '往来单位名称', width: 100 },
    ]
    //vContractIdField = "iSHHTID";
    vGoodsColumns = [
        { field: 'iSHSPID', title: '商户商品ID', hidden: true },
        { field: 'sSHDM', title: '商户代码', width: 100, hidden: true },
        { field: 'sSPDM', title: '商品代码', width: 100 },
        { field: 'sSPMC', title: '商品名称', width: 100 },
    ]
    //vGoodsidField = "iSHSPID";
    DrawTabGrid("list_brand", vBrandColumns, "iSHSBID", "sSBDM");
    DrawTabGrid("list_contract", vContractColumns, "iSHHTID", "sHTH");
    DrawTabGrid("list_goods", vGoodsColumns, "iSHSPID", "sSPDM");
    //$("#list_brand").datagrid("options").sortName = "sSBDM";
    //$("#list_contract").datagrid("options").sortName = "sHTH";
    //$("#list_goods").datagrid("options").sortName = "sSPDM";
    SearchData("list_brand");
    SearchData("list_contract");
    SearchData("list_goods");
}

function InitTree() {
    PostToCrmlib("FillTreeSHBM", { iRYID: iDJR, sSHDM: vData.SHDM, sBMDM: vData.BMDM, iLEVEL: 5 }, function (data) {
        $.fn.zTree.init($("#treeBM"), setting, data);
        var treeBM = $.fn.zTree.getZTreeObj("treeBM");
        for (var i = 0; i < vAlreadyData.length; i++) {
            if (vAlreadyData[i].iSJLX == 1)
                treeBM.checkNode(treeBM.getNodesByParam("iSHBMID", vAlreadyData[i].iSJNR, null)[0], true, true);
        }
    });
    PostToCrmlib("FillTreeSHSPFL", { iRYID: iDJR, sSHDM: vData.SHDM }, function (data) {
        $.fn.zTree.init($("#treeFL"), setting, data);
        var treeFL = $.fn.zTree.getZTreeObj("treeFL");
        for (var i = 0; i < vAlreadyData.length; i++) {
            if (vAlreadyData[i].iSJLX == 3)
                treeFL.checkNode(treeFL.getNodesByParam("iSHSPFLID", vAlreadyData[i].iSJNR, null)[0], true, true);
        }
    });
}

$(document).ready(function () {
    //AddToolButtons("查已选", "B_AlreadySearch", "bftoolbtn fa fa-search-plus");
    AddToolButtons("查询", "B_Search");
    AddToolButtons("删除", "B_Delete");
    AddToolButtons("确定", "B_Save");
    AddToolButtons("取消", "B_Cancel");
    $("#B_AlreadySearch").css("width", "96px");
    document.getElementById("B_Save").onclick = ArtSaveClick;
    document.getElementById("B_Cancel").onclick = ArtCancelClick;
    document.getElementById("B_Search").onclick = ArtSearchClick;
    //document.getElementById("B_AlreadySearch").onclick = ArtAlreadySearchClick;
    document.getElementById("B_Delete").onclick = ArtDeleteClick;
    $("#bftitle").html(vCaption);
    $(".btnsep:visible:last").hide();
    $(".tabs-wrap").css("margin-left", "-3px");
    vSB = vAlreadyData.filter(function (item) {
        return parseInt(item.iSJLX) == 4;
    });
    vHT = vAlreadyData.filter(function (item) {
        return parseInt(item.iSJLX) == 2;
    });
    vSP = vAlreadyData.filter(function (item) {
        return parseInt(item.iSJLX) == 6;
    });
    vSB = vSB.map(function (item) {
        return item.iSJNR;
    });
    vHT = vHT.map(function (item) {
        return item.iSJNR;
    });
    vSP = vSP.map(function (item) {
        return item.iSJNR;
    });
    $('#tt').tabs({
        onSelect: function (title, tab_index) {
            vTabTitle = title;
            vTabIndex = tab_index;
            vAlreadyTabData = vAlreadyData.filter(function (item) {
                return parseInt(item.iSJLX) == vTabIndex + 1;
            });
            vAlreadyTabData = vAlreadyTabData.map(function (item) {
                return item.iSJNR;
            })
            var tpSearchEnable = (vSearchEnable == true) || (vSearchEnable == false && vAlreadyTabData.length > 0);
            switch (title) {
                //case "商品商标":
                //    vTabIdField = "iSHSBID";
                //    vUrl = "../../HYKGL/HYKGL.ashx";
                //    vPageMsgID = vBrandPageId;
                //    vTabDialogName = "ListSPSB";
                //    if ($("#list_brand").datagrid("getData").rows.length <= 0 && tpSearchEnable) {
                //        SearchData("list_brand");
                //    }
                //    break;
                //case "合同":
                //    vTabIdField = "iSHHTID";
                //    vUrl = "../../CRMGL/CRMGL.ashx";
                //    vPageMsgID = vContractPageId;
                //    if ($("#list_contract").datagrid("getData").rows.length <= 0 && tpSearchEnable) {
                //        SearchData("list_contract");
                //    }
                //    break;
                //case "商品":
                //    vTabIdField = "iSHSPID";
                //    vUrl = "../../CRMGL/CRMGL.ashx";
                //    vPageMsgID = vGoodsPageId;
                //    if ($("#list_goods").datagrid("getData").rows.length <= 0 && tpSearchEnable) {
                //        SearchData("list_goods");
                //    }
                //    break;
                //case "部门":
                //    var treeObj = $.fn.zTree.getZTreeObj("treeBM");
                //    var nodes = treeObj.getNodes();
                //    vUrl = "../../HYKGL/HYKGL.ashx";
                //    vTabDialogName = "TreeSHBM";
                //    if (nodes.length <= 0 && tpSearchEnable) {
                //        SearchTreeData("treeBM");
                //    }
                //    break;
                //case "商品分类":
                //    vUrl = "../../HYKGL/HYKGL.ashx";
                //    var treeObj = $.fn.zTree.getZTreeObj("treeFL");
                //    vTabDialogName = "TreeSHSPFL";
                //    if (!treeObj && tpSearchEnable) {
                //        SearchTreeData("treeFL");
                //    }
                //    break;
            }
        },
    });
    $('#tt').tabs('select', 0);
    InitTree();
    InitGrid();


    //vAlreadyTabData = vAlreadyData.filter(function (item) {
    //    return parseInt(item.iSJLX) == vTabIndex + 1;
    //});
    //vAlreadyTabData = vAlreadyTabData.map(function (item) {
    //    return item.iSJNR;
    //})
    var tpSearchEnable = (vSearchEnable == true) || (vSearchEnable == false && vAlreadyTabData.length > 0);
    //if (tpSearchEnable) {
    //SearchTreeData("treeBM");
    //SearchTreeData("treeFL", 3);
    //}
    $("#B_Search").prop("disabled", !vSearchEnable);
    $("#B_Save").prop("disabled", !vSearchEnable);
    $("#B_Delete").prop("disabled", !vSearchEnable);
});

function DrawTabGrid(listName, vColumns, vIdField, sortName) {
    $("#" + listName).datagrid({
        width: '100%',
        height: 400,
        autoRowHeight: false,
        striped: true,
        columns: [vColumns],
        singleSelect: vSingleSelect,
        showHeader: true,
        showFooter: true,
        rownumbers: true, //添加一列显示行号
        checkOnSelect: false,
        selectOnCheck: false,
        pagePosition: 'bottom',
        rownumbers: true, //添加一列显示行号
        pagination: true,  //启用分页
        showFooter: false,
        fitColumns: true,
        pageNumber: 1,
        pageSize: 1000,
        pageList: [1000, 2000, 5000],
        sortName: sortName,
        idField: vIdField,
        onClickRow: onClickRow,
        onUnselect: onUnselect,
        onSelect: onSelect,
        onLoadSuccess: function () {
            //ShowAlreadySelected()
            $(this).datagrid('enableDnd');
        }
    });
    var pager = $('#' + listName).datagrid("getPager");
    pager.pagination({
        onSelectPage: function (pageNum, pageSize) {
            SearchData(listName, pageNum, pageSize, sortName, null);
            //ShowAlreadySelected()
        },
    });
}

function SearchData(listName, page, rows, sort, order) {
    var obj = MakeSearchJSON(listName);
    var func;
    var filt;
    var idf;
    listName = listName || "list";
    page = page || $("#" + listName).datagrid("options").pageNumber;
    rows = rows || $("#" + listName).datagrid("options").pageSize;
    sort = sort || $("#" + listName).datagrid("options").sortName;
    order = order || $("#" + listName).datagrid("options").sortOrder;
    switch (listName) {
        case "list_brand": func = vBrandPageId; break;
        case "list_contract": func = vContractPageId; break;
        case "list_goods": func = vGoodsPageId; break;
    }
    $("#" + listName).datagrid("loading");
    $.ajax({
        type: "post",
        url: vUrl + "?mode=Search&func=" + func,
        async: true,
        data: {
            json: JSON.stringify(obj),
            titles: "cybillsearch",
            page: page,
            rows: rows,
            sort: sort,
            order: order,
        },
        success: function (data) {
            if (data.indexOf("错误") >= 0 || data.indexOf("error") >= 0) {
                ShowMessage(data);
            }
            //$("#" + listName).datagrid("loadData", JSON.parse(data), "json");
            var list = JSON.parse(data);
            for (var i = 0; i < list.rows.length; i++) {
                //查找已有
                idf = $("#" + listName).datagrid("options").idField;
                filt = $("#" + listName).datagrid("getRows").filter(function (item) {
                    return parseInt(item[idf]) == list.rows[i][idf];
                });
                if (filt.length == 0)
                    $("#" + listName).datagrid("appendRow", list.rows[i]);
                else
                    $("#" + listName).datagrid("selectRecord", list.rows[i][idf]);
            }
            if (list.rows.length == 0 && vSrchCount > 2) {
                ShowMessage("没有查到数据");
            }
            vSrchCount++;
            $("#" + listName).datagrid("loaded");
            //vSearchData = data;
            ////处理已选中的信息
            //var dataRows = $("#" + listName).datagrid("getData").rows;
            //for (var i = 0; i < dataRows.length; i++) {
            //    for (var j = 0; j < vAlreadyTabData.length; j++) {
            //        var rowData = dataRows[i];
            //        if (rowData[vTabIdField] == vAlreadyTabData[j]) {
            //            $("#" + listName).datagrid("selectRecord", rowData[vTabIdField]);  //
            //        }

            //    }
            //}
        },
        error: function (data) {
            ShowMessage(data);
        }
    });
}

function MakeSearchJSON(listName) {
    var cond = MakeSearchCondition(listName);
    MakeSrchCondition2(cond, vData.SHDM, "sSHDM", "=", true);
    if (cond == null)
        return;
    var Obj = new Object();
    Obj.SearchConditions = cond;
    Obj.iLoginRYID = iDJR;
    //AddCustomerCondition(Obj);
    return Obj;
}

function MakeSearchCondition(listName) {
    var arrayObj = new Array();
    if (listName == "list_brand") {
        if (vSB.length == 0 && vSrchCount <= 2)
            vSB.push("-1");
        MakeSrchCondition(arrayObj, "TB_SBMC", "sSBMC", "like", true);
        MakeSrchCondition(arrayObj, "TB_SBPYM", "sPYM", "like", true);
        MakeSrchCondition2(arrayObj, vSB.join(","), "iSHSBID", "in", false);
        vSB.length = 0;
    }
    if (listName == "list_contract") {
        if (vHT.length == 0 && vSrchCount <= 2)
            vHT.push("-1");
        MakeSrchCondition(arrayObj, "TB_HTH", "sHTH", "=", true);
        MakeSrchCondition(arrayObj, "TB_GHSMC", "sGHSMC", "like", true);
        MakeSrchCondition2(arrayObj, vHT.join(","), "iSHHTID", "in", false);
        vHT.length = 0;
    }
    if (listName == "list_goods") {
        if (vSP.length == 0 && vSrchCount <= 2)
            vSP.push("-1");
        MakeSrchCondition(arrayObj, "TB_SPDM", "sSPDM", "=", true);
        MakeSrchCondition(arrayObj, "TB_SPMC", "sSPMC", "like", true);
        MakeSrchCondition2(arrayObj, vSP.join(","), "iSHSPID", "in", false);
        vSP.length = 0;
    }
    //if (vTabTitle == "部门") {
    //    MakeSrchCondition2(arrayObj, vAlreadyTabData.join(","), "iSHBMID", "in", false);
    //}
    //if (vTabTitle == "商品分类") {
    //    MakeSrchCondition2(arrayObj, vAlreadyTabData.join(","), "iSHSPFLID", "in", false);
    //}
    return arrayObj;
}

//function AddCustomerCondition(Obj) {
//    Obj.sDialogName = vTabDialogName;
//    Obj.dialogName = vTabDialogName;
//    if (vData) {
//        if (vData.SHDM) { Obj.sSHDM = vData.SHDM; }
//    }
//    //switch (vTabTitle) {
//    //    case "部门":
//    //    case "商品分类":
//    //    case "合同":

//    //        break;
//    //}
//    //switch (vTabTitle) {
//    //    case "部门":
//    //        break;
//    //    case "商品分类":
//    //        break;
//    //    case "合同":
//    //        Obj.sSPFLMC = $("#TB_SPFLMC").val();
//    //        break;
//    //}
//}

function ArtDeleteClick() {
    var listName;
    var del = [];
    switch (vTabTitle) {
        case "商品商标":
            listName = "list_brand";
            break;
        case "合同":
            listName = "list_contract";
            break;
        case "商品":
            listName = "list_goods";
            break;
    }
    for (var i = 0; i < $("#" + listName).datagrid("getSelections").length; i++) {
        del.push($("#" + listName).datagrid("getRowIndex", $("#" + listName).datagrid("getSelections")[i]));
    }
    del.sort();
    for (var i = del.length - 1; i >= 0; i--) {
        $("#" + listName).datagrid("deleteRow", del[i]);
    }
}

function ArtSaveClick() {
    //vReturnData应该在这里重新获取而不是在各种事件里处理
    GetTreeChecked("treeBM", 1);
    GetTreeChecked("treeFL", 3);
    GetGridChecked("list_contract", 2);
    GetGridChecked("list_brand", 4);
    GetGridChecked("list_goods", 6);
    $.dialog.data(vDialogName, JSON.stringify(vReturnData));
    //$.dialog.data('dialogSelected', vReturnData.length > 0);
    $.dialog.data('dialogSelected', true);
    $.dialog.close();
}

function GetTreeChecked(tree, sjlx) {
    var treeObj = $.fn.zTree.getZTreeObj(tree);
    if (treeObj == null)
        return;
    var nodes = treeObj.getCheckedNodes(true);
    for (var i = 0; i < nodes.length; i++) {
        if (nodes[i].getCheckStatus().checked && !nodes[i].getCheckStatus().half)
            //if (nodes[i].id.indexOf(nodes[pid].id) != 0) {
            if (nodes[i].getParentNode() == null || nodes[i].getParentNode() != null && (nodes[i].getParentNode().nocheck || nodes[i].getParentNode().getCheckStatus().checked && nodes[i].getParentNode().getCheckStatus().half)) {
                vReturnData.push({ iSJLX: sjlx, iSJNR: parseInt(nodes[i].iJLBH) });
            }
    }
}

function GetGridChecked(list, sjlx) {
    var lst = new Array();
    lst = $("#" + list).datagrid("getData").rows;
    for (var i = 0; i < lst.length; i++) {
        vReturnData.push({ iSJLX: sjlx, iSJNR: parseInt(lst[i][$("#" + list).datagrid("options").idField]) });
    }
}

function ArtCancelClick() {
    $.dialog.data('dialogSelected', false);
    $.dialog.close();
}

function ArtSearchClick() {
    vSearchAlreadyEnable = false;
    var listName;
    switch (vTabTitle) {
        case "商品商标":
            if ($("#TB_SBMC").val() == "" && $("#TB_SBPYM").val() == "") {
                ShowMessage("请输入查询条件");
                return;
            }
            listName = "list_brand";
            break;
        case "合同":
            if ($("#TB_HTH").val() == "" && $("#TB_GHSMC").val() == "") {
                ShowMessage("请输入查询条件");
                return;
            }
            listName = "list_contract";
            break;
        case "商品":
            if ($("#TB_SPDM").val() == "" && $("#TB_SPMC").val() == "") {
                ShowMessage("请输入查询条件");
                return;
            }
            listName = "list_goods";
            break;
    }
    if (listName) {
        SearchData(listName);
    }
}

function ArtAlreadySearchClick() {
    vSearchAlreadyEnable = true;
    vTmpReturnData = vReturnData.filter(function (item) {
        return item.iSJLX == (vTabIndex + 1);
    });
    vTmpReturnData = vTmpReturnData.map(function (item) {
        return item.iSJNR;
    })
    switch (vTabTitle) {
        case "商品商标":
            listName = "list_brand";
            break;
        case "合同":
            listName = "list_contract";
            break;
        case "商品":
            listName = "list_goods";
            break;
    }

    if (listName) {
        SearchData(listName);
    }
}

function onClickRow() {
    //Pass;
}

function onSelect(rowIndex, rowData) {
    //vReturnData = vReturnData.filter(function (item) {
    //    return !(item.iSJLX == (vTabIndex + 1) && item.iSJNR == parseInt(rowData[vTabIdField]));
    //});
    //vReturnData.push({ iSJLX: (vTabIndex + 1), iSJNR: rowData[vTabIdField] });
}

//function zTreeOnCheck(event, treeId, treeNode) {
//    vReturnData = vReturnData.filter(function (item) {
//        return !(item.iSJLX == (vTabIndex + 1) && item.iSJNR == parseInt(treeNode.actid));
//    });
//    //if (treeNode.checked) {
//    //    vReturnData.push({ iSJLX: (vTabIndex + 1), iSJNR: parseInt(treeNode.actid) });
//    //}
//}

function onUnselect(rowIndex, rowData) {
    //vReturnData = vReturnData.filter(function (item) {
    //    return !(item.iSJLX == (vTabIndex + 1) && item.iSJNR == parseInt(rowData[vTabIdField]));
    //});
}

//function SearchTreeData(TreeName, Type) {
//    var obj = MakeSearchJSON();
//    vPageMsgID = vTreePageId;
//    $.ajax({
//        type: "post",
//        url: vUrl + "?mode=Print&func=" + vPageMsgID,
//        async: true,
//        data: {
//            json: JSON.stringify(obj),
//            titles: 'cybillsearch',
//        },
//        success: function (data) {
//            if (data.indexOf('错误') >= 0 || data.indexOf('error') >= 0) {
//                ShowMessage(data);
//            }
//            InitTree(data, TreeName, Type);
//        },
//        error: function (data) {
//            ShowMessage(data);
//        }
//    });

//};

//function InitTree(data, TreeName, Type) {
//    var zNodes = [{ id: "-1", pId: "-1", name: "" + vTabTitle + "", open: true, "nocheck": true }];
//    if (data) {
//        data = JSON.parse(data);
//        for (var i = 0; i < data.length; i++) {
//            var obj = new Object();
//            obj.id = data[i].sID;
//            obj.name = data[i].sTalName;
//            obj.shortName = data[i].sName;
//            obj.actid = data[i].iActId;
//            obj.chkDisabled = !vSearchEnable;
//            //if (vAlreadyTabData.indexOf(obj.actid) >= 0)
//            //    obj.checked = true;
//            //此处需要封装
//            if (obj.id.length <= 2)
//                obj.pId = "-1";
//            else
//                obj.pId = obj.id.substr(0, obj.id.length - 2);
//            zNodes.push(obj);
//        }
//    }
//    if (Type != undefined) {
//        var vAlreadyTreeData = vAlreadyData.filter(function (item) {
//            return parseInt(item.iSJLX) == Type;
//        });
//        var vAlreadyTreeData = vAlreadyTreeData.map(function (item) {
//            return item.iSJNR;
//        });
//        zTreeObj = $.fn.zTree.init($("#" + TreeName), setting, zNodes);
//        //选中
//        for (var i = 0; i < vAlreadyTreeData.length; i++) {
//            var node = zTreeObj.getNodeByParam("actid", vAlreadyTreeData[i]);
//            if (node)
//                zTreeObj.checkNode(node, true, true);
//        }

//    }
//    else {
//        zTreeObj = $.fn.zTree.init($("#" + TreeName), setting, zNodes);
//        //选中
//        for (var i = 0; i < vAlreadyTabData.length; i++) {
//            var node = zTreeObj.getNodeByParam("actid", vAlreadyTabData[i]);
//            if (node)
//                zTreeObj.checkNode(node, true, true);
//        }
//    }
//}