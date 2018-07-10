vUrl = "../GTPT.ashx";
vCaption = "城市定义";

function InitGrid() {
    vColumnNames = ['城市编号', '城市名称'];
    vColumnModel = [
             { name: 'iJLBH', },
			{ name: 'sCSMC', },
    ];
}

$(document).ready(function () {
    $("#jlbh .dv_sub_left").html("城市编号");
});


//function SetControlState() {
//    $("#B_Insert").show();
//    $("#B_Exec").hide();
//    $("#status-bar").hide();
//    $("#list").trigger("reloadGrid");
//}

function IsValidData() {
    if ($("#TB_CSMC").val() == "") {
        ShowMessage("城市名称不能为空", 3);
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sCSMC = $("#TB_CSMC").val();
    return Obj;
}

function ShowData(data) {
    $("#TB_JLBH").val(data.iJLBH);
    $("#TB_CSMC").val(data.sCSMC);
}
