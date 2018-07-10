vUrl = "../GTPT.ashx";

function InitGrid() {
    vColumnNames = ["会员卡号", "会员名字", "处理类型", "日期", "未处理积分变动"];
    vColumnModel = [
            { name: 'sHYK_NO', width: 80, },
            { name: 'sHY_NAME', width: 80, },
            { name: 'iCLLX', width: 80, },
            { name: 'dCLSJ', width: 150, },
            { name: 'fWCLJFBD', width: 150, },
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
    MakeSrchCondition(arrayObj, "TB_RQ1", "dCLSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_RQ1", "dCLSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_HYKNO", "sHYK_NO", "=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};