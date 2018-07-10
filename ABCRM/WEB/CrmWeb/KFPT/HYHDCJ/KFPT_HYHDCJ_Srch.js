vUrl = "../KFPT.ashx";
//var hdcs = GetUrlParam("hdcs");
//var hdid =GetUrlParam("iHDID");
vCaption = "会员活动参加";

function InitGrid() {
    vColumnNames = ['记录编号', 'iHYID', 'iHDID', '活动名称', '会员卡号', '姓名', '电话', '证件号码', '参加时间', 'iCJDJR', '参加登记人', '报名时间', 'iBMDJR', '报名登记人', '报名人数', '参加人数', '参加备注', '备注','终止人'];
    vColumnModel = [
               { name: 'iJLBH', width: 80, },
               { name: 'iHYID', hidden: true, },
               { name: 'iHDID', hidden: true, },
               { name: 'sHDMC', width: 80, },
               { name: 'sHYK_NO', width: 80, },
               { name: 'sGKNAME', width: 80, },
               { name: 'sLXDH', width: 90, },
               { name: 'sZJHM', width: 130, },
               { name: 'dCJSJ', width: 150, },
               { name: 'iCJDJR', hidden: true, },
               { name: 'sCJDJRMC', width: 80, },
               { name: 'dBMSJ', width: 80, },
               { name: 'iBMDJR', hidden: true, },
               { name: 'sBMDJRMC', width: 80, },
               { name: 'iBMRS', width: 80, },
               { name: 'iCJRS', width: 80, },
               { name: 'sCJBZ', hidden: true, },
               { name: 'sBZ', width: 80, },
               { name: 'iZZR', hidden: true, },
    ];
}


$(document).ready(function () {
    $("#B_Insert").hide();
    $("#B_Update").text("参加");

    FillHD($("#DDL_HDID"));// 活动下拉菜单，可选状态，0已保存，1已审核，-1已终止，不选的话是所有活动
    //if (hdcs == 1)
    //    $("#DDL_HDID").val(hdid);
    RefreshButtonSep();
});


function DBClickRow(rowIndex, rowData) {
    //T--表格双击事件
    if ($("#B_Insert").css("display") != "none" || $("#B_Update").css("display") != "none") {
        var sDbCurrentPath = "";
        sDbCurrentPath = sCurrentPath + "?jlbh=" + rowData.iJLBH + "&zzr=" + rowData.iZZR;
        MakeNewTab(sDbCurrentPath, vCaption, vPageMsgID);
    }
};

function SetControlState() {
    var rowData = $("#list").datagrid("getSelected");
    if (rowData) {  //已经参加过不可再参加
        if (rowData.iCJDJR != 0) {
            document.getElementById("B_Update").disabled = true;
        }
    }

}


function MakeSearchCondition() {
    var arrayObj = new Array();
    var vSFCJ = $("[name='sfcj']:checked").val();         //判断是否参加了活动,1为参加，2为未参加
    if (vSFCJ == 1) {
        MakeSrchCondition2(arrayObj, "NOT NULL", "iCJDJR", "is", false);
    }
    else if (vSFCJ == 2) {
        MakeSrchCondition2(arrayObj, " NULL", "iCJDJR", "is", false);
    }
    MakeSrchCondition(arrayObj, "DDL_HDID", "iHDID", "=", false);
    MakeSrchCondition(arrayObj, "TB_HYK_NO", "sHYK_NO", "=", true);
    MakeSrchCondition(arrayObj, "TB_CJSJ1", "dCJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_CJSJ2", "dCJSJ", "<=", true);
    return arrayObj;
};




