vUrl = "../HYKGL.ashx";
function InitGrid() {
    vColumnNames = ["hyid", "卡号", "姓名", 'iSEX', "性别", "标签项目", "标签值", "标签来源", "mdid", "门店", "出生日期", "hyktype", "卡类型", "未处理积分", "往年未处理积分", "升级积分", "手机号码", "证件类型", "证件号码", "建卡日期", "有效期", "STATUS", "状态", "住宅电话", "QQ号", "微信", "微博", "E-mail", "邮编", "通讯地址"];
    vColumnModel = [
            { name: "iHYID", hidden: true, },
              { name: "sHYK_NO", width: 100, },
              { name: "sHY_NAME", width: 100, },
              { name: "iSEX", hidden: true, },
              { name: "sSEX", width: 50, },
              { name: "sLABELXMMC", width: 100, },
              { name: "sLABELVALUE", width: 100, },
              { name: "sBJ_TRANSSTR", width: 100, },
              { name: "iMDID", hidden: true, },
              { name: "sMDMC", width: 100, },
              { name: "dCSRQ", width: 100, },
              { name: "iHYKTYPE", hidden: true, },
              { name: "sHYKNAME", align: "left" },
              { name: "fWCLJF", width: 100, },
              { name: "fWNWCLJF", hidden: true },
              { name: "fBQJF", width: 100, },
              { name: "sSJHM", align: "left", width: 100, },
              { name: "iZJLXID", align: "left", hidden: true, },
              {
                  name: "sSFZBH", align: "left", width: 145, formatter: function (cellvalue, cell) {
                      if (bCanShowPublic) {
                          return cellvalue;
                      }
                      else {
                          return MakePrivateNumber(cellvalue);
                      }
                  }
              },
              { name: "dJKRQ", width: 100, },
              { name: "dYXQ", width: 100, },
              { name: "iSTATUS", align: "left", hidden: true, },
              { name: "sStatusName", width: 100, },
              { name: "sPHONE", width: 100, },
              { name: "sQQ", width: 100, },
              { name: "sWX", width: 100, },
              { name: "sWB", width: 100, },
              { name: "sE_MAIL", width: 100, },
              { name: "sYZBM", width: 100, },
              { name: "sTXDZ", width: 100, },
    ];
}


$(document).ready(function () {
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#B_Exec").hide();
    BFButtonClick("TB_KLX", function () {
        SelectKLX("TB_KLX", "HF_KLX", "zHF_KLX", false);
    });
    BFButtonClick("TB_MDMC", function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });
    BFButtonClick("TB_HYBQ", function () {
        SelectHYBQ("TB_HYBQ", "HF_HYBQ", "zHF_HYBQ",false);
    });
    BFButtonClick("TB_SHMC", function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", true);
    });

    //复选框 复选
    var statusarr = new Array();
    $("input[type='checkbox'][name='CB_STATUS']").click(function () {

        if (this.checked) {
            statusarr.push(parseInt($(this).val()));
        }
        else {
            var inx = $.inArray(parseInt($(this).val()), statusarr);
            var temp = statusarr[statusarr.length - 1];
            statusarr[statusarr.length - 1] = statusarr[inx];
            statusarr[inx] = temp;
            statusarr.pop();
        }
        var statusval = "";
        for (var i = 0; i < statusarr.length; i++) {
            statusval += statusarr[i] + ",";
        }
        $(this).prop("checked", this.checked);
        $("#HF_STATUS").val(statusval.substring(0, statusval.length - 1));
    });

    CheckBox("CB_KYHY", "HF_KYHY");

    RefreshButtonSep();
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_HYKNO1", "sHYKNO", ">=", true);
    MakeSrchCondition(arrayObj, "TB_HYKNO2", "sHYKNO", "<=", true);
    MakeSrchCondition(arrayObj, "TB_HYNAME", "sHY_NAME", "=", true);
    MakeSrchCondition(arrayObj, "HF_SEX", "iSEX", "=", false);
    if ($("#DDL_ZJLX").val() == 1) {
        MakeSrchCondition2(arrayObj, " null or B.ZJLXID=1 ", "iZJLXID", "is", false);
    }
    else {
        MakeSrchCondition(arrayObj, "DDL_ZJLX", "iZJLXID", "=", false);
    }
    MakeSrchCondition(arrayObj, "TB_CSRQ1", "dCSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_CSRQ2", "dCSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_SR1", "SR", ">=", true);
    MakeSrchCondition(arrayObj, "TB_SR2", "SR", "<=", true);
    MakeSrchCondition(arrayObj, "TB_Age1", "iAGE", ">=", false);
    MakeSrchCondition(arrayObj, "TB_Age2", "iAGE", "<=", false);
    MakeSrchCondition(arrayObj, "HF_KLX", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "TB_JKRQ1", "dJKRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JKRQ2", "dJKRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_YXQ1", "dYXQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_YXQ2", "dYXQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_STATUS", "iSTATUS", "in", false);
    MakeSrchCondition(arrayObj, "HF_HYBQ", "iXH", "in", false);
    MakeSrchCondition(arrayObj, "HF_SHDM", "sSHDM", "=", true);
    MakeSrchCondition(arrayObj, "DDL_BQLY", "iBJ_TRANS", "=", false);
    MakeSrchCondition(arrayObj, "TB_BQQZ", "fQZ", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}

function AddCustomerCondition(Obj) {
    Obj.bShowPublic = bCanShowPublic;
}
//function CustomerAddOtherSearchCondition(conditionStr) {
//    if (conditionStr != "") {
//        conditionStr += "','bShowPublic':'" + bCanShowPublic;
//    }
//    else {
//        conditionStr = "{'bShowPublic':'" + bCanShowPublic + "";
//    }
//    return conditionStr;
//}
function ReturnQX() {
    return bCanShowPublic;
}

function CheckBox(cbname, hfname) {
    $("input[type='checkbox'][name='" + cbname + "']").click(function () {
        if (this.checked) {
            $(this).prop("checked", this.checked).siblings().prop("checked", !this.checked);
            $("#" + hfname).val($(this).val());
        }
        else {
            $(this).prop("checked", this.checked);
            $("#" + hfname).val("");
        }
    });

}