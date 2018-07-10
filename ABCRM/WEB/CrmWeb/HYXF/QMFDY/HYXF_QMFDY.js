vUrl = "../HYXF.ashx";

$(document).ready(function () {
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", "menuContentHYKTYPE");
    $("#B_Exec").hide();
    $("#status-bar").hide();

});


function IsValidData() {

    return true;
}
function onHYKClick(e, treeId, treeNode) {
    $("#TB_HYKNAME").val(treeNode.name);
    $("#HF_HYKTYPE").val(treeNode.id);
    hideMenu("menuContentHYKTYPE");
}
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.fJF_J = $("#TB_JF_J").val();
    Obj.fJF_N = $("#TB_JF_J").val();
    Obj.iBJ_TY = $("#CB_BJ_TY")[0].checked ? "1" : "0";
    Obj.iMDID = $("#S_MD").val();
    return Obj;
}
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_HYKNAME").val(Obj.sHYKNAME);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TB_JF_J").val(Obj.fJF_J);
    $("#CB_BJ_TY").val(Obj.iBJ_TY);
    $("#CB_BJ_TY").prop("checked", Obj.iBJ_TY == "1" ? true : false);
    $("#S_MD").val(Obj.iMDID);
}
