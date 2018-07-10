vUrl = "../CRMGL.ashx";
vCaption = "系统参数定义";

function InitGrid() {
    vColumnNames = ['参数', '类型', '最小值', '最大值', '缺省值', '当前值', 'iJLBH', 'szy', 'BoolSelect'];
    vColumnModel = [
            { name: 'sLX', width: 180 },
			{ name: 'sDATATYPESTR', width: 80 },
            { name: 'iMIN_VAL', width: 100 },
            { name: 'iMAX_VAL', width: 100 },
            { name: 'iDEF_VAL', width: 100 },
            { name: 'iCUR_VAL', cellEdit: true },
            { name: 'iJLBH', hidden: true },
            { name: 'sYY', hidden: true },
            { name: 'iBoolSelect', hidden: true },
    ];
}

$(document).ready(function () {

    vProcStatus = cPS_BROWSE;
    SetControlBaseState();

    $("#DDL_CSLX").change(function () {
        PageDate_Clear();
        SearchData();
    });
    RefreshButtonSep();
});


function SetControlState() {
    $("#jlbh").hide();
    $("#B_Insert").hide();
    $("#B_Delete").hide();
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
    if ($("#TB_CURVAL").val == "") {
        Obj.iCUR_VAL = $("#TB_DEFVAL").val();
    }
    else {
        Obj.iCUR_VAL = $("#TB_CURVAL").val();
    }
    return Obj;

}

function ShowData(rowData) {
    $("#LB_CSMC").text(rowData.sLX);
    $("#TB_SJLX").val(rowData.sDATATYPESTR);
    $("#TB_MINVAL").val(rowData.iMIN_VAL);
    $("#TB_MAXVAL").val(rowData.iMAX_VAL);
    $("#TB_DEFVAL").val(rowData.iDEF_VAL);
    $("#TB_CURVAL").val(rowData.iCUR_VAL);
    $("#TB_JLBH").val(rowData.iJLBH);
    $("#LB_ZY").text(rowData.sYY);
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "DDL_CSLX", "iSYSID", "=", false);
    return arrayObj;
}

//function UpdateClickCustom() {
//    document.getElementById('DDL_CSLX').disabled = true;
//    document.getElementById('DDL_CSLX').style.background = "#EEE0C8 ";
//}
//function CancelClickCustom() {
//    document.getElementById('DDL_CSLX').disabled = false;
//    document.getElementById('DDL_CSLX').style.background = "white";
//}