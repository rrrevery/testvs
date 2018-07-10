vUrl = "../CRMGL.ashx";
var vSHDM = GetUrlParam("shdm");

$(document).ready(function () {
    FillSH($("#DDL_SHMC"));
    FillYHQ($("#DDL_YHQMC"));
    $("#B_Exec").hide();

    $("#status-bar").hide();



});

function MakeJLBH(t_jlbh) {
    //生成iJLBH的JSON
    var arrayObj = new Array();
    MakeSrchCondition2(arrayObj, t_jlbh, "iJLBH", "=", false);
    MakeSrchCondition2(arrayObj, vSHDM, "sSHDM", "=", true);
    return arrayObj;
}


function IsValidData() {
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sSHDM = $("#DDL_SHMC").combobox("getValue");
    Obj.iYHQID = $("#DDL_YHQMC").combobox("getValue");
    Obj.iBJ_SYD = $("#BJ_SYD")[0].checked ? 1 : 0;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#DDL_SHMC").combobox('select', Obj.sSHDM);
    $("#DDL_YHQMC").combobox('select', Obj.iYHQID);
    $("#BJ_SYD")[0].checked = Obj.iBJ_SYD == "1" ? true : false;
  


}


