vUrl = "../HYKGL.ashx";
vCaption = "积分变动明细查询";
var HYKNO = GetUrlParam("HYKNO");

function InitGrid() {
    vColumnNames = ["款台号", '小票号', "HYID", "HYK_NO", "处理时间", "处理类型", "CLLX", "未处理积分变动", '未处理积分', '操作员名称', '操作员代码', '摘要'];
    vColumnModel = [
             { name: 'sSKTNO', width: 70 },
              { name: 'iJLBH', width: 70 },
             { name: 'iHYID', hidden: true },
              { name: 'sHYK_NO', hidden: true },
            { name: 'dCLSJ', width: 140, },
             { name: 'sCLLXMC', width: 150, },
              { name: 'iCLLX', hidden: true },
            { name: 'fWCLJFBD', width: 80, },
            { name: 'sWCLJF', width: 70 },
            { name: 'sCZYMC', width: 70 },
            { name: 'sCZYDM', width: 70, },
             { name: 'sZY', width: 100, },
    ];
};
$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    if (HYKNO) {
        $("#TB_HYK_NO").val(HYKNO);
        window.setTimeout(function ()
        { SearchData(); }, 500);
    }
    RefreshButtonSep();
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_HYK_NO", "sHYK_NO", "=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ1", "dCLSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ2", "dCLSJ", "<=", true);
    return arrayObj;
}