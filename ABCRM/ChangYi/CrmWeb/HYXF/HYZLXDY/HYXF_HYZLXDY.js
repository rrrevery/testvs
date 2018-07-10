//vPageMsgID = CM_HYXF_HYZLXDY;
vUrl = "../HYXF.ashx";
var vBMGZ = "22222";//编码规则

$(document).ready(function () {

});

function SetControlState() {
    ;
}

function FillTree(pAsync) {
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
    }, pAsync);
}

//function FillTree(pAsync) {
//    var url = "../../CrmLib/CrmLib.ashx?func=FillHYZLXTree";

//    $.ajax({
//        url: url,
//        type: 'post',
//        dataType: "json",
//        async: pAsync,
//        success: function (data) {
//            if (data.length < 1) {
//                var zNode = '{"id":"00","pid":"0","name":"暂无数据..."}';
//                $.fn.zTree.init($("#TreeLeft"), setting, JSON.parse(zNode));
//                return;
//            }
//            var zNodes = "[";
//            for (var i = 0; i < data.length; i++) {
//                zNodes = zNodes + "{id:'" + data[i].sHYZLXDM + "',pId:'" + ((data[i].sPHYZLXDM == "") ? "0" : data[i].sPHYZLXDM) + "',name:'" + data[i].sHYZLXDM + "'+' '+'" + data[i].sHYZLXMC + "',mjbj:'" + data[i].iBJ_MJ + "',lxid:'" + data[i].iHYZLXID + "',nodeName:'" + data[i].sHYZLXMC + "'}";

//                if (i < data.length - 1) {
//                    zNodes = zNodes + ",";
//                }
//            }

//            zNodes = zNodes + "]";
//            $.fn.zTree.init($("#TreeLeft"), setting, eval(zNodes));

//        }
//    });
//}

function ShowData(treeNode) {
    $("#TB_CODE").inputmask("remove");
    $("#HF_JLBH").val(treeNode.lxid);
    $("#TB_NAME").val(treeNode.nodeName);
    $("#TB_CODE").val(treeNode.id);
    $("#CB_MJBJ").prop("checked", treeNode.mjbj == "1");

}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#HF_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sHYZLXDM = $("#TB_CODE").val();
    Obj.sHYZLXMC = $("#TB_NAME").val();
    Obj.iBJ_MJ = $("#CB_MJBJ")[0].checked ? "1" : "0";

    return Obj;
}

function IsValidData() {
    return true;
}
