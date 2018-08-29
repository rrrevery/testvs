vUrl = "../HYKGL.ashx";
var HYKNO = GetUrlParam("HYKNO");
var KCKMDID;
var vSJ = GetUrlParam("sj");
var vBJ_XFJE = 0;

//根据卡号查询信息
function GetHYXX() {
    if ($("#TB_HYKHM_OLD").val() != "") {
        var str = GetHYXXData(0, $("#TB_HYKHM_OLD").val());
        if (str) {
            var Obj = JSON.parse(str);
            $("#HF_HYID").val(Obj.iHYID);
            $("#HF_HYKTYPEOLD").val(Obj.iHYKTYPE);
            $("#LB_HYNAME").text(Obj.sHY_NAME);
            $("#LB_HYKNAMEOLD").text(Obj.sHYKNAME);
            $("#LB_WCLJF").text(Obj.fWCLJF);
            $("#LB_BQJF").text(Obj.fBQJF);
            $("#LB_BQXFJE").text(Obj.fXFJE);
            $("#LB_BGDDMC").text(Obj.sBGDDMC);
            $("#HF_BGDDDM").val(Obj.sBGDDDM);
            $("#HF_YJKRQ").val(Obj.dJKRQ);
            $("#HF_FXDW").val(Obj.iFXDW);
            //$("#HF_BJ_XFJE").val(Obj.iBJ_XFJE);

            var str2 = GetSJGZ(Obj.iHYKTYPE, vSJ, $("#LB_BQJF").text(), Obj.fXFJE, Obj.iMDID);
            if (str2 == "null" || str2 == "") {
                $("#HF_HYKTYPENEW").val("");
                $("#LB_HYKNAMENEW").text("");
                ClearHYXX();
                //if (vSJ == 1) {
                //    ShowMessage("没有找到升级规则",1);
                //    ClearHYXX();
                //}
                //else {
                //    ShowMessage("没有找到降级规则",1);
                //    ClearHYXX();
                //}
                return;
            }
            var obj2 = JSON.parse(str2);
            $("#HF_HYKTYPENEW").val(obj2.iHYKTYPE_NEW);
            $("#LB_HYKNAMENEW").text(obj2.sHYKNAME_NEW);
            $("#HF_BJ_XFJE").val(obj2.iBJ_XFJE);
            if (vSJ == 1) {  //升级
                if (obj2.iBJ_XFJE == 0) {
                    $("#LB_SJJF").text(obj2.fQDJF);
                }
                else {
                    $("#LB_SJJF").text(obj2.fDRXFJE);
                }
            }
            else {
                if (obj2.iBJ_XFJE == 0) {
                    $("#LB_SJJF").text(Obj.fBQJF);
                }
                else {
                    $("#LB_SJJF").text(Obj.fXFJE);
                }
            }

            ToggleShowText(vSJ, obj2.iBJ_XFJE);
        }
    }
}

function ClearHYXX() {
    $("#TB_HYKHM_OLD").val("");
    $("#HF_HYID").val("");
    $("#HF_HYKTYPEOLD").val("");
    $("#LB_HYNAME").text("");
    $("#LB_HYKNAMEOLD").text("");
    $("#LB_WCLJF").text("");
    $("#LB_BQJF").text("");
    $("#LB_BQXFJE").text("");
    $("#LB_BGDDMC").text("");
    $("#HF_BGDDDM").val("");
    $("#HF_YJKRQ").val("");
    $("#HF_FXDW").val("");
    $("#LB_SJJF").text("");
}

function GetKCKXX() {
    if ($("#TB_HYKHM_NEW").val() != "") {
        var str = GetKCKXXData($("#TB_HYKHM_NEW").val(), "");
        if (str == "null" || str == "") {
            ShowMessage("没有找到卡号", 3);
            $("#TB_HYKHM_NEW").val("");
            $("#LB_HYKNAMENEW").text("");
            $("#HF_HYKTYPENEW").val("");
            return;
        }

        var Obj = JSON.parse(str);

        if (Obj.sCZKHM = "") {
            ShowMessage("未找到库存卡", 3);
            return;
        }
        if ($("#HF_HYKTYPENEW").val() != Obj.iHYKTYPE) {
            ShowMessage("卡类型不符合", 3);
            $("#TB_HYKHM_NEW").val("");
            $("#LB_HYKNAMENEW").text("");
            $("#HF_HYKTYPENEW").val("");
            return;
        }
        var boolFK = CheckBGDDQX(Obj.sBGDDDM, iDJR);
        if (boolFK == "False") {
            ShowMessage("没有此卡保管地点权限", 3);
            $("#TB_HYKHM_NEW").val("");
            $("#LB_HYKNAMENEW").text("");
            $("#HF_HYKTYPENEW").val("");
            return;
        }
        if ($("#HF_FXDW").val() != Obj.iFXDWID) {
            ShowMessage("新卡与老卡发行单位不符", 3);
            $("#TB_HYKHM_NEW").val("");
            $("#LB_HYKNAMENEW").text("");
            $("#HF_HYKTYPENEW").val("");
            return;
        }
        $("#HF_BGDDDM").val(Obj.sBGDDDM);
        $("#LB_BGDDMC").text(Obj.sBGDDMC);
        KCKMDID = Obj.iMDID;
    }
}

