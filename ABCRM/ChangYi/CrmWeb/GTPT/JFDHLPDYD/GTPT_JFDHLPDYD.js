vUrl = "../GTPT.ashx";

function SetControlState() {
    if ($("#LB_ZZRMC").text() != "") {
        document.getElementById("B_Stop").disabled = true;
    }
    $("#B_Start").show();
    $("#B_Stop").show();
};

$(document).ready(function () {
    $("#ZZR").show();
    $("#ZZSJ").show();
    $("#QDR").show();
    $("#QDSJ").show();
    $("#B_Start").show();
    $("#B_Stop").show();

    BFButtonClick("TB_WXMDMC", function () {
        SelectWXMD("TB_WXMDMC", "HF_WXMDID", "zHF_WXMDID", true,iWXPID);
    });
    BFButtonClick("TB_GZMC", function () {
        SelectWX_JFDHLPGZ("TB_GZMC", "HF_GZID", "zHF_GZID", true);
    });
});

function IsValidData() {
    if ($("#TB_GZMC").val() == "") {
        ShowMessage("请选择规则");

        return false;
    }
    if (GetSelectValue("DDL_LX") == null || GetSelectValue("DDL_LX") == "") {
        ShowMessage("请选择单据类型");
        return false;
    }
    if ($("#HF_WXMDID").val()=="") {
        ShowMessage("请选择门店");
        return false;
    }
    if ($("#TB_KSRQ").val() == "") {
        ShowMessage("请选择开始日期");

        return false;
    }
    if ($("#TB_JSRQ").val() == "") {
        ShowMessage("请选择结束日期");

        return false;
    }
    if ($("#TB_LYYXQ").val() == "") {
        ShowMessage("请选择领奖有效期");
        return false;
    }
    if ($("#TB_KSRQ").val() > $("#TB_JSRQ").val()) {
        ShowMessage("开始日期不得大于结束日期");
        return false;
    }
    return true;
}
function StartClick() {
    ShowYesNoMessage("启动本单执行将会终止已启动的单据，是否继续？", function () {
        if (posttosever(SaveDataBase(), vUrl, "Start") == true) {
            vProcStatus = cPS_BROWSE;
            ShowDataBase(vJLBH);
            SetControlBaseState();
        }
    });
};


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iGZID = $("#HF_GZID").val();
    Obj.iDJLX = GetSelectValue("DDL_LX");
    //Obj.iMDID = GetSelectValue("DDL_MDID");

    Obj.iMDID = $("#HF_WXMDID").val();

    Obj.iCLFS = $("[name='CLFS']:checked").val();
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.dLJYXQ = $("#TB_LJYXQ").val();
    Obj.sBZ = $("#TB_BZ").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_GZID").val(Obj.iGZID);
    $("#TB_GZMC").val(Obj.sGZMC);
    $("#DDL_LX").val(Obj.iDJLX);
    $("[name='CLFS'][value='" + Obj.iCLFS + "']").prop("checked", true);
    $("#TB_BZ").val(Obj.sBZ);
    $("#HF_WXMDID").val(Obj.iMDID);
    $("#TB_WXMDMC").val(Obj.sMDMC);

    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#TB_LJYXQ").val(Obj.dLJYXQ);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#LB_QDRMC").text(Obj.sQDRMC);
    $("#HF_QDR").val(Obj.iQDR);
    $("#LB_QDSJ").text(Obj.dQDSJ);
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRQ").text(Obj.dZZRQ);
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID)

}