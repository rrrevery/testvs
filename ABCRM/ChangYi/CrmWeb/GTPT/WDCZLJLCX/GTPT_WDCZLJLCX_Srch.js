vUrl = "../GTPT.ashx";

function InitGrid() {
    vColumnNames = ["记录编号", "日期", "登记时间", "登记记录编号", "会员ID", "礼包号", "领奖有效期", "状态", "会员卡号", "奖品名称"];
    vColumnModel = [
            { name: 'iJLBH', width: 150, },
            { name: 'dRQ', width: 150, },
            { name: 'dDJSJ', width: 150, },
            { name: 'iDJJLBH', width: 150, },
            { name: 'iHYID', hidden: true, },
            { name: 'iLBID', hidden: true, },
            { name: 'dLJYXQ', width: 150, },
            { name: 'sSTATUS', width: 80,},
            { name: 'sHYK_NO', width: 80, },
            { name: 'sLBMC', width: 80, },
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
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_LQSJ1", "dLJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_LQSJ2", "dLJSJ", "<=", true);
    var vSTATUS = $("[name='STATUS']:checked").val();
    if (vSTATUS != 'all') {
        MakeSrchCondition2(arrayObj, vSTATUS, "iSTATUS", "=", false);
    }
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
