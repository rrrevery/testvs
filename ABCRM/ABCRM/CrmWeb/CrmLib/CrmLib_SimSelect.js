var vUrl = "";
var vDialogName = "";
var vPageMsgID = "1";
var bAutoShow = (GetUrlParam("autoshow") || 0) == 1;
var bMulSel = ((GetUrlParam("mult") == "true") || false) == true;
var bSelected = false;

$(document).ready(function () {
    AddToolButtons("查询", "B_Search", "btn-dialog");
    AddToolButtons("确定", "B_Select", "btn-dialog");
    AddToolButtons("取消", "B_Cancel", "btn-dialog");
    AddToolButtons("全选", "B_All", "btn-dialog");
    AddToolButtons("全取消", "B_None", "btn-dialog");
    AddCustomerButton();
    SetControlBaseState();
    document.getElementById("B_Search").onclick = SearchClick;
    document.getElementById("B_Select").onclick = SelectClick;
    document.getElementById("B_Cancel").onclick = CancelClick;
    document.getElementById("B_All").onclick = AllClick;
    document.getElementById("B_None").onclick = NoneClick;
    $("#B_All").hide();
    $("#B_None").hide();
    //if (!bMulSel) {
    //    $("#B_All").hide();
    //    $("#B_None").hide();
    //}
    //else {
    //    $("#B_All").show();
    //    $("#B_None").show();
    //}
    var turl = "";
    if (bAutoShow)
        turl = vUrl + "?mode=Search&func=" + vPageMsgID;
    try {
        if (typeof (eval("InitGrid")) == "function") {
            InitGrid();
            $("#list").jqGrid({
                url: turl,
                async: false,
                datatype: "json",
                //mtype:"get",//默认GET
                colNames: vColumnNames,
                colModel: vColumnModel,
                //toolbar: [true, "top"],
                //cellEdit: true,
                sortable: true,
                sortorder: "asc",
                sortname: vColumnModel[0].name,
                shrinkToFit: false,
                autoScroll: true,
                rownumbers: true,
                altRows: true,
                width: 440,
                // autowidth:true,
                autoheight: true,
                rowNum: 15,
                rowList: [15, 50, 100],
                pager: '#pager',
                viewrecords: true,
                multiselect: bMulSel,
                onSelectRow: function (rowid) {
                    SetControlBaseState();
                    //var rowData = $("#list").getRowData(rowid);
                    //var bExecuted = rowData.iZXR != undefined && rowData.iZXR != "0";//已审核
                    //var bHasData = rowData.iJLBH != "0";//有数据
                    //vJLBH = rowData.iJLBH;

                    //document.getElementById("B_Insert").disabled = !bCanEdit;
                    //document.getElementById("B_Update").disabled = !(bHasData && !bExecuted) || !bCanEdit;
                    //document.getElementById("B_Delete").disabled = !(bHasData && !bExecuted) || !bCanEdit;
                    //document.getElementById("B_Exec").disabled = !(bHasData && !bExecuted) || !bCanExec;
                },
                loadComplete: function (data) {
                    var lst = $.dialog.data("dialogInput").split(",");
                    for (var i = 0; i <= lst.length - 1; i++) {
                        for (var j = 0; j < data.rows.length; j++) {
                            if (data.rows[j][vColumnModel[0].name] == lst[i])
                                $("#list").setSelection(j + 1, false);
                        }
                    }
                }
            });

            $("#first_pager").html("首页");
            $("#prev_pager").html("上一页");
            $("#next_pager").html("下一页");
            $("#last_pager").html("末页");;
        }
    } catch (e) { }
});

function AddCustomerButton() {
    //单据特殊的按钮在这里加，因为如果直接AddToolButtons的话，会覆盖原来按钮的事件，所以加了一个这个方法
    ;
}
function SetControlBaseState() {
    //document.getElementById("B_Search").disabled = !bEditMode;
    //document.getElementById("B_Select").disabled = bEditMode || !bCanEdit;
    //document.getElementById("B_All").disabled = !(!bEditMode && bHasData && !bExecuted) || !bCanEdit;
    //document.getElementById("B_None").disabled = document.getElementById("B_All").disabled; //!(!bEditMode && bHasData && !bExecuted);
}
function SetControlBase() {
}

function SearchClick() {
    if (!IsValidSearch())
        return;
    try {
        if (typeof (eval("MakeSearchCondition")) == "function") {
            var obj = MakeSearchJSON();
            if (obj == null)
                return;
            $("#list").jqGrid('setGridParam', {
                url: vUrl + "?mode=Search&func=" + vPageMsgID,
                postData: { 'json': JSON.stringify(obj) },
                page: 1,
                loadError: function (xhr, status, error) {
                    ShowMessage(xhr.responseText);
                }
            }).trigger("reloadGrid");
        }
    } catch (e) {
        DoSearch("Search");
    }
    SetControlBaseState();
};
function IsValidSearch() {
    return true;
}

function SelectClick() {
    SetControlBaseState();
    DoSelect();
    SelectClickCustom();
    $.dialog.data("dialogSelected", true);
    $.dialog.close();
};
function SelectClickCustom() {
};

function CancelClick() {
    CancelClickCustom();
    $.dialog.data("dialogSelected", false);
    $.dialog.close();
};
function CancelClickCustom() {
};

function AllClick() {
    SetControlBaseState();
    AllClickCustom();
};
function AllClickCustom() {
};

function NoneClick() {
    SetControlBaseState();
    NoneClickCustom();
};
function NoneClickCustom() {
};

function DoSelect() {
    var inx;
    var sel = new Array();
    if (bMulSel) {
        var inx = $("#list").jqGrid("getGridParam", "selarrrow");
        if (inx != null && inx.length) {
            for (var i = 0; i < inx.length; i++) {
                sel.push($("#list").jqGrid("getRowData", inx[i]));
            }
        }
    }
    else {
        inx = $("#list").jqGrid("getGridParam", "selrow");
        sel.push($("#list").getRowData(inx));
    }
    $.dialog.data(vDialogName, sel);
    //$.dialog.close();
};

function MakeSearchJSON() {
    var cond = MakeSearchCondition();
    if (cond == null)
        return;
    var Obj = new Object();
    Obj.SearchConditions = cond;

    var colModels = "", colNames = "";
    var cols = $("#list").jqGrid("getGridParam", "colModel");
    var names = $("#list").jqGrid("getGridParam", "colNames");
    for (i = 1; i < cols.length; i++) {
        if (!cols[i].hidden) {
            colModels += cols[i].name + "|";
            colNames += names[i] + "|";
        }
    }
    Obj.sColNames = colNames;
    Obj.sColModels = colModels;
    Obj.iLoginRYID = iDJR;
    AddCustomerCondition(Obj);

    return Obj;
}

function AddCustomerCondition(Obj) {

};