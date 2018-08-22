vUrl = "../LPGL.ashx";
var vBMGZ = "22222";//编码规则
vCaption = "礼品分类定义";

$(document).ready(function () {

});

function SetControlState() {
    ;
}

function FillTree(pAsync) {
    PostToCrmlib("FillLPFLTree", {}, function (data) {
        if (data.length < 1) {
            var zNode = '{"id":"00","pid":"0","name":"暂无数据..."}';
            $.fn.zTree.init($("#TreeLeft"), setting, JSON.parse(zNode));
            return;
        }
        //将返回的数据组装成js数组对象。
        var zNodes = "[";
        for (var i = 0; i < data.length; i++) {
            zNodes = zNodes + "{id:'" + data[i].sLPFLDM + "',pId:'" + ((data[i].sPLPFLDM == "") ? "0" : data[i].sPLPFLDM) + "',name:'" + data[i].sLPFLQC + "',jlbh:'" + data[i].iJLBH + "',lpflmc:'" + data[i].sLPFLMC + "',tybj:'" + data[i].iBJ_TY + "',mjbj:'" + data[i].iBJ_MJ + "'}";
            if (i < data.length - 1)
                zNodes = zNodes + ",";
        }
        zNodes = zNodes + "]";
        //调用组件初始化函数。
        $.fn.zTree.init($("#TreeLeft"), setting, eval(zNodes));
        SetControlBaseState();
    }, pAsync);

}

function ShowData(treeNode) {
    $("#TB_CODE").inputmask("remove");
    $("#HF_JLBH").val(treeNode.jlbh);
    $("#TB_NAME").val(treeNode.lpflmc);
    $("#TB_CODE").val(treeNode.id);
    $("#CB_MJBJ").prop("checked", treeNode.mjbj == "1");
    $("#CB_BJ_TY").prop("checked", treeNode.tybj == "1");
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#HF_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sLPFLDM = $("#TB_CODE").val();
    Obj.sLPFLMC = $("#TB_NAME").val();
    Obj.iBJ_MJ = $("#CB_MJBJ")[0].checked ? "1" : "0";
    Obj.iBJ_TY = $("#CB_BJ_TY").prop("checked") ? 1 : 0;
    return Obj;
}

function IsValidData() {
    return true;
}
