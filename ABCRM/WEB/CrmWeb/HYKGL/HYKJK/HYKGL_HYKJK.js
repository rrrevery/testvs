vUrl = "../HYKGL.ashx";
//var vCZK = GetUrlParam("czk");
var vCZK = IsNullValue(GetUrlParam("czk"), "0");
vUrl = vCZK == "0" ? "../HYKGL.ashx" : "../../MZKGL/MZKGL.ashx";

$(document).ready(function () {
    FillBGDDTreeZK("TreeBGDD", "TB_BGDDMC");
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", vCZK == "1" ? 2 : 1);//会员卡建卡
    FillFXDWTree("TreeFXDW", "TB_FXDWMC");
    $("#TB_PDJE").val("0");
    if (vCZK == "0") {
        $("#div_mzye").hide();
        $("#div_zje").hide();
    }
    $("#TB_CZKHM_END").blur(function () {
        CheckKHD("TB_CZKHM_END");
        //if ($("#TB_CZKHM_END").val() != "") {
        //    if ($("#TB_CZKHM_BEGIN").val() != "")
        //        Checkbegin();
        //    Checkend();
        //    Checkjksl();
        //}
    });
    $("#TB_CZKHM_BEGIN").blur(function () {
        CheckKHD("TB_CZKHM_BEGIN");
        //if ($("#TB_CZKHM_BEGIN").val() != "") {
        //    Checkbegin();
        //    if ($("#TB_CZKHM_END").val() != "")
        //        Checkend();
        //    Checkjksl();
        //}
    });
    $("#TB_JKSL").blur(function () {
        CheckKHD("TB_JKSL");
    });
    $("#B_BGR").click(function () {
        SelectRYXX("TB_BGRMC", "HF_BGR", "", false);
        //art.dialog.open('../../WUC/DJR/DJR.aspx', { lock: true, title: '人员选择', width: 450, height: 420 }, false);
    });
    $("#btnWriteCard").click(function () {
        var a = sCurrentPath.substr(0, sCurrentPath.indexOf("CrmWeb"));
        MakeNewTab(a + "CrmWeb/HYKGL/HYKJKZK/HYKGL_HYKJKZK.aspx?action=add&czk=" + vCZK + "&jkjlbh=" + vJLBH, "制卡", vPageMsgID);
    });

});

function AddCustomerButton() {
    AddToolButtons("写卡", "btnWriteCard", "bftoolbtn fa fa-credit-card");
}

function TreeNodeClickCustom(event, treeId, treeNode) {
    switch (treeId) {
        case "TreeBGDD": $("#HF_BGDDDM").val(treeNode.sBGDDDM); break;
        case "TreeFXDW": $("#HF_FXDWID").val(treeNode.iFXDWID); break;
        case "TreeHYKTYPE":
            $("#HF_HYKTYPE").val(treeNode.iHYKTYPE);
            GetHYKDEFData();
            break;
    }
}
function GetHYKDEFData() {
    var str = GetHYKDEF($("#HF_HYKTYPE").val());
    var Obj = JSON.parse(str);
    $("HF_FS_YXQ").val(Obj.iFS_YXQ);
    if (Obj.sKHQDM == "")
        $("#LB_QDM").val("无");
    else
        $("#LB_QDM").val(Obj.sKHQDM);
    if (Obj.sKHHZM == "")
        $("#LB_HZM").val("无");
    else
        $("#LB_HZM").val(Obj.sKHHZM);
    if (Obj.iHMCD == "")
        $("#LB_CD").val("无");
    else
        $("#LB_CD").val(Obj.iHMCD);
    var str = "";
    for (var i = 0; i < Obj.iHMCD - Obj.sKHQDM.length - Obj.sKHHZM.length; i++) {
        str += "9";
    }
    $("#TB_CZKHM_BEGIN").inputmask("remove");
    $("#TB_CZKHM_END").inputmask("remove");
    $("#TB_CZKHM_BEGIN").inputmask("mask", { "mask": Obj.sKHQDM.replace(/9/g, "\\9") + str + Obj.sKHHZM.replace(/9/g, "\\9") });
    $("#TB_CZKHM_END").inputmask("mask", { "mask": Obj.sKHQDM.replace(/9/g, "\\9") + str + Obj.sKHHZM.replace(/9/g, "\\9") });

    //初始化有效期
    var yxqcd = Obj.sYXQCD;
    if (Obj.iFS_YXQ == "0") {
        var d = new Date();
        if (yxqcd.substring(yxqcd.length - 1, yxqcd.length) == "Y") {
            d.setFullYear(d.getFullYear() + parseInt(yxqcd.substring(0, yxqcd.length - 1)));
            var y = d.getFullYear();

        }
        if (yxqcd.substring(yxqcd.length - 1, yxqcd.length) == "M") {
            d.setMonth(d.getMonth() + parseInt(yxqcd.substring(0, yxqcd.length - 1)));
            var y = d.getYear();

        }
        if (yxqcd.substring(yxqcd.length - 1, yxqcd.length) == "D") {
            d.setDate(d.getDate() + parseInt(yxqcd.substring(0, yxqcd.length - 1)));
            var y = d.getFullYear();
        }
        var m = d.getMonth() + 1;
        if (m < 10) { m = "0" + m; }
        var d = d.getDate();
        if (d < 10) { d = "0" + d; }
        $("#TB_YXQ").val(y + "-" + m + "-" + d);

        $("#TB_YXQ").attr("disabled", false);
    }
    else {
        $("#TB_YXQ").val("");
        $("#TB_YXQ").attr("disabled", true);
    }
}


