vUrl = "../YHQGL.ashx";

function InitGrid() {
    vColumnNames = ["jlbh", "收款金额", "返券金额", ];
    vColumnModel = [
         { name: "iJLBH", hidden: true, },
         { name: "fSKJE", width: 90, editor: 'text' },
         { name: "fFQJE", width: 90, editor: 'text' },
    ];
};


$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#TB_MD").click(function () {
        SelectMD("TB_MD", "HF_MD", "zHF_MD", true);
    });
    $("#TB_YHQ").click(function () {
        SelectYHQ("TB_YHQ", "HF_YHQ", "zHF_YHQ", true);
    });
    $("#TB_XJYHQ").click(function () {
        SelectYHQ("TB_XJYHQ", "HF_XJYHQ", "zHF_XJYHQ", true);
    });
    $("#AddItem").click(function () {
        $("#list").datagrid("appendRow", {});
    });
    $("#DelItem").click(function () {
        DeleteRows("list");
    });

})

function SetControlState() {
    ;
}

function IsValidData() {

    return true;
}

function SaveData() {
    var Obj = new Object();

    Obj.iJLBH = $("#TB_JLBH").val();//
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iMDID = $("#HF_MD").val();
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.iYHQID = $("#HF_YHQ").val();
    Obj.iYHQID_XJ = $("#HF_XJYHQ").val();
    Obj.dYHQSYJSRQ = $("#TB_SYJSRQ").val();
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_MD").val(Obj.iMDID);
    $("#TB_MD").val(Obj.sMDMC);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#HF_YHQ").val(Obj.iYHQID);
    $("#TB_YHQ").val(Obj.sYHQMC);
    $("#HF_XJYHQ").val(Obj.iYHQID_XJ);
    $("#TB_XJYHQ").val(Obj.sYHQMC_XJ);
    $("#TB_SYJSRQ").val(Obj.dYHQSYJSRQ);

    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    //$("#LB_ZXRMC").text(Obj.sZXRMC);
    //$("#HF_ZXR").val(Obj.iZXR);
    //$("#LB_ZXRQ").text(Obj.dZXRQ);
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

