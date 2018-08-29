vUrl = "../HYKGL.ashx";
var HYKNO = GetUrlParam("HYKNO");

function SetControlState() {
    // document.getElementById("B_Update").disabled = true;
    //  document.getElementById("B_Delete").disabled=true
    ;
}

function IsValidData() {
    if ($("#HF_HYID").val() == "" || $("#HF_HYID").val() == undefined || $("#HF_HYID").val() == null) {
        ShowMessage("会员不存在！请重新录入信息");
        return false;
    }

    if (TYPE == 0 && $("#TB_YMM").val() != $("#HF_YMMYZ").val()) {
        ShowMessage("原密码不正确");
        return false;
    }

    if (TYPE == 1 && $("#TB_SFZBH").val() == "") {
        //ShowMessage("未填写身份证号码，不能重置密码");
        ShowMessage("该卡没有证件信息，不能重置密码");
        return false;
    }

    if ($("#TB_XMM").val() != $("#TB_QRMM").val()) {
        ShowMessage("两次输入新密码不匹配");
        return false;
    }

    if ($("#HF_BGDDDM").val() == "") {
        ShowMessage("请选择保管地点");
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
    Obj.sHYNAME = $("#TB_HYNAME").val();
    Obj.sPSW_OLD = $("#TB_YMM").val();
    Obj.sPSW_NEW = $("#TB_XMM").val();
    Obj.iTYPE = TYPE
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    //  Obj.sZJHM = $("#TB_ZJHM").val();
    //if (TYPE == 1 && $("#TB_SFZBH").val() != "") {
    //    if ($("#TB_SFZBH").val().substr(-1, 1).toUpperCase == "X")
    //        Obj.sPSW_NEW = $("#TB_SFZBH").val().substr(-2, 6);
    //    else
    //        Obj.sPSW_NEW = $("#TB_SFZBH").val().substr(-1, 6);
    //}
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}


$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#status-bar").hide();
    if (GetUrlParam("action") == "edit") {
        $("#B_Save").hide();
        $("#B_Cancel").hide();
    }

    if (TYPE == 1) {
        $("#DV_MMXG").hide();
        $("#TB_XMM").attr({ readonly: 'true' });
        $("#TB_SFZBH").attr({ readonly: 'true' });
        $("#TB_HYNAME").attr({ readonly: 'true' });

    }
    if (TYPE == 0) {
        $("#DV_SFZBH").hide();
    }
    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");
    if (HYKNO != "") {
        $("#TB_HYKNO").val(HYKNO);
        GetHYXX();
        vProcStatus = cPS_ADD;
        SetControlBaseState();
    }
    RefreshButtonSep();
})

function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContent");
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_HYKNO").val(Obj.sHYKNO);
    $("#HF_HYID").val(Obj.iHYID);
    $("#TB_HYNAME").val(Obj.sHY_NAME);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);

    $("#TB_XMM").val(Obj.sPSW_NEW);
    $("#TB_QRMM").val(Obj.sPSW_NEW);
    if (TYPE == 1) {
        if (Obj.sPSW_OLD != "") {
            $("#TB_YMM").val(Obj.sPSW_OLD);
        }
    }

    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);

}

function GetHYXX() {
    if ($("#TB_HYKNO").val() != "") {
        var str = GetHYXXData(0, $("#TB_HYKNO").val());
        if (str) {
            var Obj = JSON.parse(str);
            $("#HF_HYID").val(Obj.iHYID);
            $("#TB_HYNAME").val(Obj.sHY_NAME);
            $("#HF_YMMYZ").val(Obj.sPASSWORD);
            if (TYPE == 1) {//密码重置取身份证号后6位，X不取
                var ZJHM = Obj.sSFZBH;
                if (isNaN(ZJHM.substring(ZJHM.length - 1, ZJHM.length))) {
                    ZJHM = ZJHM.substring(ZJHM.length - 7, ZJHM.length - 1);
                }
                else {
                    ZJHM = ZJHM.substring(ZJHM.length - 6, ZJHM.length);
                }
                $("#TB_YMM").val("123");
                $("#TB_XMM").val(ZJHM);
                $("#TB_QRMM").val(ZJHM);
                $("#TB_SFZBH").val(Obj.sSFZBH);
            }
        }
    }
}