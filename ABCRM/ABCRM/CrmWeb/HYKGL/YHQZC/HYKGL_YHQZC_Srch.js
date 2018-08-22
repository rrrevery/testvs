vUrl = "../HYKGL.ashx";
vCaption = "优惠券转储"

function InitGrid() {
    vColumnNames = ['记录编号', '卡类型', 'HYID', '操作地点', 'sBGDDDM', '转入金额', '卡号', '登记人', '登记人', '登记时间', '审核人', '审核人', '审核日期', '摘要', ];
    vColumnModel = [
            { name: 'iJLBH',  width: 80, },
			{ name: 'sHYKNAME', width: 80, },
            { name: 'iHYID', hidden: true, },
            { name: 'sBGDDMC', width: 80, },
            { name: 'sBGDDDM', hidden: true, },
            { name: 'fZRJE', width: 80, },
			{ name: 'sHYKNO', width: 80, },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
			{ name: 'sZY', width: 120, },
    ];
}

$(document).ready(function () {
    $("#TB_HYKTYPE").click(function () {
        SelectKLX("TB_HYKTYPE", "HF_HYKTYPE", "zHF_HYKTYPE");
    });
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
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
    MakeSrchCondition(arrayObj, "TB_HYKNO", "sHYKNO", "=", true);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", " in", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", " in", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", " in", false);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);

    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};