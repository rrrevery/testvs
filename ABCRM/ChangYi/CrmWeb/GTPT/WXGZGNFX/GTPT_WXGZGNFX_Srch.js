vUrl = "../GTPT.ashx";


function InitGrid() {
    vColumnNames = ["菜单号", "菜单名称", "公共号名称", "点击率"];
    vColumnModel = [
            { name: 'sDM', width: 80, },
            { name: 'sNAME', width: 80, },
            { name: 'sPUBLICNAME', width: 80, },
            { name: 'iDJL', width: 80, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    //ZSel_MoreCondition_Load(v_ZSel_rownum);
});

function SaveData() {
    var Obj = new Object();
    var rowid = $("#list").jqGrid("getGridParam", "selrow");
    var rowData = $("#list").getRowData(rowid);
    Obj.iJLBH = rowData.iJLBH;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_RQ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_RQ2", "dRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
