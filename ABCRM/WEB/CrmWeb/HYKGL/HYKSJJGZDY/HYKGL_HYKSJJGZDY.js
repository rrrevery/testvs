vUrl = "../HYKGL.ashx";
var sj = GetUrlParam("sj");
var newtype = GetUrlParam("HYKTYPE_NEW");
var oldtype = GetUrlParam("HYKTYPE_OLD");
var xfsj = GetUrlParam("XFSJ");
var mdid = GetUrlParam("MDID");//只记录传过来的mdid，本页面选择的mdid，记录在hf_mdid控件当中
//var isFirst = true;//记录是否是第一次加载页面

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();

    //$("#TB_MDMC").click(function () {
    //    SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    //})

    FillHYKTYPETree("TreeHYKTYPE_OLD", "TB_HYKNAME_OLD");
    if (newtype != "") {
        bCanEdit = true;
    }


    $("#S_HYKNAME_NEW").change(function () {
        $("#HF_HYKTYPE_NEW").val($(this).val());
    });
    $('input[name="CB_BJ_XFJE"]').click(function () {     
        $(this).blur();
        $(this).focus();
        ToggleDiv(this.value);
    });

});

function ToggleDiv(value) {
    if (value == 0) {
        $("#div_qdjf").show();
        $("#div_drxfje").hide();
    }
    else if (value == 1) {
        $("#div_qdjf").hide();
        $("#div_drxfje").show();
    }
    else {
        $("#div_qdjf").hide();
        $("#div_drxfje").hide();
    }
    $("#TB_DRXFJE").val("");
    $("#TB_QDJF").val("");
}
function SetControlState() {

    if (vProcStatus == 1) {//点击添加按钮
        newtype = "";
    }
    if (newtype != "") {
        $("#TB_HYKNAME_OLD").prop("disabled", true);
        $("#S_HYKNAME_NEW").hide().prop("disabled", true);
        $("#TB_HYKNAME_NEW").show().prop("disabled", true);
    }
    else {
        $("#TB_HYKNAME_OLD").prop("disabled", false);
        $("#S_HYKNAME_NEW").show().prop("disabled", false);
        $("#TB_HYKNAME_NEW").hide().prop("disabled", true);
    }

}
function IsValidData() {
    if ($("#HF_HYKTYPE_OLD").val() == "") {
        ShowMessage("请选择原卡类型");
        return false;
    }
    if ($("#HF_HYKTYPE_NEW").val() == "") {
        ShowMessage("请选择新卡类型");
        return false;
    }
    var bj_xfje = $("[name='CB_BJ_XFJE']:checked").val();
    if (bj_xfje == 0 && $("#TB_QDJF").val() == "") {
        ShowMessage("请输入起点积分");
        return false;
    }
    if (bj_xfje == 1 && $("#TB_DRXFJE").val() == "") {
        ShowMessage("请输入当日消费金额");
        return false;
    }

    return true;
}
function SaveData() {
    var Obj = new Object();
    if (newtype != "") {
        //修改时，用来作为删除和查询的标记
        Obj.iHYKTYPE_OLD = oldtype;
        Obj.iHYKTYPE_NEW = newtype;
        Obj.iXFSJ = xfsj;
        Obj.iMDID = mdid;
    }
    Obj.iJLBH = $("#HF_HYKTYPE_OLD").val();
    Obj.iHYKTYPE_OLD = $("#HF_HYKTYPE_OLD").val();
    Obj.iHYKTYPE_NEW = $("#HF_HYKTYPE_NEW").val();
    Obj.iBJ_XFJE = $("[name='CB_BJ_XFJE']:checked").val();
    Obj.fQDJF = $("#TB_QDJF").val();
    Obj.fDRXFJE = $("#TB_DRXFJE").val();
    Obj.iBJ_SJ = sj;
    newtype = Obj.iHYKTYPE_NEW;
    oldtype = Obj.iHYKTYPE_OLD;
    xfsj = Obj.iXFSJ;
   // mdid = Obj.iMDID;
    return Obj;
}

