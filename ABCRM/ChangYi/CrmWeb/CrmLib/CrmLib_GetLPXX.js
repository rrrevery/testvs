//lpsxlx 0 材质 1 颜色 2 款式
function FillLPSX(selectName, lpsxlx) {
    sjson = "{'iLPSXLX':'" + lpsxlx + "'}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillLPSX",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iJLBH + "'>" + data[i].sLPSXNR + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}
function FillLPFLDEF(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillLPFLDEF",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iLPFLID + "'>" + data[i].sLPFLMC + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}

function FillLP(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillLP",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iJLBH + "'>" + data[i].sLPMC + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}
function beforeClick(treeId, treeNode) {
    var check = (treeNode.mjbj == "1");
    return check;
}
function hideMenu(menuid) {
    $("#" + menuid).fadeOut("fast");
}
function FillLPFLTree(treename, editname, menuid,iBJ_TY) {
    var settingLPFL = {
        view: {
            showIcon: false
        },
        data: {
            simpleData: {
                enable: true
            }
        },
        callback: {
            //beforeClick: beforeClick,
            onClick: onLPFLClick
        }
    };
    menu = menuid;
    $("#" + editname).click(function () {
            menu = menuid;
            var Obj = $("#" + editname);
            var Offset = $("#" + editname).offset();
            $("#" + menuid).css({ left: Offset.left + "px", top: Offset.top + Obj.outerHeight() + "px" }).slideDown("fast");

    });

    PostToCrmlib("FillLPFLTree", {iBJ_TY:iBJ_TY }, function (data) {
        var zNodes = "[";
        for (var i = 0; i < data.length; i++) {
            zNodes = zNodes + "{id:'" + data[i].sLPFLDM + "',pId:'" + ((data[i].sPLPFLDM == "") ? "0" : data[i].sPLPFLDM) + "',lpflid:'" + data[i].iLPFLID + "',mjbj:'" + data[i].iBJ_MJ + "',name:'" + data[i].sLPFLMC + "'}";
            if (i < data.length - 1)
                zNodes = zNodes + ",";
        }
        zNodes = zNodes + "]";
        $.fn.zTree.init($("#" + treename), settingLPFL, eval(zNodes));
    });

}