var vJLBH = "";
var vUrl = "";
var vPageMsgID = "1";
var vPagePersonID = "1";
var vColumnNames = [];
var vColumnModel = [];
//var vColumns = [];
var GridWidth = 1000;
var GridHeight = 300;
var bCanEdit = true;
var bCanExec = true;
var bCanSrch = true;
var vCaption = "";
var vOLDDB = GetUrlParam("old");
if (vOLDDB == "")
    vOLDDB = "0";
var vCZK = GetUrlParam("czk");
if (vCZK == "")
    vCZK = "0";
var optype = GetUrlParam("optype");
var jlbhlist = GetUrlParam("jlbhlist");
//var id = $("#accordion2 li").length + 1;
//var predivifid = "divli1";
//var curdivifid = "";
////XXM start
//$.jgrid.defaults.ajaxGridOptions = {
//    error: function (jQxHR) {
//        if (jQxHR.responseText.indexOf("error") != -1) {
//            art.dialog({ content: jQxHR.responseText, lock: true, time: 2 });
//            return;
//        }
//    }
//};
////XXM stop

//MakeNewTab放到commonfunc里

function SetControlBaseState() {
    //var rowid = $("#list").jqGrid("getGridParam", "selrow");
    var rowData = $("#list").datagrid("getSelected");
    var bExecuted = rowData == undefined || rowData == null || ((rowData.iZXR !== "") && (rowData.iZXR != undefined) && (rowData.iZXR != "0"));//$("#LB_ZXRMC").text() != "";//已审核
    //var bExecuted = ((rowData.iZXR !== "") && (rowData.iZXR != undefined) && (rowData.iZXR != "0")) || rowid == undefined;//$("#LB_ZXRMC").text() != "";//已审核
    var bHasData = $("#list").datagrid("getData").rows.length > 0;//$("#TB_JLBH").text() != "";//有数据
    if (rowData != null)
        vJLBH = rowData.iJLBH;
    //var bHasData = $("#list").getGridParam("reccount") > 0;//$("#TB_JLBH").text() != "";//有数据
    //vJLBH = rowData.iJLBH;

    //document.getElementById("B_Insert").disabled = !bCanEdit;
    //document.getElementById("B_Update").disabled = !(bHasData && !bExecuted) || !bCanEdit;
    //document.getElementById("B_Delete").disabled = !(bHasData && !bExecuted) || !bCanEdit;
    //document.getElementById("B_Exec").disabled = !(bHasData && !bExecuted) || !bCanExec;
    document.getElementById("B_Search").disabled = !bCanSrch;
    //SetControlState();
};

function AddCustomerButton() {
    //单据特殊的按钮在这里加，因为如果直接AddToolButtons的话，会覆盖原来按钮的事件，所以加了一个这个方法
    ;
}

$(document).ready(function () {
    GridWidth = document.body.clientWidth - 2;
    //GridHeight = $(document).height() - 144;
    //初始化按钮、状态
    AddToolButtons("查询", "B_Search");
    //AddToolButtons("添加", "B_Insert");
    //AddToolButtons("修改", "B_Update");
    //AddToolButtons("删除", "B_Delete");
    //AddToolButtons("审核", "B_Exec");
    AddToolButtons("导出", "B_Export");
    //AddCustomerButton();
    vPagePersonID = iDJR;
    if (sDJRMC == "") {
        alert("您已离线，请重新登录！");
        //var strFullPath = window.document.location.href;
        //var strPath = window.document.location.pathname;
        //var pos = strFullPath.indexOf(strPath);
        //var prePath = strFullPath.substring(0, pos);   
        //window.parent.location.href = prePath + "/Login.aspx";
    }

    //SetControlBaseState();
    document.getElementById("B_Search").onclick = SearchClick;
    //document.getElementById("B_Delete").onclick = DeleteClick; 
    //document.getElementById("B_Exec").onclick = ExecClick; 
    //document.getElementById("B_Export").onclick = ExportClick; 

    DrawGrid();
    SetControlBaseState();
});

function DrawGrid(listName, vColName, vColModel) {
    InitGrid();
    if (listName == undefined) { listName = "list"; }
    if (vColName == undefined) { vColName = vColumnNames; }
    if (vColModel == undefined) { vColModel = vColumnModel; }
    var vColumns = InitColumns(undefined, vColModel, vColName);
    $("#" + listName).datagrid({
        //url: vUrl + "?mode=Search&func=" + vPageMsgID,
        //method: 'post',
        width: GridWidth,
        height: GridHeight,//674,
        autoRowHeight: false,
        striped: true,
        columns: [vColumns],
        //frozenColumns: [vFrozenColumns],
        sortName: vColumns[0].field,
        singleSelect: true,
        sortOrder: 'asc',
        //remoteSort: false,
        //fitColumns: true,
        //scrollbarSize: 0,
        showHeader: true,
        showFooter: true,
        pagePosition: 'bottom',
        rownumbers: true, //添加一列显示行号
        pagination: true,  //启用分页
        pageNumber: 1,
        pageSize: 100,
        pageList: [100, 500, 1000],
        onSortColumn: function (sort, order) {
            SearchData(undefined, undefined, sort, order, listName);
        },
        onLoadSuccess: OnLoadSuccess,
        onClickRow: OnClickRow,
        //onDblClickRow: DBClickRow,
    });
    var pager = $("#" + listName).datagrid("getPager");
    pager.pagination({
        onSelectPage: function (pageNum, pageSize) {
            SearchData(pageNum, pageSize, undefined, undefined, listName);
        },
    });
    if ($("#bftitle").text() == "")
        $("#bftitle").html(vCaption);
}

