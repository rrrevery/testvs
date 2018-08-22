vUrl = "../CRMGL.ashx";
var Action = GetUrlParam("action");

function InitGrid() {
    vColumnNames = ['门店ID', "门店名称"];
    vColumnModel = [
          { name: "iMDID", width: 100, },
          { name: "sMDMC", width: 100, },
    ];
};




$(document).ready(function () {

    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#AddItem").click(function () {
        SelectMD("", "", "", false);

    });
    $("#DelItem").click(function () {
        DeleteRows("list");
    });
});



function SetControlState() {
    ;
}

function IsValidData() {
    if ($("#TB_QYMC").val() == "") {
        art.dialog({ content: '名称不能为空!' });
        return false;

    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sQYMC = $("#TB_QYMC").val();
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_QYMC").val(Obj.sQYMC);
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
}

function MoseDialogCustomerReturn(dialogName, lst) {
    if (dialogName == "ListMD") {
        $('#list').datagrid('loadData', lst, "json");
        $('#list').datagrid("loaded");
    }
}