function CheckKHD(sender) {
    if ($("#HF_HYKTYPE").val() == "") {
        ShowMessage("请先选择卡类型");
        return;
    }
    if ($("#TB_CZKHM_BEGIN").val() != "" && !CheckCZKHM($("#TB_CZKHM_BEGIN").val()))
        return
    if ($("#TB_CZKHM_END").val() != "" && !CheckCZKHM($("#TB_CZKHM_END").val()))
        return
    if ($("#TB_JKSL").val() != "" && parseInt($("#TB_JKSL").text()) <= 0) {
        ShowMessage("建卡数量必须大于0");
        return;
    }
    if (sender != "TB_JKSL" && $("#TB_CZKHM_BEGIN").val() != "" && $("#TB_CZKHM_END").val() != "") {
        if ($("#TB_CZKHM_END").val() < $("#TB_CZKHM_BEGIN").val()) {
            ShowMessage("结束卡号必须大于或等于开始卡号");
            return;
        }
        var kskh = $("#TB_CZKHM_BEGIN").val();
        var jskh = $("#TB_CZKHM_END").val();
        var hzm = "";
        if ($("#LB_HZM").val() != "无") {
            hzm = $("#LB_HZM").val();
        }
        $("#TB_JKSL").val(eval(jskh.substring(0, jskh.length - hzm.length) - kskh.substring(0, kskh.length - hzm.length) + 1));
        //$("#LB_JKSL").text(eval(jskh.substring(0, jskh.length - hzm.length) - kskh.substring(0, kskh.length - hzm.length) + 1));
        if (parseInt($("#TB_JKSL").text()) <= 0) {
            ShowMessage("建卡数量必须大于0");
            return;
        }
    }
    if (sender == "TB_JKSL" && $("#TB_CZKHM_BEGIN").val() != "" && $("#TB_JKSL").val() != "") {
        var kskh = $("#TB_CZKHM_BEGIN").val();
        var jksl = $("#TB_JKSL").val();
        var hzm = "";
        if ($("#LB_HZM").val() != "无") {
            hzm = $("#LB_HZM").val();
        }
        $("#TB_CZKHM_END").val(eval(parseInt(kskh.substring(0, kskh.length - hzm.length)) + parseInt(jksl) - 1) + hzm);
    }
}
function CheckCZKHM(sCZKHM) {
    var inx1 = sCZKHM.indexOf("_");
    sCZKHM = sCZKHM.substring(0, inx1 >= 0 ? inx1 : sCZKHM.length);
    if (sCZKHM.substring(0, $("#LB_QDM").val().length) != $("#LB_QDM").val() && $("#LB_QDM").val() != "无") {
        ShowMessage("卡号的开始位置必须是" + $("#LB_QDM").val());
        return false;
    }
    if (sCZKHM.substring(sCZKHM.length - $("#LB_HZM").val().length, sCZKHM.length) != $("#LB_HZM").val() && $("#LB_HZM").val() != "无") {
        ShowMessage("卡号的结束位置必须是" + $("#LB_HZM").val());
        return false;
    }
    if (sCZKHM.length != parseInt($("#LB_CD").val()) && $("#LB_CD").val() != "无") {
        ShowMessage("输入的卡号必须为" + $("#LB_CD").val() + "位");
        return false;
    }
    return true;
}

function SetControlState() {
    document.getElementById("btnWriteCard").disabled = !document.getElementById("B_Save").disabled || !document.getElementById("B_Exec").disabled;
    var a = vProcStatus;
}

