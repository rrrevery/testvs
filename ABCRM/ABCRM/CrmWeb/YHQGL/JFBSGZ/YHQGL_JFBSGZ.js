vUrl = "../YHQGL.ashx";

function InitGrid() {
    vColumnNames = ["销售金额", '倍数', ];
    vColumnModel = [
        { name: "fXSJE", width: 100, editable: true, editor: 'text', editrules: { required: true, number: true, minValue: 1 } },
        { name: "fBS", width: 100, editable: true, editor: 'text', editrules: { required: true, number: true, minValue: 1 } }
    ];
};



$(document).ready(function () {
    $("#AddItem").click(function () {
        $("#list").datagrid("appendRow", {});
    });
    $("#DelItem").click(function () {
        DeleteRows("list");
    });
});

function SetControlState() {
    $("#status-bar").hide();
    $("#B_Exec").hide();
}

function IsValidData() {
    if ($.trim($("#TB_GZMC").val()) == "") {
        art.dialog({ lock: true, time: 2, content: '请输入规则名称!' });
        return false;
    }
    return true;
}



function SaveData() {
    var Obj = new Object();
    //主表数据
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";

    Obj.sGZMC = $("#TB_GZMC").val();
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;

    return Obj;
}
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH == "0" ? "" : Obj.iJLBH);
    $("#TB_GZMC").val(Obj.sGZMC);
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
}