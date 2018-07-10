vUrl = "../HYKGL.ashx";
vCaption = "会员卡库存报表";

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
    ConbinDataArry["czk"] = vCZK;
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID",false);
    })
    $("#TB_BGRMC").click(function () {
        SelectRYXX("TB_BGRMC", "HF_BGR","zHF_BGR",false);
    });
    $("#TB_BGDDMC").click(function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM");
    });

    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE",false);
    });
});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    MakeSrchCondition(arrayObj, "HF_BGR", "iBGR", "in", false);
    MakeSrchCondition2(arrayObj, vCZK, "iBJ_CZK", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}