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
var bCanEdit = true;
var bCanExec = true;
var bCanStart = true;
var bCanStop = true;
var bCanDelete = true;
var bCanWriteCard = true;
var vCaption = "";
var bAutoAddAfterSave = false;//保存后自动添加，用于需要迅速保存添加的菜单，如礼品发放等
var tabName = "";
var bNeedItemData = true;
var vInitHeight = 300;

var vOLDDB = GetUrlParam("old");
if (vOLDDB == "")
    vOLDDB = "0";
var vCZK = GetUrlParam("czk");
if (vCZK == "")
    vCZK = "0";

var vUnExecMethod = "UnExec";//取消审核的方法名，默认是UnExec，可选Rollback
var bStartBeforeStop = true;//先启动后终止，否则的话审核后终止

//初始化单据子表
var vColumnNames = [];
var vColumnModel = [];
var vColumns = [];
var editIndex = undefined;
var iWXPID = 1;

var iWXPID = GetUrlParam("iLoginPUBLICID");
if (iWXPID == "" || iWXPID == undefined) {
    iWXPID = 1;
}

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
    $("#LB_DJRMC").text("");
    $("#HF_DJR").val("");
    $("#LB_DJSJ").text("");
    $("#LB_ZXRMC").text("");
    $("#HF_ZXR").val("");
    $("#LB_ZXRQ").text("");
    $("#LB_QDRMC").text("");
    $("#HF_QDR").val("");
    $("#LB_QDSJ").text("");
    $("#LB_ZZRMC").text("");
    $("#HF_ZZR").val("");
    $("#LB_ZZRQ").text("");
    var array = new Array();
    $("#MainPanel .datagrid-f").each(function () {
        $(this).datagrid("loadData", array);
        $(this).datagrid("loaded");
    })

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
    if (vProcStatus == cPS_MODIFY && $("#HF_ZXR").val() != "" && $("#HF_ZXR").val() != "0" && $("#zxr1").html() == "审核人") {
        vProcStatus = cPS_BROWSE;
    }
    var bEditMode = (vProcStatus != cPS_BROWSE);//编辑状态
    var bExecuted = $("#LB_ZXRMC").text() != "";//已审核
    var bStarted = $("#LB_QDRMC").text() != "";//已启动
    var bStopped = $("#LB_ZZRMC").text() != "";//已终止
    var bHasData = $("#TB_JLBH").val() != "";//有数据

    document.getElementById("B_Save").disabled = !bEditMode;
    document.getElementById("B_Insert").disabled = bEditMode || !bCanEdit;
    document.getElementById("B_Update").disabled = !(!bEditMode && bHasData && !bExecuted) || !bCanEdit;
    document.getElementById("B_Delete").disabled = (document.getElementById("B_Update").disabled || !bCanDelete); //!(!bEditMode && bHasData && !bExecuted);
    document.getElementById("B_Cancel").disabled = !bEditMode;
    document.getElementById("B_Exec").disabled = !(!bEditMode && bHasData && !bExecuted) || !bCanExec;
    document.getElementById("TB_JLBH").disabled = bEditMode;
    //取消审核启动终止按钮控制
    //启动=有数据 and 已审核 and 未启动 and 未终止 and 启动权限
    //取消审核=启动
    document.getElementById("B_Start").disabled = !(bHasData && bExecuted && !bStopped && !bStarted && bCanStart);
    document.getElementById("B_UnExec").disabled = document.getElementById("B_Start").disabled;
    //终止=有数据 and 已启动 and 终止权限 and 未终止
    document.getElementById("B_Stop").disabled = !(bHasData && bExecuted && (bStarted || !bStartBeforeStop) && bCanStop && !bStopped);
    PageControl(bEditMode, false);
    document.getElementById("TB_JLBH").disabled = bEditMode;
    //$("#B_Insert").hide();

    SetControlState();
    try {
        if (typeof (eval("InitGrid")) == "function") {
            //document.getElementById("AddItem").disabled = !bEditMode;
            //document.getElementById("DelItem").disabled = !bEditMode;
            $(".item_addtoolbar").prop("disabled", !bEditMode);
            $(".item_deltoolbar").prop("disabled", !bEditMode);

        }
    }
    catch (ex) { };
};

