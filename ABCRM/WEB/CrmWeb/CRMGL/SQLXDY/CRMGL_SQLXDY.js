vUrl = "../CRMGL.ashx";
vCaption = "商圈类型定义";
$(document).ready(function () {

});

function SetControlState() {
    $("#B_Exec").hide();
    $("#status-bar").hide();

}

function IsValidData() {

    return true;
}



function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    //主表数据
    Obj.sLXMC = $("#TB_LXMC").val();
    Obj.sBZ = $("#TB_BZ").val();
    return Obj;
}
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_LXMC").val(Obj.sLXMC);
    $("#TB_BZ").val(Obj.sBZ);
}

//jQgird初始化
function InitGrid(tablename, colnames, colmodels, pager) {
    jQuery("#" + tablename).jqGrid({
        async: false,
        datatype: "local",
        colNames: colnames,
        colModel: colmodels,
        cellsubmit: "clientArray",
        rownumbers: true,
        width: 800,
        pager: "#" + pager,
        viewrecords: true,
        multiselect: true,
        afterSaveCell: function (rowid, name, val, iRow, iCol) {
        }
    });
}









