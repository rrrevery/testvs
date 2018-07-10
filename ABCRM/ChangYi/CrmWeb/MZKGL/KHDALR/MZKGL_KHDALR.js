vUrl = "../MZKGL.ashx";
var LRFS = 0;
$(document).ready(function () {
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    })
    FillSelect("DDL_ZJLX", GetHYXXXM(0));
});
function SetControlState() {
    ;
}

function IsValidData() {
    var tp_msg = "";
    if ($("#TB_KHWZ").val() != "") {
        if (!IsURL($("#TB_KHWZ").val())) {
            ShowMessage("请输入正确的网址", 3);
            return false;

        }
    }
    tp_msg += zIsNull("证件类型", $("#DDL_ZJLX")[0].value);
    if ($("#DDL_ZJLX").val() == 1)
        tp_msg += IsIDCard("证件号码", $("#TB_LXRZJHM").val());
    tp_msg += zIsValidvar("客户名称", $("#TB_KHXM").val());
    tp_msg += zIsValidvar("联系人", $("#TB_LXRMC").val());
    tp_msg += zIsTelePhone("手机号码", $("#TB_LXRSJ").val());
    tp_msg += zIsInt("联系人电话", $("#TB_LXRDH").val());
    tp_msg += zIsEMail("联系人邮箱", $.trim($("#TB_LXRYX").val()));
    tp_msg += zIsInt("联系人邮编", $("#TB_LXRYB").val());
    tp_msg += zIsValidvar("联系人地址", $("#TB_LXRTXDZ").val());

    if (tp_msg != "") {
        ShowMessage(tp_msg, 3);
        return false;
    }
    return true;
}

function SaveData() {
    var obj = new Object();
    obj.iJLBH = $("#TB_JLBH").val();
    if (obj.iJLBH == "")
        obj.iJLBH = "0";
    obj.sKHDZ = $("#TB_KHDZ").val();
    obj.sKHMC = $("#TB_KHXM").val();
    // obj.iKHXZ = ($("#DDL_KHXZ")[0].value != "") ? $("#DDL_KHXZ").val() : "-1";
    //obj.iKHXZ = GetSelectValue("DDL_KHXZ");
    obj.sKHWZ = $("#TB_KHWZ").val();
    obj.sLXRXM = $("#TB_LXRMC").val();
    obj.sLXRSJ = $("#TB_LXRSJ").val();
    obj.sLXRDH = $("#TB_LXRDH").val();
    obj.sLXRTXDZ = $("#TB_LXRTXDZ").val();
    obj.sLXRYB = $("#TB_LXRYB").val();
    obj.sLXREMAIL = $("#TB_LXRYX").val();
    // obj.iLXRZJ = ($("#DDL_ZJLX")[0].value != "") ? $("#DDL_ZJLX").val() : "-1";
    obj.iLXRZJ = GetSelectValue("DDL_ZJLX");
    obj.sLXRZJHM = $("#TB_LXRZJHM").val();
    obj.sZY = $("#TB_ZY").val();
    obj.sDBConnName = "CRMDBMZK";
    obj.iMDID = $("#HF_MDID").val();
    obj.iLoginRYID = iDJR;
    obj.sLoginRYMC = sDJRMC;
    return obj;
}

function ShowData(data) {
    var obj = JSON.parse(data);
    $("#TB_JLBH").val(obj.iJLBH);
    $("#TB_PYM").val(obj.sPYM);
    $("#TB_KHXM").val(obj.sKHMC);
    $("#TB_KHDM").val(obj.iJLBH);
    $("#TB_KHDZ").val(obj.sKHDZ);
    $("#DDL_KHXZ").val(obj.iKHXZ);
    $("#TB_KHWZ").val(obj.sKHWZ);
    $("#TB_LXRMC").val(obj.sLXRXM);
    $("#TB_LXRSJ").val(obj.sLXRSJ);
    $("#TB_LXRDH").val(obj.sLXRDH);
    $("#TB_LXRTXDZ").val(obj.sLXRTXDZ);
    $("#TB_LXRYB").val(obj.sLXRYB);
    $("#TB_LXRYX").val(obj.sLXREMAIL);
    $("#DDL_ZJLX").val(obj.iLXRZJ);
    $("#TB_LXRZJHM").val(obj.sLXRZJHM);
    $("#TB_ZY").val(obj.sZY);
    $("#HF_MDID").val(obj.iMDID);
    $("#TB_MDMC").val(obj.sMDMC);
    $("#LB_DJRMC").text(obj.sDJRMC);
    $("#HF_DJR").val(obj.iDJR);
    $("#LB_DJSJ").text(obj.dDJSJ);
    $("#LB_ZXRMC").text(obj.sZXRMC);
    $("#HF_ZXR").val(obj.iZXR);
    $("#LB_ZXRQ").text(obj.dZXRQ);
}
