vUrl = "../KFPT.ashx";
vCaption = "会员活动领导评述";
function InitGrid() {
    vColumnNames = ['活动ID', '活动名称', '开始时间', '结束时间', '预计人数',  '报名人数', '参加人数', '回访人数', '客服人员ID', '客服人员姓名','终止人','评述人','评述人名称','评述时间'];
    vColumnModel = [
                    { name: 'iHDID', width: 50, },
                    { name: 'sHDMC', width: 100, },
                    { name: 'dKSSJ', width: 130, },
                    { name: 'dJSSJ', width: 130 },
                    { name: 'iRS', width: 50, },
                    { name: 'iBMRS', width: 50, },
                    //{ name: 'iQRRS', width: 50, hidden: true },
                    { name: 'iCJRS', width: 50, },
                    { name: 'iHFRS', width: 50 },
                    { name: 'iKFRYID', width: 60, hidden: true },
                    { name: 'sKFRYMC', width: 80 },
                    { name: 'iZZR', hidden: true, },
                    { name: 'iPSR', hidden: true, },
                    { name: 'sPSRMC', width: 80 },
                    { name: 'dPSSJ', width: 100 },
    ];
}

$(document).ready(function () {
    //$("#B_Update").hide();
    $("#B_Update").text("评述");
    $("#B_Insert").hide();
    RefreshButtonSep();

    //$("#list").jqGrid("setGridParam", {
    //    ondblClickRow: function (rowid) {
    //        var rowData = $("#list").getRowData(rowid);
    //        MakeNewTab("CrmWeb/KFPT/HYHDLDPS/KFPT_HYHDLDPS.aspx?jlbh=" + rowData.iHDID + "&kfryid=" + rowData.iKFRYID + "&zzr=" + rowData.iZZR, "会员活动评述", vPageMsgID);
    //    },
    //});
    FillHD($("#S_HDID"));// 活动下拉菜单，可选状态，0已保存，1已审核，-1已终止，不选的话是所有活动
});

function DBClickRow(rowIndex, rowData) {
    //T--表格双击事件
    if ($("#B_Insert").css("display") != "none" || $("#B_Update").css("display") != "none") {
        var sDbCurrentPath = "";
        sDbCurrentPath = sCurrentPath + "?jlbh=" + rowData.iHDID + "&zzr=" + rowData.iZZR + "&kfryid=" + rowData.iKFRYID;
        MakeNewTab(sDbCurrentPath, vCaption, vPageMsgID);
    }
};

function SetControlState() {
    var rowData = $("#list").datagrid("getSelected");
    if (rowData) {
        if (rowData.iPSR != 0)  //已经评述过不可再评述
            document.getElementById("B_Update").disabled = true;
    }
}


function IsValidSearch() {
    return true;
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    var vSFPS = $("[name='sfps']:checked").val();
    if (vSFPS == 1) {  //已评述
        MakeSrchCondition2(arrayObj, "not NULL", "iPSR", "is", false);
    }
    else if (vSFPS == 2) {  //未评述
        MakeSrchCondition2(arrayObj, " NULL ", "iPSR", "is", false);
    }
    MakeSrchCondition(arrayObj, "TB_PSSJ1", "dPSSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_PSSJ2", "dPSSJ", "<=", true);
    MakeSrchCondition(arrayObj, "S_HDID", "iHDID", "=", false);
    return arrayObj;
};