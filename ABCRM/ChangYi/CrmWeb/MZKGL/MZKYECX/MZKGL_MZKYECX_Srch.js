vUrl = "../MZKGL.ashx";


function InitGrid() {
    vColumnNames = ["会员卡类型", "卡号", "余额", "有效期", "卡状态", "期初余额"];
    vColumnModel = [
            { name: 'sHYKNAME', width: 80, },
			{ name: 'sHYKNO', width: 100, },
            { name: 'cYE', width: 100, },
            { name: 'dYXQ', width: 90, },
            { name: 'sSTATUS', width: 80, },
            { name: 'cQCYE', width: 100, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    BFButtonClick("TB_HYKNAME", function () {
        var condData = new Object();
        condData["vCZK"] = 1;
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, condData);
    });
    RefreshButtonSep();
});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "TB_HYKNO1", "sHYKNO", ">=", true);
    MakeSrchCondition(arrayObj, "TB_HYKNO2", "sHYKNO", "<=", true);
    MakeSrchCondition(arrayObj, "TB_YE1", "cYE", ">=", true);
    MakeSrchCondition(arrayObj, "TB_YE2", "cYE", "<=", true);
    //MakeSrchCondition(arrayObj, "TB_QCYE1", "cQCYE", ">=", true);
    //MakeSrchCondition(arrayObj, "TB_QCYE2", "cQCYE", "<=", true);
    MakeSrchCondition(arrayObj, "TB_YXQ1", "dYXQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_YXQ2", "dYXQ", "<=", true);
    return arrayObj;
};