function SetControlState() {
    ;
}

function IsValidData() {
    if ($("#HF_HYID").val() == "" || $("#HF_HYID").val() == undefined || $("#HF_HYID").val() == null) {
        ShowMessage("会员不存在！请重新录入信息", 3);
        return false;
    }
    if ($("#TB_HYKHM_NEW").val() == "") {
        ShowMessage("新卡号不能为空", 3);
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sHYKHM_OLD = $("#TB_HYKHM_OLD").val();
    Obj.sHYKHM_NEW = $("#TB_HYKHM_NEW").val();
    Obj.iHYID = $("#HF_HYID").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.iHYKTYPE_OLD = $("#HF_HYKTYPEOLD").val();
    Obj.iHYKTYPE_NEW = $("#HF_HYKTYPENEW").val();
    Obj.fWCLJF_OLD = $("#LB_WCLJF").text();
    Obj.fBQJF_OLD = $("#LB_BQJF").text();
    Obj.fJE = $("#HF_JE").val();
    Obj.fSJJF = $("#LB_SJJF").text();
    Obj.iBJ_SJ = vSJ;
    Obj.dYJKRQ = $("#HF_YJKRQ").val();
    Obj.iBJ_XFJE = $("#HF_BJ_XFJE").val();
    Obj.sZY = $("#TB_ZY").val();
    GetKCKXX();
    Obj.iMDID = KCKMDID;

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

$(document).ready(function () {
    if (HYKNO != "") {
        $("#TB_HYKHM_OLD").val(HYKNO);
        GetHYXX();
        vProcStatus = cPS_ADD;
        SetControlBaseState();
        $("#TB_HYKHM_OLD").attr("readonly", "readonly");
    }


    $("#TB_HYKHM_OLD").bind('keypress', function (event) {
        if (event.keyCode == "13") {
            GetHYXX();
        }
    });
    $("#TB_HYKHM_OLD").blur(function () {
        GetHYXX();
    });
    $("#TB_HYKHM_NEW").bind('keypress', function (event) {
        if (event.keyCode == "13") {
            GetKCKXX();
        }
    });
    $("#TB_HYKHM_NEW").blur(function () {
        GetKCKXX();
    });
    ToggleShowText(vSJ, vBJ_XFJE);

})

function ToggleShowText(vSJ, vBJ_XFJE) {
    sSJ = vSJ == 0 ? "降级" : "升级";
    sBJ_XFJE = vBJ_XFJE == 0 ? "积分" : "金额";
    $("#LB_SJJF").parent().siblings().html(sSJ + sBJ_XFJE);
}
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_HYKHM_OLD").val(Obj.sHYKHM_OLD);
    $("#TB_HYKHM_NEW").val(Obj.sHYKHM_NEW);
    $("#HF_HYID").val(Obj.iHYID);
    $("#LB_HYNAME").text(Obj.sHY_NAME);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#LB_BGDDMC").text(Obj.sBGDDMC);
    $("#HF_HYKTYPEOLD").val(Obj.iHYKTYPE_OLD);
    $("#LB_HYKNAMEOLD").text(Obj.sHYKNAME_OLD);
    $("#HF_HYKTYPENEW").val(Obj.iHYKTYPE_NEW);
    $("#LB_HYKNAMENEW").text(Obj.sHYKNAME_NEW);
    $("#LB_WCLJF").text(Obj.fWCLJF_OLD);
    $("#LB_BQJF").text(Obj.fBQJF_OLD);
    $("#LB_SJJF").text(Obj.fSJJF);
    $("#HF_BJ_XFJE").val(Obj.iBJ_XFJE);
    vSJ = Obj.iBJ_SJ;
    vBJ_XFJE = Obj.iBJ_XFJE;
    $("#HF_YJKRQ").val(Obj.dYJKRQ);
    $("#TB_ZY").val(Obj.sZY);

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}