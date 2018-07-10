vHF = GetUrlParam("hf");
vUrl = "../../MZKGL/MZKGL.ashx";
vDBName = "CRMDBMZK";
var HYKNO = GetUrlParam("HYKNO");


function IsValidData() {
    if ($("#HF_HYID").val() == "" || $("#HF_HYID").val() == undefined || $("#HF_HYID").val() == null) {
        art.dialog({ lock: true, content: "会员不存在！请重新录入信息" });
        return false;
    }

    if ($("#HF_MDID").val() == "") {
        ShowMessage("请选择门店", 3);
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
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.fJE = $("#LB_JE").text();
    Obj.sZY = $("#TB_ZY").val();
    Obj.iLX = vHF;
    Obj.iMDID = $("#HF_MDID").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

$(document).ready(function () {
    if (HYKNO != "") {
        $("#TB_HYKNO").val(HYKNO);
        GetMZKXX();
        //vProcStatus = cPS_ADD;
        //SetControlBaseState();
        $("#TB_HYKNO").attr("readonly", "readonly");
    }

    $("#TB_HYKNO").bind('keypress', function (event) {//#TB_CDNR,
        if (event.keyCode == "13") {
            GetMZKXX();
        }
    });

    $("#TB_HYKNO").change(function () {

        GetMZKXX();
    });
    $("#TB_MDMC").click(function () {

        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });
    //FillFXDWTree("TreeFXDW", "TB_FXDWMC", "menuContent");
})



function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_HYKNO").val(Obj.sHYKNO);
    $("#HF_HYID").val(Obj.iHYID);
    $("#HF_FXDWDM").val(Obj.sFXDWDM);
    $("#LB_FXDWMC").text(Obj.sFXDWMC);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#LB_HYNAME").text(Obj.sHY_NAME);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    $("#LB_JE").text(Obj.fJE);
    $("#TB_ZY").val(Obj.sZY);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    if (vHF == 0) {
        $("#LB_YZT").text("正常有效");
        $("#LB_XZT").text("挂失");

    }
    else {
        $("#LB_YZT").text("挂失");
        $("#LB_XZT").text("正常有效");
    }
}

function GetMZKXX() {
    if ($("#TB_HYKNO").val() != "") {
        var str = GetMZKXXData(0, $("#TB_HYKNO").val(), "", "CRMDBMZK");
        $("#HF_HYID").val(0);
        if (str == "null" || str == "") {
            ShowMessage("没有找到卡号或者校验失败");
            return;
        }
        if (str.indexOf("错误") >= 0) {
            ShowMessage(str);
            return;
        }

        var Obj = JSON.parse(str);
        if (Obj.iHYID == 0) {
            ShowMessage("没有找到卡号或者校验失败");
            return;
        }
        if (Obj.iSTATUS < 0 && vHF == "0") {
            ShowMessage("此卡为无效卡，无需挂失");
            return;
        }
        if (Obj.iSTATUS != -3 && vHF == "1") {
            ShowMessage("此卡非挂失状态，不能恢复");
            return;
        }
        $("#HF_HYID").val(Obj.iHYID);
        $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
        $("#LB_HYKNAME").text(Obj.sHYKNAME);
        $("#LB_JE").text(Obj.fCZJE);
        if (vHF == 0) {
            $("#LB_YZT").text("正常有效");
            $("#LB_XZT").text("挂失");

        }
        else {
            $("#LB_YZT").text("挂失");
            $("#LB_XZT").text("正常有效");
        }
    }
}

