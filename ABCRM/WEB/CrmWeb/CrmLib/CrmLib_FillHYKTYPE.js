var settingHYK = {
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
        onClick: onHYKClick
    }
};
var menu;
function beforeClick(treeId, treeNode) {
    var check = (treeNode && treeNode.pId != null);
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

function FillHYKTYPETree(treename, editname, menuid, iMode, qx) {
    menu = menuid;
    //$("#" + editname).attr("readonly", true);
    //$("body").append("<div id='menuContentHYKTYPE' class='menuContent'><ul id='TreeHYKTYPE' class='ztree'></ul></div>");
    $("#" + editname).bind('keypress', function () {
        if (event.keyCode == 13) {
            var zTree = $.fn.zTree.getZTreeObj(treename);
            var node = zTree.getNodeByParam("id", $("#" + editname).val());
            //$("#" + editname).val(node.name);
            if (node != null)
                onHYKClick(null, zTree, node);
            return false;
        };
    });
    $("#" + editname).click(function () {
        menu = menuid;
        var Obj = $("#" + editname);
        var Offset = $("#" + editname).offset();
        $("#" + menuid).css({ left: Offset.left + "px", top: Offset.top + Obj.outerHeight() + "px" }).slideDown("fast");
        $("body").bind("mousedown", onBodyDown);
    });
    iMode = arguments[3] || 0;
    qx = arguments[4] || 1;
    PostToCrmlib("FillHYKTYPETree", { iRYID: iDJR, iMODE: iMode, iQX: qx }, function (data) {
        var zNodes = "[";
        for (var i = 0; i < data.length; i++) {
            zNodes = zNodes + "{id:'" + data[i].sHYKDM
                + "',pId:'" + ((data[i].sHYKPDM == "") ? "0" : data[i].sHYKPDM)
                + "',name:'" + data[i].sHYKNAME
                + "',kfje:'" + data[i].fKFJE
                + "',bj_xfsj:'" + data[i].iBJ_XFSJ
                + "'}";
            if (i < data.length - 1)
                zNodes = zNodes + ",";
        }
        zNodes = zNodes + "]";
        //zNodes = "[{id:1,pID:0,name:'aaaa'}]";
        $.fn.zTree.init($("#" + treename), settingHYK, eval(zNodes));
    });
    //var url = "../../CrmLib/CrmLib.ashx?func=FillHYKTYPETree&RYID=" + iDJR + "&MODE=" + iMode + "&QX=" + qx;
    //$.ajax({
    //    type: 'post',
    //    url: url,
    //    dataType: "json",
    //    success: function (data) {
    //        var zNodes = "[";
    //        for (var i = 0; i < data.length; i++) {
    //            zNodes = zNodes + "{id:'" + data[i].sHYKDM
    //                + "',pId:'" + ((data[i].sHYKPDM == "") ? "0" : data[i].sHYKPDM)
    //                + "',name:'" + data[i].sHYKNAME
    //                + "',kfje:'" + data[i].fKFJE
    //                + "',bj_xfsj:'" + data[i].iBJ_XFSJ
    //                + "'}";
    //            if (i < data.length - 1)
    //                zNodes = zNodes + ",";
    //        }
    //        zNodes = zNodes + "]";
    //        //zNodes = "[{id:1,pID:0,name:'aaaa'}]";
    //        $.fn.zTree.init($("#" + treename), settingHYK, eval(zNodes));
    //    }
    //});
}
function FillKLXByQX(treename, editname, menuid, iMode, personid) {
    menu = menuid;
    $("#" + editname).click(function () {
        menu = menuid;
        var Obj = $("#" + editname);
        var Offset = $("#" + editname).offset();
        $("#" + menuid).css({ left: Offset.left + "px", top: Offset.top + Obj.outerHeight() + "px" }).slideDown("fast");
        $("body").bind("mousedown", onBodyDown);
    });
    iMode = arguments[3] || 0;
    var url = "../../CrmLib/CrmLib.ashx?func=FillKLXByQX&param=" + iMode + "&personid=" + personid;
    $.ajax({
        type: 'post',
        url: url,
        dataType: "json",
        success: function (data) {
            var zNodes = "[";
            for (var i = 0; i < data.length; i++) {
                zNodes = zNodes + "{id:'" + data[i].sHYKDM
                    + "',pId:'" + ((data[i].sHYKPDM == "") ? "0" : data[i].sHYKPDM)
                    + "',name:'" + data[i].sHYKNAME
                    + "',kfje:'" + data[i].fKFJE
                    + "',bj_xfsj:'" + data[i].iBJ_XFSJ
                    + "'}";
                if (i < data.length - 1)
                    zNodes = zNodes + ",";
            }
            zNodes = zNodes + "]";
            //zNodes = "[{id:1,pID:0,name:'aaaa'}]";
            $.fn.zTree.init($("#" + treename), settingHYK, eval(zNodes));
        }
    });
}
