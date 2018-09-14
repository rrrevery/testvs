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
        beforeClick: beforeTreeNodeClick,
        onClick: onTreeNodeClick
    }
};
var menu;
var tb;

function beforeTreeNodeClick(treeId, treeNode) {
    var check = (treeNode && !treeNode.isParent);
    return check;
}

function beforeTreeNodeClick2(treeId, treeNode) {
    return true;
}

function onTreeNodeClick(event, treeId, treeNode) {
    $("#" + tb).val(treeNode.name);
    hideMenu(menu);
    TreeNodeClickCustom(event, treeId, treeNode);
}

//function TreeNodeClickCustom(event, treeId, treeNode) {

//}

function hideMenu(menuid) {
    $("#" + menuid).fadeOut("fast");
    $("body").unbind("mousedown", onBodyDown);
}

function onBodyDown(event) {
    if (!(event.target.id == "menuBtn" || event.target.id == menu || $(event.target).parents("#" + menu).length > 0)) {
        hideMenu(menu);
    }
}

function FillTree(func, treename, tbname, param, suc, async) {
    if (async == undefined)
        async = true;
    menu = "menuContent" + treename;
    tb = tbname;
    //$("#" + tbname).attr("readonly", true);
    $("body").append("<div id='menuContent" + treename + "' class='menuContent'><ul id='" + treename + "' class='ztree'></ul></div>");
    $("#" + tbname).bind('keypress', function () {
        if (event.keyCode == 13) {
            var zTree = $.fn.zTree.getZTreeObj(treename);
            var node = zTree.getNodeByParam("id", $("#" + tbname).val());
            if (node != null)
                onTreeNodeClick(null, zTree, node);
            return false;
        };
    });
    $("#" + tbname).click(function () {
        menu = "menuContent" + treename;
        tb = tbname;
        var Obj = $("#" + tbname);
        var Offset = $("#" + tbname).offset();
        $("#" + menu).css({ left: Offset.left + "px", top: Offset.top + Obj.outerHeight() + "px" }).slideDown("fast");
        $("body").bind("mousedown", onBodyDown);
    });
    PostToCrmlib(func, param, suc, async);
}

//具体方法
function FillBGDDTree(treename, tbname, mdid) {
    if (mdid == undefined || mdid == "")
        mdid = 0;
    FillBGDDTreeBase(treename, tbname, 0, 0, 1, mdid);
}

function FillBGDDTreeZK(treename, tbname, mdid) {
    if (mdid == undefined || mdid == "")
        mdid = 0;
    FillBGDDTreeBase(treename, tbname, 1, 0, 1, mdid);
}

function FillBGDDTreeSK(treename, tbname, mdid) {
    if (mdid == undefined || mdid == "")
        mdid = 0;
    FillBGDDTreeBase(treename, tbname, 0, 1, 1, mdid);
}

function FillBGDDTreeBase(treename, tbname, zk, sk, qx, mdid) {
    FillTree("FillBGDDTree", treename, tbname, { iRYID: iDJR, iMDID: mdid, iQX: qx, iZK: zk, iSK: sk }, function (data) {
        $.fn.zTree.init($("#" + treename), setting, data);
        //var zNodes = "[";
        //for (var i = 0; i < data.length; i++) {
        //    zNodes = zNodes + "{id:'" + data[i].sBGDDDM + "',pId:'" + ((data[i].sPBGDDDM == "") ? "0" : data[i].sPBGDDDM) + "',name:'" + data[i].sBGDDMC + "',mdid:'" + data[i].iMDID + "',mjbj:'" + data[i].bMJBJ + "',xsbj:'" + data[i].bXS_BJ + "',zkbj:'" + data[i].bZK_BJ + "'}";
        //    if (i < data.length - 1)
        //        zNodes = zNodes + ",";
        //}
        //zNodes = zNodes + "]";
        //$.fn.zTree.init($("#" + treename), setting, eval(zNodes));
    });
}

function FillHYKTYPETree(treename, tbname, iMode, qx) {
    qx = qx || 1;
    FillTree("FillHYKTYPETree", treename, tbname, { iRYID: iDJR, iMODE: iMode, iQX: qx }, function (data) {
        $.fn.zTree.init($("#" + treename), setting, data);
        //var zNodes = "[";
        //for (var i = 0; i < data.length; i++) {
        //    zNodes = zNodes + "{id:'" + data[i].sHYKDM
        //        + "',pId:'" + ((data[i].sHYKPDM == "") ? "0" : data[i].sHYKPDM)
        //        + "',name:'" + data[i].sHYKNAME
        //        + "',kfje:'" + data[i].fKFJE
        //        + "',bj_xfsj:'" + data[i].iBJ_XFSJ
        //        + "'}";
        //    if (i < data.length - 1)
        //        zNodes = zNodes + ",";
        //}
        //zNodes = zNodes + "]";
        ////zNodes = "[{id:1,pID:0,name:'aaaa'}]";
        //$.fn.zTree.init($("#" + treename), setting, eval(zNodes));
    });
}

function FillFXDWTree(treename, tbname, qx) {
    qx = qx || 1;
    FillTree("FillFXDWTree", treename, tbname, { iRYID: iDJR, iQX: qx }, function (data) {
        $.fn.zTree.init($("#" + treename), setting, data);
        //var zNodes = "[";
        //for (var i = 0; i < data.length; i++) {
        //    zNodes = zNodes + "{id:'" + data[i].sFXDWDM + "',pId:'" + ((data[i].sPFXDWDM == "") ? "0" : data[i].sPFXDWDM) + "',name:'" + data[i].sFXDWMC + "',data:'" + data[i].iJLBH + "'}";
        //    if (i < data.length - 1)
        //        zNodes = zNodes + ",";
        //}
        //zNodes = zNodes + "]";
        ////zNodes = "[{id:1,pID:0,name:'aaaa'}]";
        //$.fn.zTree.init($("#" + treename), setting, eval(zNodes));
    });
}

