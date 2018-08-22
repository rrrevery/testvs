vUrl = "../KFPT.ashx";

$(document).ready(function () {
    $("#jlbh").hide();
    $("#status-bar").hide();
    $("#B_Exec").hide();
    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", true);
    });

    RefreshButtonSep();
});
function IsValidData() {
    if ($.trim($("#TB_HYKNAME").val()) == "") {
        ShowMessage("请输入会员卡类型");
        return false;
    }
    if ($("#TB_YJJE").val() == "") {
        ShowMessage("请输入预警金额");
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#HF_HYKTYPE").val();
    Obj.iYJJE = $("#TB_YJJE").val();
    return Obj;
}
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_HYKTYPE").val(Obj.iJLBH);
    $("#TB_HYKNAME").val(Obj.sHYKNAME);
    $("#TB_YJJE").val(Obj.iYJJE);
}