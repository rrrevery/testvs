vUrl = "../GTPT.ashx";
vCaption = "微调查用户查询查询";

function InitGrid() {
    vColumnNames = ["会员卡号", "会员名字", "手机号码", "日期", "调查主题", "微信号"];
    vColumnModel = [
            { name: 'sHYK_NO', width: 80, },
            { name: 'sHY_NAME', width: 80, },
            { name: 'sSJHM', width: 80, },
            { name: 'dRQ', width: 150, },
            { name: 'sDCZT', width: 80, },
            { name: 'sWX_NO', width: 80, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_RQ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_RQ1", "dRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_WX_NO", "sWX_NO", "=", true);   
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};