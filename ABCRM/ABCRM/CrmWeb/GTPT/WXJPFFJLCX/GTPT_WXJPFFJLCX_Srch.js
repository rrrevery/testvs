vUrl = "../GTPT.ashx";

function InitGrid() {
    vColumnNames = ["记录编号", "类型", "状态", "微信号", "奖品类型", "奖品名称", "奖品级次",  "金额/积分", "优惠券结束日期", "领奖有效期", "登记时间"];
    vColumnModel = [
            { name: 'iJLBH', width: 150, },
             {
                 name: 'iDJLX', width: 80, formatter: function (cellvalue, icol) {
                     switch (cellvalue) {
                         case 1:
                             return "抢红包";
                             break;
                         case 2:
                             return "抽奖";
                             break;
                         case 3:
                             return "刮刮卡";
                             break;
                     }
                 }
             },
             {
                 name: 'iSTATUS', width: 80, formatter: function (cellvalue, icol) {
                     switch (cellvalue) {
                         case 1:
                             return "已领取";
                             break;
                         case 0:
                             return "未领取";
                             break;

                     }
                 }
             },
            { name: 'sWX_NO', width: 80, },
            { name: 'iLPLX', hidden: true },
            { name: 'sJPMC', width: 80, },
            { name: 'sJCMC', width: 80, },
            { name: 'fJE', width: 80, },
            { name: 'dJSRQ', width: 150, },
            { name: 'dLJYXQ', width: 150, },
            { name: 'dDJSJ', width: 150, },
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
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_WX_NO", "sWX_NO", "=", true);
    var vDJLX = $("input[name='DJLX']:checked").val();
    var vZT=$("input[name='ZT']:checked").val();
    if (vDJLX != 'all') {
        MakeSrchCondition2(arrayObj, vDJLX, "iDJLX", "=", false);
    }
    if (vZT != 'all') {
        MakeSrchCondition2(arrayObj, vZT, "iSTATUS", "=", false);
    }
    MakeSrchCondition(arrayObj, "TB_LQSJ1", "dLJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_LQSJ2", "dLJSJ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};