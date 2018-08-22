vUrl = "../MZKGL.ashx";
vCaption = vHF == "0" ? "库存面值卡作废" : "库存面值卡恢复";

function InitGrid() {
    vColumnNames = ['记录编号', '操作门店', '卡类型', "卡类型", "保管地点", "作废总数量", "作废总金额", "登记人", '登记人', '登记时间', "审核人", '审核人', '审核日期', "摘要"];
    vColumnModel = [
            { name: 'iJLBH', width: 90 },
            { name: 'sMDMC', width: 60 },
            { name: 'iHYKTYPE', hidden: true },
            { name: 'sHYKNAME', width: 90 },
            { name: 'sBGDDMC', width: 90 },
            { name: 'iZFKSL', width: 90 },
            { name: 'fZFKJE', width: 90 },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 90, },
			{ name: 'dDJSJ', width: 150, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 90, },
			{ name: 'dZXRQ', width: 120, },
            { name: 'sZFKYY', width: 80, },
    ];
};


$(document).ready(function () {
    ConbinDataArry["hf"] = vHF;
    ConbinDataArry["old"] = vOLDDB;
    // 1与树有关js



    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });


    $("#TB_CZMD").click(function () {
        SelectMD("TB_CZMD", "HF_MDID", "zHF_MDID", false);
    })
    $("#TB_BGDDDM").click(function () {
        SelectBGDD("TB_BGDDDM", "HF_BGDDDM", "zHF_BGDDDM");
    });
    BFButtonClick("TB_HYKNAME", function () {
        var condData = new Object();
        condData["vCZK"] = 1;
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, condData);
    });
});


function onHYKClick(e, treeId, treeNode) {
    $("#TB_HYKNAME").val(treeNode.name);
    $("#HF_HYKTYPE").val(treeNode.id);
    hideMenuHYK();

}





function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", " in", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", " in", false);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZFKSL", "iZFKSL", " =", false);
    MakeSrchCondition(arrayObj, "TB_ZFKJE", "fZFKJE", " =", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "TB_ZY", "sZY", "=", true);
    MakeSrchCondition2(arrayObj, vHF, "iBJ_HF", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};