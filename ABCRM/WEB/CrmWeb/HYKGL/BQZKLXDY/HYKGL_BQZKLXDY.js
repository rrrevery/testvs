vUrl = "../HYKGL.ashx";
var row = 0;
var hyid = 0;

function InitGrid() {
    vColumnNames = ["iLABELLBID", "标签值",  "iHYKTYPE",  "会员卡类型"];
    vColumnModel = [
          { name: "iLABEL_VALUEID", hidden: true, },
          { name: "sLABELMC", width: 150, hidden: true, },
          { name: "iHYKTYPE",  hidden: true, },
          { name: "sHYKNAME", width: 100, },
    ];
};

$(document).ready(function () {
    $("#B_Insert").text("修改");
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#B_Delete").hide();
    $("#B_Update").hide();
    RefreshButtonSep();
    FillBQLB("DDL_BQLB",1);





    $("#AddItem").click(function () {
        if ($("#DDL_BQLB").val() == "" || $("#DDL_BQLB").val() == null) {
            ShowMessage("请选择标签类别！", 3);
            return;
        }
        var DataArry = new Object();
        SelectKLXList('list', DataArry, 'iHYKTYPE', false);
    });


    $("#DelItem").click(function () {
        DeleteRows("list");
    });

});

function BQLBChange() {
    DoSearch($("#DDL_BQLB").val());
}





function IsValidData() {
    //if ($("#TB_HYKNO").val() == "") {
    //    art.dialog({ lock: true, content: "请输入卡号！" });
    //    ShowMessage("请输入卡号！", 3);
    //    return;
    //}

    return true;
}

function SetControlState() {
   

}

function SaveData() {
    var Obj = new Object();
    Obj.iLABELLBID = $("#DDL_BQLB").val();
    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;
    return Obj;
}

function ShowData(data) {

}






function DoSearch(iLABELLBID) {
    sjson = "{'iLABELLBID':" + iLABELLBID + "}";
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
    $("#DDL_BQLB").val(0);
}

//function InsertClickCustom() {
//    //$("#DDL_BQLB").val(1);
//    $("#DDL_BQLB").get(0).selectedIndex = 1;
//    FillBQXMTree("TreeBQXM", "TB_BQXMMC", "menuContentBQXM", $("#DDL_BQLB").val());
//    window.setTimeout(function ()
//    {
//        var zTree = $.fn.zTree.getZTreeObj("TreeBQXM");
//        var nodes = zTree.getNodes();
//        zTree.selectNode(nodes[0]);
//        $("#TB_BQXMMC").val(nodes[0].name);
//        $("#HF_BQXMID").val(nodes[0].bqxmid);
//        if (nodes[0].mjbj == 1 && nodes[0].id != "" && nodes[0].id != "0") {
//            document.getElementById("DDL_BQZ").options.length = 1;
//            FillBQZ($("#DDL_BQZ"), $("#HF_BQXMID").val());
//        }
//    }, 100);
   
  
//};






