vUrl = "../KFPT.ashx";
vCaption = "客户消费等级组定义";

function InitGrid() {
    vColumnNames = ['消费等级组ID', '客户组名称', '累计消费金额', '----', '备注', ];
    vColumnModel = [
            { name: 'iJLBH', },
            { name: 'sKFDJZMC', },
            { name: 'iXFJE_BEGIN', },
            { name: 'iXFJE_END', },
            { name: 'sBZ', },
    ];
}
$(document).ready(function () {
    $("#JLBHCaption").html("消费等级组ID");

});



function IsValidData() {
    if ($("#TB_KFDJZMC").val() == "") {
        ShowMessage("客户组名称");
        return false;
    }
    if ($("#TB_XFJE_BEGIN").val() == "") {
        ShowMessage("累计消费金额");
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sKFDJZMC = $("#TB_KFDJZMC").val();
    Obj.iXFJE_BEGIN = $("#TB_XFJE_BEGIN").val();
    Obj.iXFJE_END = $("#TB_XFJE_END").val();
    Obj.sBZ = $("#TB_BZ").val();

    return Obj;
}
function ShowData(data) {
    $("#TB_JLBH").val(data.iJLBH);
    $("#TB_KFDJZMC").val(data.sKFDJZMC);
    $("#TB_XFJE_BEGIN").val(data.iXFJE_BEGIN);
    $("#TB_XFJE_END").val(data.iXFJE_END);
    $("#TB_BZ").val(data.sBZ);
  
}

