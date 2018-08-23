vUrl = "../HYKGL.ashx";
var HYKNO = GetUrlParam("HYKNO");
var FXDWID = 0;
var vMZK = GetUrlParam("mzk");
var DBCaption = vMZK == "1" ? "CRMDBMZK" : "CRMDB";
if (vMZK == "1") {
    vUrl = "../../MZKGL/MZKGL.ashx";
}
var vDJLX = GetUrlParam("djlx");
var img = "";
$(document).ready(function () {
    if (HYKNO != "") {
        $("#TB_HYKHM_OLD").val(HYKNO);
        GetHYXX();
        vProcStatus = cPS_ADD;
        SetControlBaseState();
        $("#TB_HYKHM_OLD").attr("readonly", "readonly");
    }
    FillHKYY();
    $("#btn_HYKHM_OLD").click(function () {
        var conData = new Object();
        conData.iBJ_KCK = 0;
        SelectSK("TB_HYKHM_OLD", "HF_HYID", "", conData);

    });

    $("#btn_HYKHM_NEW").click(function () {

        var conData = new Object();
        conData.iBJ_KCK = 1;
        SelectSK("TB_HYKHM_NEW", "", "", conData);



    });

    $("#TB_HYKHM_OLD").bind('keypress', function (event) {//#TB_CDNR,
        if (event.keyCode == "13") {

            GetHYXX();
        }
    });

    $("#TB_HYKHM_NEW").bind('keypress', function (event) {//#TB_CDNR,
        if (event.keyCode == "13") {
            GetKCKXX();
        }
    });
});

function GetHYXX() {
    if ($("#TB_HYKHM_OLD").val() != "") {
        var str = GetHYXXData(0, $("#TB_HYKHM_OLD").val(), DBCaption);
        if (str) {
            var Obj = JSON.parse(str);
            if (Obj.iSTATUS < 0 && Obj.iSTATUS != -3) {
                ShowMessage("此卡状态无法进行补卡");
                return;
            }
            $("#HF_HYID").val(Obj.iHYID);
            $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
            $("#LB_HY_NAME").text(Obj.sHY_NAME);
            $("#LB_HYKNAME").text(Obj.sHYKNAME);
            $("#HF_FXDWID").val(Obj.iFXDW);
            $("#LB_FXDWMC").text(Obj.sFXDWMC);
            $("#LB_JF").text(Obj.fWCLJF);
            $("#LB_JE").text(Obj.fCZJE);
            $("#HF_BJ_CHILD").val(Obj.iBJ_CHILD);
        }
    }
}
//查找新卡信息(HYKCARD)
function GetKCKXX() {
    if ($("#TB_HYKHM_OLD").val() == "") {
        ShowMessage("请先输入原卡号", 3);
        $("#TB_HYKHM_NEW").val("");
        return;
    }
    if ($("#TB_HYKHM_NEW").val() != "") {
        if ($("#TB_HYKHM_NEW").val() == $("#TB_HYKHM_OLD").val()) {
            ShowMessage("该卡与原卡卡号一致，请重新选择", 3);
            $("#TB_HYKHM_NEW").val("");
            return;
        }
        var str = GetKCKXXData($("#TB_HYKHM_NEW").val(), "", "", DBCaption);
        if (str == "null" || str == "") {
            ShowMessage("没有找到卡号", 3);
            $("#TB_HYKHM_NEW").val("");
            return;
        }
        var Obj = JSON.parse(str);
        if (Obj.sCZKHM == "") {
            ShowMessage("没有找到卡号", 3);
            $("#TB_HYKHM_NEW").val("");
            return;
        }
        if (Obj.iSKJLBH != 0) {
            ShowMessage("该卡在" + Obj.iSKJLBH + "号售卡单中，请重新选择", 3);
            $("#TB_HYKHM_NEW").val("");
            return;
        }
        if (Obj.iSTATUS != 1) {
            ShowMessage("该卡不是领用状态，请重新选择", 3);
            $("#TB_HYKHM_NEW").val("");
            return;
        }
        if (Obj.iHYKTYPE != $("#HF_HYKTYPE").val()) {
            ShowMessage("该卡与原卡卡类型不一致，请重新选择", 3);
            $("#TB_HYKHM_NEW").val("");
            return;
        }
        if (Obj.fQCYE != 0) {
            ShowMessage("该卡有期初余额不能换卡，请重新选择", 3);
            $("#TB_HYKHM_NEW").val("");
            return;
        }

        //if (Obj.iFXDWID != FXDWID) {
        //    ShowMessage("该卡与原卡发行单位不一致，请重新选择", 3);
        //    $("#TB_HYKHM_NEW").val("");
        //    return;
        //}
        var boolFK = CheckBGDDQX(Obj.sBGDDDM, iDJR);

        if (boolFK.iBJ_CHECK == false) {
            ShowMessage("该操作员没有该保管地点的操作权限！", 3);
            return;
        }
        else {
            $("#HF_BGDDDM").val(Obj.sBGDDDM);//获取新卡的保管地点
            $("#TB_BGDDMC").val(Obj.sBGDDMC);
        }
        //if (Obj.sBGDDDM != $("#HF_BGDDDM").val()) {
        //    art.dialog({ lock: true, content: "该卡保管地点与操作地点不符，请重新选择" });
        //    $("#TB_HYKHM_NEW").val("");
        //    return;
        //}
    }
}

