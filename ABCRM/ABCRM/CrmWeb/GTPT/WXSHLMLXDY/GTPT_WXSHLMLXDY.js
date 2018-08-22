vUrl = "../GTPT.ashx";


$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();
    RefreshButtonSep();
});


function InsertClickCustom() {
    var str = GetSelfInx("MOBILE_LMSHLXDEF", "INX");
    if (str != "" && str != null)
    {
        var Obj = JSON.parse(str);
        $("#TB_INX").val(Obj.iINX + 1);
    }   
};



function IsValidData() {
    if ($("#TB_LXMC").val() == "") {
       ShowMessage( "请输入联盟商户类型名称");
       return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sLXMC = $("#TB_LXMC").val();
    if ($("#TB_INX").val() != "" || $("#TB_INX").val() != null) {
        Obj.iINX = $("#TB_INX").val();
    }
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_LXMC").val(Obj.sLXMC);
    $("#TB_INX").val(Obj.iINX);
}


