vUrl = "../GTPT.ashx";


function InitGrid() {
    vColumnNames = ["记录编号", "ID", "内容ID", "调查主题", "名称", "内容名称", "投票数", "百分比%"];
    vColumnModel = [
            { name: 'iJLBH', width: 80, },
            { name: 'iID',  hidden: true },
            { name: 'iNRID', hidden: true },
            { name: 'sDCZT', width: 80, },
			{ name: 'sMC', width: 80, },
            { name: 'sNRMC', width: 80, },
            { name: 'iTPS', width: 80, },
            { name: 'fBFB', hidden: true },
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
    MakeSrchCondition(arrayObj, "TB_DCZT", "sDCZT", "=", true);
    MakeSrchCondition(arrayObj, "TB_DCRQ1", "dDCRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DCRQ2", "dDCRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};