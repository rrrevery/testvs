
vUrl = "../MZKGL.ashx";


function InitGrid() {
    vColumnNames = ["会员卡类型", "余额"];
    vColumnModel = [
            { name: 'sHYKNAME', width: 120, },
            { name: 'fYE', width: 140, },
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
    return arrayObj;
};