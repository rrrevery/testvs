var vJLBH = "";
var vProcStatus = 0;
var cPS_BROWSE = 0;
var cPS_ADD = 1;
var cPS_MODIFY = 2;
var cPS_ERROR = 3;
var vUrl = "";
var vAction = "";
var vPageMsgID = "1";
var vPagePersonID = "1";
var vColumnNames = [];
var vColumnModel = [];
var vColumns = [];
var vSearchData = "";
var GridWidth = 1000;
var GridHeight = 700;
var bCanEdit = true;
var bCanDelete = true;
var vCaption = "";
var CurIndex = -1;

var vOLDDB = GetUrlParam("old");
if (vOLDDB == "")
    vOLDDB = "0";
var vCZK = GetUrlParam("czk");
if (vCZK == "")
    vCZK = "0";
var RandomSession = "";

var d = art.dialog.defaults;
d.opacity = 0;//取消弹出框时背景变暗

function PageDate_Clear() {
    $("#TB_JLBH").val("");
    var Element = document.getElementById("MainPanel");
    ClearInputdata(Element);
    //清空ztree选择，暂时不放到ClearInputdata里
    var trees = $(".ztree");
    for (i = 0; i < trees.length; i++) {
        var treeObj = $.fn.zTree.getZTreeObj(trees[i].id)
        if (treeObj != null) {
            treeObj.cancelSelectedNode();
            treeObj.expandAll(false);
        }
    }
    //$("#LB_DJRMC").text("");
    //$("#HF_DJR").val("");
    //$("#LB_DJSJ").text("");
    //$("#LB_ZXRMC").text("");
    //$("#HF_ZXR").val("");
    //$("#LB_ZXRQ").text("");
    //$("#list").jqGrid("clearGridData");//清空子表数据
    //$("#list_rw").jqGrid("clearGridData");//清空子表数据

};

function GetValueRegExp(str, name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = str.match(reg);
    if (r != null)
        return unescape(r[2]);
    return "";
}

function SetControlState() {
    //增加了空方法，这样单据js不写这个方法也不会报错了，因为一般这个方法也没有内容
    ;
}

function SetControlBaseState() {
    if (sDJRMC == "") {
        alert("您已离线，请重新登录！");
    }
    var bEditMode = (vProcStatus != cPS_BROWSE);//编辑状态
    var bHasData = $("#TB_JLBH").val() != "";//有数据

    document.getElementById("B_Save").disabled = !bEditMode;
    document.getElementById("B_Insert").disabled = bEditMode || !bCanEdit;
    document.getElementById("B_Update").disabled = !(!bEditMode && bHasData) || !bCanEdit;
    document.getElementById("B_Delete").disabled = (document.getElementById("B_Update").disabled || !bCanDelete); //!(!bEditMode && bHasData && !bExecuted);
    document.getElementById("B_Cancel").disabled = !bEditMode;
    document.getElementById("TB_JLBH").disabled = bEditMode;
    //取消审核启动终止按钮控制
    //启动=有数据 and 已审核 and 未启动 and 未终止 and 启动权限
    //取消审核=启动
    //终止=有数据 and 已启动 and 终止权限 and 未终止
    PageControl(bEditMode, false);
    document.getElementById("TB_JLBH").disabled = bEditMode;
    //$("#list").trigger("reloadGrid");    
    //$("#B_Insert").hide();
    SetControlState();
};

//单据特殊的按钮在这里加，因为如果直接AddToolButtons的话，会覆盖原来按钮的事件，所以加了一个这个方法
function AddCustomerButton() {
    ;
}

