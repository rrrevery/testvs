vDJLX = GetUrlParam("djlx");
vCZK = GetUrlParam("czk");
vUrl = vCZK == "0" ? "../HYKGL.ashx" : "../../MZKGL/MZKGL.ashx";
vCaption = vCZK == "0" ? "会员卡" : "面值卡";
if (vDJLX == "0") {
    vCaption += "领取";
}
if (vDJLX == "1") {
    vCaption += "调拨";
}
if (vDJLX == "2") {
    vCaption += "退领";
}



function InitGrid() {
    vColumnNames = ['记录编号', '拨出地点', 'sBGDDDM_BC', '拨入地点', 'sBGDDDM_BR', 'iLQR', '领取人', '领取数量', 'DJR', '登记人', '登记时间', 'ZXR', '审核人', '审核日期', ];
    vColumnModel = [
			{ name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
			{ name: 'sBGDDMC_BC', },
			{ name: 'sBGDDDM_BC', hidden: true, },
			{ name: 'sBGDDMC_BR', },
			{ name: 'sBGDDDM_BR', hidden: true, },
			{ name: 'iLQR', hidden: true, },
			{ name: 'sLQRMC', width: 80, },
			{ name: 'iHYKSL', width: 60, },
			{ name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
			//{ name: 'sZY', width: 120, },
    ];
};

$(document).ready(function () {
    ConbinDataArry["djlx"] = vDJLX;
    ConbinDataArry["czk"] = vCZK;
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
    $("#TB_LQRMC").click(function () {
        SelectRYXX("TB_LQRMC", "HF_LQR", "zHF_LQR", false);
    });


    $("#TB_BGDDDM_BC").click(function () {
        SelectBGDD("TB_BGDDDM_BC", "HF_BGDDDM_BC", "zHF_BGDDDM_BC");
    });
    $("#TB_BGDDDM_BR").click(function () {
        SelectBGDD("TB_BGDDDM_BR", "HF_BGDDDM_BR", "zHF_BGDDDM_BR");
    });
    $("#TB_HYKNAME").click(function () {
        var condData = new Object();
        condData["vCZK"] = vCZK || 0;
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, condData);
    });
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_LQR", "iLQR", " =", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", " =", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", " =", false);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_HYKSL", "iHYKSL", " =", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM_BC", "sBGDDDM_BC", "in", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM_BR", "sBGDDDM_BR", "in", false);
    MakeSrchCondition2(arrayObj, vDJLX, "iDJLX", "=", false);
    MakeSrchCondition2(arrayObj, vCZK, "iBJ_CZK", "=", false);
    return arrayObj;
};

function AddCustomerCondition(Obj) {
    if ($("#TB_CZKHM").val() != "") {
        Obj.sCZKHM = $("#TB_CZKHM").val();
    }
    if (vCZK == "1") {
        Obj.sDBConnName = "CRMDBMZK";
    }
}