function IsValidData() {
    if ($("#HF_HYID").val() == "" || $("#HF_HYID").val() == undefined || $("#HF_HYID").val() == null) {
        ShowMessage("会员不存在！请重新录入信息", 3);
        return false;
    }
    if ($("#DDL_HKYY").val() == "") {
        ShowMessage("请选择换卡原因", 3);
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sHYKHM_OLD = $("#TB_HYKHM_OLD").val();
    Obj.iHYID = $("#HF_HYID").val();
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.fJF = $("#LB_JF").text();
    Obj.fJE = $("#LB_JE").text();
    Obj.sHYKHM_NEW = $("#TB_HYKHM_NEW").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.sZY = $("#TB_ZY").val();
    Obj.fGBF = $("#TB_GBF").val();
    Obj.iFXDWID = $("#HF_FXDWID").val();
    Obj.sFXDWMC = $("#LB_FXDWMC").text();
    Obj.sDBConnName = DBCaption;
    if (Obj.fGBF == "")
        Obj.fGBF = "0";
    Obj.iBJ_CHILD = $("#HF_BJ_CHILD").val();
    Obj.iHKYY = GetSelectValue("DDL_HKYY");
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_HYKHM_OLD").val(Obj.sHYKHM_OLD);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#HF_HYID").val(Obj.iHYID);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#LB_HY_NAME").text(Obj.sHY_NAME);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    $("#LB_JF").text(Obj.fJF);
    $("#LB_JE").text(Obj.fJE);
    $("#TB_HYKHM_NEW").val(Obj.sHYKHM_NEW);
    $("#TB_ZY").val(Obj.sZY);
    $("#TB_GBF").val(Obj.fGBF);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#HF_FXDWID").val(Obj.iFXDWID);
    $("#LB_FXDWMC").text(Obj.sFXDWMC);
    $("#HF_BJ_CHILD").val(Obj.iBJ_CHILD)
    $("#DDL_HKYY").val(Obj.iHKYY);

}

function FillHKYY() {

    var obj = document.getElementById('DDL_HKYY');
    obj.options.add(new Option("卡遗失换卡（收费）", 1));
    $("#tpcontent").html("<p>元</p>");
    $("#DDL_HKYY option[value=1]").attr("selected", true);
    obj.options.add(new Option("电子会员换实体卡(免费)", 3));

}
function onselectchange() {
    if ($("#DDL_HKYY").val() != "") {
        if (parseInt($("#DDL_HKYY")[0].value) != 1) {
            $("#TB_GBF").val("");
            $("#TB_GBF").prop("disabled", true);
            $("#tpcontent").html("");
        }
        else {
            $("#TB_GBF").val("");
            $("#TB_GBF").prop("disabled", false);
            $("#tpcontent").html("<p>元</p>");
        }
    }
}

function MoseDialogCustomerReturn(dialogName, lst, showField) {
    if (dialogName == "DialogSK") {
        $("#" + showField).blur()
    }
}





