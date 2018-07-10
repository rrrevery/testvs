var uploader = "";
var uploadData = new Array();
var ShowDialog = "";

function UploadInit(listName, importBtn) {
    listName = listName || "list";
    importBtn = importBtn || "B_Import";
    //upload  参数设置
    setUploadParam(listName, importBtn);
    //文件添加
    uploader.bind('FilesAdded', function (up, files) {
        var html = '';
        plupload.each(files, function (file) {
            html += '<li id="' + file.id + '">' + file.name + ' (' + plupload.formatSize(file.size) + ') <b></b></li>';
        });
        //显示文件列表
        //document.getElementById('filelist').innerHTML += html;
        //开始上传
        uploader.start();
        ShowDialog = art.dialog({ content: '正在上传,请稍候', lock: true, time: 2 });
    });
    //上传进度显示
    uploader.bind('UploadProgress', function (up, file) {
        //document.getElementById(file.id).getElementsByTagName('b')[0].innerHTML = '<span>' + file.percent + "%</span>";
    });
    //错误处理
    uploader.bind('Error', function (up, err) {
        //显示错误信息
        // document.getElementById('console').innerHTML += "\nError #" + err.code + ": " + err.message;
    });

    //正式上传
    //document.getElementById('BTN_UPLOAD').onclick = function () {
    //    //uploader.refresh();
    //    uploader.start();
    //    hykDialog = art.dialog({ content: '正在上传,请稍候', lock: true });
    //    //uploaddialog = art.dialog({ content: '正在上传', lock: true });
    //};

    //分块数据上传完成
    uploader.bind('ChunkUploaded', function (upload, file, response) {

    });

    //单个文件整体上传完成
    uploader.bind('FileUploaded', function (upload, file, response) {
        if (response.response.indexOf("错误") == 0) {
            if (ShowDialog != "") {
                ShowDialog.close();
            }
            uploader.stop();
            art.dialog({ content: response.response + "\r\n(5秒内后自动关闭...)", lock: true, time: 5 });
            return;
        }
        var arr = new Array();
        try {
            arr = eval(response.response);  //JSON.parse(response.response);
        }
        catch (e) {
            art.dialog({ content: "导入文档格式错误", time: 2, lock: true });
            return;
        }
        // arr = eval('(' + response.response + ')');
        uploadData.length = 0;
        var j = uploadData.length;
        for (var i = 0; i < arr.length; i++) {
            uploadData[j] = arr[i];
            j++;
        }

        //这样设置会导致，每上传一个文件就重新声明一个upload  
        //分块上传文件，每上传完一个文件，就重新设置一个upload(为的是重新设置后台存储文件名)，可是这样现在如果是多个文件上传的话，就会发生队列中断的事情。
        //改用在后台更改文件名（采用静态变量实现）
        //uploader.destroy();
        //UploadInit((new Date()).valueOf() + (Math.random() * 10000));

    });

    //队列中的所有文件上传结束
    uploader.bind('UploadComplete', function (upload, file, response) {
        setGridData(JSON.stringify(uploadData), listName);
        if (ShowDialog != "") {
            ShowDialog.close();
        }
    });
}


//  设置导入格式模板  colModels 中的列在excel 中必须存在 
function setUploadParam(listName, importBtn) {
    var colModels = "";
    //JqGrid 获取导入列方法
    //var cols = $("#list").jqGrid("getGridParam", "colModel");
    //for (i = 1; i < cols.length; i++) {
    //    if (cols[i].name != "cb") {
    //        colModels += cols[i].name + "|";
    //    }
    //}
    //EayUI  获取导入列方法
    var cols = vColumnModel;
    for (var i = 0; i < cols.length; i++) {
        if (cols[i].name != "cb") {
            colModels += cols[i].name + "|";
        }
    }
    uploader = new plupload.Uploader({
        browse_button: importBtn,
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

function setGridData(result, listName) {
    if (result == "") {
        art.dialog({ content: "数据绑定失败,请重新上传", times: 2 });
        return;
    }
    else {
        var log = art.dialog({ content: "正在上传，请稍后" });
    }
    //EeayUI 设置数据  格式内容如下
    $('#list').datagrid('loadData', JSON.parse(result), "json");
    $('#list').datagrid("loaded");
    //Jqgrid 设置数据  格式内容如下
    //var arr = new Array();
    //arr = JSON.parse(result);
    //$("#list").jqGrid("clearGridData");
    //var data = {
    //    "page": "1",
    //    "records": arr.length,
    //    "rows": arr
    //};
    //$("#list")[0].addJSONData(data);
    log.close();
}