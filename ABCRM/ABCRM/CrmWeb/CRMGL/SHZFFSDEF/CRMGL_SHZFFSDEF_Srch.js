
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
};

$(document).ready(function () {
    //$("#B_Insert").hide();
    $("#B_Delete").hide();
    $("#B_Exec").hide();
    FillSH($("#S_SH"))
    FillYHQ($("#DDL_YHQ"));
    RefreshButtonSep();
});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "S_SH", "sSHDM", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZFFSDM", "sZFFSDM", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZFFSMC", "sZFFSMC", "=", true);
    MakeSrchCondition(arrayObj, "DDL_YHQ", "iYHQID", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};




