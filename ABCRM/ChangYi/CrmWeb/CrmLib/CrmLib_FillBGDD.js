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
var menu;
function beforeClick(treeId, treeNode) {
    var check = (treeNode && !treeNode.isParent);
    return check;
}
function hideMenu(menuid) {
    $("#" + menuid).fadeOut("fast");
    $("body").unbind("mousedown", onBodyDown);
}

function onBodyDown(event) {
    if (!(event.target.id == "menuBtn" || event.target.id == menu || $(event.target).parents("#" + menu).length > 0)) {
        hideMenu(menu);
    }
}

function FillBGDDTree(treename, editbgdd, menuid, mdid) {
    if (mdid == undefined || mdid == "")
        mdid = 0;
    FillBGDDTreeBase(treename, editbgdd, menuid, 0, 0, 1, mdid);
}

function FillBGDDTreeZK(treename, editbgdd, menuid, mdid) {
    if (mdid == undefined || mdid == "")
        mdid = 0;
    FillBGDDTreeBase(treename, editbgdd, menuid, 1, 0, 1, mdid);
}

function FillBGDDTreeSK(treename, editbgdd, menuid, mdid) {
    if (mdid == undefined || mdid == "")
        mdid = 0;
    FillBGDDTreeBase(treename, editbgdd, menuid, 0, 1, 1, mdid);
}

function FillBGDDTreeBase(treename, editbgdd, menuid, zk, sk, qx, mdid) {
    menu = menuid;
    //$("#" + editbgdd).attr("readonly", true);
    $("#" + editbgdd).bind('keypress', function () {
        if (event.keyCode == 13) {
            var zTree = $.fn.zTree.getZTreeObj(treename);
            var node = zTree.getNodeByParam("id", $("#" + editbgdd).val());
            if (node != null)
                onClick(null, zTree, node);
            return false;
        };
    });
    $("#" + editbgdd).click(function () {
        menu = menuid;
        var Obj = $("#" + editbgdd);
        var Offset = $("#" + editbgdd).offset();
        $("#" + menuid).css({ left: Offset.left + "px", top: Offset.top + Obj.outerHeight() + "px" }).slideDown("fast");
        $("body").bind("mousedown", onBodyDown);
    });
    PostToCrmlib("FillBGDDTree", { iRYID: iDJR, iMDID: mdid, iQX: qx, iZK: zk, iSK: sk }, function (data) {
        var zNodes = "[";
        for (var i = 0; i < data.length; i++) {
            zNodes = zNodes + "{id:'" + data[i].sBGDDDM + "',pId:'" + ((data[i].sPBGDDDM == "") ? "0" : data[i].sPBGDDDM) + "',name:'" + data[i].sBGDDMC + "',mdid:'" + data[i].iMDID + "',mjbj:'" + data[i].bMJBJ + "',xsbj:'" + data[i].bXS_BJ + "',zkbj:'" + data[i].bZK_BJ + "'}";
            if (i < data.length - 1)
                zNodes = zNodes + ",";
        }
        zNodes = zNodes + "]";
        $.fn.zTree.init($("#" + treename), setting, eval(zNodes));
    });
}