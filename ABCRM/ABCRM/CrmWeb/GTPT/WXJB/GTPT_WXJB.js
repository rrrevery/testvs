vUrl = "../GTPT.ashx";
var HYKNO = GetUrlParam("HYKNO");
vDBName = "CRMDB";
$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();

    if (HYKNO != "") {
        $("#TB_HYKNO").val(HYKNO);
        GetWXHYXX1();
        vProcStatus = cPS_ADD;
        SetControlBaseState();
        $("#TB_HYKNO").attr("readonly", "readonly");
    }

    $("#TB_HYKNO").bind('keypress', function (event) {
        if (event.keyCode == "13") {
            GetWXHYXX1();
        }
    });

    //$("#TB_HYKNO").change(function () {
    //    GetWXHYXX();
    //});

})
function GetWXHYXX1() {
    if ($("#TB_HYKNO").val() != "") {

        var str = GetWXHYXXData(0, $("#TB_HYKNO").val(), vDBName);
        $("#HF_HYID").val(0);
        if (str == "null" || str == "" ||str == undefined) {
            ShowMessage("会员不存在！请重新录入信息");
            return;
        }
       

        var Obj = JSON.parse(str);

        $("#HF_HYID").val(Obj.iHYID);
        $("#HF_CODE").val(Obj.sBINDCODE);
        $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
        $("#LB_HYNAME").text(Obj.sHY_NAME);
        $("#LB_HYKNAME").text(Obj.sHYKNAME);
        if (Obj.iLX == 1) {
            $("#LB_LX").text("开卡");
        } else if (Obj.iLX == 2) {
            $("#LB_LX").text("绑卡");
        }
        $("#LB_BDSJ").text(Obj.dDJSJ);
        $("#LB_OPENID").text(Obj.sOPENID);
        $("#LB_LH").text(Obj.sUNIONID);
        $("#LB_PID").text(Obj.iPUBLICID);

    }
}

function IsValidData() {
    if ($("#HF_HYID").val() == "" || $("#HF_HYID").val() == undefined || $("#HF_HYID").val() == null) {
        ShowMessage("会员不存在！请重新录入信息");
        return false;
    }

    return true;
}
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sHYK_NO = $("#TB_HYKNO").val();
    Obj.iHYID = $("#HF_HYID").val();
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.sHYKNAME = $("#LB_HYKNAME").text();
    Obj.sOPENID = $("#LB_OPENID").text();
    Obj.sUNIONID = $("#LB_LH").text();
    Obj.iPUBLICID = $("#LB_PID").text();
    Obj.sBINDCODE = $("#HF_CODE").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    Obj.iBJ_WXKBJB = $("#C_WXKBJB").prop("checked") ? 1 : 0;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_HYKNO").val(Obj.sHYKNO);
    $("#HF_HYID").val(Obj.iHYID);
    $("#LB_OPENID").text(Obj.sOPENID);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    if (Obj.iLX == 1) {
        $("#LB_LX").text("开卡");
    } else if (Obj.iLX == 2) {
        $("#LB_LX").text("绑卡");
    }
    $("#LB_PID").text(Obj.iPUBLICID);
    $("#LB_LH").text(Obj.sUNIONID);
    $("#LB_BDSJ").text(Obj.dBDSJ);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);

    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID)


}
