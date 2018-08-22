vUrl = "../KFPT.ashx";
var hdcs = GetUrlParam("hdcs");
var hdid = GetUrlParam("iHDID");
vCaption = "会员活动报名";
function InitGrid() {
    vColumnNames = ['记录编号', 'iHYID', 'iHDID', '活动名称', '会员卡号', '姓名', '电话', '证件号码', '报名方式', '报名时间', 'iBMDJR', '报名登记人', '参加时间', 'iCJDJR', '参加登记人', '报名人数', '备注'];
    vColumnModel = [
               { name: 'iJLBH', width: 80, },
               { name: 'iHYID', hidden: true, },
               { name: 'iHDID', hidden: true, },
               { name: 'sHDMC', width: 80, },
               { name: 'sHYK_NO', width: 80, }, 
               { name: 'sGKNAME', width: 80, },
               { name: 'sLXDH', width: 90, },
               { name: 'sZJHM', width: 120, },
               { name: 'sBJ_BMFS', width: 90,},
               { name: 'dBMSJ', width: 80, },
               { name: 'iBMDJR', hidden: true, },
               { name: 'sBMDJRMC', width: 80, },
               { name: 'dCJSJ', width: 80, },
               { name: 'iCJDJR', hidden: true, },
               { name: 'sCJDJRMC', width: 80, },
               { name: 'iBMRS', width: 80, },
               { name: 'sBZ', width: 80, },
    ];
}

$(document).ready(function () {
    $("#B_Update").hide();
    BFButtonClick("TB_BMDJRMC", function () {
        SelectRYXX("TB_BMDJRMC", "HF_BMDJR", "zHF_BMDJR", false);
    });
    BFButtonClick("TB_CJDJRMC", function () {
        SelectRYXX("TB_CJDJRMC", "HF_CJDJR", "zHF_CJDJR", false);
    });
    FillHD($("#DDL_HDID"));// 活动下拉菜单，可选状态，0已保存，1已审核，-1已终止，不选的话是所有活动
    if (hdcs == 1)
        $("#DDL_HDID").val(hdid);
    RefreshButtonSep();
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_HYK_NO", "sHYK_NO", "=", true);
    MakeSrchCondition(arrayObj, "TB_GKNAME", "sGKNAME", "=", true);
    MakeSrchCondition(arrayObj, "TB_LXDH", "sLXDH", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZJHM", "sZJHM", "=", true);
    MakeSrchCondition(arrayObj, "TB_BZ", "sBZ", "like", true);
    MakeSrchCondition(arrayObj, "DDL_HDID", "iHDID", "=", false);
    MakeSrchCondition(arrayObj, "HF_BMDJR", "iBMDJR", "in", false);
    MakeSrchCondition(arrayObj, "HF_CJDJR", "iCJDJR", "in", false);
    MakeSrchCondition(arrayObj, "TB_BMSJ1", "dBMSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_BMSJ2", "dBMSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_CJSJ1", "dCJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_CJSJ2", "dCJSJ", "<=", true);

    return arrayObj;
};