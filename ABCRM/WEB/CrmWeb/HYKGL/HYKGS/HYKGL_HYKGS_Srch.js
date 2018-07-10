vHF = GetUrlParam("hf");
vCZK = GetUrlParam("czk");
vCaption = vCZK == "0" ? "会员卡" : "面值卡";
if (vHF == 0) {
    vCaption += "挂失";
}
else {
    vCaption += "挂失恢复";
}
vUrl = vCZK=="0"?"../HYKGL.ashx":"../../MZKGL/MZKGL.ashx";

function InitGrid() {
    vColumnNames = ['记录编号', '卡类型', 'HYKTYPE', '操作地点', 'sBGDDDM', '姓名', 'HYID', '卡号', '积分', '金额', '登记人', '登记人', '登记时间', '审核人', '审核人', '审核日期', '摘要', ];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
			{ name: 'sHYKNAME', width: 80, },
			{ name: 'iHYKTYPE', hidden: true, },
            { name: 'sBGDDMC', },
            { name: 'sBGDDDM', hidden: true, },
			{ name: 'sHY_NAME', width: 80, },
            { name: 'iHYID', hidden: true, },
			{ name: 'sHYKNO', width: 100, },
			{ name: 'fJF', width: 80, },
			{ name: 'fJE', width: 40, },
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
    ConbinDataArry["czk"] = vCZK;
    ConbinDataArry["hf"] = vHF;

    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

    $("#TB_BGDDMC").click(function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM");
    });


    //会员卡类型
    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });
});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_HYKNO", "sHYKNO", "=", true);
    MakeSrchCondition(arrayObj, "TB_HYNAME", "sHY_NAME", "=", true);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    //MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    //  MakeSrchCondition2(arrayObj, vHF, "iLX", "=", false);
    MakeSrchCondition(arrayObj, "TB_ZY", "sZY", "=", true);
    MakeSrchCondition(arrayObj, "TB_SFZBH", "sSFZBH", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition2(arrayObj, vHF, "iLX", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}


