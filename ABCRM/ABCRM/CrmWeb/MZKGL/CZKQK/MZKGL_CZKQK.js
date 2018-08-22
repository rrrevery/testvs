vUrl = "../MZKGL.ashx";
var HYKNO = GetUrlParam("HYKNO");

$(document).ready(function () {
    if (HYKNO != "") {
        $("#TB_HYKNO").val(HYKNO);
        GetHYXX();
        //vProcStatus = cPS_ADD;
        //SetControlBaseState();
        $("#TB_HYKNO").attr("readonly", "readonly");
    }
    $("#TB_CZMD").click(function () {
        SelectMD("TB_CZMD", "HF_MDID", "zHF_MDID", true);
    })
});

function GetHYXX() {
    if ($("#TB_HYKNO").val() != "") {
        var str = GetMZKXXData(0, $("#TB_HYKNO").val(), "", "CRMDBMZK");
        if (str == "null" || str == "") {
            ShowMessage("卡号不存在或者校验失败", 3);
            return;
        }
        var Obj = JSON.parse(str);
        if (Obj.sHYK_NO == "") {
            ShowMessage("卡号不存在或者校验失败", 3);
            return;
        }
        if (Obj.iSTATUS <0) {
            ShowMessage("卡状态错误", 3);
            return;
        }
        $("#HF_HYID").val(Obj.iHYID);
        $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
        $("#LB_HY_NAME").text(Obj.sHY_NAME);
        $("#LB_HYKNAME").text(Obj.sHYKNAME);
        $("#LB_YJE").text(Obj.fCZJE);
    }
}

function SetControlState() {
    ;
}

function IsValidData() {
    if ($("#HF_HYID").val() == "" || $("#HF_HYID").val() == undefined || $("#HF_HYID").val() == null) {
        art.dialog({ lock: true, content: "会员不存在！请重新录入信息" });
        return false;
    }
    if ($("#HF_MDID").val() == "") {
        art.dialog({ lock: true, content: '请选择操作门店' });
        return false;
    }
    if ($("#TB_QKJE").val() == "" || parseInt($("#TB_QKJE").val()) > parseInt($("#LB_YJE").text())) {
        art.dialog({ lock: true, content: '取款金额不能大于余额！' });
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
    Obj.iMDID = $("#HF_MDID").val();
    Obj.fYJE = $("#LB_YJE").text();
    Obj.fQKJE = $("#TB_QKJE").val();
    Obj.sZY = $("#TB_ZY").val();

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    Obj.sDBConnName = "CRMDBMZK";
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_CZMD").val(Obj.sMDMC);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_HYKNO").val(Obj.sHYKNO);
    $("#HF_HYID").val(Obj.iHYID);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#LB_HY_NAME").text(Obj.sHY_NAME);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    $("#LB_YJE").text(Obj.fYJE);
    $("#TB_QKJE").val(Obj.fQKJE);

    $("#TB_ZY").val(Obj.sZY);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}
