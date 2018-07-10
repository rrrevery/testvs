vUrl = "../MZKGL.ashx";

function InitGrid() {
    vColumnNames = [ "操作员","方式",  "实收金额"];
    vColumnModel = [
            { name: 'sZXRMC', width: 100, },
			{ name: 'sFS', width: 100, },         
            { name: 'fSSJE', width: 80, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    BFButtonClick("TB_ZXRMC", function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

});

function AddCustomerCondition(Obj) {
    var iCXFW = ($("[name='RD_CXFW']:checked").val() != undefined) ? $("[name='RD_CXFW']:checked").val() : -1;
    Obj.iSEARCHMODE = iCXFW;
}
function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", " =", false);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};

