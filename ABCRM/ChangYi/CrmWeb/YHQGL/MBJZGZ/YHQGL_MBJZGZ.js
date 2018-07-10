
vUrl = "../YHQGL.ashx";

function InitGrid() {
    vColumnNames = ['销售金额', '折扣金额', ];
    vColumnModel = [
         { name: "fXSJE", width: 90, editor: 'text' },
         { name: "fZKJE", width: 90, editor: 'text' },
    ];
};



function SetControlState() {
}

function IsValidData() {
    if ($("#TB_GZMC").val() == "") {
        art.dialog({ lock: true, content: "请输入规则名称！" });
        return false;
    }

    if ($("#TB_FFXE").val() == "") {
        art.dialog({ lock: true, content: "请输入发放限额！" });
        return false;
    }

    if ($("#TB_QDJE").val() == "") {
        art.dialog({ lock: true, content: "请输入起点金额！" });
        return false;
    }
    return true;
}

function SaveData() {

    var Obj = new Object();

    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";

    Obj.sMBJZGZMC = $("#TB_GZMC").val();
    Obj.fFFXE = $("#TB_FFXE").val();
    Obj.fQDJE = $("#TB_QDJE").val();
    Obj.iBJ_TY = $("#CB_BJ_TY")[0].checked ? "1" : "0";
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();

    SetControlState();
    $("#AddItem").click(function () {
        $("#list").datagrid("appendRow", {});
    });
    $("#DelItem").click(function () {
        DeleteRows("list");
    });
})

//DbClick 把数据从数据库查询出来
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);

    $("#TB_GZDM").val(Obj.iJLBH);
    $("#TB_GZMC").val(Obj.sMBJZGZMC);
    $("#TB_FFXE").val(Obj.fFFXE);
    $("#TB_QDJE").val(Obj.fQDJE);

    if (Obj.iBJ_TY == 1) {
        $("#CB_BJ_TY").attr("checked", true);
    }
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}


function CheckReapet(value) {
    var boolReapet = true;
    for (i = 0; i < $("#list").getGridParam("reccount") - 1; i++) {
        var RowDate = $("#list").getRowData(i);
        if (RowDate.fXSJE == value) {
            boolReapet = false;
        }

    }

    return boolReapet;

}

function addNavi(gridname) {
    var ROWID = "listrow_" + 0;//记录list最新rowid 
    var OLDROWID = ROWID;//记录list上一个rowid
    //操作grid的按钮
    function RowControlState(isEdit) {
        if (isEdit) {
            $("#list_iladd").hide();
            $("#list_iledit").hide();
            $("#list_ilsave").show();
            $("#list_ilcancel").show();
            $("#list_ildel").hide();
        }
        else {
            $("#list_iladd").show();
            $("#list_iledit").show();
            $("#list_ilsave").hide();
            $("#list_ilcancel").hide();
            $("#list_ildel").show();
        }
    }
    //行操作 add edit save cancel del  rowid="listrow_"+num;num以1开始，依次加1
    function ControlRow(gridId, controlString) {
        var isEdit = false;
        var success = false;
        var gridTable = $("#" + gridId);
        if (controlString == "add") {
            OLDROWID = ROWID;
            ROWID = "listrow_" + (1 + parseInt(ROWID.substring(ROWID.indexOf("_") + 1)));
            gridTable.jqGrid("addRowData", ROWID, {}, "last");
            gridTable.jqGrid("setSelection", ROWID);
            isEdit = false;
            success = true;
            $("#list_iledit").trigger("click");
            return;
        }
        var selRow = gridTable.jqGrid("getGridParam", "selrow");
        if (selRow == null || selRow == "") {
            art.dialog({ content: '无选中的行!', lock: true, time: 2 });
            return;
        }
        switch (controlString) {
            case "edit":
                gridTable.jqGrid("editRow", selRow,
                    {
                        "keys": false,
                        "oneditfunc": function (rowid) {
                            isEdit = true;
                            success = true;
                        },
                    });


                break;
            case "save":
                gridTable.jqGrid("saveRow", selRow,
                    {
                        successfunc: function (response) {
                            success = true;
                            isEdit = false;
                            return true;
                        },
                        errorfunc: function (rowid, response) {
                            isEdit = true;
                            success = false;
                        }
                    });

                break;
            case "cancel":
                gridTable.jqGrid("restoreRow", selRow,
                {
                    "afterrestorefunc": function (rowid) {
                        if ($("#" + gridId).jqGrid("getCell", rowid, 1) == "") {
                            gridTable.jqGrid("delRowData", rowid);

                        }
                        isEdit = false;
                        success = true;
                    }
                });

                break;
            case "del":
                gridTable.jqGrid("delRowData", selRow);
                isEdit = false;
                success = true;
                //删除请表行时，删除子表
                if (gridId == "list") {
                    $("#listsub_" + selRow).parents("tr[class='ui-subgrid']").remove();
                }
                break;

        }
        if (success) {
            RowControlState(isEdit);
        }
    }
    $("#" + gridname).navGrid("#pager", { edit: false, add: false, save: false, cancel: false, del: false, refresh: false, search: false, })
          .navButtonAdd('#pager', {
              caption: "添加",
              title: "回车键保存，ESC取消编辑",
              id: 'list_iladd',
              buttonicon: "ui-icon-plus",
              onClickButton: function () {
                  ControlRow("list", "add");
              },
              position: "left"
          }).navButtonAdd('#pager', {
              caption: "编辑",
              id: 'list_iledit',
              buttonicon: "ui-icon-pencil",
              onClickButton: function () {
                  ControlRow("list", "edit");
              },
              position: "left"
          }).navButtonAdd('#pager', {
              caption: "保存",
              id: 'list_ilsave',
              buttonicon: "ui-icon-disk",
              onClickButton: function () {
                  ControlRow("list", "save");
              },
              position: "left"
          }).navButtonAdd('#pager', {
              caption: "取消",
              id: 'list_ilcancel',
              buttonicon: "ui-icon-cancel",
              onClickButton: function () {
                  ControlRow("list", "cancel");
              },
              position: "left"
          }).navButtonAdd('#pager', {
              caption: "删除",
              id: 'list_ildel',
              buttonicon: "ui-icon-trash",
              onClickButton: function () {
                  ControlRow("list", "del");
              },
              position: "left"
          });
}

