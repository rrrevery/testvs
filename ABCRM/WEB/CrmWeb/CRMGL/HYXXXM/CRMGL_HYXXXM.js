vUrl = "../CRMGL.ashx";
vCaption = "会员信息项目定义";

function InitGrid() {
    vColumnNames = ['显示顺序', 'XMID', 'XMLX', '内容', ];
    vColumnModel = [
            { name: "iSXH", width: 20, editable: true, },
            { name: "iXMID", hidden: true, },
            { name: "iXMLX", hidden: true, },
            { name: "sNR", editable: true, },
    ];
}

$(document).ready(function () {

    //vProcStatus = cPS_BROWSE;
    //SetControlBaseState();
    $("#DDL_CSLX").change(function () {
        PageDate_Clear();
        SearchData();
    });
})

function SetControlState() {
    $("#jlbh").hide();
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
    Obj.iXMLX = $("#DDL_CSLX").val();
    Obj.sNR = $("#TB_CONTENT").val();
    Obj.iSXH = $("#TB_INDEX").val();
    return Obj;
}

function ShowData(data) {
    $("#TB_JLBH").val(data.iJLBH);
    $("#TB_CONTENT").val(data.sNR);
    $("#TB_INDEX").val(data.iSXH);
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "DDL_CSLX", "iXMLX", "=", false);
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