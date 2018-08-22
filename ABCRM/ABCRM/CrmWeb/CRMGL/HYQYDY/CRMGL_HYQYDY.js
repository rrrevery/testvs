vUrl = "../CRMGL.ashx";
var vBMGZ = "22222";//编码规则
vCaption = "会员区域定义";

function FillTree(pAsync) {
    PostToCrmlib("FillHYQY", { iMODE: 1 }, function (data) {
        if (data.length < 1) {
            var zNode = '{"id":"00","pid":"nodata","name":"暂无数据..."}';
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
        $.fn.zTree.init($("#TreeLeft"), setting, eval(zNodes));
        SetControlBaseState();
    }, pAsync);
}

$(document).ready(function () {
    ;
});

function IsValidData() {
    return true;
}

function ShowData(treeNode) {
    $("#HF_JLBH").val(treeNode.jlbh);
    $("#TB_CODE").inputmask('remove');
    $("#TB_NAME").val(treeNode.qymc);
    $("#TB_CODE").val(treeNode.id);
    $("#TB_YZBM").val(treeNode.yzbm);
    $("#CB_MJBJ").prop("checked", treeNode.mjbj == "1");
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#HF_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sQYDM = $("#TB_CODE").val();
    Obj.iBJ_MJ = $("#CB_MJBJ")[0].checked ? 1 : 0;
    Obj.sQYMC = $("#TB_NAME").val();
    Obj.sYZBM = $("#TB_YZBM").val();

    return Obj
}