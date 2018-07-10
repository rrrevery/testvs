vUrl = "../KFPT.ashx";
var hfjlbh = GetUrlParam("hfjlbh");
var zzr = GetUrlParam("zzr");

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#B_Insert").hide();
    $("#B_Delete").hide();
    $("#B_Update").text("回访");
    RefreshButtonSep();

});

function SetControlState() {
    if ( $("#HF_HFDJR").val()!=0)  
        document.getElementById("B_Update").disabled = true;

}


function IsValidData() {
    if ($("#TB_HFBZ").val() == "") {
        ShowMessage("请输入反馈信息");
        return false;
    }
    if ($("[name='hfjg']:checked").val() == null) {
        ShowMessage("请选择回访结果");
        return false;
    }

    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iHFJG = $("[name='hfjg']:checked").val();
    Obj.sHFBZ = $("#TB_HFBZ").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_HFBZ").val(Obj.sHFBZ);
    $("[name='hfjg'][value='" + Obj.iHFJG + "']").prop("checked", true);

    $("#LB_HDMC").text(Obj.sHDMC);
    $("#LB_HYKNO").text(Obj.sHYK_NO);
    $("#LB_NAME").text(Obj.sGKNAME);
    $("#LB_LXDH").text(Obj.sLXDH);
    $("#LB_ZJHM").text(Obj.sZJHM);
    $("#LB_BMRS").text(Obj.iBMRS);
    $("#LB_CJRS").text(Obj.iCJRS);
    $("#HF_BMDJR").val(Obj.iBMDJR);
    $("#HF_CJDJR").val(Obj.iCJDJR);
    $("#HF_HFDJR").val(Obj.iHFDJR);
    $("#LB_BMDJRMC").text(Obj.sBMDJRMC);
    $("#LB_CJDJRMC").text(Obj.sCJDJRMC);
    $("#LB_HFDJRMC").text(Obj.sHFDJRMC);
    $("#LB_BMSJ").text(Obj.dBMSJ);
    $("#LB_CJSJ").text(Obj.dCJSJ);
    $("#LB_HFSJ").text(Obj.dHFSJ);

}