$(document).ready(function () {
    GridWidth = $(document).width() - 2;
    GridHeight = $(document).height() - $(".maininput").height() - 107;
    if ($("#bftitle").text() == "")
        $("#bftitle").html(vCaption);
    //初始化按钮、状态
    AddToolButtons("添加", "B_Insert");
    AddToolButtons("修改", "B_Update");
    AddToolButtons("删除", "B_Delete");
    AddToolButtons("保存", "B_Save");
    AddToolButtons("取消", "B_Cancel");
    AddCustomerButton();
    AddButtonSep();

    var val = document.getElementById("jlbh").innerHTML;
    val += "<div class='bffld_left' id='JLBHCaption'>记录编号</div><div class='bffld_right'><input id='TB_JLBH' type='text' readonly='readonly' style='background-color: bisque;' /></div>";//"<input id='TB_JLBH' type='text'/>";
    document.getElementById("jlbh").innerHTML = val;

    //val = document.getElementById("status-bar").innerHTML;
    ////val += "<div class='bfrow'>&nbsp;</div>";
    //val += "<div class='bfrow'><div class='bffld'><div class='bffld_left'>登记人</div><div class='bffld_right'><label id='LB_DJRMC' runat='server' style='text-align:left'></label><input id='HF_DJR' type='hidden'/></div></div>";
    //val += "<div class='bffld' id='ZXR'><div class='bffld_left' id='zxr1'>审核人</div><div class='bffld_right'><label id='LB_ZXRMC' runat='server' style='text-align:left'></label></div></div></div>";
    //val += "<div class='bfrow'><div class='bffld'><div class='bffld_left'>登记时间</div><div class='bffld_right'><label id='LB_DJSJ' runat='server' style='text-align:left'></label><input id='HF_ZXR' type='hidden'/></div></div>";
    //val += "<div class='bffld' id='ZXRQ'><div class='bffld_left' id='zxrq1'>审核时间</div><div class='bffld_right'><label id='LB_ZXRQ' runat='server' style='text-align:left'></label></div></div></div>";
    //document.getElementById("status-bar").innerHTML = val;

    //var val = document.getElementById("btn-toolbar").innerHTML;
    //val = "<div class='clear:both;padding-left:200px;'>" + val + "</div>";
    //document.getElementById("btn-toolbar").innerHTML = val;
    //document.getElementById("jlbh").innerHTML = "<div class='bffld_left' id='LB_JLBH'>记录编号</div><div class='bffld_right'><label id='TB_JLBH'/></div>";//<input id='TB_JLBH' type='text'/>
    //document.getElementById("status-bar").innerHTML = val;
    //document.getElementById("jlbh").innerHTML = "<div class='bffld_left'>记录编号</div><div class='bffld_right'><label id='TB_JLBH'/></div>";//<input id='TB_JLBH' type='text'/>

    vPagePersonID = iDJR;
    if (sDJRMC == "") {
        alert("您已离线，请重新登录！");
        //var strFullPath = window.document.location.href;
        //var strPath = window.document.location.pathname;
        //var pos = strFullPath.indexOf(strPath);
        //var prePath = strFullPath.substring(0, pos);   
        //window.parent.location.href = prePath + "/Login.aspx";
    }
    vAction = GetUrlParam("action");//$.getUrlParam("action");
    vJLBH = GetUrlParam("jlbh");//$.getUrlParam("jlbh");
    vProcStatus = cPS_BROWSE;
    //$('#TB_JLBH').bind('keypress', function (event) {
    //    if (event.keyCode == "13") {
    //        vJLBH = $('#TB_JLBH').val();
    //        ShowDataBase($('#TB_JLBH').val());
    //    }
    //});
    SetControlBaseState();
    BindKey();
    $('#reg-form').easyform();//表单验证

    InitGrid();
    vColumns = InitColumns(false);
    $("#list").datagrid({
        //url: vUrl + "?mode=Search&func=" + vPageMsgID,
        //method: 'post',
        width: GridWidth,
        height: GridHeight,//674,
        autoRowHeight: true,
        striped: true,
        columns: [vColumns],
        sortName: vColumns[0].field,
        singleSelect: true,
        //sortOrder: 'desc',
        //remoteSort: false,
        fitColumns: true,
        //scrollbarSize: 0,
        showHeader: true,
        showFooter: true,
        pagePosition: 'bottom',
        rownumbers: true, //添加一列显示行号
        pagination: true,  //启用分页
        pageNumber: 1,
        pageSize: 10,
        pageList: [10, 50, 100],
        //onSortColumn: function (sort, order) {
        //    SearchData(undefined, undefined, sort, order);
        //},
        onClickRow: function (rowIndex, rowData) {
            if (vProcStatus != cPS_BROWSE) {
                $("#list").datagrid("selectRow", CurIndex);
                return;
            }
            ShowDataBase(rowData);
            CurIndex = rowIndex;
            //SetControlBaseState();
            //$("#list").jqGrid('setRowData', rowid, false, { background: 'silver' });        
        },
        //onDblClickRow: DBClickRow,
    });
    var pager = $('#list').datagrid("getPager");
    pager.pagination({
        onSelectPage: function (pageNum, pageSize) {
            SearchData("list", pageNum, pageSize);
        },
    });
    //$("#list").jqGrid(
    //{
    //    async: false,
    //    datatype: "json",
    //    url: vUrl + "?mode=Search&func=" + vPageMsgID,
    //    //postData: { 'afterFirst': JSON.stringify(arrayObj), 'ZSeldata': JSON.stringify(arrayObjMore) },
    //    //mtype:"post",
    //    colNames: vColumnNames,
    //    colModel: vColumnModel,
    //    shrinkToFit: false,
    //    rownumbers: true,
    //    //footerrow: true,
    //    altRows: true,
    //    width: 800,
    //    height: 'auto',
    //    rowNum: 10,
    //    pager: '#pager',
    //    viewrecords: true,
    //    onSelectRow: function (rowid, status, e) {
    //        if (!$("#B_Save").attr("disabled")) {
    //            return false;
    //        }
    //        var rowData = $("#list").getRowData(rowid);
    //        ShowDataBase(rowData);
    //        //SetControlBaseState();
    //        //$("#list").jqGrid('setRowData', rowid, false, { background: 'silver' });
    //    },
    //});
    SearchData();
});

