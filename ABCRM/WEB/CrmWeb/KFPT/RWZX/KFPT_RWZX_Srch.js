vUrl = "../KFPT.ashx";
vCaption = "任务执行";
function InitGrid() {
    vColumnNames = ['记录编号', '任务主题', '任务内容', 'iRWDX', '客服经理', '开始日期', '结束日期', 'iRWWCZT', '任务完成状态', '完成情况', 'iDJR', '发布人', '发布时间', 'iZXR', '执行人', '执行时间', ];
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
    ];
}


$(document).ready(function () {
    $("#B_Insert").hide();
    RefreshButtonSep();
    FillRWZT("S_RWZT");
    $("#B_Update").text("执行");
    BFButtonClick("TB_ZXRMC", function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", true);
    });
})

//function DBClickRow(rowIndex, rowData) {
//    //T--表格双击事件
//    if ($("#B_Insert").css("display") != "none" || $("#B_Update").css("display") != "none") {
//        var sDbCurrentPath = "";
//        sDbCurrentPath = sCurrentPath + "?jlbh=" + rowData.iJLBH + "&zzr=" + rowData.iZZR;
//        MakeNewTab(sDbCurrentPath, vCaption, vPageMsgID);
//    }
//};

function SetControlState() {
    var rowData = $("#list").datagrid("getSelected");
    if (rowData) {  //已经执行过的任务不可再执行
        if (rowData.iZXR != 0) {
            document.getElementById("B_Update").disabled = true;
        }
    }

}
function MakeSearchCondition() {
    var arrayObj = new Array();
    var vSFZX = $("[name='sfzx']:checked").val();         //判断是否执行了活动,1为已执行，0为未执行
    if (vSFZX == 0 || vSFZX == 1) {
        MakeSrchCondition2(arrayObj, vSFZX, "iSTATUS", "=", false);
    }
    var vRWWCZT = $("[name='rwwczt']:checked").val();
    if (vRWWCZT == 0 || vRWWCZT == 1) {
        MakeSrchCondition2(arrayObj, vRWWCZT, "iRWWCZT", "=", false);
    }
    MakeSrchCondition(arrayObj, "S_RWZT", "iJLBH_RW", "=", false);
    MakeSrchCondition(arrayObj, "TB_KSRQ1", "dKSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_KSRQ2", "dKSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ1", "dJSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ2", "dJSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "in", false);

    return arrayObj;
};
