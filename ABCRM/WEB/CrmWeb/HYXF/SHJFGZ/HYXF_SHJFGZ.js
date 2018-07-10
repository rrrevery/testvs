vUrl = "../HYXF.ashx";


$(document).ready(function () {

});

function SetControlState() {
    $("#B_Exec").hide();
    $("#status-bar").hide();

}

function IsValidData() {

    if (isNaN(($("#TB_JE").val()))) {
        ShowMessage("请输入有效金额");

        return false;
    }
    if (isNaN(($("#TB_JF").val()))) {
        ShowMessage("请输入有效积分");

        return false;
    }
    if ($("#TB_JF").val() == "") {
        ShowMessage("请输入积分");

        return false;
    }

    if ($("#TB_JE").val() == "") {
        ShowMessage("请输入金额");

        return false;
    }
    return true;
}



function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sMC = $("#TB_MC").val();
    Obj.fJE = $("#TB_JE").val();
    Obj.fJF = $("#TB_JF").val();

    return Obj;
}
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_MC").val(Obj.sMC);
    $("#TB_JE").val(Obj.fJE);
    $("#TB_JF").val(Obj.fJF);
}










