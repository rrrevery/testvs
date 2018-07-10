vUrl = "../GTPT.ashx";

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();
});

function IsValidData() {
    if ($("#TT_ASK").val() == "") {
        ShowMessage("请提问");
        return false;
    }
    return true;
}


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iPUBLICID = $("#TB_PUBLICID").val();
    //Obj.iBJ_DY = $("#CB_BJ_DY")[0].checked ? "1" : "0";
    //Obj.iBJ_NONE = $("#CB_BJ_NONE")[0].checked ? "1" : "0";
    Obj.iTYPE = $("[name='TYPE']:checked").val();
    Obj.iSTATUS = $("[name='status']:checked").val();
    Obj.sASK = $("#TT_ASK").val();
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    //$("#TB_PUBLICID").val(Obj.iPUBLICID);
    //$("#CB_BJ_DY").prop("checked", Obj.iBJ_DY == "1" ? true : false);
    //$("#CB_BJ_NONE").prop("checked", Obj.iBJ_NONE == "1" ? true : false);
    $("[name='status'][value='" + Obj.iSTATUS + "']").prop("checked", true);
    $("[name='TYPE'][value='" + Obj.iTYPE + "']").prop("checked", true);

    $("#TT_ASK").val(Obj.sASK);
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID)



}

function InsertClickCustom() {
    $("#TT_ASK").val("");
    $("[name='status'][value='1']").prop("checked", true);
};