
vUrl = "../CRMGL.ashx";
vCaption = "商品价格带定义";
function InitGrid() {
    vColumnNames = ['价格带编号', '价格带名称', 'SHDM', '商户名称', '最低价格', '最高价格'];
    vColumnModel = [
            { name: 'iJLBH', },
            { name: 'sJGDMC', },//sortable默认为true width默认150
            { name: 'sSHDM', hidden: true, },
            { name: 'sSHMC', hidden: true, },
            { name: 'fLSDJ1', },
            { name: 'fLSDJ2', },
    ];
}


$(document).ready(function () {
    $("#jlbh .dv_sub_left").html("价格带编号");
    $("#TB_SHMC").click(function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", true);
    });
});




function IsValidData() {
    if ($("#TB_JGDMC").val() == "") {
        ShowMessage("请输入价格带名称", 3);
        return false;
    }
    if ($("#TB_LSDJ1").val() == "" || $("#TB_LSDJ2").val() == "") {
        ShowMessage("请输入价格", 3);
        return false;
    }
    if (parseFloat($("#TB_LSDJ1").val()) > parseFloat($("#TB_LSDJ2").val())) {
        ShowMessage("最低价格不能高于最高价格", 3);
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sJGDMC = $("#TB_JGDMC").val();
    Obj.fLSDJ1 = $("#TB_LSDJ1").val();
    Obj.fLSDJ2 = $("#TB_LSDJ2").val();

    return Obj;
}

function ShowData(data) {
    $("#TB_JLBH").val(data.iJLBH);
    $("#TB_JGDMC").val(data.sJGDMC);
    $("#TB_LSDJ1").val(data.fLSDJ1);
    $("#TB_LSDJ2").val(data.fLSDJ2);
}

var clearNoNum = function (obj) {
    obj.value = obj.value.replace(/[^\d.]/g, "");
    obj.value = obj.value.replace(/^\./g, "");
    obj.value = obj.value.replace(/\.{2,}/g, ".");
    obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
    obj.value = obj.value.replace(/^(\-)*(\d+)\.(\d\d).*$/, '$1$2.$3');
};