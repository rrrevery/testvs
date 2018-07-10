
vUrl = "../MZKGL.ashx";
vCaption = "面值卡取款";

function InitGrid() {
    vColumnNames = ["记录编号", "hyid", "卡号", "hyktype", "卡类型", "原金额", "取款金额", "摘要", "MDID", "门店", "DJR", "登记人", "登记时间", "ZXR", "审核人", "审核日期", ];
    vColumnModel = [
           	{ name: "iJLBH", width: 80, },
            { name: "iHYID", hidden: true, },
            { name: "sHYKNO", },
            { name: "iHYKTYPE", hidden: true, },
            { name: "sHYKNAME", },
            { name: "fYJE", },
            { name: "fQKJE", },
            { name: "sZY", },
            { name: "iMDID", hidden: true, },
            { name: "sMDMC", },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
    ];
};
$(document).ready(function () {

    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR");
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR");
    });
    $("#TB_CZMD").click(function () {
        SelectMD("TB_CZMD", "HF_MDID", "zHF_MDID");
    })
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_HYKNO", "sHYKNO", "=", true);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "=", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};