vUrl = "../GTPT.ashx";
vGZLX = GetUrlParam("gzlx");
var vCaption = vGZLX == "1" ? "微信开卡赠积分规则定义" : "微信绑卡赠积分规则定义";

function InitGrid() {
    vColumnNames = ['记录编号', '开始日期', '结束日期', '登记人', 'DJR', '登记时间', '审核人', 'ZXR', '审核时间', '启动人', 'QDR', '启动时间', '终止人', 'ZZR', '终止时间', '领奖有效期', '微信摘要','状态'],
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },
        { name: 'dKSRQ', width: 120, },
        { name: 'dJSRQ', width: 120, },
		{ name: 'sDJRMC', width: 80, },
        { name: 'iDJR', hidden: true, },
		{ name: 'dDJSJ', width: 150, },
		{ name: 'sSHRMC', width: 80, },
        { name: 'iSHR', hidden: true, },
		{ name: 'dSHRQ', width: 150, },
        { name: 'sQDRMC', width: 80, },
        { name: 'iQDR', hidden: true, },
		{ name: 'dQDRQ', width: 100, },
        { name: 'sZZRMC', width: 80, },
        { name: 'iZZR', hidden: true, },
		{ name: 'dZZRQ', width: 150, },
        { name: 'dLJYXQ', width: 120, },
        { name: 'sWXZY', width: 120, },
        {
            name: 'iSTATUS', width: 80, formatter: function (cellvalue, icol) {
                switch (cellvalue) {
                    case 0:
                        return "保存";
                        break;
                    case 1:
                        return "审核";
                        break;
                    case 2:
                        return "启动";
                        break;
                    case 3:
                        return "终止";
                        break;
                }
            }
        },
    ]
}
$(document).ready(function () {
    ConbinDataArry["gzlx"] = vGZLX;

    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
    $("#TB_DQRMC").click(function () {
        SelectRYXX("TB_DQRMC", "HF_QDR", "zHF_QDR", false);
    });
   

    $("#TB_ZZRMC").click(function () {
        SelectRYXX("TB_ZZRMC", "HF_ZZR", "zHF_ZZR", false);
    });
})
function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition2(arrayObj, vGZLX, "iLX", "=", false);
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_XZSL", "iXZSL", "=", false);
    MakeSrchCondition(arrayObj, "TB_KSRQ1", "dKSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_KSRQ2", "dKSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ1", "dJSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ2", "dJSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "in", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "in", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_QDR", "iQDR", "in", false);
    MakeSrchCondition(arrayObj, "HF_ZZR", "iZZR", "in", false);
    MakeSrchCondition(arrayObj, "TB_QDSJ1", "dQDRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_QDSJ2", "dQDRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZZSJ1", "dZZRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZZSJ2", "dZZRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_LJYXQ1", "dLJYXQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_LJYXQ2", "dLJYXQ", "<=", true);

    MakeSrchCondition(arrayObj, "TB_WXZY", "sWXZY", "like", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};