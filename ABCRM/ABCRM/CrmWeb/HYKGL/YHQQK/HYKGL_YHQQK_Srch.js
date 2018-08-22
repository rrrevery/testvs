//vPageMsgID = CM_HYKGL_YHQZHQKCL;
vUrl = "../HYKGL.ashx";

vCaption = "优惠券取款"
function InitGrid() {
    vColumnNames = ['记录编号', '卡号', '姓名', 'HYID', '优惠券', 'iYHQID', '结束日期', 'sMDFWDM', '促销活动', 'iCXID', '取款金额', '原金额', 'DJR', '登记人', '登记时间', 'ZXR', '审核人', '审核日期', '摘要', ],//'操作地点', 'sBGDDDM','操作门店'
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
            //{ name: 'sBGDDMC',  },
            //{ name: 'sBGDDDM', hidden: true, },
            { name: 'sHYKNO', width: 80, },
			{ name: 'sHY_NAME', width: 80, },
            { name: 'iHYID', hidden: true, },
            //{ name: 'sMDMC', width:80 },

            { name: 'sYHQMC', },
            { name: 'iYHQID', hidden: true, },
            { name: 'dJSRQ', width: 120, },
            { name: 'sMDFWDM', hidden: true, },
            { name: 'sCXZT', },
            { name: 'iCXID', hidden: true, },
            { name: 'fQKJE', width: 80, },
            { name: 'fYYE', width: 80, },

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

    $("#TB_BGDDMC").click(function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM", false);
    });

    $("#TB_YHQ").click(function () {
        SelectYHQ("TB_YHQ", "HF_YHQ", "zHF_YHQ", false);
    });
    $("#TB_CXHD").click(function () {
        SelectCXHD("TB_CXHD", "HF_CXHD", "zHF_CXHD", false);
    });



});
function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    //MakeSrchCondition(arrayObj, "TB_BGDDDM", "sBGDDMC", "in", true);
    MakeSrchCondition(arrayObj, "TB_HYKNO", "sHYKNO", "=", true);
    MakeSrchCondition(arrayObj, "HF_YHQ", "iYHQID", "in", false);
    MakeSrchCondition(arrayObj, "HF_CXHD", "iCXID", "in", false);
    MakeSrchCondition(arrayObj, "TB_QKJE", "fQKJE", "=", false);
    //MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    //MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_YYE", "fYYE", "=", false);
    //MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "=", false);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);

    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};

