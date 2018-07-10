//vPageMsgID = CM_CRMGL_ZFFSDY;
vUrl = "../CRMGL.ashx";
var vBMGZ = "22222";//编码规则
vCaption = "支付方式定义";

var addChildren = false;
var Edit = false;
var update = false;


function SetControlState() {
    ;
}

$(document).ready(function () {
    $("#mjbj .bffld_left").text("CRM收款标记");
    $("label[for='CB_MJBJ']").text("发售卡中是否可以使用该支付方式");
    $("#TB_TYPE").change(function () {
        switch ($("#TB_TYPE").val()) {
            case "0": $("#TA_SM").val("一般收款：无特殊处理的支付方式。");
                break;
            case "1": $("#TA_SM").val("对支持银行联网的系统：使用银行信用卡，与银行联网使用。");
                break;
            case "2": $("#TA_SM").val("使用储值卡刷卡。");
                break;
            case "3": $("#TA_SM").val("使用商场发行的优惠券，多余的收款不找零，对应的销售不记入报表的“销售金额”，而是记入“优惠金额”项。");
                break;
            case "4": $("#TA_SM").val("使用商场发行的卡保存的优惠券金额,对应的销售不记入报表的“销售金额”，而是记入“优惠金额”项。");
                break;
            case "5": $("#TA_SM").val("使用商场定义的IC卡刷卡。");
                break;
            case "6": $("#TA_SM").val("使用支票进行支付。");
                break;
            case "7": $("#TA_SM").val("使用 银行卡(国内) 进行支付。");
                break;
            case "8": $("#TA_SM").val("使用 银行卡(国外) 进行支付。");
                break;
            case "9": $("#TA_SM").val("使用 信贷卡 进行支付。");
                break;
            case "10": $("#TA_SM").val("");
                break;
            case "11": $("#TA_SM").val("使用 承兑 进行支付");
                break;
            default: $("#TA_SM").val("");
                break;
        }
    })
});

//从数据库查询出数据，进行数据绑定
function FillTree(async) {
    PostToCrmlib("FillZFFSTree", {}, function (data) {
        if (data.length < 1) {
            var zNode = '{"id":"00","pid":"nodata","name":"暂无数据..."}';
            $.fn.zTree.init($("#TreeLeft"), setting, JSON.parse(zNode));
            return;
        }
        //返回数据，对数据进行绑定。
        //PID用来实现子级关系 ,在数据库中是比较代码前缀。
        var zNodes = "[";
        for (var i = 0; i < data.length; i++) {
            zNodes = zNodes + "{id:'" + data[i].sZFFSDM + "',pId:'" + ((data[i].PZFFSDM == "") ? 0 : data[i].sPZFFSDM) + "',name:'" + data[i].sZFFSQC + "',data:'" + data[i].iJLBH + "',bj_mj:'" + data[i].iBJ_MJ + "',type:'" + data[i].iTYPE + "',jlbh:'"
                + data[i].iZFFSID + "',zffsmc:'" + data[i].sZFFSMC + "',bj_dzqdczk:" + data[i].iBJ_DZQDCZK + ",iBJ_KF:" + data[i].iBJ_KF + ",iBJ_XSMD:" + data[i].iBJ_XSMD + "}";
            if (i < data.length - 1)
                zNodes = zNodes + ",";
        }
        zNodes = zNodes + "]";//将zNodes组装成js数组数据格式。每一个数组元素是一个对象。
        //zNodes = "[{id:1,pID:0,name:'aaaa'}]";
        $.fn.zTree.init($("#TreeLeft"), setting, eval(zNodes));
        SetControlBaseState();
    }, async);
}

//事件处理函数
function onClick(e, treeId, treeNode) {
    Edit = false;
    setControlEnabled(true);
    $("#TB_ZFFSDM").inputmask('remove');
    $("#HF_ZFFSID").val(treeNode.jlbh);
    $("#TB_ZFFSMC").val(treeNode.zffsmc);
    $("#TB_ZFFSDM").val(treeNode.id);

    if (treeNode.bj_mj == "1") {
        $("#CK_MJBJ").prop("checked", true);
        $("#BTN_ADDXIAJI").prop("disabled", true);
    }
    else {
        $("#BTN_ADDXIAJI").prop("disabled", false);
        $("#CK_MJBJ").prop("checked", false);
    }

    if (treeNode.BJ_DZQDCZK == 1) {
        $("#CB_BJ_DZQDCZK").prop("checked", true);
    }
    else {
        $("#CB_BJ_DZQDCZK").prop("checked", false);
    }
    $("#TB_TYPE").val(treeNode.type);
    $("#TB_TYPE").change();
    $("#BTN_ADDTONGJI").css("background-color", "rgb(60, 148, 210)");
    $("#BTN_ADDXIAJI").css("background-color", "rgb(60, 148, 210)");
    $("#BTN_DELETE").prop("disabled", treeNode.isParent);
}

function IsValidData() {
    if ($("#TB_TYPE").val() == "") {
        ShowMessage("请选择支付方式类型");
        return false;
    }
    var treeObj = $.fn.zTree.getZTreeObj("TreeLeft");
    var node = treeObj.getNodeByParam("id", $("#TB_ZFFSDM").val());
    if (node != null && !Edit) {
        ShowMessage("支付方式代码已存在！");
        return false;
    }
    return true;
}
function ShowData(treeNode) {
    $("#TB_CODE").inputmask("remove");
    $("#HF_JLBH").val(treeNode.jlbh)
    $("#TB_NAME").val(treeNode.zffsmc);
    $("#TB_CODE").val(treeNode.id);
    $("#TB_TYPE").val(treeNode.type);
    $("#CB_MJBJ").attr("checked", treeNode.bj_mj == "1");
    $("#CB_BJ_DZQDCZK").attr("checked", treeNode.bj_dzqdczk == "1");
    //   $("#CB_XSMD").prop("checked", treeNode.iBJ_XSMD == "1");
    //$("#DDL_ZFLX").val(treeNode.iBJ_XSMD);
   // $("#DDL_KF").val(treeNode.iBJ_KF);
    //$("#CB_KF").prop("checked", treeNode.iBJ_KF == "1");
    $("TA_SM").val(treeNode.sm);
}
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#HF_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sZFFSDM = $("#TB_CODE").val();
    Obj.sZFFSMC = $("#TB_NAME").val();
    Obj.iTYPE = $("#TB_TYPE")[0].value;
    Obj.iBJ_MJ = $("#CB_MJBJ")[0].checked ? "1" : "0";
    Obj.iBJ_DZQDCZK = $("#CB_BJ_DZQDCZK")[0].checked ? "1" : "0";
   // Obj.iBJ_XSMD = GetSelectValue("DDL_ZFLX")// $("#CB_XSMD")[0].checked ? "1" : "0";
   // Obj.iBJ_KF = GetSelectValue("DDL_KF");
    return Obj;

}