function OnClickRow(rowIndex, rowData) {
}

function OnLoadSuccess(rowIndex, rowData) {
}

function IsValidSearch() {
    return true;
}
function SearchClick() {
    if (!IsValidSearch())
        return;
    SearchData();
    SetControlBaseState();
};

function MakeOtherSearchCondition() {
    return "}";
};
function ExportClick() {
    $.ajax({
        type: 'post',
        url: vUrl + "?mode=Export&func=" + vPageMsgID + "&RYID=" + iDJR,
        dataType: "json",
        async: false,
        postData: { 'afterFirst': JSON.stringify(MakeSearchCondition()) },
        //data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = JSON.stringify(data);
        },
        error: function (data) {
            result = data.responseText;
            var oPop = window.open("../../../temp/" + data.responseText);
            for (; oPop.document.readyState != "complete";) {
                if (oPop.document.readyState == "complete") break;
            }
            oPop.document.execCommand("SaveAs");
            oPop.close();
        }
    });
    //DoSearch("Export");
    SetControlBaseState();
};

function MakeSrchCondition(arrayObj, ElementName, FieldName, Sign, Quot) {
    if ($.trim($("#" + ElementName).val()) != "") {
        var ObjJLBH = new Object();
        ObjJLBH.ElementName = FieldName;
        ObjJLBH.ComparisonSign = Sign;
        ObjJLBH.Value1 = $("#" + ElementName).val();
        ObjJLBH.InQuotationMarks = Quot;
        if (FieldName[0] == "d" && Sign == "<=") {
            //当日期条件为<=某天时，为避免时分秒的问题，强制转换成<某天+1
            var date = new Date(ObjJLBH.Value1);
            AddDays(date, 1);
            ObjJLBH.Value1 = FormatDate(date, "yyyy-MM-dd");
            ObjJLBH.ComparisonSign = "<";
        }
        arrayObj.push(ObjJLBH);
    }
}

function MakeSrchCondition2(arrayObj, Value, FieldName, Sign, Quot) {
    if (Value != undefined) {
        var ObjJLBH = new Object();
        ObjJLBH.ElementName = FieldName;
        ObjJLBH.ComparisonSign = Sign;
        ObjJLBH.Value1 = Value;
        ObjJLBH.InQuotationMarks = Quot;
        if (FieldName[0] == "d" && Sign == "<=") {
            //当日期条件为<=某天时，为避免时分秒的问题，强制转换成<某天+1
            var date = new Date(ObjJLBH.Value1);
            AddDays(date, 1);
            ObjJLBH.Value1 = FormatDate(date, "yyyy-MM-dd");
            ObjJLBH.ComparisonSign = "<";
        }
        arrayObj.push(ObjJLBH);
    }
}

//用来保存选择框dialog返回的数据,参数分别是input[type='text'] 的控件名  type=hidden的控件名 type=hidden的控件名
function WUC_Dialog_Return(viewDataIn, trueDataIn, jsonStringIn) {
    var tp_return = $.dialog.data('IpValuesReturn');//跨框架数据共享读取接口。指定name即返回数据，任何引用了artDialog的页面都有效，如果还指定值，就是设置其值

    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + viewDataIn + "").val(tp_mc);//首先清空数据
            var jsonInput = JSON.parse(jsonString);//将jsonString字符串，解析转换成json数据对象类型
            var contractValues = new Array();
            contractValues = jsonInput.Depts;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += "'" + contractValues[i].Code + "',";
            }
            $("#" + viewDataIn + "").val(tp_mc);
            $("#" + trueDataIn + "").val(tp_hf.substr(0, tp_hf.length - 1));//不取最后一个 单引号' 防止sql查询出错
            $("#" + jsonStringIn + "").val(jsonString);//记录返回的数据
        }
    }
}

function SearchData(page, rows, sort, order, listName) {
    if (listName == undefined) { listName = "list"; }
    var obj = MakeSearchJSON(listName);
    //page页码,rows每页行数,sort排序字段,order排序方式
    page = page || $('#' + listName + '').datagrid("options").pageNumber;
    rows = rows || $('#' + listName + '').datagrid("options").pageSize;
    sort = sort || $('#' + listName + '').datagrid("options").sortName;
    order = order || $('#' + listName + '').datagrid("options").sortOrder;
    $('#' + listName + '').datagrid("loading");
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
            $('#' + listName + '').datagrid('loadData', JSON.parse(data), "json");
            $('#' + listName + '').datagrid("loaded");
            vSearchData = data;
        },
        error: function (data) {
            ShowMessage(data);
        }
    });
}
