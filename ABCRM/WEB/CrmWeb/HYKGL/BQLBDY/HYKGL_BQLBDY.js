vUrl = "../HYKGL.ashx";
vCaption = "标签类别定义";

function InitGrid() {
    vColumnNames = ['标签类别ID', '标签类别名称', '停用标记']; 
    vColumnModel = [
            { name: 'iJLBH',width:80 },
            { name: 'sLABELMC', width: 80 },
            { name: 'iSTATUS', formatter: BoolCellFormat,width: 80  },
    ];
}

$(document).ready(function () {
    $("#JLBHCaption").html("标签类别ID");
});


function IsValidData() {
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sLABELMC = $("#TB_BQMC").val();
    Obj.iSTATUS = $("#CB_TY")[0].checked ? "1" : "0";
    return Obj;
}

function ShowData(data) {
    $("#TB_JLBH").val(data.iJLBH);
    $("#TB_BQMC").val(data.sLABELMC);
    $("#CB_TY").prop("checked", data.iSTATUS == "1" ? true : false);
}
