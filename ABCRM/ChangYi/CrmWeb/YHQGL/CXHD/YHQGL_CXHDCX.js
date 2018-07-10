vUrl = "../YHQGL.ashx";
var kssj = GetUrlParam("kssj");

$(document).ready(function () {
    $("#TB_SHMC").click(function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", true);
    });
    if (kssj)
        $("#TB_KSSJ").val(kssj);
});


function IsValidData() {
    if ($("#DDL_SHDM").val() == "") {
        ShowMessage("请选择门店", 3);
        return false;
    }
    if ($("#TB_CXHDMC").val() == "") {
        ShowMessage("请填写活动名称", 3);
        return false;
    }
    if ($("#TB_NIAN").val() == "") {
        ShowMessage("请填写活动年份", 3);
        return false;
    }
    if ($("#TB_KSSJ").val() > $("#TB_JSSJ").val()) {
        ShowMessage("开始时间不能大于结束时间", 3);
        return false;
    }
    return true;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_CXHDMC").val(Obj.sCXZT);
    $("#TB_SHMC").val(Obj.sSHMC);
    $("#HF_SHDM").val(Obj.sSHDM);
    $("#TB_KSSJ").val(Obj.dKSSJ);
    $("#TB_JSSJ").val(Obj.dJSSJ);
    $("#TB_NIAN").val(Obj.iNIAN);
    $("#TB_CXQS").val(Obj.iCXQS);
    $("#TB_CXNR").val(Obj.sCXNR);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}



function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sCXZT = $("#TB_CXHDMC").val();
    Obj.sSHDM = $("#HF_SHDM").val();
    Obj.sCXNR = $("#TB_CXNR").val();
    Obj.dKSSJ = $("#TB_KSSJ").val();
    Obj.dJSSJ = $("#TB_JSSJ").val();
    Obj.iNIAN = $("#TB_NIAN").val();
    Obj.iCXQS = $("#TB_CXQS").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}