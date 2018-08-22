vUrl = "../GTPT.ashx";
var irow = 0;
$(document).ready(function () {
    BFButtonClick("TB_GZMC", function () {
        SelectWXYHQGZ("TB_GZMC", "HF_GZID", "zHF_GZID", false);
    });

})

function SetControlState() {
    $("#B_Stop").show();
    $("#B_Start").show();
    $("#ZZR").show();
    $("#ZZSJ").show();
    $("#QDR").show();
    $("#QDSJ").show();
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.iGZID = $("#HF_GZID").val();

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
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
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#TB_GZMC").val(Obj.sGZMC);
    $("#HF_GZID").val(Obj.iGZID);

    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#HF_QDR").val(Obj.iQDR);
    $("#LB_QDRMC").text(Obj.sQDRMC);
    $("#LB_QDSJ").text(Obj.dQDSJ);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#LB_ZZRQ").text(Obj.dZZRQ);
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID)


}


function IsValidData() {
    if ($("#HF_GZID").val() == "") {
        ShowMessage("请选择规则名称", 3);
        return false;
    } if ($("#TB_JSRQ").val() == "") {
        ShowMessage("请输入选择结束日期", 3);
        return false;
    }
    if ($("#TB_KSRQ").val() == "") {
        ShowMessage("请输入选择开始日期", 3);
        return false;
    }
    if ($("#TB_KSRQ").val() > $("#TB_JSRQ").val()) {
        ShowMessage("开始日期不得大于结束日期", 3);
        return false;
    }

    return true;
}
