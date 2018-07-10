vUrl = "../CRMGL.ashx";

var Edit = false;

function ShowQX(personid) {
    if (lx == 1) {
        FillCheckTree("TreeKLXList", "FillCheckTreeKLX", personid);
        FillCheckTree("TreeBGDD", "FillCheckTreeBGDD", personid);
        FillCheckTree("TreeSHBM", "FillCheckTreeSHBM", personid);
        FillCheckTree("TreeMDList", "FillCheckTreeMD", personid);
        FillCheckTree("TreeFXDW", "FillCheckTreeFXDW", personid);
    }
    if (lx == 0) {
        FillCheckTree("TreeKLXList", "FillCheckTreeKLX_GROUP", personid);
        FillCheckTree("TreeBGDD", "FillCheckTreeBGDD_GROUP", personid);
        FillCheckTree("TreeSHBM", "FillCheckTreeSHBM_GROUP", personid);
        FillCheckTree("TreeMDList", "FillCheckTreeMD_GROUP", personid);
        FillCheckTree("TreeFXDW", "FillCheckTreeFXDW_GROUP", personid);
    }
    /*
    //部门数据绑定------------------------------------------------------------------------------------------
    $.ajax({
        type: "get",
        //dataType: "json",
        async:false,
        url: "../../WUC/BM/WUC_BM_IF.ashx?queryType=department&personid="+personid,
        beforeSend: function (XMLHttpRequest) {
        },
        success: function (data, textStatus) {
            //生成树
            $.fn.zTree.init($("#TreeSHBM"), settingCheck, eval(data));
            //数据绑定
            //if ($.dialog.data('IpValues')) {
            //    var jsonString = parentInput.val();
            //    if (jsonString != "") {
            //        var jsonInput = JSON.parse(jsonString);
            //        var DepartmentJsong = jsonInput.Depts;
            //        checkTrees("treeDepartment", DepartmentJsong);
            //    }
            //    else {
            //        checkTrees("treeDepartment", "");
            //    }
            //};

            //var treeObj = $.fn.zTree.getZTreeObj("treeDepartment");
            //treeObj.expandAll(true);
        },
        complete: function (XMLHttpRequest, textStatus) {

        },
        error: function (data) {
            alert(data);
        }
    });
    */
}

