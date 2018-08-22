vUrl = "../KFPT.ashx";


$(document).ready(function () {
    $("#B_Insert").hide();
    $("#B_Delete").hide();
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#B_Update").text("执行");

    RefreshButtonSep();
});

function SetControlState() {
    document.getElementById("TT_RW").disabled = true;
    if ($("#HF_ZXR").val() != 0)
        document.getElementById("B_Update").disabled = true;

}

function IsValidData() {
    return true;
}


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    Obj.iRWWCZT = $("[name='rwwczt']:checked").val();
    Obj.sWCQK = $("#TT_WCQK").val();

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
}
