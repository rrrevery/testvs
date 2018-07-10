vUrl = "../GTPT.ashx";

function InitGrid() {
    vColumnNames = ["会员卡号", "类型", "奖品名称", '礼品类型', '状态', "金额/积分", "优惠券结束日期", "领奖有效期", "登记时间"];
    vColumnModel = [
            { name: 'sHYK_NO', width: 80, },
              {
                  name: 'iLX', width: 80, formatter: function (cellvalue, icol) {
                      switch (cellvalue) {
                          case 1:
                              return "开卡";
                              break;
                          case 2:
                              return "绑卡";
                              break;
                      }
                  }
              },
            { name: 'sJPMC', width: 80, },
            { name: 'sLPLXMC', width: 80, },
                {
                    name: 'iSTATUS', width: 80, formatter: function (cellvalue, icol) {
                        switch (cellvalue) {
                            case 0:
                                return "未领取";
                                break;
                            case 1:
                                return "存入账户";
                                break;
                            case 2:
                                return "已领取";
                                break;
                        }
                    }
                },
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
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_HYK_NO", "sHYK_NO", "=", true);
    MakeSrchCondition2(arrayObj, $("input[name='DJLX']:checked").val(), "iLX", "=", false);
    MakeSrchCondition2(arrayObj, $("input[name='ZT']:checked").val(), "iSTATUS", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};