vUrl = "../GTPT.ashx";
var vBMGZ = "22222";//编码规则
var iWXPID = 1;

$(document).ready(function () {
    document.getElementById("B_AddTJ").disabled = false;
    $("#nbdm").hide();
    $("#url").hide();
    $("#urlNote").hide();
    $("#ask").hide();
    $("input[type='radio'][name='RD_GNLX']").click(function () {
        switch (parseInt($("input[name='RD_GNLX']:checked").val())) {
            case 0:
                {
                    $("#nbdm").hide();
                    $("#url").hide();
                    $("#urlNote").hide();
                    $("#ask").hide();
                }
                break;
            case 1:
                {
                    $("#nbdm").show();
                    $("#url").hide();
                    $("#urlNote").hide();
                    $("#ask").show();

                }
                break;
            case 2:
                {
                    $("#nbdm").show();
                    $("#url").show();
                    $("#urlNote").show();
                    $("#ask").hide();
                }
                break;
        }
    });
    FillWT($("#DDL_WT"), 1, 0);
    FillNBDM($("#DDL_NBDM"), "", 1);
    document.getElementById("B_POST").onclick = postTowx;
    document.getElementById("B_Save").onclick = savePostTOsever;
    $("#selectPublicID").combobox({
        onSelect: function (record) {
            iWXPID = record.value;
            sWXPIF = record.pif;
            FillTree(true);
            PageDate_Clear();
            $("#TB_CODE").val("");
            curNode = null;

            document.getElementById("B_AddTJ").disabled = false;


        }
    });
    $.parser.parse("#WXPublicID");
    FillPublicID($("#selectPublicID"));



});

function FillTree(pAsync) {


    PostToCrmlib("FillWXCDDYTree", { iPUBLICID: iWXPID }, function (data) {
        if (data.length < 1) {
            ShowMessage("还未定义菜单");
            $("#TreeLeft").html("");
            return;
        }
        var zNodes = "[";
        for (var i = 0; i < data.length; i++) {
            zNodes = zNodes + "{id:'" + data[i].sDM + "',pId:'" + ((data[i].sPDM == "") ? "0" : data[i].sPDM) + "',name:'" + data[i].sDMQC + "',name:'" + data[i].sNAME
                + "',nbdm:'" + data[i].sNBDM + "',type:'" + data[i].iTYPE + "',urlASD:'" + data[i].sURL + "',PUBLICID:'" + data[i].iPUBLICID + "',askid:'" + data[i].iASKID + "',asKNAME:'" + data[i].sASK + "'}"; //,url:'" + data[i].sURL + "'
            if (i < data.length - 1)
                zNodes = zNodes + ",";
        }
        zNodes = zNodes + "]";
        $.fn.zTree.init($("#TreeLeft"), setting, eval(zNodes));
        SetControlBaseState();
    }, pAsync);

}

function IsValidData() {
    if ($("#TB_CODE").val() == "") {
        ShowMessage("请输入代码");
        return false;
    }
    if ($("#TB_NAME").val() == "") {
        ShowMessage("请输入名称");
        return false;
    }
    var zTree = $.fn.zTree.getZTreeObj("TreeLeft");
    if (zTree != null && zTree != undefined && curNode != undefined) {
        if (curNode.level == 0 && curNode.children != null) {
            if ($("input[name='RD_GNLX']:checked").val() > 0) {
                ShowMessage("一级菜单（" + curNode.name + "）下有二级菜单，不可选择推送/网页模式");
                return false;
            }
        } else if (curNode.getParentNode() != null && curNode.getParentNode().type != 0) {
            ShowMessage("一级菜单(" + curNode.getParentNode().name + ")定义为发送信息或者跳转页面不能加下级");
            return false;
        }
        var node = zTree.getNodeByParam("id", $("#TB_CODE").val());
        if (vProcStatus == cPS_ADD && node != null) {
            ShowMessage("代码已存在");
            return false;
        }
        if (curNode.level == 1 && $("input[name='RD_GNLX']:checked").val() == 0) {
            ShowMessage("定义二级菜单不可选择一级菜单");
            return false;
        }
    }
    if ($("#TB_CODE").val().indexOf("_") >= 0) {
        ShowMessage("请将代码填写完整");
        return false;
    }
    switch (parseInt($("input[name='RD_GNLX']:checked").val())) {
        case 0:
            {
                $("#DDL_NBDM").val("");
                $("#TB_URL").val("");
            }
            break;
        case 1:
            {
                if ($('#DDL_NBDM').combobox('getValue') != null && $('#DDL_NBDM').combobox('getValue') != "") {
                    $("#TB_URL").val("");
                } else {

                    ShowMessage("推送模式下未选择内部代码");
                    return false;
                }
                if ($('#DDL_WT').combobox('getValue') == null || $('#DDL_WT').combobox('getValue')== "") {
                    ShowMessage("请选择问题/关键字");
                    return false;
                }
                //if ($("#DDL_WT").val() == "") {
                //    ShowMessage("请选择问题/关键字");
                //    return false;
                //}
            }
            break;
        case 2:
            {
                //$("#DDL_NBDM").val("");
                if (!IsURL($("#TB_URL").val())) {
                    ShowMessage("网页模式下URL填写不正确");
                    return false;
                }
            }
            break;
    }
    return true;
}




