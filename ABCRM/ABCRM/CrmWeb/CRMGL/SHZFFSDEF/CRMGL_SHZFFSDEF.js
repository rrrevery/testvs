vUrl = "../CRMGL.ashx";
vCaption = "商户支付方式定义";

function InitGrid() {
    vColumnNames = ['记录编号', '支付方式名称', '支付方式代码', '商户', '积分标记', '返券标记', '满百减折标记', '优惠券代码', "优惠券", '积分比例'];
    vColumnModel = [
         { name: 'iJLBH', hidden: true, },
        { name: 'sZFFSMC' },
        { name: 'sZFFSDM', width: 80 },
        { name: 'sSHMC', width: 80 },
        { name: 'iBJ_JF', width: 60, formatter: BoolCellFormat },
        { name: 'iBJ_FQ', width: 60, formatter: BoolCellFormat },
        { name: 'iBJ_MBJZ', width: 90, formatter: BoolCellFormat },
        { name: 'iYHQID', hidden: true, },
        { name: "sYHQMC", width: 90 },
        { name: 'fJFBL', hidden: true },
    ];
}

function IsValidData() {
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.fJFBL = $("#TB_JFBL").val();
    Obj.iBJ_JF = $("#TB_BJ_JF")[0].checked ? "1" : "0";
    Obj.iBJ_FQ = $("#TB_BJ_FQ")[0].checked ? "1" : "0";
    Obj.iBJ_MBJZ = $("#TB_BJ_MBJZ")[0].checked ? "1" : "0";

    return Obj;
}

$(document).ready(function () {
    $("#B_Insert").hide();
    $("#B_Delete").hide();
    $("#B_Exec").hide();
    $("#status-bar").hide();
    RefreshButtonSep();
})

function ShowData(Obj) {
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_ZFFSDM").text(Obj.sZFFSDM);
    $("#TB_ZFFSMC").text(Obj.sZFFSMC);
    $("#TB_JFBL").val(Obj.fJFBL);
    $("#TB_BJ_JF").prop("checked", Obj.iBJ_JF == 1);
    $("#TB_BJ_FQ").prop("checked", Obj.iBJ_FQ == 1);
    $("#TB_BJ_MBJZ").prop("checked", Obj.iBJ_MBJZ == 1);
}