function BindKey() {
    //处理按钮事件    
    document.getElementById("B_Insert").onclick = InsertClick;
    document.getElementById("B_Update").onclick = UpdateClick;
    document.getElementById("B_Delete").onclick = DeleteClick;
    document.getElementById("B_Save").onclick = SaveClick;
    document.getElementById("B_Cancel").onclick = CancelClick;
}
//按钮事件单独写
function InsertClick() {
    PageDate_Clear();
    vProcStatus = cPS_ADD;
    $("#LB_DJRMC").text(sDJRMC);
    $("#HF_DJR").val(iDJR);
    SetControlBaseState();
    InsertClickCustom();
};
function InsertClickCustom() {
};
function UpdateClick() {
    vProcStatus = cPS_MODIFY;
    SetControlBaseState();
    UpdateClickCustom();
};
function UpdateClickCustom() {
};
function DeleteClick() {
    ShowYesNoMessage("是否删除？", function () {
        if (posttosever(SaveDataBase(), vUrl, "Delete", "操作成功", false) == true) {
            PageDate_Clear();
            vProcStatus = cPS_BROWSE;
            SearchData();
            SetControlBaseState();
        }
    });

};

function CancelClick() {
    ShowYesNoMessage("是否取消？", function () {
        vProcStatus = cPS_BROWSE;
        SetControlBaseState();
        if (vJLBH == "") {
            PageDate_Clear();
        } else {
            //ShowDataBase(vJLBH);
        }
    });
    CancelClickCustom();
};
function CancelClickCustom() {
    ;
}
function SaveClick() {
    var vMode;
    if (IsValidData()) {
        if (vJLBH != "") {
            vMode = "Update";
        }
        else {
            vMode = "Insert";
        }
        if (posttosever(SaveDataBase(), vUrl, vMode) == true) {
            vProcStatus = cPS_BROWSE;
            SearchData();
            SetControlBaseState();

        }
    }
};

function SaveDataBase() {
     return SaveData();
   // return AutoSaveData();
};

function AutoSaveData() {
    var AutoObj = new Object();
    var itemList = $("[data-autobind]");
    itemList.each(function (index, element) {
        if ($(element)[0].tagName == "INPUT") {
            switch ($(element).attr("type")) {
                case "text":
                case "hidden":
                    AutoObj[$(element).attr("data-autobind")] = $(element).val();
                    break;
                case "checkbox":
                    AutoObj[$(element).attr("data-autobind")] = $(element)[0].checked ? 1 : 0;
                case "radio":
                    AutoObj[$(element).attr("data-autobind")] = $(element)[0].checked ? $(element)[0].defaultValue : 0;
            }
        }
        if ($(element)[0].tagName == "SELECT") {
            AutoObj[$(element).attr("data-autobind")] = $(element).val();
        }

        if ($(element)[0].tagName == "DIV") {
            AutoObj[$(element).attr("data-autobind")] = $("#" + $(element).attr("id")).datagrid("getRows");
        }
    })
    return AutoObj;
}

function AutoShowData(AutoObj) {
    //var AutoObj = JSON.parse(data);
    var itemList = $("[data-autobind]");
    itemList.each(function (index, element) {
        if ($(element)[0].tagName == "INPUT") {
            switch ($(element).attr("type")) {
                case "text":
                case "hidden":
                    $(element).val(AutoObj[$(element).attr("data-autobind")]);
                    break;
                case "checkbox":
                    $(element)[0].checked = AutoObj[$(element).attr("data-autobind")] == 1 ? true : false;
                case "radio":
                    $(element)[0].checked = AutoObj[$(element).attr("data-autobind")] == $(element)[0].defaultValue ? true : false;
            }
        }
        if ($(element)[0].tagName == "SELECT") {
            $(element).val(AutoObj[$(element).attr("data-autobind")]);
        }

        if ($(element)[0].tagName == "DIV") {
            $("#" + $(element).attr("id")).datagrid("loadData", AutoObj[$(element).attr("data-autobind")]);
            $("#" + $(element).attr("id")).datagrid("loaded");
        }
    })

}



