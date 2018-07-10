vUrl = "../GTPT.ashx";
vCaption = "级次定义";

function InitGrid() {
    vColumnNames = ['级次ID', '级次名称', ];
    vColumnModel = [
        { name: 'iJC', },
		{ name: 'sMC', },
    ];
}


$(document).ready(function () {


    $("#B_Exec").hide();
    $("#status-bar").hide();
   
});


function SetControlState() {
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#list").trigger("reloadGrid");
}

function IsValidData() {
    if ($("#TB_MC").val() == "") {
        art.dialog({ content: "级次名称不能为空", lock: true, time: 2 });
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
    return Obj;
}
function ShowData(data) {
    $("#TB_JLBH").val(data.iJLBH);
    $("#TB_MC").val(data.sMC);
}

