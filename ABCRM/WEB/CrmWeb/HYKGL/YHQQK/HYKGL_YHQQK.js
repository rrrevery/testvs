vUrl = "../HYKGL.ashx";
var HYKNO = GetUrlParam("HYKNO");
$(document).ready(function () {
    if (HYKNO != "") {
        $("#TB_HYKNO").val(HYKNO);
        GetHYXX();
        vProcStatus = cPS_ADD;
        SetControlBaseState();
    }

    $("#btn_HYKHM").click(function () {
        var conData = new Object();
        conData.iBJ_KCK = 0;
        SelectSK("TB_HYKNO", "HF_HYID", "", conData);       
    });
    $("#BTN_YHQZH").click(function () {
        if ($("#HF_HYID").val() == "") {
            ShowMessage("请输入卡号!", 3);
            return;
        }
        var condData = new Object();
        condData.iHYID = $("#HF_HYID").val();
        var checkRepeatField = [];
        SelectYHQZH("list", condData, checkRepeatField,true);
    });
    FillBGDDTree("TreeBGDD", "TB_BGDDMC");
})

function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    $("#LB_YHQMC").text(lst[0].sYHQMC);
    $("#HF_YHQID").val(lst[0].iYHQID);
    $("#LB_JSRQ").text(lst[0].dJSRQ);
    $("#LB_MDFWDM").text(lst[0].sMDFWDM);
    $("#LB_MDFWMC").text(lst[0].sMDFWMC);
    $("#LB_YJE").text(lst[0].fJE);
    $("#LB_CXHD").text(lst[0].sCXZT);
    $("#HF_CXHD").val(lst[0].iCXID);
}

function IsValidData() {
    if ($("#HF_HYID").val() == "" || $("#HF_HYID").val() == undefined || $("#HF_HYID").val() == null) {
        ShowMessage("会员不存在！请重新录入信息!", 3);
        return false;
    }
    if ($("#LB_YJE").text() == "" || $("#LB_YJE").text() == "0") {
        ShowMessage("请选择有效优惠券账户!", 3);

        return false;
    }
    if ($("#HF_BGDDDM").val() == "") {
        ShowMessage("请选择操作地点!", 3);

        return false;
    }
    if ($("#TB_QKJE").val() == "" || $("#TB_QKJE").val()=="0") {
        ShowMessage("请输入有效取款金额!", 3);
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sHYKNO = $("#TB_HYKNO").val();
    Obj.iHYID = $("#HF_HYID").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.iYHQID = $("#HF_YHQID").val();
    Obj.dJSRQ = $("#LB_JSRQ").text();
    Obj.sMDFWDM = $("#LB_MDFWDM").text() || " "; 
    Obj.iCXID = $("#HF_CXHD").val();
    Obj.fYYE = $("#LB_YJE").text();
    Obj.fQKJE = $("#TB_QKJE").val();
    Obj.sZY = $("#TB_ZY").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    if ($("#HF_CZYMDID").val() != "") {
        Obj.iMDID = $("#HF_CZYMDID").val();//操作员门店
    }
    return Obj;
}



function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);

    $("#TB_HYKNO").val(Obj.sHYKNO);
    $("#HF_HYID").val(Obj.iHYID);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#LB_YHQMC").text(Obj.sYHQMC);
    $("#HF_YHQID").val(Obj.iYHQID);
    $("#LB_MDFWDM").text(Obj.sMDFWDM);
    $("#LB_MDFWMC").text(Obj.sMDFWMC);
    $("#LB_JSRQ").text(Obj.dJSRQ);
    $("#LB_CXHD").text(Obj.sCXZT);
    $("#HF_CXHD").val(Obj.iCXID);
    $("#LB_YJE").text(Obj.fYYE);
    $("#TB_QKJE").val(Obj.fQKJE);

    $("#TB_ZY").val(Obj.sZY);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#HF_CZYMDID").val(Obj.iMDID);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
}

function TreeNodeClickCustom(e, treeId, treeNode) {
    $("#HF_BGDDDM").val(treeNode.id);
}

function GetHYXX() {
    if ($("#TB_HYKNO").val() != "") {
        var str = GetHYXXData(0, $("#TB_HYKNO").val());
        if (str) {
            var Obj = JSON.parse(str);
            $("#HF_HYID").val(Obj.iHYID);
            $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
            $("#LB_HYKNAME").text(Obj.sHYKNAME);
        }
    }
}

function PDYE() {
    if (parseInt($("#LB_YJE").text(), 10) < parseInt($("#TB_QKJE").val(), 10)) {
        ShowMessage("取款金额不应超过余额，请重新输入!", 3);
        return false;
    }
    return true;
}
