vCaption = GetUrlParam("sj") == "1" ? "会员卡升级" : "会员卡降级";
vUrl = "../HYKGL.ashx";

function InitGrid() {
    vColumnNames = ['记录编号', '操作地点', 'sBGDDDM','操作门店', '原卡类型', 'HYKTYPE_OLD', '原卡号', '新卡类型', 'HYKTYPE_NEW', '新卡号', '姓名', 'HYID', 'DJR', '登记人', '登记时间', 'ZXR', '审核人', '审核日期', '摘要', ];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },
            { name: 'sBGDDMC', },
            { name: 'sBGDDDM', hidden: true, },
            { name: 'sMDMC',width:80},
			{ name: 'sHYKNAME_OLD', width: 80, },
			{ name: 'iHYKTYPE_OLD', hidden: true, },
            { name: 'sHYKHM_OLD', width: 80, },
			{ name: 'sHYKNAME_NEW', width: 80, },
			{ name: 'iHYKTYPE_NEW', hidden: true, },
            { name: 'sHYKHM_NEW', width: 80, },
			{ name: 'sHY_NAME', width: 80, },
            { name: 'iHYID', hidden: true, },
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
    ConbinDataArry["sj"] = sj;

    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR",false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR",false);
    });

    $("#TB_HYKNAME_OLD").click(function () {
        SelectKLX("TB_HYKNAME_OLD", "HF_HYKTYPE_OLD", "zHF_HYKTYPE_OLD", false);
    });
    $("#TB_HYKNAME_NEW").click(function () {
        SelectKLX("TB_HYKNAME_NEW", "HF_HYKTYPE_NEW", "zHF_HYKTYPE_NEW", false);
    });

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });

    $("#TB_BGDDMC").click(function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM",false);
    });
});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "TB_HYKHM_OLD", "sHYKHM_OLD", "=", true);
    MakeSrchCondition(arrayObj, "TB_HYNAME", "sHY_NAME", "=", true);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE_OLD", "iHYKTYPE_OLD", "in", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    MakeSrchCondition(arrayObj, "TB_HYKHM_NEW", "sHYKHM_NEW", "=", true);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE_NEW", "iHYKTYPE_NEW", "in", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "in", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "in", false);
    MakeSrchCondition(arrayObj, "TB_ZY", "sZY", "like", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition2(arrayObj, sj, "iBJ_SJ", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}



//if ($("#HF_MDID").val() == "" && $("#TB_HYKHM_NEW").val() == "" && $("#TB_HYKHM_OLD").val() == "") {
//    art.dialog({ lock: true, time: 2, content: "请先选择操作门店或者输入新卡号或就卡号" });
//    return;
//}