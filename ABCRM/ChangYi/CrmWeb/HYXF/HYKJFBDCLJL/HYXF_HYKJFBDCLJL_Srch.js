vUrl = "../HYXF.ashx";
vCaption = "会员卡积分变动及处理记录查询";

function InitGrid() {
    vColumnNames = ["卡号", "交易编号", "操作门店", "会员编号",  "处理时间", "处理类型", "记录编号", "未处理积分变动", "未处理积分", "操作员名称", "收款台号","摘要" ];
    vColumnModel = [
            { name: 'sHYKNO', width: 80, },
			{ name: 'iJYBH', width: 80, },
			{ name: 'iCZMD', hidden:true },
	        { name: 'iHYID', hidden: true },
            { name: 'dCLSJ', width: 80, },
            { name: 'sCLLX', width: 80, },
            { name: 'iJLBH', width: 80, },
            { name: 'fWCLJFBD', width: 80, },
            { name: 'fWCLJF', width: 80, },
            { name: 'sCZYMC', width: 80, },
            { name: 'sSKTNO', width: 80, },
            { name: 'sZY', width: 80, },
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
    MakeSrchCondition(arrayObj, "TB_HYKNO", "sHYKNO", "=", true);
    MakeSrchCondition(arrayObj, "TB_BQJF", "fBQJF", "=", false);
    MakeSrchCondition(arrayObj, "TB_BQBDJF", "fBQJFBD", "=", false);
    MakeSrchCondition(arrayObj, "TB_WCLJF", "fWCLJF", "=", false);
    MakeSrchCondition(arrayObj, "TB_WCLBDJF", "fWCLJFBD", "=", false);  
    MakeSrchCondition(arrayObj, "TB_CLRQ1", "dCLSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_CLRQ2", "dCLSJ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};