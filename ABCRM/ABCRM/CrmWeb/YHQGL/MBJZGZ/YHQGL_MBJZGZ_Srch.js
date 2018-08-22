vUrl = "../YHQGL.ashx";
vCaption = "满百减折规则定义";

function InitGrid() {
    vColumnNames = ['规则编号', '规则名称', '停用标记', '发放限额', '起点金额'];
    vColumnModel = [
        { name: 'iJLBH', index: 'iMBJZGZID', width: 100 },
        { name: 'sMBJZGZMC' },
        { name: 'iBJ_TY', width: 100, formatter: "checkbox" },
        { name: 'fFFXE', width: 100 },
        { name: 'fQDJE', width: 100 },
    ];
};



function SetControlState() {
    ;
}

function SaveData() {
    var Obj = new Object();
    var rowid = $("#list").jqGrid("getGridParam", "selrow");
    var rowData = $("#list").getRowData(rowid);
    Obj.iJLBH = rowData.iJLBH;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}


$(document).ready(function () {
    //sCurrentPath = "CrmWeb/YHQGL/MBJZGZ/YHQGL_MBJZGZDY.aspx";
    $("#B_Exec").hide();
});




function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_GZDM", "iMBJZGZID", "=", false);
    MakeSrchCondition(arrayObj, "TB_GZMC", "sMBJZGZMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_JZXE", "fFFXE", "=", false);
    MakeSrchCondition(arrayObj, "TB_QDJE", "fQDJE", "=", false);
   // MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};