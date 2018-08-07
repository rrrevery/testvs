vUrl = "../CRMGL.ashx";
var currentGridCol;
var currentGridRow;
var mdid = 0;
var rowNumer = 0;
function bindZtree() {
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
            onClick: onClick
        }
    };

    if ($("#HF_MDID").val() != "") {
        PostToCrmlib("FillSQTree", { iMDID: $("#HF_MDID").val() }, function (data) {
            if (data.length < 1) {
                var zNode = '{"id":"00","pid":"0","name":"暂无数据..."}';
                $.fn.zTree.init($("#TreeSQ"), setting, JSON.parse(zNode));
                return;
            }
            var zNodes = "[";
            for (var i = 0; i < data.length; i++) {
                zNodes = zNodes + "{id:'" + data[i].iSQID + "',pId:'0',name:'" + data[i].sSQMC + "',sqms:'" + data[i].sSQMS + "'}";
                if (i < data.length - 1)
                    zNodes = zNodes + ",";
            }
            zNodes = zNodes + "]";
            $.fn.zTree.init($("#TreeSQ"), setting, eval(zNodes));
            SetControlBaseState();
        }, true);
    }
    else
        $.fn.zTree.destroy("TreeLeft");

}

function InitGrid() {
    vColumnNames = ['商圈名称', '商圈编号', '小区编号', '小区名称'];
    vColumnModel = [
            { name: 'sSQMC', width: 100 },
            { name: 'iSQID', hidden: true },
            { name: 'iXQID', hidden: true },
            { name: 'sXQMC', width: 100, },
    ];
}

$(document).ready(function () {
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });
    vProcStatus = cPS_BROWSE;

    $("#DelItem").click(function () {
        DeleteRows("list");
    });

    $("#AddItem").click(function () {
        var zTree = $.fn.zTree.getZTreeObj("TreeSQ");
        nodes = zTree.getSelectedNodes();
        if (nodes.length == 0) {
            ShowMessage("请先选择商圈", 3);
            return;
        }
        else {
            if (parseInt(nodes[0].id) == 0) {
                ShowMessage("请选择有效商圈", 3);
                return;
            }
            else {
                var dialogUrl = "../../CrmArt/ListXQ/CrmArt_ListXQ.aspx?";
                vDialogName = "ListXQ";
                var DataObject = new Object();
                OpenDialog(dialogUrl, "list", DataObject, vDialogName, "iXQID", false);
            }
        }
    });
    RefreshButtonSep();
})

//function InsertClick() {
//    vProcStatus = cPS_MODIFY;
//    SetControlBaseState();
//};

function DeleteClick() {
    ShowYesNoMessage("是否删除？", function () {
        var rows = $('#list').datagrid("getSelections");
        var copyRows = [];
        if (rows.length != 0) {
            for (var j = 0; j < rows.length; j++) {
                copyRows.push(rows[j]);
            }
            for (var i = 0; i < copyRows.length; i++) {
                var index = $('#list').datagrid('getRowIndex', copyRows[i]);
                $('#list').datagrid('deleteRow', index);
            }
            $('#list').datagrid('clearSelections');
            SaveClick();
        }
        else {
            ShowMessage("请选择要删除的行", 3);
        }
    });
}





function beforeClick() {
    ;
}

function onClick(e, treeId, treeNode) {
    $("#list").datagrid("loadData", { total: 0, rows: [] });
    var zTree = $.fn.zTree.getZTreeObj("TreeSQ");
    nodes = zTree.getSelectedNodes();
    if (nodes[0].id != "" && nodes[0].id != "00") {
        $("#TB_JLBH").val(nodes[0].id);
        DoSearch(nodes[0].id);
    }
    else {
        $("#TreeBQ").prop("disabled", true);
        if (vProcStatus == cPS_MODIFY) {
            vProcStatus = cPS_BROWSE;
            SetControlBaseState();
        }
    }
}

function SetControlState() {
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#B_Exec").hide();
    $("#status-bar").hide();
}

function IsValidData() {
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iSQID = Obj.iJLBH;
    Obj.iMDID = $("#HF_MDID").val();
    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;
    return Obj;
}

function ShowData() {
    ;
};

function DoSearch(SQID) {
    sjson = "{'iSQID':'" + SQID + "'}";
    $('#list').datagrid("loading");
    $.ajax({
        type: "post",
        url: vUrl + "?mode=Search&func=" + vPageMsgID,
        async: true,
        data: {
            json: sjson,
            titles: 'cybillsearch',
            page: $('#list').datagrid("options").pageNumber,
            rows: $('#list').datagrid("options").pageSize,
        },
        success: function (data) {
            if (data.indexOf('错误') >= 0 || data.indexOf('error') >= 0) {
                ShowMessage(data);
            }
            $('#list').datagrid('loadData', JSON.parse(data), "json");
            $('#list').datagrid("loaded");
        },
        error: function (data) {
            ShowMessage(data);
        }
    });

};




function MoseDialogCustomerReturn(dialogName, lstData, showField) {
    bindZtree();
    $("#list").datagrid("loadData", { total: 0, rows: [] });
};

function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    var zTree = $.fn.zTree.getZTreeObj("TreeSQ");
    nodes = zTree.getSelectedNodes();

    for (var i = 0; i < lst.length; i++) {
        if (CheckReapet(array, CheckFieldId, lst[i])) {
            if (true) {
                $('#' + listName).datagrid('appendRow', {
                    iSQID: nodes[0].id,
                    sSQMC: nodes[0].name,
                    iXQID: lst[0].iXQID,
                    sXQMC: lst[0].sXQMC,
                });
            }
        }
    }
}


//function WUC_MD_ReturnCustom() {
//    mdid = $("#HF_MDID").val()
//    bindZtree();
//}

//function LoadData(rowData) {
//    var rowNum1 = $("#list").getGridParam("reccount");
//    var array = new Array();
//    if (rowNum1 <= 0) {
//        for (i = 0; i < rowData.length; i++) {
//            $("#list").addRowData(rowNumer, {
//                sSQMC: nodes[0].name,
//                iSQID: nodes[0].id,
//                iXQID: rowData[i].iXQID,
//                sXQMC: rowData[i].sXQMC,
//            })
//            rowNumer = rowNumer + 1;
//        }
//    }

//    else {
//        if (rowNumer == 0) {
//            var rowIDs = $("#list").getDataIDs();
//            rowNumer = rowIDs[rowIDs.length - 1] + 1;
//        }
//        for (j = 0; j < rowNumer; j++) {
//            if ($("#list").getRowData(j) != "") {
//                var ListRow = $("#list").getRowData(j);
//                array[j] = ListRow.iXQID;
//            }
//        }

//        for (q = 0; q < rowData.length; q++) {
//            if (checkRepeat(array, rowData[q].iXQID)) {
//                $("#list").addRowData(rowNumer, {
//                    sSQMC: nodes[0].name,
//                    iSQID: nodes[0].id,
//                    iXQID: rowData[q].iXQID,
//                    sXQMC: rowData[q].sXQMC,
//                })
//                rowNumer = rowNumer + 1;
//            }
//        }
//    }
//}

//function checkRepeat(array, lpDate) {
//    var boolInsert = true;
//    for (i = 0; i < array.length; i++) {
//        if (array[i] == lpDate) {
//            boolInsert = false;
//        }
//    }
//    return boolInsert;
//}