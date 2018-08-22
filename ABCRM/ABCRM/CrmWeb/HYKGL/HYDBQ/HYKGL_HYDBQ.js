vUrl = "../HYKGL.ashx";
var hykno = GetUrlParam("HYKNO");
var row = 0;
var hyid = 0;

function InitGrid() {
    vColumnNames = ["iLABELXMID", "标签项目", "iLABEL_VALUEID", "标签值", "标签ID", "有效期", "iBJ_WY", "唯一标记"];
    vColumnModel = [
            { name: "iLABELXMID", hidden: true, },
          { name: "sLABELXMMC", width: 150, },
          { name: "iLABEL_VALUEID", hidden: true, },
          { name: "sLABEL_VALUE", width: 150, },
          { name: "iLABELID", hidden: true },
          { name: "dYXQ", width: 100, },
          { name: "iBJ_WY", hidden: true, },
          { name: "sBJ_WY", width: 80, },
    ];
};

$(document).ready(function () {
    $("#B_Insert").text("修改");
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#B_Delete").hide();
    $("#B_Update").hide();
    RefreshButtonSep();
    FillBQLB("DDL_BQLB");

    if (hykno != "") {
        $("#TB_HYKNO").val(hykno);
        GetHYXX();
        DoSearch($("#HF_HYID").val());
    }

    $("#TB_HYKNO").change(function () {
        $("#HF_HYID").val("");
        GetHYXX();
        DoSearch($("#HF_HYID").val());
        ClearData();
    });
    $("#TB_HYKNO").bind('keypress', function (event) {
        if (event.keyCode == "13") {
            $("#HF_HYID").val("");
            GetHYXX();
            DoSearch($("#HF_HYID").val());
            ClearData();
        }
    });


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
        if (checkRepeat($("#HF_BQXMID").val(), $("#DDL_BQZ").val()) == false) {
            var myData = {
                iLABELXMID: $("#HF_BQXMID").val(),
                sLABELXMMC: $("#TB_BQXMMC").val(),
                iLABEL_VALUEID: $("#DDL_BQZ").val(),
                sLABEL_VALUE: $("#DDL_BQZ").find("option:selected").text(),
                iLABELID: $("#DDL_BQZ").find("option:selected").attr("labelid"),
                dYXQ: $("#TB_YXQ").val(),
                iBJ_WY: parseInt($("#CB_WY")[0].checked ? "1" : "0"),
                sBJ_WY: $("#CB_WY")[0].checked ? "是" : "否",
            };
            $("#list").datagrid("appendRow", myData);
        }
    });


    $("#DelItem").click(function () {
        DeleteRows("list");
    });

});

function BQLBChange() {
    $.fn.zTree.destroy("TreeBQXM");
    $("#TB_BQXMMC").val("");
    $("#HF_BQXMID").val("");
    document.getElementById("DDL_BQZ").options.length = 1;
    if ($("#DDL_BQLB").val() != "" && $("#DDL_BQLB").val() != null) {
        FillBQXMTree("TreeBQXM", "TB_BQXMMC", $("#DDL_BQLB").val());
    }
}

function TreeNodeClickCustom(e, treeId, treeNode) {
    $("#HF_BQXMID").val(treeNode.iLABELXMID);
    $("#CB_WY").prop("checked", treeNode.iBJ_WY == "1" ? true : false);
    var zTree = $.fn.zTree.getZTreeObj("TreeBQXM");
    nodes = zTree.getSelectedNodes();
    if (nodes[0].iSTATUS == 1 && nodes[0].id != "" && nodes[0].id != "0") {
        document.getElementById("DDL_BQZ").options.length = 1;
        FillBQZ($("#DDL_BQZ"), $("#HF_BQXMID").val());
    }
}

function IsValidData() {
    if ($("#TB_HYKNO").val() == "") {
        art.dialog({ lock: true, content: "请输入卡号！" });
        ShowMessage("请输入卡号！", 3);
        return;
    }

    return true;
}

function SetControlState() {
    document.getElementById("CB_WY").disabled = true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iHYID = $("#HF_HYID").val();
    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;
    return Obj;
}

function ShowData(data) {

}


function GetHYXX() {
    if ($("#TB_HYKNO").val() != "") {
        var str = GetHYXXData(0, $("#TB_HYKNO").val());
        if (str == "null" || str == "") {
            ShowMessage("没有找到卡号", 3);
            $("#TB_HYKNO").val("");
            return;
        }

        var Obj = JSON.parse(str);
        $("#HF_HYID").val(Obj.iHYID);
        hyid = Obj.iHYID;
        if (vProcStatus != cPS_ADD)
            document.getElementById("B_Insert").disabled = false;
    }
}


function checkRepeat(iLABELXMID, iLABEL_VALUEID) {
    var rows = $("#list").datagrid("getRows");
    for (var i = 0; i < rows.length; i++) {
        var rowData = rows[i];
        if (iLABELXMID == rowData.iLABELXMID) {
            if (rowData.iBJ_WY == 1) {
                ShowMessage("该标签项目值只能选一个", 3);
                return true;
            }
            else {
                if (iLABEL_VALUEID == rowData.iLABEL_VALUEID) {
                    ShowMessage("不能重复添加!", 3);
                    return true;
                }
            }
        }
    }
    return false;
}

function DoSearch(HYID) {
    sjson = "{'iHYID':" + HYID + "}";
    $('#list').datagrid("loading");
    $.ajax({
        type: "post",
        url: vUrl + "?mode=Search&func=" + vPageMsgID,
        async: true,
        data: {
            json: sjson,
            titles: 'cybillsearch',
            page: $('#list').datagrid("options").pageNumber,
            rows: $('#list').datagrid("options").pageSize,
        },
        success: function (data) {
            if (data.indexOf('错误') >= 0 || data.indexOf('error') >= 0) {
                ShowMessage(data);
            }
            $('#list').datagrid('loadData', JSON.parse(data), "json");
            $('#list').datagrid("loaded");
        },
        error: function (data) {
            ShowMessage(data);
        }
    });
};

function ClearData() {
    document.getElementById("DDL_BQZ").options.length = 1;
    $("#TB_BQXMMC").val("");
    $("#HF_BQXMID").val("");
    $("#DDL_BQLB").val(0);
    $("#TB_YXQ").val("");
    $("#CB_WY").attr("checked", false);
}







