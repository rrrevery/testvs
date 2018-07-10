vUrl = "../YHQGL.ashx";

$(document).ready(function () {


    $("#ZZR").show();
    $("#ZZSJ").show();
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", "menuContentHYKTYPE", 1, iDJR);
    var date = new Date();
    $("#TB_KSRQ").val(FormatDate(date, "yyyy-MM-dd"));
    $("#TB_JSRQ").val(FormatDate(date, "yyyy-MM-dd"));
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });
    bStartBeforeStop = false;
});
function onHYKClick(e, treeId, treeNode) {
    $("#TB_HYKNAME").val(treeNode.name);
    $("#HF_HYKTYPE").val(treeNode.id);
    hideMenu("menuContentHYKTYPE");
};

function SetControlState() {
    $("#B_Stop").show();
    //终止控制
    if ($("#LB_ZXRQ").text() != "" && $("#LB_ZZRQ").text() == "") {
        $("#B_Stop").prop("disabled", false);
    }
}

function IsValidData() {
    if ($.trim($("#HF_HYKTYPE").val()) == "") {
        ShowMessage("请选择卡类型");
        return false;
    }
    if ($.trim($("#TB_DHJF").val()) == "") {
        ShowMessage("请输入兑换积分");
        return false;
    }
    if ($.trim($("#TB_DHJE").val()) == "") {
        ShowMessage("请输入兑换金额");
        return false;
    }
    if ($.trim($("#HF_MDID").val()) == "") {
        ShowMessage("请选择门店名称");
        return false;
    }
    if ($.trim($("#TB_KSRQ").val()) == "") {
        ShowMessage("请输入开始日期");
        return false;
    }
    if ($.trim($("#TB_JSRQ").val()) == "") {
        ShowMessage("请输入结束日期");
        return false;
    }

    return true;
}


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";

    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.fDHJF = $("#TB_DHJF").val();
    Obj.fDHJE = $("#TB_DHJE").val();
    Obj.fQDJF = $("#TB_QDJF").val() == "" ? 0 : $("#TB_QDJF").val();
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.iMDID = $("#HF_MDID").val();

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_HYKNAME").val(Obj.sHYKNAME);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TB_DHJF").val(Obj.fDHJF);
    $("#TB_DHJE").val(Obj.fDHJE);
    $("#TB_QDJF").val(Obj.fQDJF);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_MDMC").val(Obj.sMDMC);

    //停用人
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRQ").text(Obj.dZZRQ);
    //登记人
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    //审核人
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);

}