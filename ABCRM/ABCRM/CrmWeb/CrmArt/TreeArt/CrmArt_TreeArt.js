vCaption = $.dialog.data("dialogTitle");
vUrl = "../../HYKGL/HYKGL.ashx";
var vDialogName = $.dialog.data("dialogName");
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);
if ($.dialog.data("SingleSelect") != undefined)
    vSingleSelect = $.dialog.data("SingleSelect");
else
    vSingleSelect = false
if (vSingleSelect) {

    var setting = {
        check: {
            chkStyle: "radio",
            enable: true,
            radioType: "all",
        },
        data: {
            simpleData: {
                enable: true
            }
        },
        view: {
            showIcon: false,
        },
        callback: {
            onCheck: zTreeOnChecking
        }
    };

}
else {
    var setting = {
        check: {
            enable: true

        },
        data: {
            simpleData: {
                enable: true
            }
        },
        view: {
            showIcon: false,
        },

    };

}

function zTreeOnChecking(event, treeId, treeNode) {
    if (vDialogName == "TreeHYBQ") {
        var twostr=treeNode.id.substring(0, 2);
        if (twostr == "BB" || twostr == "AA") {
            var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
            treeObj.checkNode(treeNode, false, true);
            treeNode.chkDisabled = true;
        }
    }

};


var lstAlready = [];
var zTreeObj;
$(document).ready(function () {
    if ($.dialog.data("dialogInput")) {
        lstAlready = JSON.parse($.dialog.data("dialogInput"));
    }
    AddToolButtons("保存", "B_Save");
    AddToolButtons("取消", "B_Cancel");
    document.getElementById("B_Save").onclick = ArtSaveClick;
    document.getElementById("B_Cancel").onclick = ArtCancelClick;
    $(".btnsep:visible:last").hide();
    SearchData();
    $("#bftitle").html(vCaption);
});

function ArtSaveClick() {
    //T--确认事件
    var Rows = new Array();
    if (zTreeObj != null) {
        var nodesArray = zTreeObj.getCheckedNodes(true);
        for (var i = 0; i < nodesArray.length; i++) {
            var node = nodesArray[i];
            if (!vSingleSelect) {
                if (!node.isParent)
                    Rows.push(node);
            }
            else
                Rows.push(node);

        }
    }
    $.dialog.data(vDialogName, JSON.stringify(Rows));
    $.dialog.data('dialogSelected', Rows.length > 0);
    $.dialog.close();

};

function ArtCancelClick() {
    $.dialog.data('dialogSelected', false);
    $.dialog.close();
};

function SearchData() {
    var obj = MakeSearchJSON();
    $.ajax({
        type: "post",
        url: vUrl + "?mode=Print&func=" + vPageMsgID,
        async: true,
        data: {
            json: JSON.stringify(obj),
            titles: 'cybillsearch',
        },
        success: function (data) {
            if (data.indexOf('错误') >= 0 || data.indexOf('error') >= 0) {
                ShowMessage(data);
            }
            InitTree(data);
        },
        error: function (data) {
            ShowMessage(data);
        }
    });

}

function InitTree(data) {
    var zNodes = [{ id: "00", pId: "00", name: "" + vCaption + "", open: true, "nocheck": true }];
    if (data) {
        data = JSON.parse(data);
        for (var i = 0; i < data.length; i++) {
            var obj = new Object();
            obj.id = data[i].sID;
            obj.name = data[i].sTalName;
            obj.shortName = data[i].sName;
            obj.actid = data[i].iActId;
            //此处需要封装
            if (vDialogName == "TreeHYBQ") {
                obj.pId = data[i].sPID;
            }
            else {
                if (obj.id.length <= 2)
                    obj.pId = "00";
                else
                    obj.pId = obj.id.substr(0, obj.id.length - 2);
            }
            zNodes.push(obj);
        }
    }
    zTreeObj = $.fn.zTree.init($("#treeDemo"), setting, zNodes);
    checkTreeNodes();
}

function MakeSearchJSON() {
    var Obj = new Object();
    var cond = MakeSearchCondition();
    if (cond != null)
        Obj.SearchConditions = cond;
    Obj.iLoginRYID = iDJR;
    AddCustomerCondition(Obj);
    return Obj;
}

function MakeSearchCondition() {
    ;
}

function AddCustomerCondition(Obj) {
    Obj.sDialogName = vDialogName;
    if (data) {
        if (data.sSHDM)
            Obj.sSHDM = data.sSHDM;
        if (data.iLABELLX == 0 || data.iLABELLX == 1)
            Obj.iLABELLX = data.iLABELLX;
    }


}

function checkTreeNodes() {
    if (lstAlready == null) {
        return;
    }
    var nodes = zTreeObj.transformToArray(zTreeObj.getNodes());
    for (var i = 0; i < nodes.length; i++) {
        for (var j = 0; j < lstAlready.length; j++) {
            if ((lstAlready[j].id == nodes[i].id) && ((lstAlready[j].pId == nodes[i].pId) || (lstAlready[j].shortName == nodes[i].shortName))) {
                zTreeObj.checkNode(nodes[i], true, true);
            }
        }
    }
}