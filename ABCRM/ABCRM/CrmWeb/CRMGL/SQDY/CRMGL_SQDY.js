vUrl = "../CRMGL.ashx";
vCaption = "商圈定义";
$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();
    document.getElementById("JLBHCaption").innerHTML = "商圈ID";
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });
});
function SetControlState() {
    ;
};

function IsValidData() {
    //if ($("#TB_MDMC").val() == "")
    //{
    //    ShowMessage("请选择门店");        
    //    return false;
    //}
    if ($("#TB_SQMC").val() == "")
    {
        ShowMessage("请输入商圈名称");
        return false;
    }
    return true;
}
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if ($("#TB_JLBH").val() == "") {
        Obj.iJLBH = 0;
    }
    Obj.sSQMC = $("#TB_SQMC").val();
    Obj.sSQMS = $("#TB_SQMS").val();
    Obj.iMDID = $("#HF_MDID").val();
    return Obj;
}

function ShowData(data) {
    var Obj = new Object();
    Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_SQMC").val(Obj.sSQMC);
    $("#TB_SQMS").val(Obj.sSQMS);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_MDMC").val(Obj.sMDMC);
}





