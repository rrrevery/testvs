vDJLX = GetUrlParam("djlx");
vCZK = GetUrlParam("czk");
vUrl = vCZK == "0" ? "../HYKGL.ashx" : "../../MZKGL/MZKGL.ashx";
vCaption = vCZK == "0" ? "会员卡" : "面值卡";
if (vDJLX == "0") {
    vCaption += "领用申请";
}
//if (vDJLX == "1") {
//    vCaption += "调拨";
//}
//if (vDJLX == "2") {
//    vCaption += "退领";
//}

function InitGrid() {
    vColumnNames = ['记录编号', '拨入地点', 'sBGDDDM_BR',  '领取数量', 'DJR', '登记人', '登记时间', 'ZXR', '审核人', '审核日期', '备注'];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
            { name: 'sBGDDMC_BR', },
            { name: 'sBGDDDM_BR', hidden: true, },
            { name: 'iHYKSL', width: 60, },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
			{ name: 'sBZ', width: 120, },
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


    $("#TB_BGDDDM_BR").click(function () {
        SelectBGDD("TB_BGDDDM_BR", "HF_BGDDDM_BR", "zHF_BGDDDM_BR");
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
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", " =", false);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_HYKSL", "iHYKSL", " =", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM_BR", "sBGDDDM_BR", "in", false);
    MakeSrchCondition2(arrayObj, vDJLX, "iDJLX", "=", false);
    MakeSrchCondition2(arrayObj, vCZK, "iBJ_CZK", "=", false);
    return arrayObj;
};

function AddCustomerCondition(Obj) {
    if (vCZK == "1") {
        Obj.sDBConnName = "CRMDBMZK";
    }
}
