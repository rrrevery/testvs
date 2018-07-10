vUrl = "../CRMGL.ashx";
vCaption = "年龄段定义";
function InitGrid() {
    vColumnNames = ["年龄段ID", "年龄段名称", "起始年龄", "终止年龄"];
    vColumnModel = [
            { name: "iJLBH", width: 180, },
			{ name: "sNLDMC", width: 180, },
			{ name: "iNL1", width: 180, },
			{ name: "iNL2", width: 180, },
    ];
}

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#jlbh .bffld_left").html("年龄段ID");

});


function IsValidData() {
    if ($("#TB_NLDMC").val() == "") {
        ShowMessage("请输入年龄段名称", 3);
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sNLDMC = $("#TB_NLDMC").val();
    Obj.iNL1 = $("#TB_NL1").val();
    Obj.iNL2 = $("#TB_NL2").val();

    return Obj;
}

function ShowData(data) {
    $("#TB_JLBH").val(data.iJLBH);
    $("#TB_NLDMC").val(data.sNLDMC);
    $("#TB_NL1").val(data.iNL1);
    $("#TB_NL2").val(data.iNL2);
}
