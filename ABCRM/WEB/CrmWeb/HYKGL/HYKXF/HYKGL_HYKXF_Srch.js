vUrl = "../HYKGL.ashx";
vCaption = "会员卡续费";

function InitGrid() {
    vColumnNames = ["记录编号", "卡号", "卡类型", "门店名称", "单据状态", "DJR", "登记人", "登记时间", "ZXR", "审核人", "审核时间"],
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 85, },
            { name: 'sHYK_NO', width: 70, },
            { name: 'sHYKNAME', width: 70, },
            { name: 'sMDMC', width: 70, },
            { name: 'iSTATUS', width: 55, hidden: true },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 70, },
			{ name: 'dDJSJ', width: 135, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 70, },
			{ name: 'dZXRQ', width: 135, },
    ]
}

$(document).ready(function () {

    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
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
    return arrayObj;
}