function ShowData(data) {
    var Obj = new Object();
    Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_HYKNAME_OLD").val(Obj.sHYKNAME);
    $("#HF_HYKTYPE_OLD").val(Obj.iHYKTYPE_OLD);

    $("#S_HYKNAME_NEW").val(Obj.iHYKTYPE_NEW);
    $("#TB_HYKNAME_NEW").val(Obj.sHYKNAME_NEW);
    $("#HF_HYKTYPE_NEW").val(Obj.iHYKTYPE_NEW);

    $("[name='CB_BJ_XFJE'][value='" + Obj.iBJ_XFJE + "']").prop("checked", true);
    ToggleDiv(Obj.iBJ_XFJE);
    $("#HF_BJ_SJ").val(Obj.iBJ_SJ);
    $("#TB_DRXFJE").val(Obj.fDRXFJE);
    $("#TB_QDJF").val(Obj.fQDJF);

    //保存最新的标识(一般是双击查看某一项详细时，再点击添加按钮时，并保存以后的新记录的标识)
    newtype = Obj.iHYKTYPE_NEW;
    oldtype = Obj.iHYKTYPE_OLD;
    xfsj = Obj.iXFSJ;
    //mdid = Obj.iMDID;
}

function TreeNodeClickCustom(e, treeId, treeNode) {
    if (treeNode.pId != "0")//非0才是卡类型 id是hyktype
    {
        FillKSJHYKTYPE("S_HYKNAME_NEW", treeNode.id, sj);
        $("#TB_HYKNAME_OLD").val(treeNode.name);
        $("#HF_HYKTYPE_OLD").val(treeNode.id);
        hideMenu("menuContentHYKTYPE_OLD");
    }
};

//可升（降）到的,同卡种的卡类型绑定 
//参数:  hyktype  sj= 1 0  升级  降级
function FillKSJHYKTYPE(selectname, phyktype, psj) {
    var Params = { iHYKTYPE: phyktype, iSJ: psj };
    var url = "../../CrmLib/CrmLib.ashx?func=FillKSJHYKTYPE";
    var str = "<option ></option>";
    $.ajax({
        type: 'post',
        url: url,
        async: false,
        data: { json: JSON.stringify(Params) },
        success: function (data) {
            if (data == "") {
                art.dialog({ content: '没有符合条件的新卡类型', lock: true, time: 2 });
                return;
            }
            if (data.indexOf("错误") == "0") {
                art.dialog({ content: data, lock: true });
                return;
            }
            data = JSON.parse(data);
            for (i = 0; i < data.length; i++) {
                str += "<option value='" + data[i].iHYKTYPE + "'>" + data[i].sHYKNAME + "</option>";
            }
            $("#" + selectname).html(str);
        },
        error: function (data) {
            art.dialog({ content: data, lock: true });
        }
    });
}
//根据newtype oldtype  xfsj mdid 确定一条记录

function MakeJLBH(t_jlbh) {
    var arrayObj = new Array();
    //   MakeSrchCondition2(arrayObj, t_jlbh, "iJLBH", "=", false);
    var iHYKTYPE_OLD = $("#HF_HYKTYPE_OLD").val() || oldtype;
    MakeSrchCondition2(arrayObj, iHYKTYPE_OLD, "iHYKTYPE_OLD", "=", false);
    var iHYKTYPE_NEW = $("#HF_HYKTYPE_NEW").val() || newtype || NEWTYPE;
    MakeSrchCondition2(arrayObj, iHYKTYPE_NEW, "iHYKTYPE_NEW", "=", false);
    // var iXFSJ = isFirst ? xfsj : ($("#HF_XFSJ").val() || xfsj);
    //MakeSrchCondition2(arrayObj, iXFSJ, "iXFSJ", "=", false);
    //var iMDID = $("#HF_MDID").val() || mdid;
    //MakeSrchCondition2(arrayObj, iMDID, "iMDID", "=", false);
    return arrayObj;
}

