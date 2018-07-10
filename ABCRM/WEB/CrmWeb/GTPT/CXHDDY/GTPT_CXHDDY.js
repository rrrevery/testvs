vUrl = "../GTPT.ashx";
$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();
    document.getElementById("JLBHCaption").innerHTML = "促销ID";

});


function IsValidData() {
    if ($("#TB_CXZT").val() == "") {
        ShowMessage("请输入促销主题");
        return false;
    }
    if ($("#TB_START_RQ").val() == "") {
        ShowMessage("请选择开始日期");
        return false;
    }
    if ($("#TB_END_RQ").val() == "") {
        ShowMessage("请选择结束日期");
        return false;
    }
    return true;
}
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sCXZT = $("#TB_CXZT").val();
    Obj.sCXNR = $("#TB_CXNR").val();
    Obj.dSTART_RQ = $("#TB_START_RQ").val();
    Obj.dEND_RQ = $("#TB_END_RQ").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_CXZT").val(Obj.sCXZT);
    $("#TB_CXNR").val(Obj.sCXNR);
    $("#TB_START_RQ").val(Obj.dSTART_RQ);
    $("#TB_END_RQ").val(Obj.dEND_RQ);
}
