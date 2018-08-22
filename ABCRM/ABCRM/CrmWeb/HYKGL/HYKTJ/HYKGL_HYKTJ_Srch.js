vUrl = "../HYKGL.ashx";

function InitGrid() {
    vColumnNames = ['DJR', '发放人',  '卡类型','操作地点', '售卡数量', '期初金额', '卡费金额' ];
    vColumnModel = [
            //{ name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
            { name: 'iZXR', hidden: true, },
            { name: 'sZXRMC', },
            //{ name: 'iHYKTYPE', hidden: true, },
            { name: 'sHYKNAME', },
            { name: 'sBGDDMC', width: 130 },
            { name: 'iSKSL', },
            { name: 'fQCYE', },
			{ name: 'fKFJE', },
    ];
};

$(document).ready(function () {

    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#B_Save").hide();
    $("#B_Cancel").hide();
    $("#B_Exec").hide();


    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR",true);
    });

    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false)
    });

    $("#TB_BGDDMC").click(function ()
    {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM", false);
    });
    RefreshButtonSep();
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "=", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iZXR", "=", true);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    MakeSrchCondition(arrayObj, "TB_FFSJ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_FFSJ2", "dZXRQ", "<=", true);
    return arrayObj;
};

function b_khjl_loaddata(pId, pName) {
    $("#LB_ZXR").text(pId);
    $("#LB_ZXRMC").text(pName);
}