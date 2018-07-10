vUrl = "../KFPT.ashx";
var rowNumer = 0;
function InitGrid() {
    vColumnNames = ['会员卡号', '姓名', '电话'];
    vColumnModel = [
            { name: 'sHYK_NO', width: 120, },
            { name: 'sHY_NAME', width: 120, },
            { name: 'sLXDH', width: 150 },
    ];
}

$(document).ready(function () {

    $("#TB_KFJLMC").click(function () {
        SelectKFJL("TB_KFJLMC", "HF_KFJL");
    });

    //$("#AddItem").click(function () {
    //    if ($("#TB_KFJLMC").val() == "") {
    //        art.dialog({ lock: true, content: '请选择客户经理' });
    //        return;
    //    }
    //    art.dialog.open("../../CrmArt/ListHYK/CrmArt_ListHYK.aspx?", { lock: true, title: '添加会员', width: 650, height: 520 }, false);
    //});
    $("#AddItem").click(function () {
        if ($("#TB_KFJLMC").val() == "") {
            ShowMessage("请选择客户经理", 3);
            return;
        }
        var DataArry = new Object();
        SelectHYK('list', DataArry, 'ListHYK', 'iHYID');

    });
    //$("#DelItem").click(function () {
    //    var selrowid = $("#list").getGridParam("selrow");
    //    if (selrowid == null) {
    //        ShowMessage("没有选中记录");
    //        return false;
    //    }
    //    $("#list").delRowData(selrowid);

    //});
    $("#DelItem").click(function () {
        DeleteRows("list");
    });
});

function IsValidData() {
    if ($("#TB_KFJLMC").val() == "") {
        art.dialog({ lock: true, content: '请选择客户经理' });
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iKFRYID = $("#HF_KFJL").val();
    Obj.sRYMC = $("#TB_KFJLMC").val();
    Obj.sBZ = $("#TB_BZ").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    //var lst = new Array();
    //for (i = 0; i < $("#list").getGridParam("reccount") ; i++) {
    //    var rowData = $("#list").jqGrid("getRowData")[i];
    //    lst.push(rowData);
    //}
    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.KFDYJLITEM = lst;
    Obj.KFDYJLITEM = lst;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_KFJLMC").val(Obj.sRYMC);
    $("#HF_KFJL").val(Obj.iKFRYID);
    $("#TB_BZ").val(Obj.sBZ);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    //$("#list").jqGrid("clearGridData");
    //for (i = 1; i <= Obj.KFDYJLITEM.length ; i++) {
    //    var rowid = "jqg" + i;
    //    $("#list").addRowData(rowid, Obj.KFDYJLITEM[i - 1]);
    //}
    $('#list').datagrid('loadData', Obj.KFDYJLITEM, "json");
    $('#list').datagrid("loaded");

}

function LoadData(rowData) {
    var rowNum1 = $("#list").getGridParam("reccount");
    var array = new Array();
    if (rowNum1 <= 0) {
        for (i = 0; i < rowData.length; i++) {
            $("#list").addRowData(rowNumer, {
                sHYK_NO: rowData[i].sHYK_NO,
                iHYID: rowData[i].iHYID,
                sHY_NAME: rowData[i].sHY_NAME,
                sLXDH: rowData[i].sSJHM,
            })
            rowNumer = rowNumer + 1;
        }
    }

    else {
        if (rowNumer == 0) {
            var rowIDs = $("#list").getDataIDs();
            rowNumer = rowIDs[rowIDs.length - 1] + 1;
        }
        for (j = 0; j < rowNumer; j++) {
            if ($("#list").getRowData(j) != "") {
                var ListRow = $("#list").getRowData(j);
                array[j] = ListRow.sHYK_NO;
            }
        }

        for (q = 0; q < rowData.length; q++) {
            if (checkRepeat(array, rowData[q].sHYK_NO)) {
                $("#list").addRowData(rowNumer, {
                    sHYK_NO: rowData[q].sHYK_NO,
                    iHYID: rowData[q].iHYID,
                    sHY_NAME: rowData[q].sHY_NAME,
                    sLXDH: rowData[q].sSJHM,
                })
                rowNumer = rowNumer + 1;
            }
        }

    }

}

function checkRepeat(array, lpDate) {
    var boolInsert = true;
    for (i = 0; i < array.length; i++) {
        if (array[i] == lpDate) {
            boolInsert = false;
        }
    }
    return boolInsert;
}


var uploader = "";
var uploadData = new Array();
var hykDialog = "";
$(function () {
    UploadInit();
});
function UploadInit() {
    //upload
    uploader = new plupload.Uploader({
        browse_button: 'ExcelImport',
        url: 'KFPT_KFJLDY.ashx?t=' + new Date(),
        filters: {
            mime_types: [
              { title: "Excel Files", extensions: "xlsx,xls" },
            ]
        },
        chunk_size: "10000kb",

    });

    //初始化
    uploader.init();

    var uploaddialog = "";
    //文件添加
    uploader.bind('FilesAdded', function (up, files) {
        plupload.each(files, function (file) {
            var result = /\.[^\.]+/.exec(file.name);
            if (result == ".xls")
                uploader.setOption("url", 'KFPT_KFJLDY.ashx?t=' + new Date() + "&mod=1");
            else if (result == ".xlsx")
                uploader.setOption("url", 'KFPT_KFJLDY.ashx?t=' + new Date() + "&mod=2");
        });
        uploader.start();
    });
    uploader.bind('Error', function (up, err) {
        document.getElementById('console').innerHTML += "\nError #" + err.code + ": " + err.message;
    });

    //单个文件整体上传完成
    uploader.bind('FileUploaded', function (upload, file, response) {
        $("#list").jqGrid("clearGridData");
        if (response.response.indexOf("错误") == 0) {
            if (hykDialog != "") {
                hykDialog.close();
            }
            uploader.stop();
            art.dialog({ content: response.response + "\r\n(5秒内后自动关闭...)", lock: true, time: 5 });
            return;
        }
        setGridData(response.response);
    });

}

var index = 0;
function setGridData(result) {
    if (result == "") {
        art.dialog({ content: "数据绑定失败,请重新上传", times: 2 });
        return;
    }
    var arr = new Array();
    arr = JSON.parse(result);
    arr = unique(arr);
    var length = $("#list").getGridParam("reccount");
    var a = art.dialog({ content: '上传完成，正在导入，请稍候......', lock: true });
    window.setTimeout(function () {
        for (var i = 0; i < arr.length; i++) {
            $("#list").addRowData(length, arr[i]);
            length = $("#list").getGridParam("reccount");
        }
        a.close();
    }, 1500);
}

function unique(arr) {
    var result = [], hash = {};
    for (var i = 0; i < arr.length; i++) {
        if (!hash[arr[i].sHYK_NO]) {
            result.push(arr[i]);
            hash[arr[i].sHYK_NO] = true;
        }
    }
    return result;
}

