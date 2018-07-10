var settingFXDW = {
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
        onClick: onFXDWClick
    }
};
var menu;
function beforeClick(treeId, treeNode) {
    var check = (treeNode && !treeNode.isParent);
    //if (!check) alert("只能选择城市...");
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

//function FillTree(treename) {
//    var url = "../../CrmLib/CrmLib.ashx?func=FillFXDWTree";
//    $.ajax({
//        url: url,
//        dataType: "json",
//        success: function (data) {
//            var zNodes = "[";
//            for (var i = 0; i < data.length; i++) {
//                zNodes = zNodes + "{id:'" + data[i].sBGDDDM + "',pId:'" + ((data[i].sPBGDDDM == "") ? "0" : data[i].sPBGDDDM) + "',name:'" + data[i].sBGDDMC + "'}";
//                if (i < data.length - 1)
//                    zNodes = zNodes + ",";
//            }
//            zNodes = zNodes + "]";
//            //zNodes = "[{id:1,pID:0,name:'aaaa'}]";
//            $.fn.zTree.init($("#" + treename), setting, eval(zNodes));
//        }
//    });
//}

function FillFXDWTree(treename, editbgdd, menuid, qx) {
    menu = menuid;
    $("#" + editbgdd).attr("readonly", true);
    $("#" + editbgdd).click(function () {
        menu = menuid;
        var Obj = $("#" + editbgdd);
        var Offset = $("#" + editbgdd).offset();
        $("#" + menuid).css({ left: Offset.left + "px", top: Offset.top + Obj.outerHeight() + "px" }).slideDown("fast");
        $("body").bind("mousedown", onBodyDown);
    });
    qx = arguments[3] || 1;
    PostToCrmlib("FillFXDWTree", { iRYID: iDJR, iQX: qx }, function (data) {
        var zNodes = "[";
        for (var i = 0; i < data.length; i++) {
            zNodes = zNodes + "{id:'" + data[i].sFXDWDM + "',pId:'" + ((data[i].sPFXDWDM == "") ? "0" : data[i].sPFXDWDM) + "',name:'" + data[i].sFXDWMC + "',data:'" + data[i].iJLBH + "'}";
            if (i < data.length - 1)
                zNodes = zNodes + ",";
        }
        zNodes = zNodes + "]";
        //zNodes = "[{id:1,pID:0,name:'aaaa'}]";
        $.fn.zTree.init($("#" + treename), settingFXDW, eval(zNodes));
    });
    //var url = "../../CrmLib/CrmLib.ashx?func=FillFXDWTree&RYID=" + iDJR + "&QX=" + qx;
    //$.ajax({
    //    type: 'post',
    //    url: url,
    //    dataType: "json",
    //    success: function (data) {
    //        var zNodes = "[";
    //        for (var i = 0; i < data.length; i++) {
    //            zNodes = zNodes + "{id:'" + data[i].sFXDWDM + "',pId:'" + ((data[i].sPFXDWDM == "") ? "0" : data[i].sPFXDWDM) + "',name:'" + data[i].sFXDWMC + "',data:'" + data[i].iJLBH + "'}";
    //            if (i < data.length - 1)
    //                zNodes = zNodes + ",";
    //        }
    //        zNodes = zNodes + "]";
    //        //zNodes = "[{id:1,pID:0,name:'aaaa'}]";
    //        $.fn.zTree.init($("#" + treename), settingFXDW, eval(zNodes));
    //    }
    //});
}