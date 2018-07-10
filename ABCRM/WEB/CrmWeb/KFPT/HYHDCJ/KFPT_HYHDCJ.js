vUrl = "../KFPT.ashx";
var zzr = GetUrlParam("zzr");

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#B_Insert").hide();
    $("#B_Delete").hide();
    $("#B_Update").text("参加");

    RefreshButtonSep();
});

function SetControlState() {
    if ($("#HF_CJDJR").val() != 0)
        document.getElementById("B_Update").disabled = true;

}
function UpdateClickCustom() {
    if (zzr != 0) {
        ShowMessage("已终止的活动不能再参加");
        cPS_BROWSE = 2;
        SetControlBaseState();
        return false;
    }
};
function IsValidData() {
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iCJRS = $("#TB_CJRS").val();
    Obj.sCJBZ = $("#TB_CJBZ").val();

    Obj.iCJDJR = iDJR;
    Obj.sCJDJRMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_CJRS").val(Obj.iCJRS);
    $("#TB_CJBZ").val(Obj.sCJBZ);

    $("#LB_HDMC").text(Obj.sHDMC);
    $("#LB_HYKNO").text(Obj.sHYK_NO);
    $("#LB_NAME").text(Obj.sGKNAME);
    $("#LB_LXDH").text(Obj.sLXDH);
    $("#LB_ZJHM").text(Obj.sZJHM);
    $("#LB_BMRS").text(Obj.iBMRS);
    $("#HF_BMDJR").val(Obj.iBMDJR);
    $("#HF_CJDJR").val(Obj.iCJDJR);
    $("#LB_BMDJRMC").text(Obj.sBMDJRMC);
    $("#LB_CJDJRMC").text(Obj.sCJDJRMC);
    $("#LB_BMSJ").text(Obj.dBMSJ);
    $("#LB_CJSJ").text(Obj.dCJSJ);
}