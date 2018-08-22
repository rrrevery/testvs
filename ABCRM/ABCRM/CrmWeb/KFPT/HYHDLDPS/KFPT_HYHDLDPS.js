vUrl = "../KFPT.ashx";
var kfryid = GetUrlParam("kfryid");
var zzr = GetUrlParam("zzr");

$(document).ready(function () {
    $("#JLBHCaption").text("活动ID");
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#B_Insert").hide();
    $("#B_Delete").hide();
    $("#B_Update").text("评述");
    RefreshButtonSep();

});

function SetControlState() {
    if ($("#HF_PSR").val() != 0)
        document.getElementById("B_Update").disabled = true;

}
function UpdateClickCustom() {
    if (zzr != 0) {
        ShowMessage("已终止的活动不能再评述");
        cPS_BROWSE = 2;
        SetControlBaseState();
        return false;
    }
};
function IsValidData() {
    if ($("#TB_LDPY").val() == "") {
        ShowMessage("请输入评语");
        return false;
    }
    if ($("#TB_PF").val() == "") {
        ShowMessage("请输入评分");
        return false;
    }
    if ($("#TB_PF").val() < 0 || $("#TB_PF").val() > 100) {
        ShowMessage("评分范围0~100");
        return false;
    }
    return true;
}

//function MakeJLBH(t_jlbh) {
//    //生成iJLBH的JSON
//    var Obj = new Object();
//    Obj.iJLBH = t_jlbh;
//    Obj.iKFRYID = kfryid;
//    if (GetUrlParam("mzk") == "1") {
//        Obj.sDBConnName = "CRMDBMZK";
//    }
//    return Obj;
//}

function SaveData() {
    var Obj = new Object();
    Obj.iHDID = $("#TB_JLBH").val();
    Obj.iKFRYID = $("#HF_KFRYID").val();
    Obj.fFZ = $("#TB_PF").val();
    Obj.sLDPY = $("#TB_LDPY").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_LDPY").val(Obj.sLDPY);
    $("#TB_PF").val(Obj.fFZ);

    $("#TB_JLBH").val(Obj.iHDID);
    $("#LB_HDMC").text(Obj.sHDMC);
    $("#HF_KFRYID").val(Obj.iKFRYID);
    $("#LB_KFRYMC").text(Obj.sKFRYMC);
    $("#HF_PSR").val(Obj.iPSR);
    $("#LB_PSRMC").text(Obj.sPSRMC);
    $("#LB_PSSJ").text(Obj.dPSSJ);

}


