vUrl = "../YHQGL.ashx";
vCaption = "来店赠券活动定义";
function InitGrid() {
    vColumnNames = ['记录编号', '活动主题', '活动内容', ];
    vColumnModel = [
        { name: 'iJLBH', },//sortable默认为true width默认150
	    { name: 'sCXZT', },
        { name: 'sCXNR', width: 300, },
    ];
}

function ShowData(data) {
    $("#TB_JLBH").val(rowData.iJLBH);
    $("#TB_CXZT").val(rowData.sCXZT);
    $("#TB_CXNR").val(rowData.sCXNR);
}

$(document).ready(function () {
    //
});


function SetControlState() {
    $("#B_Insert").show();
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
    Obj.sCXZT = $("#TB_CXZT").val();
    Obj.sCXNR = $("#TB_CXNR").val();
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_CXZT").val(Obj.sCXZT);
    $("#TB_CXNR").val(Obj.sCXNR);
}