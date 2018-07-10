vUrl = "../GTPT.ashx";

function InitGrid() {
    vColumnNames = ['记录编号', '规则ID', '规则',   '开始日期', '结束日期', '登记人', 'DJR', '登记时间', '审核人', 'ZXR', '审核时间', '启动人', 'QDR', '启动时间', '终止人', 'ZZR', '终止时间', '领奖有效期','状态'],// '登记类型','启动人', 'QDR', '启动时间',
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },
		{ name: 'iGZID', hidden: true, },
        { name: 'sGZMC', width: 120, },
        { name: 'dKSRQ', width: 120, },
        { name: 'dJSRQ', width: 120, },        
		{ name: 'sDJRMC', width: 80, },
        { name: 'iDJR', hidden: true, },
		{ name: 'dDJSJ', width: 150, },
		{ name: 'sZXRMC', width: 80, },
        { name: 'iZXR', hidden: true, },
		{ name: 'dZXRQ', width: 150, },
        { name: 'sQDRMC', width: 80, },
        { name: 'iQDR', hidden: true, },
		{ name: 'dQDSJ', width: 100, },
        { name: 'sZZRMC', width: 80, },
        { name: 'iZZR', hidden: true, },
		{ name: 'dZZSJ', width: 150, },
        { name: 'dLJYXQ', width: 120, },
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
    ConbinDataArry['djlx'] = vDJLX;
    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    BFButtonClick("TB_ZXRMC", function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

    BFButtonClick("TB_QDRMC", function () {
        SelectRYXX("TB_QDRMC", "HF_QDR", "zHF_QDR", false);
    });
    BFButtonClick("TB_ZZRMC", function () {
        SelectRYXX("TB_ZZRMC", "HF_ZZR", "zHF_ZZR", false);
    });

    BFButtonClick("TB_GZMC", function () {
        var ConData = new Object();
        ConData["iGZLX"] = vDJLX;
        SelectWXLPFFGZ("TB_GZMC", "HF_GZID", "zHF_GZID", false, ConData);
    });
})

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition2(arrayObj, vDJLX, "iDJLX", "=", false);
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_GZID", "iGZID", "in", false);
    MakeSrchCondition(arrayObj, "TB_KSRQ1", "dKSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_KSRQ2", "dKSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ1", "dJSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ2", "dJSRQ", "<=", true);  
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", " in", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", " in", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_QDR", "iQDR", " in", false);
    MakeSrchCondition(arrayObj, "HF_ZZR", "iZZR", " in", false);
    MakeSrchCondition(arrayObj, "TB_QDSJ1", "dQDSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_QDSJ2", "dQDSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZZSJ1", "dZZSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZZSJ2", "dZZSJ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};