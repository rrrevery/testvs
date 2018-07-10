vUrl = "../MZKGL.ashx";

$(document).ready(function () {
});

function SetControlState() {
    ;
}

function IsValidData() {
    if ($("#TB_FKRMC").val() == "") {
        ShowMessage("付款人名称不能为空" );
        return false;
    }
    if ($("#TB_DZYH").val() == "") {
        ShowMessage("到账银行不能为空" );
        return false;
    }
    if ($("#TB_ZPJE").val() == "") {
        ShowMessage("支票金额不能为空" );
        return false;
    }
    if ($("#TB_DZRQ").val() == "") {
        ShowMessage( "到账日期不能为空" );
        return false;
    }
    if ($("#TB_SYJE").val() == "") {
        ShowMessage("使用金额不能为空");
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sFKRMC = $("#TB_FKRMC").val();
    Obj.sDZYH = $("#TB_DZYH").val();
    Obj.fJE = $("#TB_ZPJE").val();
    Obj.dDZRQ = $("#TB_DZRQ").val();
    Obj.sZY = $("#TB_ZY").val();
    Obj.fSYJE = $("#TB_SYJE").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_FKRMC").val(Obj.sFKRMC);
    $("#TB_DZYH").val(Obj.sDZYH);
    $("#TB_ZPJE").val(Obj.fJE);
    $("#TB_DZRQ").val(Obj.dDZRQ);
    $("#TB_ZY").val(Obj.sZY);
    $("#TB_SYJE").val(Obj.fSYJE);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}