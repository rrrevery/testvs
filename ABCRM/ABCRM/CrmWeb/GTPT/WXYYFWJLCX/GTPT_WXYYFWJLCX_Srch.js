vUrl = "../GTPT.ashx";

function InitGrid() {
    vColumnNames = ["记录编号", "ID", "预约主题",  "会员卡号", "日期", "顾客姓名", "联系电话", "登记时间","预约状态"];
    vColumnModel = [
           { name: 'iJLBH', width: 80, },
            { name: 'iID', hidden: true, },
            { name: 'sMC', width: 80, },
            { name: 'sHYK_NO', width: 80, },
            { name: 'dRQ', width: 80, },
             { name: 'sGKXM', width: 80, },
            { name: 'sLXDH', width: 150, },
            { name: 'dDJSJ', width: 80, },
            { name: 'sSTATUS', width: 80, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    RefreshButtonSep();
});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_SJHM", "sLXDH", "=", true);
    MakeSrchCondition(arrayObj, "TB_HYK_NO", "sHYK_NO", "=", true);
    MakeSrchCondition(arrayObj, "TB_RQ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_RQ1", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_WX_NO", "sWX_NO", "=", true);
    var vSTATUS = $("[name='STATUS']:checked").val();
    if (vSTATUS!="all") {
        MakeSrchCondition2(arrayObj, vSTATUS, "iSTATUS", "=", false);
    }
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};