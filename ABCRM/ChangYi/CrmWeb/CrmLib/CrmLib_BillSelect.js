$(document).ready(function () {
    AddToolButtons("查询", "B_Search");
    AddToolButtons("取消", "B_Cancel");
    //AddToolButtons("选择", "B_Select");
    AddCustomerButton();


    try {
        if (typeof (eval("InitGrid")) == "function") {
            InitGrid();
            $("#list").jqGrid({
                async: false,
                datatype: "json",
                //mtype:"get",//默认GET
                colNames: vColumnNames,
                colModel: vColumnModel,
                toolbar: [true, "top"],
                //cellEdit: true,
                sortable: true,
                sortorder: "asc",
                sortname: vColumnModel[0].name,
                shrinkToFit: false,
                rownumbers: true,
                altRows: true,
                width: 800,
                height: 'auto',
                rowNum: 15,
                rowList: [15, 50, 100],
                pager: '#pager',
                viewrecords: true,
                //合计
                footerrow: true,
                userDataOnFooter: true,
                onSelectRow: function (rowid) {
                    SetControlBaseState();
                    //var rowData = $("#list").getRowData(rowid);
                    //var bExecuted = rowData.iZXR != undefined && rowData.iZXR != "0";//已审核
                    //var bHasData = rowData.iJLBH != "0";//有数据
                    //vJLBH = rowData.iJLBH;

                    //document.getElementById("B_Insert").disabled = !bCanEdit;
                    //document.getElementById("B_Update").disabled = !(bHasData && !bExecuted) || !bCanEdit;
                    //document.getElementById("B_Delete").disabled = !(bHasData && !bExecuted) || !bCanEdit;
                    //document.getElementById("B_Exec").disabled = !(bHasData && !bExecuted) || !bCanExec;
                },
            });

            $("#first_pager").html("首页");
            $("#prev_pager").html("上一页");
            $("#next_pager").html("下一页");
            $("#last_pager").html("末页");;
        }
    } catch (e) { }
});

function AddCustomerButton() {
    //单据特殊的按钮在这里加，因为如果直接AddToolButtons的话，会覆盖原来按钮的事件，所以加了一个这个方法
    ;
}
