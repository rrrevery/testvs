vUrl = "../CRMGL.ashx";


$(document).ready(function () {
    $("#DJR").hide();
    $("#ZXR").hide();
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });

});


function IsValidData() {
    if ($("#TB_NAME").val() == "") {
        ShowMessage("请输入业务员名称", 3);
        return false;
    }
    //if ($("#TB_YWYDM").val() == "") {
    //    ShowMessage("请输入业务员代码", 3);
    //    return false;
    //}
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sYWYMC = $("#TB_NAME").val();
    Obj.iMDID = $("#HF_MDID").val();
    Obj.sYWYDM = $("#TB_YWYDM").val();
    Obj.iBJ_TY = $("#CB_TY").prop("checked") ? "1" : "0";
    Obj.sZY = $("#TB_ZY").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_NAME").val(Obj.sYWYMC);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_YWYDM").val(Obj.sYWYDM);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#TB_ZY").val(Obj.sZY);
    $("#CB_TY").prop("checked", Obj.iBJ_TY == "1");
}