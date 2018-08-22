vUrl = "../GTPT.ashx";
vCaption = "微信促销活动定义";
function InitGrid() {
    vColumnNames = ['促销ID', '促销主题', '开始时间', '结束时间', '内容'],
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },
		{ name: 'sCXZT', width: 120, },
        { name: 'dSTART_RQ', width: 120, },
        { name: 'dEND_RQ', width: 120, },
        { name: 'sCXNR', width: 120 },
    ]

}
$(document).ready(function () {
})



function SaveData() {
    var Obj = new Object();
    var rowid = $("#list").jqGrid("getGridParam", "selrow");
    var rowData = $("#list").getRowData(rowid);
    Obj.iJLBH = rowData.iJLBH;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_CXZT", "sCXZT", "=", true);
    MakeSrchCondition(arrayObj, "TB_START_RQ", "dSTART_RQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_END_RQ", "dEND_RQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_CXNR", "sCXNR", "=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}