function posttosever(Obj, str_url, str_mode, str_suc, async) {
    var result = false;
    result = PostToServer(Obj, str_url, str_mode, str_suc, async, function (data) {
        SearchData();
        SetControlBaseState();
        vJLBH = GetValueRegExp(data, "jlbh")
        if (vJLBH != "" && !isNaN(vJLBH))
            ShowDataBase2(vJLBH);
        //SearchData();

    });
    return result;
}

function ShowDataBase(Obj) {
    $("#B_Update").prop("disabled", false);
    $("#B_Delete").prop("disabled", false);
     ShowData(Obj);
    //AutoShowData(Obj);
    //SetControlBaseState();
}

//function SearchData(page, rows, sort, order) {
//    var obj = MakeSearchJSON();
//    //page页码,rows每页行数,sort排序字段,order排序方式
//    page = page || $('#list').datagrid("options").pageNumber;
//    rows = rows || $('#list').datagrid("options").pageSize;
//    sort = sort || $('#list').datagrid("options").sortName;
//    order = order || $('#list').datagrid("options").sortOrder;
//    $('#list').datagrid("loading");
//    $.ajax({
//        type: "post",
//        url: vUrl + "?mode=Search&func=" + vPageMsgID,
//        async: true,
//        data: {
//            json: JSON.stringify(obj),
//            titles: 'cybillsearch',
//            page: page,
//            rows: rows,
//            sort: sort,
//            order: order,
//        },
//        success: function (data) {
//            if (data.indexOf('错误') >= 0 || data.indexOf('error') >= 0) {
//                ShowMessage(data);
//            }
//            $('#list').datagrid('loadData', JSON.parse(data), "json");
//            $('#list').datagrid("loaded");
//            vSearchData = data;
//            if (CurIndex >= 0) {
//                $("#list").datagrid("selectRow", CurIndex);
//            }
//        },
//        error: function (data) {
//            ShowMessage(data);
//        }
//    });

//}

//function MakeSearchJSON() {
//    var cond = MakeSearchCondition();
//    if (cond == null)
//        return;
//    var Obj = new Object();
//    Obj.SearchConditions = cond;
//    Obj.iLoginRYID = iDJR;
//    AddCustomerCondition(Obj);
//    return Obj;
//}

//function MakeSearchCondition() {
//    ;
//}
//function AddCustomerCondition() {
//    ;
//}

function ShowDataBase2(t_jlbh) {
    var Obj = new Object();
    Obj.SearchConditions = MakeJLBH(t_jlbh);
    Obj.iJLBH = t_jlbh;
    Obj.iLoginPUBLICID = iPID;
    var canBeClose = false;
    var success = false;
    var myDialog = art.dialog({
        lock: true,
        content: "<div class='bfdialog otherlog' style='width:200px'>正在查询数据,请等待……</div>",
        close: function () {
            if (canBeClose) {
                return true;
            }
            return false;
        }
    });
    $.ajax({
        //async:false,
        type: "post",
        url: vUrl + "?mode=View" + "&func=" + vPageMsgID,
        data: { json: JSON.stringify(Obj).replace(/\\/g, "").replace(/\"{/g, "{").replace(/}\"}/g, "}}"), titles: 'cybillview' },
        success: function (data) {
            canBeClose = true;
            myDialog.close();
            //if (data.indexOf('错误') >= 0) {
            //    //initbuttonStatus(modifyType1);
            //    //changeBtnStatusByModifyType(lastbuttonenum);
            //    art.dialog({ lock: true, content: data })
            //    return;
            //}

            if (data.length > 1) {//&& data.indexOf("错误") < 0) {//判断返回的数据长度 添加时返回billid，其他则返回错误信息
                var Obj = JSON.parse(data);
                ShowData(Obj);
                //vProcStatus = cPS_BROWSE;
                SetControlBaseState();
            }
            else {
                ShowErrMessage("操作失败");
            }
        },
        error: function (data) {
            canBeClose = true;
            myDialog.close();
            ShowErrMessage("查询失败");
        }
    });
}

function MakeJLBH(t_jlbh) {
    //生成iJLBH的JSON
    var arrayObj = new Array();
    MakeSrchCondition2(arrayObj, t_jlbh, "iJLBH", "=", false);
    if (GetUrlParam("mzk") == "1") {
        Obj.sDBConnName = "CRMDBMZK";
    }
    return arrayObj;
}