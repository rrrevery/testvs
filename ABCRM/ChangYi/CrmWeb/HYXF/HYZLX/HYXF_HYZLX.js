//vPageMsgID = CM_HYXF_HYZLXDY;
vUrl = "../HYXF.ashx";
var hyzlxid = "";
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


function InitGrid() {
    vColumnNames = ['记录编号', '客群组类型', '客群组编号', '客群组名称', ];
    vColumnModel = [
            { name: 'iJLBH' },
            { name: 'sHYZLXMC', width: 150, },
            { name: 'iGRPID', width: 150 },
            { name: 'sGRPMC', width: 150 },
    ];
};
$(document).ready(function () {
    FillTree();
    $("input[type='checkbox']").change(function () {
        var hf = $(this).attr("name").replace("CB", "HF");
        var s = $("#" + hf).val();
        if (this.checked) {
            if (s == "") {
                s += $(this).val();
            }
            else {
                s += "," + $(this).val();
            }
        }
        else {
            if (s.length <= 2) {
                s = "";  // -1  1这两种情况
            }
            else {
                s = s.replace(',' + $(this).val(), ""); // -1,0 去除0
                s = s.replace($(this).val() + ',', "");// 0,-1
            }

        }
        $("#" + hf).val(s);
        $(this).prop("checked", this.checked);
        SearchData();
    });
});

function SetControlState() {
    ;
}

function FillTree() {
    PostToCrmlib("FillHYZLXTree", {}, function (data) {
        if (data.length < 1) {
            var zNode = '{"id":"00","pid":"0","name":"暂无数据..."}';
            $.fn.zTree.init($("#TreeLeft"), setting, JSON.parse(zNode));
            return;
        }
        var zNodes = "[";
        for (var i = 0; i < data.length; i++) {
            zNodes = zNodes + "{id:'" + data[i].sHYZLXDM + "',pId:'" + ((data[i].sPHYZLXDM == "") ? "0" : data[i].sPHYZLXDM) + "',name:'" + data[i].sHYZLXDM + "'+' '+'" + data[i].sHYZLXMC + "',mjbj:'" + data[i].iBJ_MJ + "',lxid:'" + data[i].iHYZLXID + "',nodeName:'" + data[i].sHYZLXMC + "'}";

            if (i < data.length - 1) {
                zNodes = zNodes + ",";
            }
        }
        zNodes = zNodes + "]";
        //zNodes = "[{id:1,pID:0,name:'aaaa'}]";
        $.fn.zTree.init($("#TreeLeft"), setting, eval(zNodes));
        SetControlBaseState();
    }, true);
}

function beforeClick(treeId, treeNode) {

    return  !treeNode.isParent;
}

function onClick(e, treeId, treeNode) {
    $('#list').datagrid('loadData', { total: 0, rows: [] });
    hyzlxid = treeNode.lxid
    //$("#TB_HYZLXID").val(treeNode.lxid);
    SearchData();
    //window.setTimeout(function ()
    //{ SearchData(); }, 500);
    
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    if (hyzlxid != "") {
        MakeSrchCondition2(arrayObj, hyzlxid, "iHYZLXID", "=", false);
    }
    MakeSrchCondition(arrayObj, "HF_STATUS", "iSTATUS", "in", false);    
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}



function IsValidData() {
    return true;
}
