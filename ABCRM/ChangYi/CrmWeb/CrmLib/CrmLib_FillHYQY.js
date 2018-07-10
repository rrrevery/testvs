var setting = {
    view: {
        showIcon: false,
    },
    data: {
        simpleData: {
            enable: true,
            idKey: "id",
            pIdKey: "pId"
        }
    },
    callback: {
        //beforeClick: beforeClick,
        onClick: onClick
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

function onBodyDown(event, menuid) {
    if (!(event.target.id == "menuBtn" || event.target.id == menu || $(event.target).parents("#" + menu).length > 0)) {
        hideMenu(menuid);
    }
}

function FillQYTree(treename, input) {
    menu = "menuContent";
    $("#" + input).click(function () {
        menu = "menuContent";
        var Obj = $("#" + input);
        var Offset = $("#" + input).offset();
        $("#menuContent").css({ left: Offset.left + "px", top: Offset.top + Obj.outerHeight() + "px" }).slideDown("fast");
        $("body").bind("mousedown", onBodyDown);
    });

    PostToCrmlib("FillHYQY", {}, function (data) {
        if (data.length < 1) {
            var zNode = '{"id":"00","pid":"0","name":"暂无会员区域数据..."}';
            $.fn.zTree.init($("#TreeLeft"), setting, JSON.parse(zNode));
            return;
        }
        var zNodes = "[";
        for (var i = 0; i < data.length; i++) {
            zNodes = zNodes + "{id:'" + data[i].sQYDM + "',pId:'" + ((data[i].sPQYDM == "") ? "0" : data[i].sPQYDM) + "',name:'" + data[i].sQYQC + "',qymc:'" + data[i].sQYMC + "',yzbm:'" + data[i].sYZBM + "',jlbh:'" + data[i].iJLBH + "',mjbj:" + data[i].iBJ_MJ + "}";
            if (i < data.length - 1)
                zNodes = zNodes + ",";
        }
        zNodes = zNodes + "]";
        //zNodes = "[{id:1,pID:0,name:'aaaa'}]";
        $.fn.zTree.init($("#" + treename), setting, eval(zNodes));
        SetControlBaseState();
    }, false);
}