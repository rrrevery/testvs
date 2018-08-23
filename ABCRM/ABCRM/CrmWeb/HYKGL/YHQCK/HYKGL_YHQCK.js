vUrl = "../HYKGL.ashx";
var HYKNO = GetUrlParam("HYKNO");

function IsValidData() {
    if ($("#HF_BGDDDM").val() == "") {
        ShowMessage("请选择操作地点!", 3);
        return false;
    }
    if ($("#HF_FS_YQMDFW").val() == "") {
        ShowMessage("请选择优惠券!", 3);
        return false;
    }
    if ($("#HF_CXHD").val() == "") {
        ShowMessage("请选择促销活动!", 3);
        return false;
    }
    if ($("#TB_CKJE").val() == "") {
        ShowMessage("请输入存款金额!", 3);
        return false;
    }
    if ($("#HF_HYID").val() == "" || $("#HF_HYID").val() == undefined || $("#HF_HYID").val() == null) {
        ShowMessage("会员不存在！请重新录入信息", 3);
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sHYKNO = $("#TB_HYKNO").val();
    Obj.iHYID = $("#HF_HYID").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.iYHQID = $("#HF_YHQ").val();//禁用状态val()获取不到值
    Obj.dJSRQ = $("#TB_JSRQ").val();

    if ($("#HF_FS_YQMDFW").val() == 3) {
        var md = JSON.parse($("#zHF_YQFWDM").val());
        Obj.sMDFWDM = md[0].sMDDM;
    }
    else
        Obj.sMDFWDM = $("#HF_YQFWDM").val();
    Obj.iCXID = $("#HF_CXHD").val();
    Obj.fYYE = "0";
    Obj.fCKJE = $("#TB_CKJE").val();
    Obj.sZY = $("#TB_ZY").val();
    if ($("#HF_CZYMDID").val() != "") {
        Obj.iMDID = $("#HF_CZYMDID").val();//操作员门店
    }
    Obj.iFS_YQMDFW = $("#HF_FS_YQMDFW").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

$(document).ready(function () {
    if (HYKNO != "") {
        $("#TB_HYKNO").val(HYKNO);
        GetHYXX();
        vProcStatus = cPS_ADD;
        SetControlBaseState();
    }
    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");
    // FillMDDM($("#S_MDFW"));

    $("#TB_CXHD").click(function () {
        var yhqid = $("#HF_YHQ").val();
        if (yhqid == "") {
            ShowMessage("请选择优惠券!", 3);
            return;
        }
        SelectCXHD("TB_CXHD", "HF_CXHD", "zHF_CXHD", false, "", yhqid);
    });

    $("#TB_YHQ").click(function () {
        SelectYHQ("TB_YHQ", "HF_YHQ", "zHF_YHQ", false);
    });

    $("#TB_YQFWMC").click(function () {
        if ($("#HF_YHQ").val() == "") {
            ShowMessage("请选择优惠券!", 3);
            return;
        }
        var Obj = JSON.parse($("#zHF_YHQ").val());
        $("#HF_FS_YQMDFW").val(Obj[0].iFS_YQMDFW)
        switch (parseInt($("#HF_FS_YQMDFW").val())) {
            case 1:
                $("#TB_YQFWMC").val("集团").prop("readonly", true);
                $("#HF_YQFWDM").val("");
                break;
            case 2:
                $("#TB_YQFWMC").prop("readonly", false);
                $("#zHF_YQFWDM").val("");
                SelectSH("TB_YQFWMC", "HF_YQFWDM", "zHF_YQFWDM", false);
                break;
            case 3:
                $("#TB_YQFWMC").prop("readonly", false);
                $("#zHF_YQFWDM").val("");
                SelectMD("TB_YQFWMC", "HF_YQFWDM", "zHF_YQFWDM", false);
                break;
        }
    });


    $("#btn_HYKHM").click(function () {
        var conData = new Object();
        conData.iBJ_KCK = 0;
        SelectSK("TB_HYKNO", "HF_HYID", "", conData)
    });
})

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);

    $("#TB_HYKNO").val(Obj.sHYKNO);
    $("#HF_HYID").val(Obj.iHYID);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#HF_YHQ").val(Obj.iYHQID);
    $("#TB_YHQ").val(Obj.sYHQMC);
    $("#TB_YQFWMC").val(Obj.sMDFWMC);
    $("#HF_YQFWDM").val(Obj.sMDFWDM);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#HF_CXHD").val(Obj.iCXID);
    $("#TB_CXHD").val(Obj.sCXZT);
    $("#HF_YJE").val(Obj.fYYE);
    $("#TB_CKJE").val(Obj.fCKJE);

    $("#TB_ZY").val(Obj.sZY);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#HF_CZYMDID").val(Obj.iMDID);
    $("#HF_FS_YQMDFW").val(Obj.iFS_YQMDFW);
    //  $("[name='cklx']")[0].checked = true;
}

function GetHYXX() {
    if ($("#TB_HYKNO").val() != "") {
        var str = GetHYXXData(0, $("#TB_HYKNO").val());
        if (str) {
            var Obj = JSON.parse(str);
            if (Obj.sHYK_NO == "") {
                ShowMessage("没有找到卡号!", 3);
                $("#TB_HYKNO").val("");
                $("#HF_HYID").val("");
                return;
            }
            $("#HF_HYID").val(Obj.iHYID);
        }
    }
}

function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContent");
}

function WUC_Return(MC, ID, zID, SHDM, SHMC, jqxhr) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            var extendname1 = "";
            var extendname2 = "";
            $("#" + MC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Code + ",";
                if (tp_mc != "undefined;") {
                    extendname1 += contractValues[i].ExtendName.split(",")[0] + ",";
                    extendname2 += contractValues[i].ExtendName.split(",")[1] + ",";
                }

            }
            if (tp_mc != "undefined;") {
                $("#" + MC).val(tp_mc);
                $("#" + ID).val(tp_hf.substr(0, tp_hf.length - 1));
                $("#" + zID).val(jsonString);
                $("#" + SHDM).val(extendname1.substr(0, extendname1.length - 1));
                $("#" + SHMC).text(extendname2.substr(0, extendname2.length - 1));
            }
            else {
                $("#" + MC).val("");
                $("#" + ID).val("");
                $("#" + zID).val("");
                $("#" + SHDM).val("");
                $("#" + SHMC).text("");
            }
            //xxm start
            if (jqxhr) {
                jqxhr.resolve("ok");
            }
            //stop
            WUC_MD_ReturnCustom();
        }
    }
};

function WUC_YHQ_Return(YHQMC, YHQID, zYHQID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            var tp_yqfw = "";
            $("#" + YHQMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id + ",";
                tp_yqfw += contractValues[i].Id1 + ",";

            }
            $("#" + YHQMC).val(tp_mc);
            $("#" + YHQID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#HF_FS_YQMDFW").val(tp_yqfw.substr(0, tp_yqfw.length - 1));
            $("#" + zYHQID).val(jsonString);
            WUC_YHQ_ReturnCustom();
        }
    }
}

function WUC_YHQ_ReturnCustom() {
    $("#TB_YQFWMC").val("");
    $("#HF_YQFWDM").val("");
    $("#zHF_YQFWDM").val("");
    $("#HF_CXHD").val("");
    $("#TB_CXHD").val("");
    $("#zHF_CXHD").val("");

}