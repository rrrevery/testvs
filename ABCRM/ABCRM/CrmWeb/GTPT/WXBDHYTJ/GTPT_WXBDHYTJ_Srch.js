vUrl = "../GTPT.ashx";

function InitGrid() {
    vColumnNames = ["关注人数", "取消关注人数", "绑定会员人数"];
    vColumnModel = [
            { name: 'iGZRS', width: 80, },
            { name: 'iQXGZRS', width: 80, },
            { name: 'iBDHYRS', width: 80, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    RefreshButtonSep();

});



function AddCustomerCondition(Obj) {
    if ($("#TB_DJSJ1").val() != "")
        Obj.dRQ1 = $("#TB_DJSJ1").val();
    if ($("#TB_DJSJ2").val() != "")
        Obj.dRQ2 = $("#TB_DJSJ2").val();
};

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};