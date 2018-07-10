vUrl = "../GTPT.ashx";


function InitGrid() {
    vColumnNames = ["iHYKTYPE", "卡类型", "人数", "消费人数", "消费金额"];
    vColumnModel = [
            { name: 'iHYKTYPE', hidden: true, },
            { name: 'sHYKNAME', width: 80, },
            { name: 'fRS', width: 80, },
            { name: 'fXFRS', width: 80, },
            { name: 'fXFJE', width: 80, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    BFButtonClick("TB_HYKNAME", function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });
    RefreshButtonSep();
});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};

function AddCustomerCondition(Obj) {
    if ($("#HF_HYKTYPE").val() != "")
        Obj.sHYKTYPE = $("#HF_HYKTYPE").val();
    if ($("#TB_RQ1").val() != "")
        Obj.dDJSJ1 = $("#TB_RQ1").val();
    if ($("#TB_RQ2").val() != "")
        Obj.dDJSJ2 = $("#TB_RQ2").val();
};