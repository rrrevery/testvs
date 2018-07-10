vUrl = "../MZKGL.ashx";

function InitGrid() {
    vColumnNames = ["审核人", "方式", "支付方式ID", "支付方式", "实收金额"];
    vColumnModel = [
            { name: 'sZXRMC', width: 80, },//sortable默认为true width默认150
			{ name: 'sFS', width: 100, },
            { name: 'iZFFSID', hidden:true },
            { name: 'sZFFSMC', width: 80, },
            { name: 'fSSJE', width: 80, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR");
    });

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID",false);
    });

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "in", false);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    var vDJLX = $("[name='RD_CXFW']:checked").val();
    if (vDJLX != 0)
        MakeSrchCondition2(arrayObj, vDJLX, "iDJLX", "=", false);
    return arrayObj;
};



