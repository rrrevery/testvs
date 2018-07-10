vUrl = "../HYXF.ashx";
vLX = GetUrlParam("lx");
if (vLX == "0") {
    vCaption = "会员特定积分定义单";
} else {
    vCaption = "会员特定折扣定义单";
}
function InitGrid() {
    vColumnNames = ["记录编号", "单据类型", "开始日期", "结束日期", "MDID", "门店", "状态", "DJR", "登记人", "登记时间", "ZXR", "审核人", "审核日期", "ZZR", "终止人", "终止日期", ],
       vColumnModel = [
               { name: "iJLBH", width: 80, },
            {
                name: "iDJLX", formatter: function (cellvalue, options, rowObject) {
                    if (cellvalue == 0) { return "积分"; }
                    if (cellvalue == 1) { return "折扣"; }
                },
            },
            { name: "dKSRQ", width: 80, },
            { name: "dJSRQ", width: 80, },
            { name: "iMDID", hidden: true, },
            { name: "sMDMC", width: 80, },
            { name: "iSTATUS", width: 80, formatter: DJZTCellFormat, },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
			{ name: 'iZZR', hidden: true, },
			{ name: 'sZZRMC', width: 80, },
			{ name: 'dZZRQ', width: 120, },
       ]
}
$(document).ready(function () {
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
    $("#TB_ZZRMC").click(function () {
        SelectRYXX("TB_ZZRMC", "HF_ZZR", "zHF_ZZR", false);
    });
    ConbinDataArry['djlx'] = vLX;

});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition2(arrayObj, vLX, "iDJLX", "=", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "TB_KSRQ1", "dKSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_KSRQ2", "dKSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ1", "dJSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ2", "dJSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_ZZR", "iZZR", "=", false);
    MakeSrchCondition(arrayObj, "TB_ZZRQ1", "dZZRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZZRQ2", "dZZRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "in", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "in", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};