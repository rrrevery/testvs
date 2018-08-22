vUrl = "../MZKGL.ashx";
vCZK = GetUrlParam("czk");

function InitGrid() {
    vColumnNames = ["卡类别", "保管地点", "保管人", "数量", "余额", ];
    vColumnModel = [
            { name: 'sHYKNAME', width: 80, },//sortable默认为true width默认150
			{ name: 'sBGDDMC', width: 160, },
            { name: 'sBGRMC', width: 80, },
            { name: 'iZSL', width: 80, },
            { name: 'fZYE', width: 80, },

    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    BFButtonClick("TB_BGRMC", function () {
        SelectRYXX("TB_BGRMC", "HF_BGR", "", true);
    });

    //$("#TB_BGRMC").click(function () {
    //    SelectRYXX("TB_BGRMC", "HF_BGR", "", true);
    //});
    //$("#TB_BGDDMC").click(function () {
    //    SelectBGDD_Muti("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM");
    //});

    //$("#TB_HYKNAME").click(function () {
    //    SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, 1);
    //});

    BFButtonClick("TB_BGDDMC", function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM", false);
    });
    BFButtonClick("TB_HYKNAME", function () {
        var condData = new Object();
        condData["vCZK"] = 1;
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, condData);
    });
    RefreshButtonSep();
});





function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_HYKNO", "sHYKNO", "=", true);
    MakeSrchCondition(arrayObj, "TB_JKBH", "sJKBH", "=", true);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    MakeSrchCondition(arrayObj, "HF_BGR", "iBGR", "in", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
