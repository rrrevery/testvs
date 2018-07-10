vUrl = "../GTPT.ashx";
var vCaption = "编码券发放单";

function InitGrid() {
    vColumnNames = ['记录编号', '活动名称', '编码券名称', '促销主题',  '开始日期', '结束日期', '券开始日期', '券结束日期', '券有效期天数', '渠道', '状态', '登记人', 'DJR', '登记时间', '审核人', 'ZXR', '审核时间', '启动人', 'QDR', '启动时间', '终止人', 'ZZR', '终止时间'],
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },
        { name: 'sNAME', width: 100, },
        { name: 'sBMQMC', width: 100, },
        { name: 'sCXZT', width: 100, },
        { name: 'dKSRQ', width: 100, },
        { name: 'dJSRQ', width: 100, },
        { name: 'dQKSRQ', width: 100, },
        { name: 'dQJSRQ', width: 100, },
        { name: 'iQYXQTS', width: 80, },
        {
            name: 'iFFPT', width: 100, formatter: function (values) {
                if (values == 0)
                    return "微信";
                else if (values == 1)
                    return "APP";
                else if (values == 2)
                    return "后台";
                else
                    return "";
            }
        },
        {
            name: 'iSTATUS', width: 80, formatter: function (values) {
                if (values == 0)
                    return "已登记";
                else if (values == 1)
                    return "已审核";
                else if (values == 2)
                    return "已启动";
                else if (values == 3)
                    return "已终止";
                else
                    return "";
            }
        },
		{ name: 'sDJRMC', width: 80, },
        { name: 'iDJR', hidden: true, },
		{ name: 'dDJSJ', width: 150, },
		{ name: 'sZXRMC', width: 80, },
        { name: 'iZXR', hidden: true, },
		{ name: 'dZXRQ', width: 150, },
        { name: 'sQDRMC', width: 80, },
        { name: 'iQDR', hidden: true, },
		{ name: 'dQDSJ', width: 150, },
        { name: 'sZZRMC', width: 80, },
        { name: 'iZZR', hidden: true, },
		{ name: 'dZZSJ', width: 150, },
    ]

}
$(document).ready(function () {
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
    $("#TB_QDRMC").click(function () {
        SelectRYXX("TB_QDRMC", "HF_QDR", "zHF_QDR", false);
    });
    $("#TB_ZZRMC").click(function () {
        SelectRYXX("TB_ZZRMC", "HF_ZZR", "zHF_ZZR", false);
    });
  

    BFButtonClick("TB_CXZT", function () {
        SelectCXHD("TB_CXZT", "HF_CXID", "zHF_CXID", false);
    });

    BFButtonClick("TB_BMQMC", function () {
        SelectBMQDY("TB_BMQMC", "HF_BMQID", "zHF_BMQID", false);
    });




})



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_NAME", "sNAME", "=", true);
    MakeSrchCondition(arrayObj, "HF_BMQID", "iBMQID", "in", false);
    MakeSrchCondition(arrayObj, "TB_KSRQ", "dKSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ", "dJSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_QKSRQ", "dQKSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_QJSRQ", "dQJSRQ", "<=", true);
    var vCHANNELID = GetSelectValue("DDL_CHANNELID");
    if (vCHANNELID != "") {
        MakeSrchCondition2(arrayObj, vCHANNELID, "iFFPT", "=", false);
    }
    MakeSrchCondition(arrayObj, "HF_LMSHID", "iLMSHID", "in", false);
    MakeSrchCondition(arrayObj, "HF_CXID", "iCXID", "in", false);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_QDRMC", "sQDRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZZRMC", "sZZRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_QDSJ1", "dQDSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_QDSJ2", "dQDSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZZSJ1", "dZZSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZZSJ2", "dZZSJ", ">=", true);
    MakeSrchCondition2(arrayObj, $("input[name='DJZT']:checked").val(), "iSTATUS", "=", false);

    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};