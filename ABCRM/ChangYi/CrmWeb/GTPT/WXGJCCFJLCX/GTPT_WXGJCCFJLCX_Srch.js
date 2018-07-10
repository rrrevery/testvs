vUrl = "../GTPT.ashx";


function InitGrid() {
    vColumnNames = ["微信号", "登记时间", "问题"];
    vColumnModel = [
            { name: 'sWX_NO', width: 100, },
            { name: 'dDJSJ', width: 150, },
            { name: 'sASK', width: 100, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    RefreshButtonSep();
});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_WX_NO", "sWX_NO", "=", true);
    MakeSrchCondition(arrayObj, "TB_RQ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_RQ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_GJZ", "sASK", "like", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};