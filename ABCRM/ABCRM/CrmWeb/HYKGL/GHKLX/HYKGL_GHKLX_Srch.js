vUrl = "../HYKGL.ashx";
vCaption = "更换卡类型";
function InitGrid() {
    vColumnNames = ['记录编号', 'HYID', '姓名',  '证件类型', '证件号码', '原卡类型', 'HYKTYPE_OLD', '新卡类型', 'HYKTYPE_NEW', '原卡号', '新卡号', '积分', '金额', '变动积分', '保管地点', 'BGDDDM', '登记人', '登记人', '登记时间', '审核人', '审核人', '审核日期', '摘要', ];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
            { name: 'iHYID', hidden: true, },
			{ name: 'sHY_NAME', width: 80, },
            { name: 'sZJLX', width: 80, },
            {
                name: 'sSFZBH', width: 160, formatter: function (cellvalue, cell) {
                    return cellvalue.substring(0, cellvalue.length - 4) + "****";
                }
            },
			{ name: 'sHYKNAME_OLD', width: 80, },
			{ name: 'iHYKTYPE_OLD', hidden: true, },
            { name: 'sHYKNAME_NEW', width: 80, },
	        { name: 'iHYKTYPE_NEW', hidden: true, },
			{ name: 'sHYKHM_OLD', width: 100, },
			{ name: 'sHYKHM_NEW', width: 100, },
			{ name: 'fJF', width: 80, },
			{ name: 'fJE', width: 40, },
			{ name: 'fBDJF', width: 80, },
            { name: 'sBGDDMC', width: 80, },
            { name: 'sBGDDDM', hidden: true, },
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

    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

    //卡类型弹出框 
    $("#TB_HYKNAME_OLD").click(function () {
        SelectKLX("TB_HYKNAME_OLD", "HF_HYKTYPE_OLD", "zHF_HYKTYPE_OLD", false);
    });
    //卡类型弹出框 
    $("#TB_HYKNAME_NEW").click(function () {
        SelectKLX("TB_HYKNAME_NEW", "HF_HYKTYPE_NEW", "zHF_HYKTYPE_NEW", false);
    });

    //$("#TB_MDMC").click(function () {
    //    SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    //});
    $("#TB_BGDDMC").click(function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM", false);
    });
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_HYKHM_OLD", "sHYKHM_OLD", " =", true);
    MakeSrchCondition(arrayObj, "TB_HYKHM_NEW", "sHYKHM_NEW", "=", true);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE_OLD", "iHYKTYPE_OLD", "in", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE_NEW", "iHYKTYPE_NEW", "in", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    MakeSrchCondition(arrayObj, "TB_SFZBH", "sSFZBH", "=", true);
    MakeSrchCondition(arrayObj, "TB_JF", "fJF", "=", false);
    MakeSrchCondition(arrayObj, "TB_JE", "fJE", "=", false);
    MakeSrchCondition(arrayObj, "TB_ZY", "sZY", "=", true);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", " =", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", " =", false);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}

