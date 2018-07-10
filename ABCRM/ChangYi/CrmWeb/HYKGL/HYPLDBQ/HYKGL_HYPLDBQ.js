vUrl = "../HYKGL.ashx";

function InitGrid() {
    vColumnNames = ["HYID", "会员卡号", "姓名"];
    vColumnModel = [
            { name: "iHYID", hidden: true, },
            { name: "sHYK_NO", width: 150, },
            { name: "sHY_NAME", width: 100, },
    ];
}

$(document).ready(function () {
    FillBQLB("DDL_BQLB");
    //RefreshButtonSep();

    $("#AddItem").click(function () {
        if ($("#DDL_BQLB").val() == "" || $("#DDL_BQLB").val() == null) {
            ShowMessage("请选择标签类别！", 3);
            return;
        }
        if ($("#TB_BQXMMC").val() == "") {
            ShowMessage("请选择标签项目！", 3);
            return;
        }
        if ($("#DDL_BQZ").val() == "" || $("#DDL_BQZ").val() == null) {
            ShowMessage("请选择标签值！", 3);
            return;
        }
        //art.dialog.open("../../CrmLib/SelectHYXX/SelectHYKXX.aspx?", { lock: true, title: '添加会员', width: 650, height: 520 }, false);
        var DataArry = new Object();
        DataArry["vZT"] = 2;
        SelectWXHYK('list', DataArry, 'ListHYK', 'iHYID');
    });


    $("#DelItem").click(function () {
        DeleteRows("list");
    });
});

function BQLBChange() {
    if ($("#DDL_BQLB").val() != "" && $("#DDL_BQLB").val() != null) {
        $.fn.zTree.destroy("TreeBQXM");
        $("#TB_BQXMMC").val("");
        $("#HF_BQXMID").val("");
        document.getElementById("DDL_BQZ").options.length = 1;
        FillBQXMTree("TreeBQXM", "TB_BQXMMC", $("#DDL_BQLB").val());
    }
    else {
        $.fn.zTree.destroy("TreeBQXM");
        $("#TB_BQXMMC").val("");
        $("#HF_BQXMID").val("");
        document.getElementById("DDL_BQZ").options.length = 1;
    }
}

function TreeNodeClickCustom(e, treeId, treeNode) {
    $("#HF_BQXMID").val(treeNode.iLABELXMID);
    var zTree = $.fn.zTree.getZTreeObj("TreeBQXM");
    nodes = zTree.getSelectedNodes();
    if (nodes[0].iSTATUS == 1 && nodes[0].id != "" && nodes[0].id != "0") {
        document.getElementById("DDL_BQZ").options.length = 1;
        FillBQZ($("#DDL_BQZ"), $("#HF_BQXMID").val());
    }
}
function SetControlState() {
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#B_Delete").hide();
    $("#B_Update").hide();
}

function IsValidData() {
    return true;
}



function SaveData() {
    var Obj = new Object();
    Obj.iLABELXMID = $("#HF_BQXMID").val();
    Obj.sLABELXMMC = $("#TB_BQXMMC").val();
    Obj.iLABEL_VALUEID = $("#DDL_BQZ").val();
    Obj.sLABEL_VALUE = $("#DDL_BQZ").find("option:selected").text();
    Obj.iLABELID = $("#DDL_BQZ").find("option:selected").attr("labelid")
    Obj.dYXQ = $("#TB_YXQ").val();
    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;
    return Obj;
}
function ShowData(data) {

}








