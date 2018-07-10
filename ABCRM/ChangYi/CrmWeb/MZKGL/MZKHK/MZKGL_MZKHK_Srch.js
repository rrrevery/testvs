vUrl = "../MZKGL.ashx";
var vMZK = GetUrlParam("mzk");
var DBCaption = "CRMDBMZK"
var vCaption = "面值卡换卡"
function InitGrid() {
    vColumnNames = ['记录编号', '卡类型', 'HYKTYPE', 'CZMD', 'sBGDDDM','操作地点' ,'HYID', '旧卡号', '新卡号', '积分', '金额', '工本费', '登记人', '登记人', '登记时间', '审核人', '审核人', '审核日期', '摘要', ];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
			{ name: 'sHYKNAME', width: 80, },
			{ name: 'iHYKTYPE', hidden: true, },
            { name: "iMDID", hidden: true, },
            { name: "sBGDDDM", hidden: true, },
            { name: "sBGDDMC", width: 80, },
			{ name: 'iHYID', hidden: true, },
			{ name: 'sHYKHM_OLD', width: 80, },
			{ name: 'sHYKHM_NEW', width: 80, },
			{ name: 'fJF', width: 80, hidden: true, },
			{ name: 'fJE', width: 40, },
			{ name: 'fGBF', width: 80, },
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
    $("#TB_BGDDDM").click(function () {
        SelectBGDD("TB_BGDDDM", "HF_BGDDDM", "zHF_BGDDDM", false);
    });

    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR");
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR");
    });
    BFButtonClick("TB_HYKNAME", function () {
        var condData = new Object();
        condData["vCZK"] = 1;
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, condData);
    });

});




function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", " =", false);
    MakeSrchCondition(arrayObj, "TB_HYKHM_OLD", "sHYKHM_OLD", " =", true);
    MakeSrchCondition(arrayObj, "TB_HYKHM_NEW", "sHYKHM_NEW", "=", true);
    MakeSrchCondition(arrayObj, "TB_HYNAME", "sHY_NAME", " like", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", " =", false);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}