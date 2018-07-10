vUrl = "../HYKGL.ashx";

function InitGrid() {
    vColumnNames = ["mdid", "门店", "hyid", "卡号", "姓名", 'iSEX', "性别", "出生日期", "hyktype", "卡类型", "未处理积分", "往年未处理积分", "升级积分", "手机号码", "证件类型", "证件号码", "建卡日期", "有效期", "STATUS", "状态", "推荐人", "住宅电话", "QQ号", "微信", "微博", "E-mail", "邮编", "通讯地址", "职业类型", "教育程度", "家庭月收入", "交通工具", "汽车品牌", "车牌号码", "婚姻状态", "结婚纪念日", "品牌会员", "客户经理", "商圈名称", "小区名称"
, "DJR", "登记人", "登记时间", ];
    vColumnModel = [
			  { name: "iMDID", hidden: true, },
			  { name: "sMDMC", width: 100 },
			  { name: "iHYID", hidden: true, },
			  { name: "sHYK_NO", width: 100, },
			  { name: "sHY_NAME", align: "left" },
			  { name: "iSEX", hidden: true, },
			  { name: "sSEX", width: 50, },
			  { name: "dCSRQ", width: 100, },
			  { name: "iHYKTYPE", hidden: true, },
			  { name: "sHYKNAME", align: "left", width: 60, },
			  { name: "fWCLJF", },
			  { name: "fWNWCLJF", hidden: true },
			   { name: "fBQJF", },
			  { name: "sSJHM", align: "left" },
			  { name: "iZJLXID", align: "left", hidden: true, },
			  {
			      name: "sSFZBH", align: "left", width: 150, formatter: function (cellvalue, cell) {
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
			  { name: "sStatusName", },
			  { name: "sTJRMC", },
			  { name: "sPHONE", },
			  { name: "sQQ", },
			  { name: "sWX", },
			  { name: "sWB", },
			  { name: "sE_MAIL", },
			  { name: "sYZBM", },
			  { name: "sTXDZ", },
			  {
			      name: "iZYID", formatter: function (cellvalue, options, rowObject) {
			          return getHYXXXMMC(cellvalue);
			      }
			  },
			  {
			      name: "iXLID", formatter: function (cellvalue, options, rowObject) {
			          return getHYXXXMMC(cellvalue);
			      }
			  },
			  {
			      name: "iJTSRID", formatter: function (cellvalue, options, rowObject) {
			          return getHYXXXMMC(cellvalue);
			      }
			  },
			  {
			      name: "iJTGJID", formatter: function (cellvalue, options, rowObject) {
			          return getHYXXXMMC(cellvalue);
			      }
			  },
			  { name: "sQCPP", },
			  { name: "sCPH", },
			  { name: "sHYZK", },
			  { name: "dJHJNR", },
			  { name: "sPPHY", },
			  { name: "sKHJLRYMC", },
			  { name: "sSQMC", hidden: true },
			  { name: "sXQMC", },
			  { name: "iDJR", hidden: true, },
			  { name: "sDJRMC", align: "left" },
			  { name: "dDJSJ", width: 160, },
    ];
};

$(document).ready(function () {
    //  $("#B_ExportMember").show();

    FillSelect("DDL_ZJLX", GetHYXXXM(0));
    FillSelect("DDL_ZY", GetHYXXXM(1));
    FillSelect("DDL_JTSR", GetHYXXXM(2));
    FillSelect("DDL_XL", GetHYXXXM(3));
    FillSelect("DDL_JTGJ", GetHYXXXM(5));
    FillSelect("S_QCPP", GetHYXXXM(11));
    FillSelect("DDL_JTCY", GetHYXXXM(6));

    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#B_Exec").hide();

    BFButtonClick("TB_FXDWMC", function () {
        SelectFXDW("TB_FXDWMC", "HF_FXDWID", "zHF_FXDWID", false);
    });
    BFButtonClick("TB_KLX", function () {
        SelectKLX("TB_KLX", "HF_KLX", "zHF_KLX", false);
    });
    BFButtonClick("TB_MD", function () {
        SelectMD("TB_MD", "HF_MD", "zHF_MD", false);
    });
    //BFButtonClick("TB_HYBQ", function () {
    //    SelectHYBQ("TB_HYBQ", "HF_HYBQ", "zHF_HYBQ");
    //});

    //BFButtonClick("TB_SHMC", function () {
    //    SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", false);
    //});

    //BFButtonClick("TB_XQMC", function () {
    //    SelectLQXQ("TB_XQMC", "HF_XQID", "zHF_XQID", false);
    //});

    //BFButtonClick("TB_SQMC", function () {
    //    SelectSQ("TB_SQMC", "HF_SQID", "zHF_SQID", false);
    //});
    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    CheckBox("CB_KYHY", "HF_KYHY");

    RefreshButtonSep();


    $("input[type='checkbox'][name='CB_SEX']").click(function () {
        if (this.checked) {
            $(this).prop("checked", this.checked).siblings().prop("checked", !this.checked);
            $("#HF_SEX").val($(this).val());
        }
        else {
            $(this).prop("checked", this.checked);
            $("#HF_SEX").val("");
        }
        if ($.trim($(this).val()) == "") {
            $("#HF_SEX").val("");
        }

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

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    //var KYHY = $("#HF_KYHY").val();
    //if (KYHY == "1")
    //    MakeSrchCondition2(arrayObj, 1, "iBJ_KY", " != ", false);
    //if (KYHY == "2")
    //    MakeSrchCondition2(arrayObj, 1, "iBJ_KY", "= ", false);
    MakeSrchCondition(arrayObj, "TB_HYKNO", "sHYKNO", "=", true);
    MakeSrchCondition(arrayObj, "TB_HYNAME", "sHY_NAME", "=", true);
    MakeSrchCondition(arrayObj, "HF_SEX", "iSEX", "=", false);
    MakeSrchCondition(arrayObj, "TB_SJHM", "sSJHM", "=", true);
    MakeSrchCondition(arrayObj, "DDL_ZJLX", "iZJLXID", "=", false);
    MakeSrchCondition(arrayObj, "TB_ZJHM", "sSFZBH", "=", true);
    MakeSrchCondition(arrayObj, "TB_CSRQ1", "dCSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_CSRQ2", "dCSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_SR1", "SR", ">=", true);
    MakeSrchCondition(arrayObj, "TB_SR2", "SR", "<=", true);
    MakeSrchCondition(arrayObj, "TB_Age1", "iAGE", ">=", false);
    MakeSrchCondition(arrayObj, "TB_Age2", "iAGE", "<=", false);
    MakeSrchCondition(arrayObj, "HF_FXDWID", "sFXDWDM", "in", false);
    MakeSrchCondition(arrayObj, "DDL_XL", "iXLID", "=", false);
    MakeSrchCondition(arrayObj, "DDL_JTSR", "iJTSRID", "=", false);
    MakeSrchCondition(arrayObj, "DDL_JTCY", "iJTCYID", "=", false);
    MakeSrchCondition(arrayObj, "DDL_JTGJ", "iJTGJID", "=", false);
    MakeSrchCondition(arrayObj, "HF_KLX", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "HF_MD", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "TB_JKRQ1", "dJKRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JKRQ2", "dJKRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_YXQ1", "dYXQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_YXQ2", "dYXQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_STATUS", "iSTATUS", "in", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "in", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_XQID", "iXQID", "in", false);
    //MakeSrchCondition(arrayObj, "HF_HYBQ", "iXH", "in", false);
    //MakeSrchCondition(arrayObj, "HF_SHDM", "sSHDM", "=", true);
    MakeMoreSrchCondition(arrayObj);

    return arrayObj;
}
function AddCustomerCondition(Obj) {
    Obj.bShowPublic = bCanShowPublic;
    if ($("#HF_HYBQ").val() != "") {
        Obj.sXH = $("#HF_HYBQ").val();
    }
}



//function WUC_ReturnLQXQ(MC, ID, zID, SQID) {
//    var tp_return = $.dialog.data('IpValuesReturn');
//    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
//    if (tp_return) {
//        var jsonString = tp_return;
//        if (jsonString != null && jsonString.length > 0) {
//            var tp_mc = "";
//            var tp_hf = "";
//            var tp_sqmc = "";
//            var tp_SQID = "";
//            $("#" + MC).val(tp_mc);
//            var jsonInput = JSON.parse(jsonString);
//            var contractValues = new Array();
//            contractValues = jsonInput.Articles;
//            for (var i = 0; i <= contractValues.length - 1; i++) {
//                tp_mc += contractValues[i].Name + ";";
//                tp_sqmc += contractValues[i].FreeField + ";";
//                tp_hf += contractValues[i].Id + ",";
//                tp_SQID += contractValues[i].Id1 + ",";
//            }
//            $("#" + MC).val(tp_mc);
//            $("#" + ID).val(tp_hf.substr(0, tp_hf.length - 1));
//            $("#" + zID).val(jsonString);
//        }
//    }
//}


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

function getHYXXXMMC(cellvalue) {
    var str = GetHYXXXMMC(cellvalue);
    if (str == "null" || str == "") {
        return "";
    }
    else {
        var Obj = JSON.parse(str);
        return Obj.sNR;
    }
}