function IsValidData() {
    GetHYKDEFXX(); 
    if ($("#TB_CZKHM_BEGIN").val() == "" || $("#TB_CZKHM_END").val() == "") {
        ShowMessage("请输入卡号信息");
        return false;
    }
    if (!CheckCZKHM($("#TB_CZKHM_BEGIN").val()))
        return false;
    if (!CheckCZKHM($("#TB_CZKHM_END").val()))
        return false;
    if ($("#HF_HYKTYPE").val() == "") {
        ShowMessage("请选择卡类型");
        return false;
    }
    if (!document.getElementById("TB_YXQ").disabled && $("#TB_YXQ").val() == "") {
        ShowMessage("有效期必须输入");
        return false;
    }
    if ($("#HF_BGR").val() == "") {
        ShowMessage("请选择保管人");
        return false;
    }
    if ($("#HF_FXDWID").val() == "") {
        ShowMessage("请选择发行单位");
        return false;
    }

    if (vCZK == "1" && $("#TB_MZYE").val() == "") {
        ShowMessage("请输入面值金额");
        return false;
    }
    if (parseInt($("#TB_JKSL").val()) <= 0) {
        ShowMessage("建卡数量必须大于0");
        return;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.iFXDWID = $("#HF_FXDWID").val();
    if (Obj.iFXDWID == "")
        Obj.iFXDWID = 0;
    Obj.sCZKHM_BEGIN = $("#TB_CZKHM_BEGIN").val();
    Obj.sCZKHM_END = $("#TB_CZKHM_END").val();
    //Obj.iJKSL = IsNullValue($("#LB_JKSL").text(), 0);
    Obj.iJKSL = IsNullValue($("#TB_JKSL").val(), 0);
    Obj.iXKSL = IsNullValue($("#LB_XKSL").text(), 0);
    Obj.sZY = $("#TB_ZY").val();
    Obj.iBJ_CZK = vCZK;
    Obj.dYXQ = $("#TB_YXQ").val();
    Obj.iBGR = $("#HF_BGR").val();
    Obj.sBGRMC = $("#TB_BGRMC").val();
    if (vCZK == "1") {
        Obj.fQCYE = $("#TB_MZYE").val();
        Obj.fPDJE = $("#TB_PDJE").val();
    }
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TB_HYKNAME").val(Obj.sHYKNAME);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#TB_FXDWMC").val(Obj.sFXDWMC);
    $("#HF_FXDWID").val(Obj.iFXDWID);
    $("#TB_CZKHM_BEGIN").val(Obj.sCZKHM_BEGIN);
    $("#TB_CZKHM_END").val(Obj.sCZKHM_END);
    //$("#LB_JKSL").text(Obj.iJKSL);
    $("#TB_JKSL").val(Obj.iJKSL);
    $("#LB_XKSL").text(Obj.iXKSL);
    $("#TB_ZY").val(Obj.sZY);
    $("#HF_BGR").val(Obj.iBGR);
    $("#TB_BGRMC").val(Obj.sBGRMC);
    $("#TB_YXQ").val(Obj.dYXQ);
    if (vCZK == "1") {
        $("#TB_MZYE").val(Obj.fQCYE);
        $("#TB_PDJE").val(Obj.fPDJE);
        $("#LB_ZJE").text(Obj.iJKSL * Obj.fQCYE);
    }

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}

function UpdateClickCustom() {
    GetHYKDEFXX();
};

function ExecClickCustom() {
    GetHYKDEFXX();
};


function GetHYKDEFXX() {  //只是为了获得QDM HZM CD
    var str = GetHYKDEF($("#HF_HYKTYPE").val());
    var Obj = JSON.parse(str);
    $("HF_FS_YXQ").val(Obj.iFS_YXQ);
    if (Obj.sKHQDM == "")
        $("#LB_QDM").val("无");
    else
        $("#LB_QDM").val(Obj.sKHQDM);
    if (Obj.sKHHZM == "")
        $("#LB_HZM").val("无");
    else
        $("#LB_HZM").val(Obj.sKHHZM);
    if (Obj.iHMCD == "")
        $("#LB_CD").val("无");
    else
        $("#LB_CD").val(Obj.iHMCD);
    var yxqcd = Obj.sYXQCD;
    if (Obj.iFS_YXQ == "0") {
        var d = new Date();
        if (yxqcd.substring(yxqcd.length - 1, yxqcd.length) == "Y") {
            d.setFullYear(d.getFullYear() + parseInt(yxqcd.substring(0, yxqcd.length - 1)));
            var y = d.getFullYear();

        }
        if (yxqcd.substring(yxqcd.length - 1, yxqcd.length) == "M") {
            d.setMonth(d.getMonth() + parseInt(yxqcd.substring(0, yxqcd.length - 1)));
            var y = d.getYear();

        }
        if (yxqcd.substring(yxqcd.length - 1, yxqcd.length) == "D") {
            d.setDate(d.getDate() + parseInt(yxqcd.substring(0, yxqcd.length - 1)));
            var y = d.getFullYear();
        }
        var m = d.getMonth() + 1;
        if (m < 10) { m = "0" + m; }
        var d = d.getDate();
        if (d < 10) { d = "0" + d; }
        $("#TB_YXQ").val(y + "-" + m + "-" + d);

        $("#TB_YXQ").attr("disabled", false);
    }
    else {
        $("#TB_YXQ").val("");
        $("#TB_YXQ").attr("disabled", true);
    }
}
