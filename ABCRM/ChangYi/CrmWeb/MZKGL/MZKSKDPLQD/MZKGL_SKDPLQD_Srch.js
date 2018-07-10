vUrl = "../MZKGL.ashx";
vCaption = "面值卡售卡单批量启动";


function InitGrid() {
    vColumnNames = ["记录编号", "czdd", "操作地点", "摘要", "DJR", "登记人", "登记时间", "ZXR", "审核人", "审核日期", ];
    vColumnModel = [
			{ name: "iJLBH", width: 80, },
            { name: "sBGDDDM", hidden: true, },
            { name: "sBGDDMC", width: 120, },
            { name: "sZY", width: 120, },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 150, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 100, },
    ];
};

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

});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", " =", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", " =", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    return arrayObj;

};



