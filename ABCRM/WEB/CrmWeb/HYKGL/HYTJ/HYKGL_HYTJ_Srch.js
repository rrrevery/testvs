vUrl = "../HYKGL.ashx";
vCaption = "会员推荐规则";

function InitGrid() {
    vColumnNames = ['记录编号', '开始日期', '结束日期', '奖励方式', '消费金额', '消费次数', '消费积分', '登记人', '登记时间', '执行人', '执行时间', '终止人', '终止时间'];   // '优惠券ID', '有效期天数', '状态',
    vColumnModel = [
           { name: 'iJLBH', width: 80 },
            { name: 'dKSRQ', width: 150 },
            { name: 'dJSRQ', width: 150 },
            {
                name: 'iJLFS', width: 80, formatter: function (cellvalues) {
                    if (cellvalues == 1)
                        return "积分"
                    if (cellvalues == 2)
                        return "电子券";

                }
            },
            { name: 'fXFJE', width: 80 },
            { name: 'iXFCS', width: 80 },
            { name: 'fXFJF', width: 80 },
            { name: 'sDJRMC', width: 80 },
            { name: 'dDJSJ', width: 150 },
            { name: 'sZXRMC', width: 80 },
            { name: 'dZXRQ', width: 150 },
            { name: 'sZZRMC', width: 80 },
            { name: 'dZZRQ', width: 150 },

    ];
};




$(document).ready(function () {
    $("#B_Exec").hide();

    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR","zHF_DJR",false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR","zHF_ZXR",false);
    });
    $("#TB_ZZRMC").click(function () {
        SelectRYXX("TB_ZZRMC", "HF_ZZR","zHF_ZZR",false);
    });


});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "DDL_JLFS", "iJLFS", "=", false);
    MakeSrchCondition(arrayObj, "TB_KSRQ1", "dKSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_KSRQ2", "dKSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ1", "dJSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ2", "dJSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "=", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "=", false);
    MakeSrchCondition(arrayObj, "HF_ZZR", "iZZR", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZZRQ1", "dZZRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZZRQ2", "dZZRQ", "<=", true);
    var djzt = $("[name='djzt']:checked").val();
    if (djzt != 10)
        MakeSrchCondition2(arrayObj, djzt, "iSTATUS", "=", false);
    return arrayObj;
};