$(document).ready(function () {
    var personid = GetUrlParam("personid");
    $("#HF_DJR").val(personid);
    $("#TB_DJRMC").val(personid);

    //AddToolButtons("添加", "B_Insert");
    AddToolButtons("修改", "B_Update");
    AddToolButtons("删除", "B_Delete");
    AddToolButtons("保存", "B_Save");
    AddToolButtons("取消", "B_Cancel");
    if (personid == "") { personid = -1; }
    ShowQX(personid);
    location.hash = "tab1-tab";

    $("#TB_DJRMC").click(function () {
        SelectRYXX2("HF_DJR", "TB_DJRMC", "zHF_DJR");
    });
    $("#TB_HYZ").click(function () {
        var data = $("#zHF_DJR").val();
        var el = $("<input>", { type: 'text', val: data });
        $.dialog.data('IpValuesReturn', "");
        $.dialog.data('IpValues', el);
        $.dialog.data('IpValuesChoiceOne', true);
        $.dialog.open("../../WUC/HYZ/WUC_HYZ.aspx", {
            lock: true, width: 480, height: 420, cancel: false
            , close: function () {
                WUC_RYXX_LX2_Return("HF_DJR", "TB_HYZ", "zHF_DJR");
            }
        }, false);
    });

    $("#All").click(function () {
        CheckAllNodes(true);
    });

    $("#None").click(function () {
        CheckAllNodes(false);
    });

    setControlEnabled(false);
    //加同级按钮 事件绑定


    //保存操作
    $("#B_Save").click(function () {
        if (!IsValidData()) {
            return false;
        }

        var Obj = new Object();
        Obj.iJLBH = $("#HF_DJR").val();
        Obj.iLX = lx;
        Obj.itemTable = new Array();
        var zTree = $.fn.zTree.getZTreeObj("TreeKLXList");
        var nodes = zTree.getNodes();
        for (var i = 0, l = nodes.length; i < l; i++) {
            if (nodes[i].checked) {
                var item = new Object();
                item.iDJLX = 0;
                item.iQXID = nodes[i].id;
                Obj.itemTable.push(item);
            }
        }
        pID = "-";
        zTree = $.fn.zTree.getZTreeObj("TreeBGDD");
        nodes = zTree.transformToArray(zTree.getNodes());
        for (var i = 0, l = nodes.length; i < l; i++) {
            if (nodes[i].getCheckStatus().checked && !nodes[i].getCheckStatus().half && nodes[i].pId != pID) {
                var item = new Object();
                item.iDJLX = 1;
                item.sQXDM = nodes[i].id;
                Obj.itemTable.push(item);
                //if (nodes[i].children)//简单数据结构无法使用children?
                //    pID = nodes[i].id;//此处存在BUG，无法跳过再往下的一级
                var childrenNodes = zTree.getNodesByParamFuzzy("id", nodes[i].id, zTree.getNodeByParam("id", nodes[i].id));
                i += childrenNodes.length;
            }
        }
        zTree = $.fn.zTree.getZTreeObj("TreeSHBM");
        nodes = zTree.transformToArray(zTree.getNodes());
        for (var i = 0, l = nodes.length; i < l; i++) {
            if (nodes[i].getCheckStatus().checked && !nodes[i].getCheckStatus().half) {
                var item = new Object();
                item.iDJLX = 2;
                //var node = zTree.getNodesByParam("id", nodes[i].id);
                //item.sQXDM2 = node.getParentNode().pi;
                //if (nodes[i].pId == null)
                item.sQXDM2 = nodes[i].rootId;//SHDM
                //else                
                item.sQXDM = nodes[i].id.substr(item.sQXDM2.length);//BMDM
                if (item.sQXDM == "") { item.sQXDM = " "; }
                Obj.itemTable.push(item);

                var childrenNodes = zTree.getNodesByParamFuzzy("id", nodes[i].id, zTree.getNodeByParam("id", nodes[i].id));
                i += childrenNodes.length;
            }
        }
        zTree = $.fn.zTree.getZTreeObj("TreeMDList");
        //nodes = zTree.getNodes();
        nodes = zTree.transformToArray(zTree.getNodes());//门店按业态分组 无锡华地 2014.11.4
        for (var i = 0, l = nodes.length; i < l; i++) {
            if (nodes[i].getCheckStatus().checked && nodes[i].level == 1) {
                var item = new Object();
                item.iDJLX = 3;
                item.iQXID = nodes[i].id;
                Obj.itemTable.push(item);
            }
        }
        pID = "-";
        zTree = $.fn.zTree.getZTreeObj("TreeFXDW");
        nodes = zTree.transformToArray(zTree.getNodes());
        var item = new Object();
        for (var i = 0, l = nodes.length; i < l; i++) {
            if (nodes[i].getCheckStatus().checked && !nodes[i].getCheckStatus().half && nodes[i].pId != pID) {
                var item = new Object();
                item.iDJLX = 4;
                item.sQXDM = nodes[i].id;
                Obj.itemTable.push(item);
                //if (nodes[i].children)
                //    pID = nodes[i].id;
                var childrenNodes = zTree.getNodesByParamFuzzy("id", nodes[i].id, zTree.getNodeByParam("id", nodes[i].id));
                i += childrenNodes.length;
            }
        }
        //node=zTree.get
        //while ()

        $.ajax({
            type: 'post',
            url: vUrl + "?mode=Update&func=" + vPageMsgID,
            dataType: "text",
            data: { json: JSON.stringify(Obj) },
            success: function (data) {
                if (data.indexOf('错误') >= 0 || data.indexOf('error') >= 0) {
                    // art.dialog({ lock: true, content: data })
                    ShowErrMessage("错误" + data);
                    return;
                }
                setControlEnabled(false);
                // art.dialog({ lock: true, content: "保存成功" });
                ShowMessage("保存成功");
                Edit = false;
            }
        });

    });
    //修改操作
    $("#B_Update").click(function () {
        if ($("#HF_DJR").val() == "") {
            ShowErrMessage("请先选择一个操作员");
            //art.dialog({ lock: true, content: "请先选择一个操作员" });
            return;
        }
        Edit = true;
        setControlEnabled(true);

    });
    //取消操作
    $("#B_Cancel").click(function () {
        ShowQX($("#HF_DJR").val());
        setControlEnabled(false);
        //$("#TB_FXDWDM").inputmask('remove');
        //var zTree = $.fn.zTree.getZTreeObj("TreeFXDW");
        //nodes = zTree.getSelectedNodes();
        //treeNode = nodes[0];
        //$("#TB_FXDWMC").val(treeNode.fxdwmc);
        //$("#TB_FXDWDM").val(treeNode.id);
    });
    //删除操作
    $("#B_Delete").click(function () {
        if ($("#HF_DJR").val() == "") {
            ShowErrMessage("请先选择一个操作员");
            // art.dialog({ lock: true, content: "请先选择一个操作员" });
            return;
        }
        art.dialog({
            title: "删除",
            lock: true,
            content: "确定清空其所有权限？",
            ok: function () {
                var Obj = new Object();
                Obj.iJLBH = $("#HF_DJR").val();

                sjson = "{iJLBH:'" + Obj.iJLBH + "'}";
                $.ajax({
                    type: 'post',
                    url: vUrl + "?mode=Delete&func=" + vPageMsgID,
                    dataType: "text",
                    data: { json: JSON.stringify(sjson), titles: 'cecece' },
                    success: function (data) {
                        if (data == "yes") {               //删除成功
                            ShowMessage("删除成功");
                            // art.dialog({ lock: true, content: "删除成功" });
                            //数据刷新
                            ShowQX(Obj.iJLBH);
                        } else {
                            ShowErrMessage("删除失败");
                            // art.dialog({ lock: true, content: "删除失败" });
                            return;
                        }
                    }
                });
            },
            okVal: '是',
            cancelVal: '否',
            cancel: true
        });
    });

});