function FillHYZLXTree(treename, tbname) {
    FillTree("FillHYZLXTree", treename, tbname, {}, function (data) {
        $.fn.zTree.init($("#" + treename), setting, data);
        //var zNodes = "[";
        //for (var i = 0; i < data.length; i++) {
        //    zNodes = zNodes + "{id:'" + data[i].sHYZLXDM + "',pId:'" + ((data[i].sPHYZLXDM == "") ? "0" : data[i].sPHYZLXDM) + "',name:'" + data[i].sHYZLXDM + "'+' '+'" + data[i].sHYZLXMC + "',mjbj:'" + data[i].iBJ_MJ + "',lxid:'" + data[i].iHYZLXID + "',nodeName:'" + data[i].sHYZLXMC + "'}";

        //    if (i < data.length - 1) {
        //        zNodes = zNodes + ",";
        //    }
        //}
        //zNodes = zNodes + "]";
        //$.fn.zTree.init($("#" + treename), setting, eval(zNodes));
    });
}

function FillZFFSTree(treename, tbname) {
    FillTree("FillZFFSTree", treename, tbname, {}, function (data) {
        $.fn.zTree.init($("#" + treename), setting, data);
        //if (data.length < 1) {
        //    var zNode = '{"id":"00","pid":"0","name":"暂无数据..."}';
        //    $.fn.zTree.init($("#TreeLeft"), setting, JSON.parse(zNode));
        //    return;
        //}
        ////返回数据，对数据进行绑定。
        ////PID用来实现子级关系 ,在数据库中是比较代码前缀。
        //var zNodes = "[";
        //for (var i = 0; i < data.length; i++) {
        //    zNodes = zNodes + "{id:'" + data[i].sZFFSDM + "',pId:'" + ((data[i].PZFFSDM == "") ? 0 : data[i].sPZFFSDM) + "',name:'" + data[i].sZFFSQC + "',data:'" + data[i].iJLBH + "',bj_mj:'" + data[i].iBJ_MJ + "',type:'" + data[i].iTYPE + "',jlbh:'"
        //        + data[i].iZFFSID + "',zffsmc:'" + data[i].sZFFSMC + "',bj_dzqdczk:" + data[i].iBJ_DZQDCZK + "}";
        //    if (i < data.length - 1)
        //        zNodes = zNodes + ",";
        //}
        //zNodes = zNodes + "]";//将zNodes组装成js数组数据格式。每一个数组元素是一个对象。
        //$.fn.zTree.init($("#" + treename), setting, eval(zNodes));
    });
}

function FillBQXMTree(treename, tbname, bqlb) {
    FillTree("FillBQXMTree", treename, tbname, { iLABELLBID: bqlb }, function (data) {
        $.fn.zTree.init($("#" + treename), setting, data);
        //var zNodes = "[";
        //for (var i = 0; i < data.length; i++) {
        //    zNodes = zNodes + "{id:'" + data[i].sLABELXMDM + "',pId:'" + ((data[i].sPLABELXMDM == "") ? "0" : data[i].sPLABELXMDM) + "',name:'" + data[i].sLABELXMMC + "',bqmc:'" + data[i].sLABELXMMC + "',bqms:'" + data[i].sLABELXMMS + "',bqxmid:'" + data[i].iLABELXMID + "',bjwy:'" + data[i].iBJ_WY + "',mjbj:" + data[i].iSTATUS + "}";
        //    if (i < data.length - 1)
        //        zNodes = zNodes + ",";
        //}
        //zNodes = zNodes + "]";
        //$.fn.zTree.init($("#" + treename), setting, eval(zNodes));
    });

}

function FillQYTree(treename, tbname, async) {
    FillTree("FillHYQY", treename, tbname, { iMODE: 1 }, function (data) {
        $.fn.zTree.init($("#" + treename), setting, data);
        //if (data.length < 1) {
        //    var zNode = '{"id":"00","pid":"0","name":"暂无会员区域数据..."}';
        //    $.fn.zTree.init($("#TreeLeft"), setting, JSON.parse(zNode));
        //    return;
        //}
        //var zNodes = "[";
        //for (var i = 0; i < data.length; i++) {
        //    zNodes = zNodes + "{id:'" + data[i].sQYDM + "',pId:'" + ((data[i].sPQYDM == "") ? "0" : data[i].sPQYDM) + "',name:'" + data[i].sQYQC + "',qymc:'" + data[i].sQYMC + "',yzbm:'" + data[i].sYZBM + "',jlbh:'" + data[i].iJLBH + "',mjbj:" + data[i].iBJ_MJ + "}";
        //    if (i < data.length - 1)
        //        zNodes = zNodes + ",";
        //}
        //zNodes = zNodes + "]";
        ////zNodes = "[{id:1,pID:0,name:'aaaa'}]";
        //$.fn.zTree.init($("#" + treename), setting, eval(zNodes));
    }, async);
}

function FillSHBMTreeBase(treename, tbname, shdm, lvl) {
    FillTree("FillTreeSHBM", treename, tbname, { iRYID: iDJR, sSHDM: shdm, iLEVEL: lvl }, function (data) {
        setting.callback.beforeClick = beforeTreeNodeClick2;
        $.fn.zTree.init($("#" + treename), setting, data);
    });
}

function FillLPFLTree(treename, tbname) {
    FillTree("FillLPFLTree", treename, tbname, {}, function (data) {
        $.fn.zTree.init($("#" + treename), setting, data);
    });
}
