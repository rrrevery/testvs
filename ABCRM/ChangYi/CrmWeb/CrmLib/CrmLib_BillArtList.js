var vUrl = "";
var vPageMsgID = "1";
var vCaption = "";
var vSingleSelect = false;
var vAutoShow = true;
var vDialogName = "";
var vIdField = "";
var lstAlready = [];
var iWXPID = 1;
$(document).ready(function () {
    if ($.dialog.data("dialogInput")) {
        lstAlready = JSON.parse($.dialog.data("dialogInput"));
    }
    if ($.dialog.data("SingleSelect") != undefined)
        vSingleSelect = $.dialog.data("SingleSelect");
    if ($.dialog.data("autoShow") != undefined)
        vAutoShow = $.dialog.data("autoShow");

    var height = 550 - ($(".bfrow").length + 1) * 45
    AddToolButtons("查询", "B_Search");
    AddToolButtons("确定", "B_Save");
    AddToolButtons("取消", "B_Cancel");
    document.getElementById("B_Save").onclick = ArtSaveClick;
    document.getElementById("B_Cancel").onclick = ArtCancelClick;
    document.getElementById("B_Search").onclick = ArtSearchClick;
    $(".btnsep:visible:last").hide();
    InitGrid();
    $("#list").datagrid({
        width: '100%',
        height: height,//450,
        autoRowHeight: false,
        striped: true,
        columns: [vColumns],
        sortName: vColumns[0].field,
        sortOrder: 'desc',
        singleSelect: vSingleSelect,
        showHeader: true,
        showFooter: true,
        rownumbers: true, //添加一列显示行号
        checkOnSelect: false,
        selectOnCheck: false,
        pagePosition: 'bottom',
        rownumbers: true, //添加一列显示行号
        pagination: true,  //启用分页
        showFooter: false,
        fitColumns: true,
        pageNumber: 1,
        pageSize: 10,
        idField: vIdField,
        onClickRow: onClickRow,
        onUnselect: onUnselect,
        onLoadSuccess: function () {
            ShowAlreadySelected()
            $(this).datagrid('enableDnd');
        }
    });
    var pager = $('#list').datagrid("getPager");
    pager.pagination({
        onSelectPage: function (pageNum, pageSize) {
            SearchData(pageNum, pageSize);
            ShowAlreadySelected()
        },
    });
    $("#bftitle").html(vCaption);
    if (vAutoShow)
        SearchData();
});

function onClickRow(index) {
    //T--点击行事件
};

function onUnselect(rowIndex, rowData) {
    if (lstAlready.length > 0) {
        for (var i = 0; i < lstAlready.length; i++) {
            if (lstAlready[i][vIdField] == rowData[vIdField]) {
                lstAlready.splice(i, 1);
            }
        }
    }
}

function ArtSaveClick() {
    //T--确认事件
    var Rows = $("#list").datagrid("getSelections");


    $.dialog.data(vDialogName, JSON.stringify(Rows));
    $.dialog.data('dialogSelected', Rows.length > 0);
    $.dialog.close();

};

function ArtCancelClick() {
    $.dialog.data('dialogSelected', false);
    $.dialog.close();
};

function ArtSearchClick() {
    SearchData();
};

function SearchData(page, rows, sort, order) {
    var obj = MakeSearchJSON();
    //page页码,rows每页行数,sort排序字段,order排序方式
    page = page || $('#list').datagrid("options").pageNumber;
    rows = rows || $('#list').datagrid("options").pageSize;
    sort = sort || $('#list').datagrid("options").sortName;
    order = order || $('#list').datagrid("options").sortOrder;
    $('#list').datagrid("loading");
    $.ajax({
        type: "post",
        url: vUrl + "?mode=Search&func=" + vPageMsgID,
        async: true,
        data: {
            json: JSON.stringify(obj),
            titles: 'cybillsearch',
            page: page,
            rows: rows,
            sort: sort,
            order: order,
        },
        success: function (data) {
            if (data.indexOf('错误') >= 0 || data.indexOf('error') >= 0) {
                ShowMessage(data);
            }
            $('#list').datagrid('loadData', JSON.parse(data), "json");
            $('#list').datagrid("loaded");
            vSearchData = data;
        },
        error: function (data) {
            ShowMessage(data);
        }
    });

}

function MakeSearchJSON() {
    var cond = MakeSearchCondition();
    if (cond == null)
        return;
    var Obj = new Object();
    Obj.SearchConditions = cond;
    var colModels = "", colNames = "", colWidths = "";
    var cols = $("#list").data('datagrid').options.columns[0];
    for (var i = 0; i < cols.length; i++) {
        if (!cols[i].hidden) {
            colModels += cols[i].field + "|";
            colNames += cols[i].title + "|";
            colWidths += cols[i].width + "|";
        }
    }
    Obj.sColNames = colNames;
    Obj.sColModels = colModels;
    Obj.sColWidths = colWidths;
    Obj.sSortFiled = $("#list").data('datagrid').options.sortName;// cols[0].field;
    Obj.sSortType = $("#list").data('datagrid').options.sortOrder;
    Obj.iLoginRYID = iDJR;
    Obj.iLoginPUBLICID = iWXPID;
    AddCustomerCondition(Obj);

    return Obj;
}

function MakeSearchCondition() {
    ;
}
function AddCustomerCondition(Obj) {
    ;
}

function ShowAlreadySelected() {
    if (lstAlready.length > 0) {
        for (var i = 0; i < lstAlready.length; i++) {
            $("#list").datagrid("selectRecord", lstAlready[i][vIdField]);
        }
    }
}