vUrl = "../KFPT.ashx";
vCaption = "任务处理";
function InitGrid() {
    vColumnNames = ['记录编号', '任务主题', '任务内容', 'iRWDX', '客服经理', '开始日期', '结束日期', 'iRWWCZT', '任务完成状态', '完成情况', 'iDJR', '发布人', '发布时间', 'iZXR', '执行人', '执行时间', 'iPYR', '评语人', '评价时间',"领导评语","分值","状态" ];
    vColumnModel = [
               { name: 'iJLBH', width: 80, },
               { name: 'sRWZT', width: 80, },
               { name: 'sRW', width: 160, },
               { name: 'iRWDX', hidden: true, },
               { name: 'sPERSON_NAME', width: 80, },
			   { name: 'dKSRQ', width: 100, },
			   { name: 'dJSRQ', width: 100, },
               { name: 'iRWWCZT', hidden: true, },
               { name: 'sRWWCZT', width: 80, },
               { name: 'sWCQK', width: 80, },
               { name: 'iDJR', hidden: true, },
			   { name: 'sDJRMC', width: 80, },
			   { name: 'dDJSJ', width: 140, },
               { name: 'iZXR', hidden: true, },
			   { name: 'sZXRMC', width: 80, },
			   { name: 'dZXRQ', width: 140, },
               { name: 'iPYR', hidden: true, },
			   { name: 'sPYRMC', width: 80, },
			   { name: 'dPYRQ', width: 140, },
               { name: 'sLDPY', width: 80, },
			   { name: 'fFZ', width: 80, },
               { name: 'sSTATUS', width: 80, },
    ];
}


$(document).ready(function () {
    $("#B_Insert").hide();
    RefreshButtonSep();
    FillRWZT("S_RWZT");
    $("#B_Update").text("处理");
    BFButtonClick("TB_PYRMC", function () {
        SelectRYXX("TB_PYRMC", "HF_PYR", "zHF_PYR", true);
    });
})

function SetControlState() {
    var rowData = $("#list").datagrid("getSelected");
    if (rowData) {  //已经处理过的任务不可再处理
        if (rowData.iPYR != 0) {
            document.getElementById("B_Update").disabled = true;
        }
        else {
            document.getElementById("B_Update").disabled = false;
        }
    }

}

function MakeSearchCondition() {
    var arrayObj = new Array();
    var vSFZX = $("[name='sfzx']:checked").val();         //判断是否处理了活动,1为已执行未处理，2为已处理
    if (vSFZX == 2 || vSFZX == 1) {
        MakeSrchCondition2(arrayObj, vSFZX, "iSTATUS", "=", false);
    }
    MakeSrchCondition(arrayObj, "S_RWZT", "iJLBH_RW", "=", false);
    MakeSrchCondition(arrayObj, "TB_KSRQ1", "dKSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_KSRQ2", "dKSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ1", "dJSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ2", "dJSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_PYR", "iPYR", "in", false);

    return arrayObj;
};
