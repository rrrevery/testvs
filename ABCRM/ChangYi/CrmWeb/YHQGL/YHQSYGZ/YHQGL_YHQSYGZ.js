vUrl = "../YHQGL.ashx";

function IsValidData() {
    if ($("#TB_GZMC").val() == "") {
        art.dialog({ lock: true, content: "请输入规则名称" });
        return false;
    }
    if ($("#TB_SYSX").val() == "") {
        art.dialog({ lock: true, content: "请输入用券上限" });
        return false;
    }
    return true;
}

function InitGrid() {
    vColumnNames = ['销售金额', '用券金额', ];
    vColumnModel = [
         { name: "fYQBL_XFJE", width: 90, editor: 'text' },
         { name: "fYQBL_YHQJE", width: 90, editor: 'text' },
    ];
};

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();//
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sYHQSYGZMC = $("#TB_GZMC").val();
    Obj.fZDYQJE = $("#TB_SYSX").val();
    Obj.iMDID = $("#HF_MDID").val() == "" ? "-1" : $("#HF_MDID").val();
    Obj.iBJ_TY = $("#CB_BJ_TY")[0].checked ? "1" : "0";
    Obj.iBJ_PB = ($("[name='BJ_PB']:checked").val() != undefined) ? $("[name='BJ_PB']:checked").val() : "-1";
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
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });

    $("#AddItem").click(function () {
        $("#list").datagrid("appendRow", {});
    });
    $("#DelItem").click(function () {
        DeleteRows("list");
    });

})

function CheckReapet(value) {
    var boolReapet = true;
    for (i = 0; i < $("#list").getGridParam("reccount") - 1; i++) {
        var RowDate = $("#list").getRowData(i);
        if (RowDate.fYQBL_XFJE == value) {
            boolReapet = false;
        }

    }
    return boolReapet;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_GZMC").val(Obj.sYHQSYGZMC);
    $("#TB_SYSX").val(Obj.fZDYQJE);
    if (Obj.iBJ_TY == 1) {
        $("#CB_BJ_TY").attr("checked", true);
    }
    $("[name='BJ_PB'][value='" + Obj.iBJ_PB + "']").attr("checked", true);
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}
