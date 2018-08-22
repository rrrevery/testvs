vUrl = "../GTPT.ashx";


function InitGrid() {
    vColumnNames = ["时间", "新增关注人数", "取消关注人数", "净增关注人数", "累计关注人数"];
    vColumnModel = [
            { name: 'dSJ', width: 120, },
            { name: 'fXZRS', width: 80, },
            { name: 'fQXRS', width: 80, },
            { name: 'fJZRS', width: 80, },
            { name: 'fLJRS', width: 80, },
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
    MakeSrchCondition(arrayObj, "TB_RQ1", "dSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_RQ2", "dSJ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};