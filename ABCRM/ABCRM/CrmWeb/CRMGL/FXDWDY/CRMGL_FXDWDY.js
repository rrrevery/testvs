vUrl = "../CRMGL.ashx";
var vBMGZ = "22222";//编码规则
vCaption = "发行单位定义";

$(document).ready(function () {

});

function FillTree(pAsync) {
    PostToCrmlib("FillFXDWTree", {}, function (data) {
        if (data.length < 1) {
            var zNode = '{"id":"00","pid":"0","name":"暂无数据..."}';
            $.fn.zTree.init($("#TreeLeft"), setting, JSON.parse(zNode));
            return;
        }
        //将返回的数据组装成js数组对象。
        var zNodes = "[";
        for (var i = 0; i < data.length; i++) {
            zNodes = zNodes + "{id:'" + data[i].sFXDWDM + "',pId:'" + ((data[i].sPFXDWDM == "") ? "0" : data[i].sPFXDWDM) + "',fxdwmc:'" + data[i].sFXDWMC + "',name:'" + data[i].sFXDWQC + "',title:'" + data[i].iJLBH + "',mjbj:" + data[i].iMJBJ + ",bj_mr:" + data[i].iBJ_MR + ",bj_xsmd:" + data[i].iBJ_XSMD + "}";
            if (i < data.length - 1)
                zNodes = zNodes + ",";
        }
        zNodes = zNodes + "]";
        //zNodes = "[{id:1,pID:0,name:'aaaa'}]";
        //调用组件初始化函数。
        $.fn.zTree.init($("#TreeLeft"), setting, eval(zNodes));
        SetControlBaseState();
    }, pAsync);
}

function ShowData(treeNode) {
    $("#TB_CODE").inputmask("remove");
    $("#HF_JLBH").val(treeNode.title);
    $("#TB_NAME").val(treeNode.fxdwmc);
    $("#TB_CODE").val(treeNode.id);
    $("#CB_MJBJ").prop("checked", treeNode.mjbj == "1");
    //$("#CB_XSMD").prop("checked", treeNode.bj_xsmd == "1");
    $("#CB_MR").prop("checked", treeNode.bj_mr == "1");
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#HF_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sFXDWDM = $("#TB_CODE").val();
    Obj.sFXDWMC = $("#TB_NAME").val();
    Obj.iMJBJ = $("#CB_MJBJ")[0].checked ? "1" : "0";
    //Obj.iBJ_XSMD = $("#CB_XSMD")[0].checked ? "1" : "0";
    Obj.iBJ_MR = $("#CB_MR").prop("checked") ? "1" : "0";

    return Obj;
}

function IsValidData() {
    return true;
}