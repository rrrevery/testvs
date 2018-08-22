vUrl = "../KFPT.ashx";
var hdcs = GetUrlParam("hdcs");
var hdid = GetUrlParam("iHDID");
vCaption = "会员活动回访";
function InitGrid() {
    vColumnNames = ['记录编号', 'iHYID', 'iHDID', '活动名称', '会员卡号', '姓名', '电话', '证件号码', '参加时间', 'iCJDJR', '参加登记人', '报名时间', 'iBMDJR', '报名登记人', '回访时间','回访登记人','报名人数','参加人数','回访标记', '备注','终止人'];
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
               { name: 'dHFSJ', width: 80, },
               { name: 'sHFDJRMC', width: 80, },
               { name: 'iBMRS', width: 80, },
               { name: 'iCJRS', width: 80, },
               { name: 'iHFBJ', hidden: true, },
               { name: 'sBZ', hidden: true, },
               { name: 'iZZR', hidden: true, },
    ];
}

$(document).ready(function () {
    $("#B_Insert").hide();
    $("#B_Update").text("回访");
    RefreshButtonSep();

    FillHD($("#S_HDID"));
    if (hdcs == 1)
        $("#S_HDID").val(hdid);
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
    if (rowData) {  //已经回访过不可再回访
        if (rowData.iHFBJ == 1) {
            document.getElementById("B_Update").disabled = true;
        }
    }

}


function MakeSearchCondition() {
    var arrayObj = new Array();
    var vSFHF = $("[name='sfhf']:checked").val();         //判断是否回访了活动,1为已回访，2为未回访
    if (vSFHF == 1) {
        MakeSrchCondition2(arrayObj, vSFHF, "iHFBJ", "=", false);
    }
    else if (vSFHF == 0) {
        MakeSrchCondition2(arrayObj, " NULL or H.HFBJ=0", "iHFBJ", "is", false);
    }

    MakeSrchCondition(arrayObj, "S_HDID", "iHDID", "=", false);
    MakeSrchCondition(arrayObj, "TB_HYK_NO", "sHYK_NO", "=", true);
    MakeSrchCondition(arrayObj, "TB_GKNAME", "sGKNAME", "=", true);
    MakeSrchCondition(arrayObj, "TB_HFSJ1", "dHFSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_HFSJ2", "dHFSJ", "<=", true);
    return arrayObj;
};
