vUrl = "../KFPT.ashx";

$(document).ready(function () {
    $("#B_Insert").hide();
    $("#B_Delete").hide();
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#B_Update").text("处理");
    RefreshButtonSep();

})



function SetControlState() {
    document.getElementById("div_wczt").disabled = true;
    document.getElementById("TT_RW").disabled = true;
    document.getElementById("TT_WCQK").disabled = true;
    if ($("#HF_PYR").val() != 0)
        document.getElementById("B_Update").disabled = true;
    else
        document.getElementById("B_Update").disabled = false;

}

function IsValidData() {
    if ($("#TB_FZ").val() < 0 || $("#TB_FZ").val() > 100) {
        ShowMessage("分值范围为0-100");
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    Obj.sLDPY = $("#TT_LDPY").val();
    Obj.fFZ = $("#TB_FZ").val();
    Obj.iPYR = iDJR;
    Obj.sPYRMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_RWDX").val(Obj.iRWDX);
    $("#LB_PERSON_NAME").text(Obj.sPERSON_NAME);
    $("#LB_RWZT").text(Obj.sRWZT);
    $("#TT_RW").text(Obj.sRW);
    $("[name='rwwczt'][value='" + Obj.iRWWCZT + "']").prop("checked", true);
    $("#TT_WCQK").text(Obj.sWCQK);
    $("#LB_HYKNO").text(Obj.sHYKNO);
    $("#LB_HYNAME").text(Obj.sHYNAME);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);

    $("#TT_LDPY").val(Obj.sLDPY);
    $("#TB_FZ").val(Obj.fFZ);
    $("#LB_PYRMC").text(Obj.sPYRMC);
    $("#HF_PYR").val(Obj.iPYR);
    $("#LB_PYRQ").text(Obj.dPYRQ);

}
