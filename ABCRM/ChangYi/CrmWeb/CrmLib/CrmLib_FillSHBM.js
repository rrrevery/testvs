var settingSHBM = {
    view: {
        showIcon: false
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    callback: {
        //beforeClick: beforeClickSHBM,
        onClick: onClick
    }
};
var menu;
function beforeClickSHBM(treeId, treeNode) {
    var check = (treeNode && !treeNode.isParent);
    return check;
}

function hideMenuSHBM(menuid) {
    $("#" + menuid).fadeOut("fast");
    $("body").unbind("mousedown", onBodyDown);
}

function onBodyDown(event) {
    if (!(event.target.id == "menuBtn" || event.target.id == menu || $(event.target).parents("#" + menu).length > 0)) {
        hideMenuSHBM(menu);
    }
}

function FillSHBMTreeBase(treename, editbgdd, menuid, shdm, lvl) {
    menu = menuid;
    //$("#" + editbgdd).attr("readonly", true);
    $("#" + editbgdd).click(function () {
        menu = menuid;
        var Obj = $("#" + editbgdd);
        var Offset = $("#" + editbgdd).offset();
        $("#" + menuid).css({ left: Offset.left + "px", top: Offset.top + Obj.outerHeight() + "px" }).slideDown("fast");
        $("body").bind("mousedown", onBodyDown);
    });

    PostToCrmlib("FillTreeSHBM", { iRYID: iDJR, sSHDM: shdm, iLEVEL: lvl }, function (data) {
        $.fn.zTree.init($("#" + treename), settingSHBM, data);
        //var zNodes = "[";
        //for (var i = 0; i < data.length; i++) {
        //    zNodes = zNodes + "{id:'" + data[i].sDM + "',pId:'" + ((data[i].sPDM == "") ? "0" : data[i].sPDM) + "',name:'" + data[i].sMC + "'}";
        //    if (i < data.length - 1)
        //        zNodes = zNodes + ",";
        //}
        //zNodes = zNodes + "]";
        ////zNodes = "[{id:1,pID:0,name:'aaaa'}]";
        //$.fn.zTree.init($("#" + treename), settingSHBM, eval(zNodes));
    });
}
function hideMenu(menuid) {
    $("#" + menuid).fadeOut("fast");
    $("body").unbind("mousedown", onBodyDown);
}
