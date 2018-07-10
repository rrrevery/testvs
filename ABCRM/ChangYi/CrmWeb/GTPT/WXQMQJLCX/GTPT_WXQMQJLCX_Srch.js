vUrl = "../GTPT.ashx";


function InitGrid() {

    vColumnNames = ["会员卡号", "门店ID", "门店名称", "YHQ", "优惠券名称", "优惠券金额(元)", "优惠券结束日期", "充值时间", "充值金额(元)", "状态", "是否退货"];
    vColumnModel = [
            { name: 'sHYK_NO', width: 80, },
             { name: 'iMDID', hidden: true },
             { name: 'sMDMC', width: 80, },
            { name: 'iYHQID', hidden: true },
            { name: 'sYHQMC', width: 100, },
            { name: 'fJE', width: 80, },
            { name: 'dSYJSRQ', width: 150, },
            { name: 'dDJSJ', width: 150, },
            { name: 'iJE', width: 80, },
            {
                name: 'iSTATUS', width: 80,
                formatter: function (cellvalues) {

                    if (cellvalues == 0)
                        return "录入";
                    else if 
                        (cellvalues == 1)
                        return "成功";
                    else if (cellvalues == 2)
                        return "失败";

                },
            },
             {
                 name: 'sTH', width: 80,
                 formatter: function (value) {
                     if (value == "")
                         return "否";
                     else
                         return "是";


                 },
             },


    ];

};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#TB_YHQMC").click(function () {
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID");
    });

    RefreshButtonSep();
});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_YHQID", "iYHQID", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<", true);
    MakeSrchCondition(arrayObj, "TB_HYK_NO", "sHYK_NO", "=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;

};