//单据特殊的按钮在这里加，因为如果直接AddToolButtons的话，会覆盖原来按钮的事件，所以加了一个这个方法
function AddCustomerButton() {
    ;
}

$(document).ready(function () {
    $("h2").hide();
    //$("h3").hide();
    //初始化按钮、状态
    AddToolButtons("添加", "B_Insert");
    AddToolButtons("修改", "B_Update");
    AddToolButtons("删除", "B_Delete");
    AddToolButtons("保存", "B_Save");
    AddToolButtons("取消", "B_Cancel");
    AddToolButtons("预览", "B_Preview");
    AddToolButtons("审核", "B_Exec");
    AddToolButtons("取消审核", "B_UnExec");
    AddToolButtons("启动", "B_Start");
    AddToolButtons("终止", "B_Stop");
    AddToolButtons("打印", "B_Print");
    AddCustomerButton();

    var val = document.getElementById("jlbh").innerHTML;
    val += "<div class='bffld_left' id='JLBHCaption'>记录编号</div><div class='bffld_right'><input id='TB_JLBH' type='text' readonly='readonly' style='background-color: bisque;' /></div>";//"<input id='TB_JLBH' type='text'/>";
    document.getElementById("jlbh").innerHTML = val;

    val = document.getElementById("status-bar").innerHTML;
    //val += "<div class='bfrow'>&nbsp;</div>";
    val += "<div class='bfrow'>";
    val += "<div class='bffld' id='DJR'><div class='bffld_left' id='djr1'>登记人</div><div class='bffld_right'><label id='LB_DJRMC' class='djr'></label><label id='LB_DJSJ' class='djsj'></label></div></div>";
    val += "<div class='bffld' id='ZXR'><div class='bffld_left' id='zxr1'>审核人</div><div class='bffld_right'><label id='LB_ZXRMC' class='djr'></label><label id='LB_ZXRQ' class='djsj'></label></div></div></div>";
    //val += "<div class='bfrow'><div class='bffld' id='DJSJ'><div class='bffld_left'>登记时间</div><div class='bffld_right'><label id='LB_DJSJ' runat='server' style='text-align:left'></label></div></div>";
    //val += "<div class='bffld' id='ZXRQ'><div class='bffld_left' id='zxrq1'>审核时间</div><div class='bffld_right'><label id='LB_ZXRQ' runat='server' style='text-align:left'></label></div></div></div>";
    //启动终止
    val += "<div class='bfrow'>";
    val += "<div class='bffld' id='QDR'><div class='bffld_left' id='qdr1'>启动人</div><div class='bffld_right'><label id='LB_QDRMC' class='djr'></label><label id='LB_QDSJ' class='djsj'></label></div></div>";
    val += "<div class='bffld' id='ZZR'><div class='bffld_left' id='zzr1'>终止人</div><div class='bffld_right'><label id='LB_ZZRMC' class='djr'></label><label id='LB_ZZRQ' class='djsj'></label></div></div></div>";
    //val += "<div class='bfrow'><div class='bffld' id='QDSJ'><div class='bffld_left' id='qdsj1'>启动时间</div><div class='bffld_right'><label id='LB_QDSJ' runat='server' style='text-align:left'></label></div></div>";
    //val += "<div class='bffld' id='ZZSJ'><div class='bffld_left'>终止时间</div><div class='bffld_right'><label id='LB_ZZRQ' runat='server' style='text-align:left'></label></div></div></div>";
    //hidden放一起
    val += "<input id='HF_DJR' type='hidden'/><input id='HF_ZXR' type='hidden'/><input id='HF_QDR' type='hidden'/><input id='HF_ZZR' type='hidden'/>";
    document.getElementById("status-bar").innerHTML = val;

    //var val = document.getElementById("btn-toolbar").innerHTML;
    //val = "<div class='clear:both;padding-left:200px;'>" + val + "</div>";
    //document.getElementById("btn-toolbar").innerHTML = val;
    //document.getElementById("jlbh").innerHTML = "<div class='bffld_left' id='LB_JLBH'>记录编号</div><div class='bffld_right'><label id='TB_JLBH'/></div>";//<input id='TB_JLBH' type='text'/>
    //document.getElementById("status-bar").innerHTML = val;
    //document.getElementById("jlbh").innerHTML = "<div class='bffld_left'>记录编号</div><div class='bffld_right'><label id='TB_JLBH'/></div>";//<input id='TB_JLBH' type='text'/>

    //val = document.getElementById("status-bar1").innerHTML;
    //val += "<div class='bffld'><div class='bffld_left'>登记人</div><div class='bffld_right'><label id='LB_DJRMC' runat='server'/><input id='HF_DJR' type='hidden' /></div></div><div class='div3'><div class='bffld_left'>审核人</div><div class='bffld_right'><label id='LB_ZXRMC' runat='server'/><input id='HF_ZXR' type='hidden' /></div></div>";
    //document.getElementById("status-bar1").innerHTML = val;
    //val = document.getElementById("status-bar2").innerHTML;
    //val += "<div class='bffld'><div class='bffld_left'>登记时间</div><div class='bffld_right'><label id='LB_DJSJ' runat='server'/></div></div><div class='div3'><div class='bffld_left'>审核日期</div><div class='bffld_right'><label id='LB_ZXRQ' runat='server'/></div></div>";
    //document.getElementById("status-bar2").innerHTML = val;

    //$("[class='fa fa-list-ul fa-lg']").click(function () {
    //    ToggleNavigationMen();
    //});

    //默认不显示取消审核启动终止
    $("#B_UnExec").hide();
    $("#B_Start").hide();
    $("#B_Stop").hide();
    $("#QDR").hide();
    $("#QDSJ").hide();
    $("#ZZR").hide();
    $("#ZZSJ").hide();
    $("#B_Preview").hide();
    $("#B_Print").hide();
    AddButtonSep();
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
    RefreshButtonSep();
    //$('#reg-form').easyform();
    if (vAction == "add") {
        document.getElementById("B_Insert").onclick();
        //vProcStatus = cPS_ADD;
        //$("#LB_DJRMC").text(sDJRMC);
        //$("#HF_DJR").val(iDJR);
    }
    if (vAction == "edit")
        vProcStatus = cPS_MODIFY;

    if (vJLBH != "") {
        ShowDataBase(vJLBH, iWXPID);

        //ShowDataBase(vJLBH);
        SearchListData();
    };

    //可以通过传action来外部直接调用审核、删除等按钮
    if (vJLBH != "") {
        if (vAction == "exec") {
            document.getElementById("B_Exec").onclick();
        };
        if (vAction == "delete") {
            document.getElementById("B_Delete").onclick();
        };
        if (vAction == "unexec") {
            document.getElementById("B_UnExec").onclick();
        };
        if (vAction == "start") {
            document.getElementById("B_Start").onclick();
        };
        if (vAction == "stop") {
            document.getElementById("B_Stop").onclick();
        };
    }
    if (vCaption == "")
        vCaption = GetUrlCaption("bftitle");
    if ($("#bftitle").text() == "")
        $("#bftitle").html(vCaption);
    $(".btnsep:visible:last").hide();
    //window.onbeforeunload = onclose;
    //function onclose() {
    //    return "您确定退出吗？";
    //}    
    GridWidth = $(document).width() - 18;

    try {
        if (typeof (eval("InitGrid")) == "function") {
            DrawGrid();
        }
    }
    catch (ex) { }

    //try {
    //    if (typeof (eval("InitGrid")) == "function") {
    //        InitGrid();
    //        vColumns = InitColumns();
    //        $("#list").datagrid({
    //            width: '100%',
    //            height: 300,
    //            autoRowHeight: false,
    //            singleSelect: false,
    //            striped: true,
    //            columns: [vColumns],
    //            sortName: vColumns[0].field,
    //            sortOrder: 'desc',
    //            fitColumns: true,
    //            showHeader: true,
    //            showFooter: true,
    //            pagePosition: 'bottom',
    //            rownumbers: true, //添加一列显示行号
    //            // pagination: true,  //启用分页
    //            pageNumber: 1,
    //            pageSize: 1000,
    //            toolbar: '#tb'
    //        });
    //    }
    //}
    //catch (ex) { }
    //$('input[id=files]').change(function () {
    //    $('#fileCover').val($(this).val());
    //});
});

