vUrl = "../CRMGL.ashx";
vCZK = GetUrlParam("czk");
vCaption = "卡种定义";
function InitGrid() {
    vColumnNames = ["卡种编号", "卡种名称", "发行方式", "销售记录", "参与消费积分", "开通优惠券账户", "储值账户", "储值卡", "卡号长度", "前导码", "后置码", "磁道介质", "卡有效期指定方式", "需要系统制卡", "需要强制验卡", "有效期长度", "磁道内容加密"];
    vColumnModel = [
            { name: "iJLBH", hidden: true, },
          { name: "sHYKKZNAME", width: 100, },
          { name: "sFXFS", width: 100, },
          { name: "iBJ_XSJL", width: 60, formatter: BoolCellFormat, },
          { name: "iBJ_JF", width: 60, formatter: BoolCellFormat, },
          { name: "iBJ_YHQZH", width: 60, formatter: BoolCellFormat, },
          { name: "iBJ_CZZH", width: 60, formatter: BoolCellFormat, },
          { name: "iBJ_CZK", width: 60, formatter: BoolCellFormat, },
          { name: "iHMCD", width: 100, },
          { name: "sKHQDM", width: 100, },
          { name: "sKHHZM", width: 100, },
          { name: "sCDJZ", width: 100, },
          { name: "sFS_YXQ", width: 100, },
          { name: "iBJ_XTZK", width: 60, formatter: BoolCellFormat, },
          { name: "iBJ_QZYK", width: 60, formatter: BoolCellFormat, },
          { name: "sYXQCD", width: 100, },
          { name: "iBJ_CDNRJM", width: 60, formatter: BoolCellFormat, },
    ];
};

$(document).ready(function () {
    ConbinDataArry["czk"] = vCZK;
    $("#B_Exec").hide();

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_HYKKZID", "iHYKKZID", "=", false);
    MakeSrchCondition(arrayObj, "TB_HYKKZNAME", "sHYKKZNAME", "=", true);
    MakeSrchCondition2(arrayObj, vCZK, "iBJ_CZK", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};

function InsertClick1() {
    MakeNewTab("CrmWeb/CRMGL/KZDY/Webform1.aspx", vCaption, vPageMsgID);
};
