vUrl = "../HYKGL.ashx";
vCaption = "会员标签批量导入";

function InitGrid() {
    vColumnNames = ["身份证号", "手机号码", "姓名", "XH1", "标签1", "权重1", "月份1", "XH2", "标签2", "权重2", "月份2", "XH3", "标签3", "权重3", "月份3", "XH4", "标签4", "权重4", "月份4", "XH5", "标签5", "权重5", "月份5", ];
    vColumnModel = [
          { name: "sSFZBH", width: 200, },
          { name: "sSJHM", width: 100, },
          { name: "sHY_NAME", width: 100, },

          { name: "iXH1", hidden: true, },
          { name: "sBQ1", width: 150, },
          { name: "fQZ1", width: 100, },
          { name: "iMONTH1", width: 100, },

          { name: "iXH2", hidden: true, },
          { name: "sBQ2", width: 150, },
          { name: "fQZ2", width: 100, },
          { name: "iMONTH2", width: 100, },

          { name: "iXH3", hidden: true, },
          { name: "sBQ3", width: 150, },
          { name: "fQZ3", width: 100, },
          { name: "iMONTH3", width: 100, },

          { name: "iXH4", hidden: true, },
          { name: "sBQ4", width: 150, },
          { name: "fQZ4", width: 100, },
          { name: "iMONTH4", width: 100, },

          { name: "iXH5", hidden: true, },
          { name: "sBQ5", width: 150, },
          { name: "fQZ5", width: 100, },
          { name: "iMONTH5", width: 100, },
    ];
}

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#B_Delete").hide();
    $("#B_Update").hide();

    $("#B_Insert").click(function () {
        // $("#list").clearGridData();
        $("#list").datagrid("loadData", { total: 0, rows: [] });
        PageDate_Clear();
        vProcStatus = cPS_ADD;
        SetControlBaseState();
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });
    UploadInit();
    RefreshButtonSep();
});



function IsValidData() {

    //var idList = $("#list").getDataIDs();
    //if (idList.length <= 0) {
    //    art.dialog({ lock: true, content: "请添加标签！" });
    //    return false;
    //}
    return true;
}


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = 0;
    var lst = new Array();
    // var rowIDs = $("#list").getDataIDs();
    var rowIDs = $("#list").datagrid("getRows");
    for (var i = 0; i < rowIDs.length; i++) {
        var rowData = rowIDs[i];
        if (rowData.iXH1 == "") {
            rowData.iXH1 = 0;
            rowData.iMONTH1 = 0;
            rowData.fQZ1 = 0;
        }
        if (rowData.iXH2 == "") {
            rowData.iXH2 = 0;
            rowData.iMONTH2 = 0;
            rowData.fQZ2 = 0;
        }
        if (rowData.iXH3 == "") {
            rowData.iXH3 = 0;
            rowData.iMONTH3 = 0;
            rowData.fQZ3 = 0;
        }
        if (rowData.iXH4 == "") {
            rowData.iXH4 = 0;
            rowData.iMONTH4 = 0;
            rowData.fQZ4 = 0;
        }
        if (rowData.iXH5 == "") {
            rowData.iXH5 = 0;
            rowData.iMONTH5 = 0;
            rowData.fQZ5 = 0;
        }
        lst.push(rowData);
    }
    Obj.itemTable = lst;
    return Obj;
}

function ShowData(data) {

}


function setUploadParam() {
    var colModels = "";
    //var cols = $("#list").jqGrid("getGridParam", "colModel");
    //for (i = 1; i < cols.length; i++) {
    //    if (cols[i].name != "cb") {
    //        colModels += cols[i].name + "|";
    //    }
    //}
    var cols = $("#list").data('datagrid').options.columns[0];
    for (var i = 0; i < cols.length; i++) {
        if (cols[i].field!="cb") {
            colModels += cols[i].field + "|";
        }
    }
    uploader = new plupload.Uploader({
        browse_button: 'B_Import',
        url: '../../CrmLib/CrmLib_BaseImport.ashx?cols=' + colModels,
        filters: {
            mime_types: [
              { title: "Excel Files", extensions: "xlsx,xls" },
            ]
        },
        chunk_size: "200kb",
    });
    //初始化
    uploader.init();
}

function setGridData(result) {

    if (result == "") {
        art.dialog({ content: "数据绑定失败,请重新上传", times: 2 });
        return;
    }
    else {
        var log = art.dialog({ content: "正在上传，请稍后" });    
    }
    var arr = new Array();
    arr = JSON.parse(result);
    $("#list").jqGrid("clearGridData");
    var data = {
        "page": "1",
        "records": arr.length,
        "rows": arr
    };

    $("#list")[0].addJSONData(data);
    //for (var i = 0; i < arr.length; i++) {
    //    if (arr[i] != null && arr[i] != "") {
    //        //if (CheckReapet(arr[i])) {
    //            //var id = $("#list").getGridParam("reccount")
    //            $("#list").addRowData(i, arr[i]);
    //        //}
    //    }
    //}
    log.close();
}

function CheckReapet(arr) {
    var boolInsert = true;
    var rowIDs = $("#list").datagrid("getRows");//$("#list").getDataIDs();
    for (var i = 0; i < rowIDs.length; i++) {
        var rowData = rowIDs[i];//$("#list").getRowData(rowIDs[i]);
        if (rowData.sSFZBH == arr.sSFZBH && rowData.sSJHM == arr.sSJHM && rowData.iXH1 == arr.iXH1 && rowData.iXH2 == arr.iXH2 && rowData.iXH3 == arr.iXH3 && rowData.iXH4 == arr.iXH4 && rowData.iXH5 == arr.iXH5) {
            boolInsert = false;
        }

    }
    return boolInsert;
}





