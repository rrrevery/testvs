vUrl = "../KFPT.ashx";

$(document).ready(function () {


});


function SetControlState() {
    $("#jlbh").hide();
    $("#B_Stop").show();
    var bHasData = $("#TB_JLBH").val() != "";//有数据
    var bExecuted = $("#LB_ZXRMC").text() != "";//已审核
    document.getElementById("B_Stop").disabled = !(bHasData && bExecuted);
    if ($("#LB_ZXRMC").text() != "") {
        $("#ZZR").show();
        $("#ZZSJ").show();
    }
    if ($("#LB_ZZRMC").text() != "") {
        document.getElementById("B_Stop").disabled = true;

    }


}

function IsValidData() {

    if ($("#TB_RS").val() == "") {
        ShowMessage("预计人数不能为空");
        return false;
    }
    if ($("#TB_HDMC").val() == "") {
        ShowMessage("活动名称不能为空");
        return false;
    }
    if ($("#TB_KSSJ").val() == "") {
        ShowMessage("活动开始时间不能为空");
        return false;
    }
    if ($("#TB_JSSJ").val() == "") {
        ShowMessage("活动结束时间不能为空");
        return false;
    }
    if ($("#TB_KSSJ").val() > $("#TB_JSSJ").val()) {
        ShowMessage("开始时间不能大于结束时间");
        return false;
    }
    if ($("#TB_BM_RQ1").val() == "") {
        ShowMessage("报名开始时间不能为空");
        return false;
    }
    if ($("#TB_BM_RQ2").val() == "") {
        ShowMessage("报名结束时间不能为空");
        return false;
    }
    if ($("#TB_BM_RQ1").val() > $("#TB_BM_RQ2").val()) {
        ShowMessage("报名开始时间不能大于报名结束时间");
        return false;
    }

    //if ($("#TB_QR_RQ1").val() == "") {
    //    art.dialog({ content: "确认开始时间不能为空", lock: true, time: 2 });
    //    return false;
    //}
    //if ($("#TB_QR_RQ2").val() == "") {
    //    art.dialog({ content: "确认结束时间不能为空", lock: true, time: 2 });
    //    return false;
    //}
    //if ($("#TB_QR_RQ1").val() > $("#TB_QR_RQ2").val()) {
    //    art.dialog({ content: "确认开始时间不能大于确认结束时间", lock: true, time: 2 });
    //    return false;
    //}
    if ($("#TB_CJ_RQ1").val() == "") {
        ShowMessage("参加开始时间不能为空");
        return false;
    }
    if ($("#TB_CJ_RQ2").val() == "") {
        ShowMessage("参加结束时间不能为空");
        return false;
    }
    if ($("#TB_CJ_RQ1").val() > $("#TB_CJ_RQ2").val()) {
        ShowMessage("参加开始时间不能大于参加结束时间");
        return false;
    }

    if ($("#TB_HF_RQ1").val() == "") {
        ShowMessage("恢复开始时间不能为空");
        return false;
    }
    if ($("#TB_HF_RQ2").val() == "") {
        ShowMessage("恢复结束时间不能为空");
        return false;
    }
    if ($("#TB_HF_RQ1").val() > $("#TB_HF_RQ2").val()) {
        ShowMessage("恢复开始时间不能大于回复结束时间");
        return false;
    }
    if ($("#TB_PS_RQ1").val() == "") {
        ShowMessage("评述开始时间不能为空");
        return false;
    }
    if ($("#TB_PS_RQ2").val() == "") {
        ShowMessage("评述结束时间不能为空");
        return false;
    }
    if ($("#TB_PS_RQ1").val() > $("#TB_PS_RQ2").val()) {
        ShowMessage("评述开始时间不能大于评述结束时间");
        return false;
    }

    if ($("#TB_BM_RQ1").val() < $("#TB_KSSJ").val() || $("#TB_BM_RQ2").val() > $("#TB_JSSJ").val()) {
        ShowMessage("报名时间必须在活动时间范围之内");
        return false;
    }

    //if ($("#TB_QR_RQ1").val() < $("#TB_KSSJ").val() || $("#TB_QR_RQ2").val() > $("#TB_JSSJ").val()) {
    //    art.dialog({ content: "确认时间必须在活动时间范围之内", lock: true, time: 2 });
    //    return false;
    //}


    if ($("#TB_CJ_RQ1").val() < $("#TB_KSSJ").val() || $("#TB_CJ_RQ2").val() > $("#TB_JSSJ").val()) {
        ShowMessage("参加时间必须在活动时间范围之内");
        return false;
    }


    if ($("#TB_HF_RQ1").val() < $("#TB_KSSJ").val() || $("#TB_HF_RQ2").val() > $("#TB_JSSJ").val()) {
        ShowMessage("回访时间必须在活动时间范围之内");
        return false;
    }
    if ($("#TB_PS_RQ1").val() < $("#TB_KSSJ").val()) {
        ShowMessage("评述开始时间不能小于活动开始时间");
        return false;
    }

    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";

    Obj.sHDMC = $("#TB_HDMC").val();
    Obj.sHDNR = $("#TB_HDNR").val();
    Obj.iRS = $("#TB_RS").val();

    Obj.dKSSJ = $("#TB_KSSJ").val();
    Obj.dJSSJ = $("#TB_JSSJ").val();
    Obj.dBM_RQ1 = $("#TB_BM_RQ1").val();
    Obj.dBM_RQ2 = $("#TB_BM_RQ2").val();
    Obj.dQR_RQ1 = $("#TB_QR_RQ1").val();
    Obj.dQR_RQ2 = $("#TB_QR_RQ2").val();
    Obj.dCJ_RQ1 = $("#TB_CJ_RQ1").val();
    Obj.dCJ_RQ2 = $("#TB_CJ_RQ2").val();
    Obj.dHF_RQ1 = $("#TB_HF_RQ1").val();
    Obj.dHF_RQ2 = $("#TB_HF_RQ2").val();
    Obj.dPS_RQ1 = $("#TB_PS_RQ1").val();
    Obj.dPS_RQ2 = $("#TB_PS_RQ2").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);

    $("#TB_HDMC").val(Obj.sHDMC);
    $("#TB_HDNR").val(Obj.sHDNR);
    $("#TB_RS").val(Obj.iRS);
    $("#TB_KSSJ").val(Obj.dKSSJ);
    $("#TB_JSSJ").val(Obj.dJSSJ);

    $("#TB_BM_RQ1").val(Obj.dBM_RQ1);
    $("#TB_BM_RQ2").val(Obj.dBM_RQ2);
    $("#TB_QR_RQ1").val(Obj.dQR_RQ1);
    $("#TB_QR_RQ2").val(Obj.dQR_RQ2);
    $("#TB_CJ_RQ1").val(Obj.dCJ_RQ1);
    $("#TB_CJ_RQ2").val(Obj.dCJ_RQ2);
    $("#TB_HF_RQ1").val(Obj.dHF_RQ1);
    $("#TB_HF_RQ2").val(Obj.dHF_RQ2);
    $("#TB_PS_RQ1").val(Obj.dPS_RQ1);
    $("#TB_PS_RQ2").val(Obj.dPS_RQ2);
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
