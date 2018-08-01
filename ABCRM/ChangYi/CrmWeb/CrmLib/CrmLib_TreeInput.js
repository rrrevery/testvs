var vBMGZ = "22222";//编码规则
var curNode;//选中的节点

var saveCode;

var setting = {
    view: {
        showIcon: false
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    callback: {
        beforeClick: beforeClick,
        onClick: onClick
    }
};

$(document).ready(function () {
    $("h2").hide();
    //$("h3").hide();
    //初始化按钮、状态
    AddToolButtons("加同级", "B_AddTJ");
    AddToolButtons("加下级", "B_AddXJ");
    AddToolButtons("修改", "B_Update");
    AddToolButtons("删除", "B_Delete");
    AddToolButtons("保存", "B_Save");
    AddToolButtons("取消", "B_Cancel");
    AddCustomerButton();
    AddButtonSep();
    BindKey();
    RefreshButtonSep();

    $("#B_AddTJ").css("width", "86px");
    $("#B_AddXJ").css("width", "86px");
    var val = document.getElementById("mjbj").innerHTML;
    val += "<div class='bffld_left'>末级标记</div><div class='bffld_right'><input type='checkbox' class='magic-checkbox' id='CB_MJBJ' value='0' /><label for='CB_MJBJ'></label></div>";

    document.getElementById("mjbj").innerHTML = val;

    val = "";
    val += "<div class='bfrow'>";
    val += "<div class='bffld'><div class='bffld_left'>编码规则</div><div class='bffld_right'><label id='LB_BMGZ' style='text-align: left'>XXXX</label></div></div></div>";
    val += "<div class='bfrow'>";
    val += "<div class='bffld'><div class='bffld_left'>代码</div><div class='bffld_right'><input id='TB_CODE' type='text' /><input id='HF_JLBH' type='hidden' /></div></div></div>";
    val += "<div class='bfrow'>";
    val += "<div class='bffld'><div class='bffld_left'>名称</div><div class='bffld_right'><input id='TB_NAME' type='text' /></div></div></div>";
    //val += document.getElementById("MainPanel").innerHTML;
    //document.getElementById("MainPanel").innerHTML = val;
    $("#MainPanel").prepend(val);//尼玛上边那样会影响EasyUI控件的初始化

    val = document.getElementById("TreePanel").innerHTML;
    val += "<ul id='TreeLeft' class='ztree' style='margin-top: 0;'></ul>";
    document.getElementById("TreePanel").innerHTML = val;

    FillTree(true);

    $("#LB_BMGZ").text(vBMGZ);
    vPagePersonID = iDJR;
    vProcStatus = cPS_BROWSE;

    SetControlBaseState();

    $("#TreePanel").height($(document).height() - 93);
    $("#MainPanel").height($(document).height() - 93);

    if ($("#bftitle").text() == "")
        $("#bftitle").html(vCaption);
});

function BindKey() {
    //处理按钮事件
    document.getElementById("B_AddTJ").onclick = AddTJClick;
    document.getElementById("B_AddXJ").onclick = AddXJClick;
    document.getElementById("B_Update").onclick = UpdateClick;
    document.getElementById("B_Delete").onclick = DeleteClick;
    document.getElementById("B_Cancel").onclick = CancelClick;
    document.getElementById("B_Save").onclick = SaveClick;
}

function AddTJClick() {
    PageDate_Clear();
    vProcStatus = cPS_ADD;
    SetControlBaseState();
    var lvl = 2;

    if (curNode != null && curNode != undefined) {
        lvl = vBMGZ[curNode.level];
    }

    var msk = new Array(parseInt(lvl) + 1).join("9");
    if (curNode != null && curNode != undefined) {
        msk = curNode.id.substr(0, curNode.id.length - parseInt(vBMGZ[curNode.level])) + msk;

    }
    else
        msk = msk;
        $("#TB_CODE").inputmask("mask", { "mask": msk });
    
    //else
    //    lvl = 0;
    //curNode = 0;

  
    
    AddTJClickCustom();
};
function AddTJClickCustom() {

}
function AddXJClick() {
    PageDate_Clear();
    vProcStatus = cPS_ADD;
    SetControlBaseState();
    var lvl = vBMGZ[curNode.level + 1];
    var msk = new Array(parseInt(lvl) + 1).join("9");
    msk = curNode.id + msk
    $("#TB_CODE").inputmask("mask", { "mask": msk });
    //加下级时顺便展开
    var treeObj = $.fn.zTree.getZTreeObj("TreeLeft");
    treeObj.expandNode(curNode, true);
    AddXJClickCustom();
};
function AddXJClickCustom() {

}
function UpdateClick() {
    vProcStatus = cPS_MODIFY;
    SetControlBaseState();
    var lvl = vBMGZ[curNode.level];
    var msk = new Array(parseInt(lvl) + 1).join("9");
    msk = curNode.id.substr(0, curNode.id.length - parseInt(vBMGZ[curNode.level])) + msk;
    $("#TB_CODE").inputmask("mask", { "mask": msk });
    $("#TB_CODE").val(curNode.id);
    UpdateClickCustom();
};
function UpdateClickCustom() {

}
function DeleteClick() {
    ShowYesNoMessage("是否删除？", function () {
        if (curNode.getPreNode() == null) {
            if (curNode.getParentNode() == null)
                saveCode = "";
            else
                saveCode = curNode.getParentNode().id;
        }
        else
            saveCode = curNode.getPreNode().id;
        if (posttosever(SaveData(), vUrl, "Delete") == true) {
            //PageDate_Clear();
            vProcStatus = cPS_BROWSE;
            SetControlBaseState();

        }
    });
};
function CancelClick() {
    ShowYesNoMessage("是否取消？", function () {
        vProcStatus = cPS_BROWSE;
        SetControlBaseState();
        ShowData(curNode);
    });
};
function SaveClick() {
    var vMode;
    if (IsValidDataBase()) {
        if (vProcStatus == cPS_MODIFY) {
            vMode = "Update";
        }
        else {
            vMode = "Insert";
        }
        saveCode = $("#TB_CODE").val();
        if (posttosever(SaveData(), vUrl, vMode) == true) {
            vProcStatus = cPS_BROWSE;
            SetControlBaseState();
        }
    }
};

function IsValidDataBase() {
    if ($("#TB_CODE").val() == "") {
        ShowMessage("请输入代码");
        return false;
    }
    if ($("#TB_NAME").val() == "") {
        ShowMessage("请输入名称");
        return false;
    }
    if ($.fn.zTree.getZTreeObj("TreeLeft") != null) {
        var zTree = $.fn.zTree.getZTreeObj("TreeLeft");
        var node = zTree.getNodeByParam("id", $("#TB_CODE").val());
    }
    if (vProcStatus == cPS_ADD && node != null) {
        ShowMessage("代码已存在");
        return false;
    }
    if (vProcStatus == cPS_MODIFY && $("#CB_MJBJ").prop("checked") && curNode.isParent) {
        ShowMessage("非末级节点不需要选中末级标记");
        return false;
    }
    if ($("#TB_CODE").val().indexOf("_") >= 0) {
        ShowMessage("请将代码填写完整");
        return false;
    }
    return true && IsValidData();
}
function validDataSave() {
    return true;
}
function PageDate_Clear() {
    var Element = document.getElementById("MainPanel");
    ClearInputdata(Element);
};

function SetControlBaseState() {
    //if (sDJRMC == "") {
    //    alert("您已离线，请重新登录！");
    //}
    $("#LB_BMGZ").text(vBMGZ);
    var bHasData = false;//是否选中数据
    var bParent = false;//是否有子节点
    var bInitData = false;//是否初始化数据
    var zTree = $.fn.zTree.getZTreeObj("TreeLeft");
    var level = 0;
    var nodes;
    if (zTree != null) {
        nodes = zTree.getSelectedNodes();
        bHasData = nodes.length > 0;
        if (nodes.length == 1 && nodes[0].pid == "nodata") {
            bHasData = false;
            bInitData = true;
        }
        if (bHasData) {
            bParent = nodes[0].isParent;
            level = nodes[0].level + 1;
        }
    }
    else
        bHasData = false;

    var bEditMode = (vProcStatus != cPS_BROWSE);//编辑状态    

    document.getElementById("B_Save").disabled = !bEditMode;
    document.getElementById("B_Update").disabled = !(!bEditMode && bHasData) || !bCanEdit;
    document.getElementById("B_Delete").disabled = document.getElementById("B_Update").disabled || bParent; //!(!bEditMode && bHasData && !bExecuted);
    document.getElementById("B_AddTJ").disabled = document.getElementById("B_Update").disabled && !bInitData || bEditMode;
    document.getElementById("B_AddXJ").disabled = document.getElementById("B_Update").disabled || $("#CB_MJBJ").prop("checked") || level == vBMGZ.length;
    document.getElementById("B_Cancel").disabled = !bEditMode;
    document.getElementById("MainPanel").disabled = bEditMode;
    PageControl(bEditMode, false);
    SetControlState();
};

function posttosever(Obj, str_url, str_mode, str_suc, async) {
    PostToServer(Obj, str_url, str_mode, str_suc, async, function (data) {
        FillTree(false);
        var zTree = $.fn.zTree.getZTreeObj("TreeLeft");
        if (saveCode != "") {
            curNode = zTree.getNodeByParam("id", saveCode, null);
            zTree.selectNode(curNode);
            ShowData(curNode);
            saveCode = "";
            SetControlBaseState();
        }
        else
            PageDate_Clear();
    });
}

function beforeClick(treeId, treeNode) {
    //浏览状态下才可以点击tree
    return vProcStatus == cPS_BROWSE;
}

function onClick(e, treeId, treeNode) {
    vProcStatus = cPS_BROWSE;
    ShowData(treeNode);
    curNode = treeNode;
    SetControlBaseState();
    OnClickCustom(e, treeId, treeNode);
}
function OnClickCustom(e, treeId, treeNode) {
    ;
}

function postTowx() {
    if ($("#selectPublicID").combobox("getValue") == "选择公众号") {
        ShowMessage("请选择公众号");
        return;
    }
    var treeObj = $.fn.zTree.getZTreeObj("TreeLeft");
    if (treeObj == null) {
        ShowMessage("还未定义微信菜单！无法发布");
        return;
    } else if (validDataSave()) {
        var ceshi = '';
        $.ajax({
            type: "post",
            url: "../GTPT_WX.ashx?requestType=post&PUBLICID=" + iWXPID+ "&PUBLICIF=" + sWXPIF,
            data: { SC: ceshi },
            success: function (data) {
                if (data == "") {
                    ShowMessage("请求成功");
                } else {
                    ShowMessage(data);
                }
            },
            error: function (data) {
                ShowMessage(data)
            }
        });
    }
}

function savePostTOsever() {
    if (validDataSave() && IsValidDataBase()) {
        if (vProcStatus == cPS_MODIFY) {
            vMode = "Update";
        }
        else {
            vMode = "Insert";
        }
        saveCode = $("#TB_CODE").val();
        if (posttosever(SaveData(), vUrl, vMode) == true) {
            //清空选中状态
            var treeObj = $.fn.zTree.getZTreeObj("TreeLeft");
            treeObj.cancelSelectedNode();
            curNode = undefined;
            vProcStatus = cPS_BROWSE;
            SetControlBaseState();
            PageDate_Clear();
        }
    }
}