function CheckAllNodes(check) {
    var s = $('#tabs').tabs('getSelected')[0].id;
    if (s == "klx")
        var zTree = $.fn.zTree.getZTreeObj("TreeKLXList");
    else if (s == "bgdd")
        var zTree = $.fn.zTree.getZTreeObj("TreeBGDD");
    else if (s == "shbm")
        var zTree = $.fn.zTree.getZTreeObj("TreeSHBM");
    else if (s == "md")
        var zTree = $.fn.zTree.getZTreeObj("TreeMDList");
    else if (s == "fxdw")
        var zTree = $.fn.zTree.getZTreeObj("TreeFXDW");
    zTree.checkAllNodes(check);
}

function SelectRYXX2(DJR, DJRMC, zDJR) {
    //新的选择人员方式
    var data = $("#" + zDJR).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValues', el);//data
    //$.dialog.data('IpValuesLX', "2");//type
    $.dialog.data('IpValuesChoiceOne', true);//choice one
    $.dialog.open("../../WUC/RYXX/WUC_RYXX.aspx", {
        lock: true, width: 480, height: 410, cancel: false
        , close: function () {
            WUC_RYXX_LX2_Return(DJR, DJRMC, zDJR);
        }
    }, false);
}

function WUC_RYXX_LX2_Return(DJR, DJRMC, zDJR) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + DJRMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ",";
                tp_hf += contractValues[i].Id + ",";
            }
            $("#" + DJRMC).val(tp_mc.substr(0, tp_mc.length - 1));
            $("#" + DJR).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zDJR).val(jsonString);
        }
        ShowQX($("#" + DJR).val());
        setControlEnabled(false);
    }
}

//设置控件可用性
function setControlEnabled(flag) {
    $("#B_Update").attr("disabled", flag);
    $("#B_Delete").attr("disabled", flag);
    $("#B_Save").attr("disabled", !flag);
    $("#B_Cancel").attr("disabled", !flag);

    SetTreeState("TreeKLXList", flag);
    SetTreeState("TreeBGDD", flag);
    SetTreeState("TreeSHBM", flag);
    SetTreeState("TreeMDList", flag);
    SetTreeState("TreeFXDW", flag);
    //ShowQX(-1);
}

function SetTreeState(Tree, Enabled) {
    var zTree = $.fn.zTree.getZTreeObj(Tree);
    //var nodes = zTree.getNodes();
    //for (var i = 0, l = nodes.length; i < l; i++) {
    //    zTree.setChkDisabled(nodes[i], Enabled, true, true);
    //}
    if (zTree != null) {
        zTree.setting.callback = { beforeCheck: zTreeBeforeCheck };
    }
    settingCheck.callback = { beforeCheck: zTreeBeforeCheck };
    function zTreeBeforeCheck(treeId, treeNode) {
        return Enabled;
    };
}

function SaveTreeData(Tree, Obj, iDJLX) {
    var zTree = $.fn.zTree.getZTreeObj(Tree);
    var nodes = zTree.getNodes();
    var item = new Object();
    for (var i = 0, l = nodes.length; i < l; i++) {
        item.iDJLX = iDJLX;
        item.
        Obj.itemTable.push(item);
        /*        public int iQXID=0;//具体权限的ID，如HYKTYPE、MDID等
        public string sQXDM = " ";//BMDM、BGDDDM等
        public string sQXDM2 = " ";//SHDM，仅用于部门权限
*/
    }
}

function IsValidData() {
    var zTree = $.fn.zTree.getZTreeObj("TreeSHBM");
    if (zTree == null) {
        ShowMessage("请等待商户部门加载完成");
        return false;
    }
    //if ($("#TB_FXDWDM").val() == "") {
    //    art.dialog({ lock: true, content: "请输入代码" });
    //    return false;
    //}
    //if ($("#TB_FXDWMC").val() == "") {
    //    art.dialog({ lock: true, content: "请输入名称" });
    //    return false;
    //}
    //if ($("#TB_FXDWDM").val().indexOf("_") >= 0) {
    //    art.dialog({ lock: true, content: "请将代码填写完整" });
    //    return false;
    //}

    //var treeObj = $.fn.zTree.getZTreeObj("TreeFXDW");
    //var node = treeObj.getNodeByParam("id", $("#TB_FXDWDM").val());
    //if (Edit && $("#TB_FXDWDM").val() == treeObj.getSelectedNodes.id) {
    //    return true;
    //}

    //if (node != null && Edit == false) {
    //    art.dialog({ lock: true, content: "发行单位代码已存在！" });
    //    return false;
    //}
    return true;
}
