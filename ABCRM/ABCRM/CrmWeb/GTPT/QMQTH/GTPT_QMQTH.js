vUrl = "../GTPT.ashx";
$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#B_Delete").hide();
    $("#B_Cancel").hide();
    $("#B_Update").hide();
    document.getElementById("B_Insert").innerHTML = "添加订单号"

    document.getElementById("B_Save").innerHTML = "确认退款"



});




function IsValidData() {

    if (!isInteger($("#TB_JE").val())) {
        ShowMessage("金额为正整数");
        return false;
    }
    return true;
}
function isInteger(obj) {
    return obj % 1 == 0;
}
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.code = $("#TB_CODE").val();
    Obj.totalMoney = $("#TB_JE").val();
    Obj.cardNo = $("#TB_HYK_NO").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
  

    $("#LB_ERRORM").text(Obj.sERRORM);



}