function DrawGrid(listName, vColName, vColModel, vSingle, vHeight) {
    //为简化查询模板开发流程，统一Grid格式，新的查询可以使用InitGrid函数初始化vColumnNames和vColumnModel
    InitGrid();
    if (vHeight == undefined) { vHeight = vInitHeight; }
    if (vSingle == undefined) { vSingle = false; }
    if (listName == undefined) { listName = "list"; tabName = "#tb"; }
    if (listName != "list") { tabName = "#" + listName + "_tb" }
    if (vColName == undefined) { vColName = vColumnNames; }
    if (vColModel == undefined) { vColModel = vColumnModel; }
    if (vColumns.length == 0 || vColName != vColumnNames) {
        vColumns = InitColumns(undefined, vColModel, vColName);
        vAllColumns = vColumns;
    }
    $("#" + listName + "").datagrid({
        width: '100%',
        height: vHeight,
        autoRowHeight: false,
        singleSelect: vSingle,
        striped: true,
        columns: [vColumns],
        sortName: vColumns[0].field,
        sortOrder: 'desc',
        fitColumns: true,
        showHeader: true,
        showFooter: false,
        pagePosition: 'bottom',
        rownumbers: true, //添加一列显示行号
        // pagination: true,  //启用分页
        pageNumber: 1,
        pageSize: 1000,
        toolbar: '' + tabName + '', //+ tabName,
        onClickCell: onClickCell,
        onClickRow: OnClickRow,
    });
}

