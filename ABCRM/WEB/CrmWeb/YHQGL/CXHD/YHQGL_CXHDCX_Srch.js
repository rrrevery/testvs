vUrl = "../YHQGL.ashx";
vCaption = "促销活动主题定义";
function InitGrid() {
    vColumnNames = ['促销ID', '商户代码', "商户", '促销活动编号', '年度', '促销主题', '促销期数', '开始时间', '结束时间', 'iDJR', '登记人', '登记时间', 'iZXR', '审核人', '审核日期'];
    vColumnModel = [
            { name: 'iJLBH', index: 'iCXID',width: 80 },
            { name: 'sSHDM', width: 60, hidden: true },
            { name: 'sSHMC', width: 80, },
			{ name: 'iCXHDBH', width: 80, hidden: true },
            { name: 'iNIAN', width: 60, },
            { name: 'sCXZT', width: 160, },
            { name: 'iCXQS', width: 60, },
            { name: 'dKSSJ', width: 80, },
            { name: 'dJSSJ', width: 80, },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
    ];
}

$(document).ready(function () {
    $("#TB_SHMC").click(function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", true);
    });
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_SHDM", "sSHDM", "in", true);
    MakeSrchCondition(arrayObj, "TB_NIAN", "iNIAN", "=", false);
    MakeSrchCondition(arrayObj, "TB_CXQS", "iCXQS", "=", true);
    MakeSrchCondition(arrayObj, "TB_KSSJ1", "dKSSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_KSSJ2", "dKSSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_JSSJ1", "dJSSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSSJ2", "dJSSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_CXZT", "sCXZT", "like", true);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};