function ShowData(treeNode) {
    $("[name='RD_GNLX']").prop("checked", false);
    $("#TB_CODE").inputmask("remove");
    $("#HF_JLBH").val(treeNode.id);
    $("#TB_NAME").val(treeNode.name);
    $("#TB_CODE").val(treeNode.id);
    //$("#DDL_NBDM").val(treeNode.nbdm);
    $('#DDL_NBDM').combobox('setValue', treeNode.nbdm);
    $("#TB_URL").val(treeNode.urlASD);
    $("input[name='RD_GNLX'][value='" + treeNode.type + "']").prop("checked", true);
    //$("#DDL_WT").val(treeNode.askid);
    $('#DDL_WT').combobox('setValue', treeNode.askid);
    $("#selectPublicID").combobox("setValue", treeNode.PUBLICID)




}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#HF_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sDM = $("#TB_CODE").val();
    Obj.sNAME = $("#TB_NAME").val();
    if ($('#DDL_NBDM').combobox('getValue') != null && $('#DDL_NBDM').combobox('getValue') != "")
        //Obj.sNBDM = G$('#DDL_NBDM').combobox('getValue');
        Obj.sNBDM = $('#DDL_NBDM').combobox('getValue');

    Obj.sURL = $("#TB_URL").val();
    //Obj.bMJBJ = $("#CB_MJBJ")[0].checked ? "1" : "0";
    Obj.iTYPE = $("[name='RD_GNLX']:checked").val();
    if (Obj.iTYPE == 1) {
        Obj.iASKID = $('#DDL_WT').combobox('getValue');
    }
    Obj.iLoginPUBLICID = iWXPID;
    return Obj;
}



function OnClickCustom(e, treeId, treeNode) {
    var treeObj = $.fn.zTree.getZTreeObj(treeId);
    //获取当前节点对象
    var sNodes = treeObj.getSelectedNodes();

    //判断父子节点并提取长度
    //微信菜单只有两级所以可以直接取出一级节点数量
    var parentLength = treeObj.getNodes().length;
    var childLength = 0;
    if (treeNode.level == 0 && treeNode.children != null) {
        //一级节点下子节点数量
        childLength = treeNode.children.length;
    } else if (treeNode.level > 0) {
        //除一级节点外 当前节点的同级节点数量
        childLength = treeNode.getParentNode().children.length;
    }
    switch (parseInt(treeNode.type)) {
        case 0:
            {
                $("#nbdm").hide();
                $("#url").hide();
                $("#urlNote").hide();
                $("#ask").hide();
            }
            break;
        case 1:
            {
                $("#nbdm").show();
                $("#url").hide();
                $("#urlNote").hide();
                $("#ask").show();
            }
            break;
        case 2:
            {
                $("#nbdm").show();
                $("#url").show();
                $("#urlNote").show();
                $("#ask").hide();
            }
            break;
    }
    vProcStatus = cPS_BROWSE;
    ShowData(treeNode);
    curNode = treeNode;
    SetControlBaseState();
    if (parentLength >= 3 && treeNode.level == 0) {
        //一级菜单达到三个
        document.getElementById("B_AddTJ").disabled = true;
    }
    if (childLength >= 5 && parentLength >= 3) {
        //二级菜单达到五个 一级菜单达到3个
        document.getElementById("B_AddTJ").disabled = true;
        document.getElementById("B_AddXJ").disabled = true;
    }
    if (treeNode.level >= 1) {
        //不能加三级菜单
        document.getElementById("B_AddXJ").disabled = true;
    }
    if (treeNode.type != 0) {
        document.getElementById("B_AddXJ").disabled = true;
    }

}

function AddCustomerButton() {
    //单据特殊的按钮在这里加，因为如果直接AddToolButtons的话，会覆盖原来按钮的事件，所以加了一个这个方法
    AddToolButtons("发布", "B_POST");
};


function FillWT(selectName, vTYPE, vBJ_NONE) {
    PostToCrmlib("FillWT", { iTYPE: vTYPE }, function (data) {
        var arr = [];
        for (i = 0; i < data.length; i++) {
            arr.push({ value: data[i].iID, text: data[i].sASK });
        }
        selectName.combobox("loadData", arr);
        selectName.combobox("setValue", "");
    }, false);
}

function AddXJClick() {
    var TYPE = $("[name='RD_GNLX']:checked").val();

    if (TYPE == 2) {
        ShowMessage("网页模式不可以加下级");
        return false;


    }

    PageDate_Clear();
    vProcStatus = cPS_ADD;
    SetControlBaseState();
    var lvl = vBMGZ[curNode.level + 1];
    var msk = new Array(parseInt(lvl) + 1).join("9");
    msk = curNode.id + msk
    $("#TB_CODE").inputmask("mask", { "mask": msk });
    //加下级时顺便展开
    var treeObj = $.fn.zTree.getZTreeObj("TreeLeft");
    treeObj.expandNode(curNode);
    AddXJClickCustom();
};