function OnClickRow(rowIndex, rowData) {
    ;
}

$.extend($.fn.datagrid.methods, {
    editCell: function (jq, param) {
        return jq.each(function () {
            var opts = $(this).datagrid('options');
            var fields = $(this).datagrid('getColumnFields', true).concat($(this).datagrid('getColumnFields'));
            for (var i = 0; i < fields.length; i++) {
                var col = $(this).datagrid('getColumnOption', fields[i]);
                col.editor1 = col.editor;
                if (fields[i] != param.field) {
                    col.editor = null;
                }
            }
            $(this).datagrid('beginEdit', param.index);
            for (var i = 0; i < fields.length; i++) {
                var col = $(this).datagrid('getColumnOption', fields[i]);
                col.editor = col.editor1;
            }
        });
    }
});


function endEditing(listname) {
    if (editIndex == undefined) { return true }
    listname = listname || "list";
    if ($('#' + listname).datagrid('validateRow', editIndex)) {
        $('#' + listname).datagrid('endEdit', editIndex);
        editIndex = undefined;
        return true;
    } else {
        return false;
    }
}
function onClickCell(index, field) {
    var listname = this.id;
    if (endEditing(listname) && vProcStatus != cPS_BROWSE) {
        $('#' + listname).datagrid('selectRow', index)
                .datagrid('editCell', { index: index, field: field });
        editIndex = index;

        var ed = $(this).datagrid('getEditor', { index: index, field: field });
        if (ed) {
            $(ed.target).bind("keypress", function (event) {
                if (event.keyCode == 13) {
                    if ($('#' + listname).datagrid('validateRow', editIndex)) {
                        $('#' + listname).datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return true;
                    }
                }
            })
        }
    }
}
function BindKey() {
    //处理按钮事件    
    document.getElementById("B_Insert").onclick = InsertClick;
    document.getElementById("B_Update").onclick = UpdateClick;
    document.getElementById("B_Delete").onclick = DeleteClick;
    document.getElementById("B_Exec").onclick = ExecClick;
    document.getElementById("B_UnExec").onclick = UnExecClick;
    document.getElementById("B_Cancel").onclick = CancelClick;
    document.getElementById("B_Save").onclick = SaveClick;
    document.getElementById("B_Start").onclick = StartClick;
    document.getElementById("B_Stop").onclick = StopClick;
    //document.getElementById("B_Print").onclick = PrintClick;
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
            SetControlBaseState();
            if ($("list") != null) {
                $("#list").trigger("reloadGrid");
            }
            if ($("list_rw") != null) {
                $("#list_rw").trigger("reloadGrid");
            }

        }
    });
};
function ExecClick() {
    ExecClickCustom();
    if (IsValidInputData()) {
        ShowYesNoMessage("审核后不可修改，是否审核？", function () {
            if (posttosever(SaveDataBase(), vUrl, "Exec") == true) {
                vProcStatus = cPS_BROWSE;
                ShowDataBase(vJLBH, iWXPID);
                SetControlBaseState();
            }
        });
    }
};
function ExecClickCustom() {
};
function UnExecClick() {
    UnExecClickCustom();
    ShowYesNoMessage("是否取消审核？", function () {
        if (posttosever(SaveDataBase(), vUrl, vUnExecMethod) == true) {
            vProcStatus = cPS_BROWSE;
            ShowDataBase(vJLBH, iWXPID);
            SetControlBaseState();
        }
    });
};
function UnExecClickCustom() {
};
function CancelClick() {
    ShowYesNoMessage("是否取消？", function () {
        vProcStatus = cPS_BROWSE;
        SetControlBaseState();
        if (vJLBH == "") {
            PageDate_Clear();
        } else {
            ShowDataBase(vJLBH, iWXPID);
        }
    });
};
function SaveClick() {
    var vMode;
    if (IsValidInputData()) {
        if (vJLBH != "") {
            vMode = "Update";
        }
        else {
            vMode = "Insert";
        }
        if (posttosever(SaveDataBase(), vUrl, vMode) == true) {
            vProcStatus = cPS_BROWSE;
            SetControlBaseState();
        }
    }
};
function StartClick() {
    ShowYesNoMessage("是否启动？", function () {
        if (posttosever(SaveDataBase(), vUrl, "Start") == true) {
            vProcStatus = cPS_BROWSE;
            ShowDataBase(vJLBH, iWXPID);
            SetControlBaseState();
        }
    });
};
function StopClick() {
    ShowYesNoMessage("是否终止？", function () {
        if (posttosever(SaveDataBase(), vUrl, "Stop") == true) {
            vProcStatus = cPS_BROWSE;
            ShowDataBase(vJLBH, iWXPID);
            SetControlBaseState();
        }
    });
};
function PrintClick() {
};

