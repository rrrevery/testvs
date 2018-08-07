var vColumns;
var allColumns;
var listColumns;
var frozenColumns;
var vRowIndex;

function InitGrid() {
    vColumns = [
        { field: 'title', title: '列名', width: 100 },
        { field: 'field', title: '列名', hidden: true },
        { field: 'width', title: '列名', hidden: true },
        {
            field: 'hidden', title: '隐藏', width: 110,
            formatter: function (value, row, index) {
                if (value == true) {
                    return "<input type='checkbox' checked='true' id='" + row.field + "_hidden'/>"
                }
                else {
                    return "<input type='checkbox'  id='" + row.field + "_hidden'/>"

                }
            }
        },
        {
            field: 'frozen', title: '冻结', width: 110,
            formatter: function (value, row, index) {
                if (value == true) {
                    return "<input type='checkbox' checked='true' id='" + row.field + "_frozen'/>"
                }
                else {
                    return "<input type='checkbox'  id='" + row.field + "_frozen'/>"
                }
            }
        },
        {
            field: 'up', title: '上移', width: 60,
            formatter: function (value, row, index) {
                return "<i class='fa fa-arrow-up'></i>"
            }
        },
        {
            field: 'down', title: '下移', width: 60,
            formatter: function (value, row, index) {
                return "<i class='fa fa-arrow-down'></i>"
            }
        },
        { field: 'sortable', hidden: true },
        { field: 'align', hidden: true },
    ];
}

$(document).ready(function () {
    AddToolButtons("保存", "B_Save");
    AddToolButtons("取消", "B_Cancel");
    document.getElementById("B_Save").onclick = ArtSaveClick;
    document.getElementById("B_Cancel").onclick = ArtCancelClick;
    InitGrid();
    $("#list").datagrid({
        width: '100%',
        height: 458,
        autoRowHeight: false,
        striped: true,
        columns: [vColumns],
        //sortName: vColumns[0].field,
        singleSelect: true,
        showHeader: true,
        showFooter: true,
        rownumbers: true, //添加一列显示行号
        checkOnSelect: false,
        selectOnCheck: false,
        //pageNumber: 1,
        //pageSize: 10,
        onClickRow: onClickRow,
        onClickCell: onClickCell,
        onLoadSuccess: function () {
            $(this).datagrid('enableDnd');
        }
    });
    allColumns = $.dialog.data('allColumns');
    listColumns = $.dialog.data('Columns');
    frozenColumns = $.dialog.data('frozenColumns');
    $('#list').datagrid('loadData', allColumns);
    $('#list').datagrid("loaded");
});

function onClickRow(index) {
    // $(this).datagrid('unselectRow', index);
}

function onClickCell(rowIndex, field, value) {
    vRowIndex = rowIndex;
    switch (field) {
        case "up":
            MoveUp();
            break;
        case "down":
            MoveDown();
            break;
        case "hidden":
            SetValue("hidden");
            break;
        case "frozen":
            SetValue("frozen");
            break;
    }
}


function ArtSaveClick() {
    allColumns = new Array();
    frozenColumns = new Array();
    listColumns = new Array();
    var colsAttrData = $('#list').datagrid("getData").rows;
    for (var i = 0; i < colsAttrData.length; i++) {
        var colObj = new Object();
        colObj.field = colsAttrData[i].field;
        colObj.title = colsAttrData[i].title;
        colObj.hidden = $("#" + colObj.field + "_hidden")[0].checked;
        colObj.width = colsAttrData[i].width;
        colObj.frozen = $("#" + colObj.field + "_frozen")[0].checked;
        if ($("#" + colObj.field + "_frozen")[0].checked)
            frozenColumns.push(colObj);
        else
            listColumns.push(colObj);
        colObj.sortable = colsAttrData[i].sortable;
        colObj.align = colsAttrData[i].align;
        allColumns.push(colObj);
    }
    $.dialog.data('allColumns', allColumns);
    $.dialog.data('Columns', listColumns);
    $.dialog.data('frozenColumns', frozenColumns);
    $.dialog.data('dialogSelected', true);
    $.dialog.close();
}

function ArtCancelClick() {
    $.dialog.close();
}
function SetValue(fieldName) {
    var row = $('#list').datagrid('getData').rows[vRowIndex];
    var tp_value = $("#" + row.field + "_" + fieldName + "")[0].checked
    if (fieldName == "hidden") {
        row.hidden = tp_value;
    }
    else {
        row.frozen = tp_value;
    }
    $('#list').datagrid('updateRow', {
        index: vRowIndex,
        row: row
    });
    $('#list').datagrid('refreshRow', vRowIndex);

}

function MoveUp() {
    var row = $("#list").datagrid('getSelected');
    var index = $("#list").datagrid('getRowIndex', row);
    mysort(vRowIndex, 'up', 'list');
    $("#list").datagrid('selectRow', vRowIndex - 1);
}

function MoveDown() {
    var row = $("#list").datagrid('getSelected');
    var index = $("#list").datagrid('getRowIndex', row);
    mysort(vRowIndex, 'down', 'list');
    $("#list").datagrid('selectRow', vRowIndex + 1);
}

function mysort(index, type, gridname) {
    if ("up" == type) {
        if (index != 0) {
            var toup = $('#' + gridname).datagrid('getData').rows[index];
            var todown = $('#' + gridname).datagrid('getData').rows[index - 1];
            $('#' + gridname).datagrid('getData').rows[index] = todown;
            $('#' + gridname).datagrid('getData').rows[index - 1] = toup;
            $('#' + gridname).datagrid('refreshRow', index);
            $('#' + gridname).datagrid('refreshRow', index - 1);
            $('#' + gridname).datagrid('unselectRow', index);
            $('#' + gridname).datagrid('selectRow', index - 1);
        }
    } else if ("down" == type) {
        var rows = $('#' + gridname).datagrid('getRows').length;
        if (index != rows - 1) {
            var todown = $('#' + gridname).datagrid('getData').rows[index];
            var toup = $('#' + gridname).datagrid('getData').rows[index + 1];
            $('#' + gridname).datagrid('getData').rows[index + 1] = todown;
            $('#' + gridname).datagrid('getData').rows[index] = toup;
            $('#' + gridname).datagrid('refreshRow', index);
            $('#' + gridname).datagrid('refreshRow', index + 1);
            $('#' + gridname).datagrid('unselectRow', index);
            $('#' + gridname).datagrid('selectRow', index + 1);
        }
    }

}