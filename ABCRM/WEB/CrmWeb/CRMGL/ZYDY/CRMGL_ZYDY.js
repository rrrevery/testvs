vUrl = "../CRMGL.ashx";
var vBMGZ = "22222";//编码规则
vCaption = "职业定义";

function SetControlState() {
    ;
}

$(document).ready(function () {
    ;
});

function FillTree(pAsync) {
    PostToCrmlib("FillZYLXTree", { iRYID: iDJR }, function (data) {
        if (data.length < 1) {
            var zNode = '{"id":"00","pid":"0","name":"暂无数据..."}';
            $.fn.zTree.init($("#TreeLeft"), setting, JSON.parse(zNode));
            return;
        }
        var zNodes = "[";
        for (var i = 0; i < data.length; i++) {
            zNodes = zNodes + "{id:'" + data[i].sZYDM + "',pId:'" + ((data[i].sPZYDM == "") ? "0" : data[i].sPZYDM) + "',name:'" + data[i].sZYQC + "',nameqc:'" + data[i].sZYMC
                + "',jlbh:'" + data[i].iJLBH + "',mjbj:'" + data[i].iBJ_MJ + "',tybj:'" + data[i].iBJ_TY + "'}";
            if (i < data.length - 1)
                zNodes = zNodes + ",";
        }
        zNodes = zNodes + "]";
        $.fn.zTree.init($("#TreeLeft"), setting, eval(zNodes));
        SetControlBaseState();
    }, pAsync);
}




function IsValidData() {
    if (!$("#CB_MJBJ").prop("checked") && $("#CB_TY").prop("checked")) {
        art.dialog({ lock: true, content: "只有末级地点允许停用" });
        return false;
    }
    return true;
}

function ShowData(treeNode) {
    $("#TB_CODE").inputmask("remove");
    $("#HF_JLBH").val(treeNode.jlbh);
    $("#TB_NAME").val(treeNode.nameqc);
    $("#TB_CODE").val(treeNode.id);
    $("#CB_MJBJ").prop("checked", treeNode.mjbj == "1");
    $("#CB_TY").prop("checked", treeNode.tybj == "1");
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#HF_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sZYDM = $("#TB_CODE").val();
    Obj.sZYMC = $("#TB_NAME").val();
    Obj.bMJBJ = $("#CB_MJBJ")[0].checked ? "1" : "0";
    Obj.bTY_BJ = $("#CB_TY").prop("checked") ? "1" : "0";
    return Obj;
}