function SaveDataBase() {
    var Obj = new Object();
    Obj = SaveData(Obj);
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    //Obj.iLoginPUBLICID = iPID;
    Obj.sPTToken = GetUrlParam("ck");
    Obj.iLoginPUBLICID = iWXPID;

    return Obj;
};

function posttosever(Obj, str_url, str_mode, str_suc, async) {
    return PostToServer(Obj, str_url, str_mode, str_suc, async, function (data) {
        SetControlBaseState();
        vJLBH = GetValueRegExp(data, "jlbh")
        if (bAutoAddAfterSave) {
            document.getElementById("B_Insert").onclick();
        }
        else if (vJLBH != "")
            ShowDataBase(vJLBH, iWXPID);
        if (str_mode == "Insert") {
            SearchListData();
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

function ShowDataBase(t_jlbh, iWXPID) {
    var Obj = new Object();
    Obj.SearchConditions = MakeJLBH(t_jlbh);
    Obj.iJLBH = t_jlbh;
    Obj.iLoginPUBLICID = iWXPID;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
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
                ShowData(data);
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

function IsValidInputData() {
    try {
        if (typeof (eval("InitGrid")) == "function") {
            var rows = $("#list").datagrid("getData").rows;
            if (bNeedItemData && rows.length <= 0) {
                ShowMessage("子表没有数据，请添加!", 3);
                return false;
            }
            for (var i = 0; i < rows.length; i++) {
                $("#list").datagrid("endEdit", i);
            }
            if ($(".datagrid-editable-input").length > 0) {
                ShowMessage("子表数据正在编辑中，请保存!", 3);
                return false;
            }

        }

    }
    catch (ex) { };
    if (!IsValidData())
        return false;
    return true;
}

function SearchListData() {
    //T--单据界面查询列表
}
