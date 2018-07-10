vUrl = "../MZKGL.ashx";
var HYKNO = GetUrlParam("HYKNO");
var FXDWID = 0;
var vMZK = 1;
var DBCaption = "CRMDBMZK";
$(document).ready(function () {
    if (HYKNO != "") {
        $("#TB_HYKHM_OLD").val(HYKNO);
        GetMZKXX();
        vProcStatus = cPS_ADD;
        SetControlBaseState();
        $("#TB_HYKHM_OLD").attr("readonly", "readonly");
    }

    $("#btn_HYKHM_OLD").click(function () {
        $.dialog.open("../../WUC/SK/WUC_SK.aspx?czk=2&mzk=1", {
            lock: true, width: 600, height: 200,
            close: function () {
                $("#TB_HYKHM_OLD").val($.dialog.data("passValue"));
                $.dialog.data("passValue", "");
                //    document.getElementById("TB_HYKHM_OLD").focus();
                document.getElementById("TB_HYKHM_OLD").onblur();

            }
        }, false);
    });

    $("#btn_HYKHM_NEW").click(function () {
        //   $("#TB_HYKHM_NEW").val("");
        $.dialog.open("../../WUC/SK/WUC_SK.aspx?czk=1&mzk=" + vMZK + "", {
            lock: true, width: 600, height: 200,
            close: function () {
                $("#TB_HYKHM_NEW").val($.dialog.data("passValue1"));
                $.dialog.data("passValue1", "");
                document.getElementById("TB_HYKHM_NEW").onblur();
            }
        }, false);
    });

    $("#TB_HYKHM_OLD").bind('keypress', function (event) {//#TB_CDNR,
        if (event.keyCode == "13") {
            GetMZKXX();
        }
    });

    $("#TB_HYKHM_NEW").bind('keypress', function (event) {//#TB_CDNR,
        if (event.keyCode == "13") {
            GetKCKXX();
        }
    });

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });

});

//查找新卡信息(HYKCARD)
function GetKCKXX() {
    if ($("#TB_HYKHM_OLD").val() == "") {
        ShowMessage("请先输入原卡号" );
        $("#TB_HYKHM_NEW").val("");
        return;
    }
    if ($("#TB_HYKHM_NEW").val() != "") {
        if ($("#TB_HYKHM_NEW").val() == $("#TB_HYKHM_OLD").val()) {
            ShowMessage( "该卡与原卡卡号一致，请重新选择");
            $("#TB_HYKHM_NEW").val("");
            return;
        }
        var str = GetMZKKCKXXData($("#TB_HYKHM_NEW").val(), "", "CRMDBMZK");
        if (str == "null" || str == "") {
            ShowMessage( "没有找到卡号" );
            $("#TB_HYKHM_NEW").val("");
            return;
        }
        var Obj = JSON.parse(str);
        if (Obj.iSTATUS != 1) {
            ShowMessage( "该卡不是领用状态，请重新选择" );
            $("#TB_HYKHM_NEW").val("");
            return;
        }
        if (Obj.iHYKTYPE != $("#HF_HYKTYPE").val()) {
            ShowMessage( "该卡与原卡卡类型不一致，请重新选择" );
            $("#TB_HYKHM_NEW").val("");
            return;
        }
        if (Obj.fQCYE != 0) {
            ShowMessage( "该卡有期初余额不能换卡，请重新选择" );
            $("#TB_HYKHM_NEW").val("");
            return;
        }

        var boolFK = CheckBGDDQX(Obj.sBGDDDM, iDJR);

        if (boolFK.iBJ_CHECK == false) {
            ShowMessage("该操作员没有该保管地点的操作权限！", 3);
            return;
        }
        else {
            $("#HF_BGDDDM").val(Obj.sBGDDDM);//获取新卡的保管地点
            $("#TB_BGDDMC").val(Obj.sBGDDMC);
        }
        //if (Obj.sBGDDDM != $("#HF_BGDDDM").val()) {
        //    art.dialog({ lock: true, content: "该卡保管地点与操作地点不符，请重新选择" });
        //    $("#TB_HYKHM_NEW").val("");
        //    return;
        //}
    }
}

function IsValidData() {
    if ($("#HF_HYID").val() == "" || $("#HF_HYID").val() == undefined || $("#HF_HYID").val() == null) {
        aShowMessage("会员不存在！请重新录入信息" );
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
    Obj.iHYID = $("#HF_HYID").val();
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    //Obj.fJF = $("#LB_JF").text();
    Obj.fJE = $("#LB_JE").text();
    Obj.sHYKHM_NEW = $("#TB_HYKHM_NEW").val();
    Obj.iMDID = $("#HF_MDID").val();
    Obj.sZY = $("#TB_ZY").val();
    Obj.fGBF = $("#TB_GBF").val();
    Obj.iFXDWID = $("#HF_FXDWID").val();
    Obj.sFXDWMC = $("#LB_FXDWMC").text();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.sDBConnName = DBCaption;
    if (Obj.fGBF == "")
        Obj.fGBF = "0";
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_HYKHM_OLD").val(Obj.sHYKHM_OLD);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#HF_MDID").val(Obj.iMDID);
    $("#HF_HYID").val(Obj.iHYID);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#LB_HY_NAME").text(Obj.sHY_NAME);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    //$("#LB_JF").text(Obj.fJF);
    $("#LB_JE").text(Obj.fJE);
    $("#TB_HYKHM_NEW").val(Obj.sHYKHM_NEW);
    $("#TB_ZY").val(Obj.sZY);
    $("#TB_GBF").val(Obj.fGBF);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#HF_FXDWID").val(Obj.iFXDWID);
    $("#LB_FXDWMC").text(Obj.sFXDWMC);
}

function GetMZKXX() {
    if ($("#TB_HYKHM_OLD").val() != "") {
        var str = GetMZKXXData(0, $("#TB_HYKHM_OLD").val(), "", "CRMDBMZK");
        if (str == "null" || str == "") {
            ShowMessage("卡号不存在或者校验失败");
            return;
        }
        var Obj = JSON.parse(str);
        if (Obj.sHYK_NO == "") {
            ShowMessage("卡号不存在或者校验失败", 3);
            return;
        }
        if (Obj.iSTATUS < 0)
        {
            ShowMessage("卡状态错误", 3);
            return;
        }
        $("#HF_HYID").val(Obj.iHYID);
        $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
        $("#LB_HYKNAME").text(Obj.sHYKNAME);
        $("#LB_JE").text(Obj.fCZJE);
    }
}
