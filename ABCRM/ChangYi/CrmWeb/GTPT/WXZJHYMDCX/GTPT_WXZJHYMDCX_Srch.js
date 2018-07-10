vUrl = "../GTPT.ashx";
function InitGrid() {
    vColumnNames = ["门店名称", "规则名称", "奖品级次名称", "会员卡号", "会员姓名", "联系电话", "中奖时间"];
    vColumnModel = [
            { name: 'sMDMC', width: 150, },
            { name: 'sGZMC', width: 150, },
            { name: 'sJPJCMC', width: 150, },
            { name: 'sHYK_NO', width: 150, },
            { name: 'sHY_NAME', width: 150, },
            { name: 'sSJHM', width: 150, },
            { name: 'sDJSJ', width: 150, },
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
    MakeSrchCondition(arrayObj, "DDL_CJLX", "iDJLX", "=", false);
    MakeSrchCondition(arrayObj, "TB_HYMC", "sHY_NAME", "like", true);
    MakeSrchCondition(arrayObj, "TB_SJHM", "sSJHM", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "sDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "sDJSJ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};