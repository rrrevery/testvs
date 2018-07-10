vUrl = "../CRMGL.ashx";
vCaption = "商户定义";

function InitGrid() {
    vColumnNames = ['商户代码', '商户名称', ];
    vColumnModel = [
        { name: 'sSHDM', width: 80 },
        { name: 'sSHMC', width: 200 },
    ];
}

$(document).ready(function () {
    //$("#jlbh").hide();
    //$('#reg-form').easyform();
});

function IsValidData() {
    if ($("#TB_SHDM").val() == "") {
        ShowMessage("商户代码称不能为空");
        return false;
    }
    if ($("#TB_SHMC").val() == "") {
        ShowMessage("商户名称不能为空");
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = 0;
    Obj.sOldSHDM = $("#HF_SHDM").val();
    Obj.sSHDM = $("#TB_SHDM").val();
    Obj.sSHMC = $("#TB_SHMC").val();

    return Obj;
}
function ShowData(data) {
    $("#HF_SHDM").val(data.sSHDM);
    $("#TB_SHDM").val(data.sSHDM);
    $("#TB_SHMC").val(data.sSHMC);
    $("#TB_JLBH").val("1");
}
