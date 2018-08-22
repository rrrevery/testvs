vUrl = "../CRMGL.ashx";
vCaption = "发行渠道定义";


$(document).ready(function () {
    //
});

function FillTree(pAsync) {
    PostToCrmlib("FillFXQDTree", {}, function (data) {
        if (data.length < 1) {
            var zNode = '{"id":"00","pid":"0","name":"暂无数据..."}';
            $.fn.zTree.init($("#TreeLeft"), setting, JSON.parse(zNode));
            return;
        }
        var zNodes = "[";
        for (var i = 0; i < data.length; i++) {
            zNodes = zNodes + "{id:'" + data[i].sFXQDDM + "',pId:'" + ((data[i].sPFXQDDM == "") ? "0" : data[i].sPFXQDDM) + "',name:'" + data[i].sFXQDQC + "',fxdwmc:'" + data[i].sFXQDMC
                + "',jlbh:'" + data[i].iJLBH + "',mjbj:'" + data[i].bMJBJ + "'}";
            if (i < data.length - 1)
                zNodes = zNodes + ",";
        }
        zNodes = zNodes + "]";
        //zNodes = "[{id:1,pID:0,name:'aaaa'}]";
        $.fn.zTree.init($("#TreeLeft"), setting, eval(zNodes));
        SetControlBaseState();
    }, pAsync);
}



function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#HF_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iMJBJ = $("#CB_MJBJ")[0].checked ? "1" : "0";
    Obj.sFXQDDM = $("#TB_CODE").val();
    Obj.sFXQDMC = $("#TB_NAME").val();
    return Obj;
}


function ShowData(treeNode) {
    $("#HF_JLBH").val(treeNode.jlbh);
    $("#TB_CODE").inputmask('remove');
    $("#TB_NAME").val(treeNode.fxdwmc);
    $("#TB_CODE").val(treeNode.id);
    $("#CB_MJBJ").prop("checked", treeNode.mjbj == "1");
}

function IsValidData() {

    var treeObj = $.fn.zTree.getZTreeObj("TreeLeft");
    var node = treeObj.getNodeByParam("id", $("#TB_CODE").val());
    if (Edit && $("#TB_CODE").val() == treeObj.getSelectedNodes.id) {
        return true;
    }

    if (node != null && Edit == false) {
        art.dialog({ lock: true, content: "发行单位代码已存在！" });
        return false;
    }
    return true;
}