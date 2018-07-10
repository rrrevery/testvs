vUrl = "../HYKGL.ashx";
var GKID = 0;
var pPSW = 0;
var timeCDNR = setInterval(function () { $("#TB_CDNR").focus(); }, 1000);
vCaption = "会员卡快速发卡"

$(document).ready(function () {
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#B_Exec").hide();
    $("#B_Cancel").hide();
    $("#status-bar").hide();
    $("#jlbh").hide();
    $("#B_Save").text("发卡");

    //$("#TB_SJHM").inputmask("9{11}");
    $("#TB_HYKNO").bind('keypress', function (event) {//#TB_CDNR,
        if (event.keyCode == "13") {
            PageDate_Clear();
            GetKCKXX($("#TB_HYKNO").val(), "");
        }
    });

    $("#btnICK").click(function () {
        sCDNR = rwcard.ReadCard("4;4");
        if (sCDNR != "") {
            $("#TB_CDNR").val(";" + sCDNR + "?");
            ProcIt();
        }
    });

    $("#TB_CDNR").keypress(function (event) {
        if (event.keyCode == "13") {
            ProcIt();
        }
    });
    $("#TB_SJHM").change(function () {
        if ($("#TB_SJHM").val() == "") {
            ShowMessage("请输入手机号码", 3);
            return;
        }
        getGKDA("", $("#TB_SJHM").val());

    });
    RefreshButtonSep();
});

function ProcIt() {
    var BJCDNR = ToCDB($("#TB_CDNR").val());
    //var inx1 = BJCDNR.indexOf(";");
    //var inx2 = BJCDNR.indexOf("+");
    //var inx3 = BJCDNR.indexOf("?");
    //var inx4 = BJCDNR.indexOf("?", inx2);
    //var cdnr = BJCDNR.substring(inx1 + 1, inx3);
    ////   cdnr = ToCDB(cdnr);
    //var hykno = BJCDNR.substring(inx2 + 1, inx4);
    var inx1 = BJCDNR.indexOf(";");
    var inx2 = BJCDNR.indexOf("+");
    var inx3 = BJCDNR.indexOf("?");
    var inx4 = BJCDNR.indexOf("?", inx2);
    var cdnr = BJCDNR;
    cdnr = cdnr.substring(inx1 + 1, inx3 >= 0 ? inx3 : cdnr.length);
    var hykno = $("#TB_CDNR").val().substring(inx2 + 1, inx4);
    if (cdnr == "") {
        art.dialog({
            content: '磁道内容不合法 (2秒后关闭)', lock: true, time: 2, close: function () {
                $("#TB_CDNR").focus();
            }
        });
        $("#TB_CDNR").val("");
        $("#TB_CDNR").focus();
        return false;
    }
    GetKCKXX("", cdnr);
    window.clearInterval(timeCDNR);
    $("#TB_CDNR").val("");
    $("#TB_SJHM").val("");
    document.getElementById("TB_SJHM").focus();
    //document.getElementById("BJ_MMBS").disabled = true;
}

function SetControlState() {
    if ($("#B_Save").prop("disabled") == true) {
        $("#TB_CDNR").focus();
    }
}

function IsValidData() {
    var tp_msg = "";
    if ($("#SP_BJ_SJHM").is(":visible") == true) { tp_msg += zIsTelePhone("手机号码", $("#TB_SJHM").val()); }
    if (tp_msg != "") {
        art.dialog({ lock: true, content: tp_msg });
        return false;
    }
    //现以改为一个GKID，可以对应多个HYK_NO 在HYK_HYXX表中，如果，有此顾客的信息，就反写这个GKID到此卡中。

    return true;
}

//function AddCustomerButton() {
//    AddToolButtons("IC卡", "btnICK");
//}

function SaveData() {
    $("#TB_CDNR").val("");
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iFS = 0;
    Obj.iSKSL = 1;
    //Obj.TB_GBF = $("#TB_GBF").val();
    //Obj.fYSZE = $("#TB_GBF").val();
    //Obj.fSSJE = $("#TB_GBF").val();
    Obj.dYXQ = $("#LB_YXQ").text();
    Obj.sBGDDDM = $("#LB_BGDDDM").text();
    Obj.iMDID = $("#HF_MDID").val();

    Obj.iBJ_FAST = 1;
    Obj.sZY = "快速发放";
    Obj.iBJ_PSW = pPSW;
    var obj3 = new Object();
    obj3.sHYK_NO = $("#LB_HYKNO").text();
    obj3.iHYKTYPE = $("#LB_HYKTYPE").text();
    obj3.dYXQ = $("#LB_YXQ").text();
    obj3.sSJHM = $("#TB_SJHM").val();
    //-------------------------------------------------------------

    Obj.HYKXX = obj3;

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val("0");//快速发放连续操作



    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}

function GetKCKXX(hykno, cdnr) {

    var str = GetKCKXXData("", cdnr);
    if (!str) {
        art.dialog({
            content: '没有找到卡 (2秒后关闭)', lock: true, time: 2, close: function () {
                $("#TB_CDNR").val("");
                $("#TB_CDNR").focus();
            }
        });
        $("#TB_CDNR").val("");
        $("#TB_CDNR").focus();
        $("#TB_HYKNO").val("");
        return;
    }

    var Obj = JSON.parse(str);
    //$("#TB_JLBH").val("0");
    //$("#HF_HYID").val(Obj.iHYID);
    if (Obj.sCZKHM == "") {
        ShowMessage("没有找到卡");
        $("#TB_CDNR").focus();
        return;
    }
    if (Obj.iSTATUS != 1) {
        ShowMessage("该卡不是领取状态，无法发放");
        $("#TB_CDNR").focus();
        return;
    }
    $("#LB_HYKNO").text(Obj.sCZKHM);
    $("#LB_HYKTYPE").text(Obj.iHYKTYPE);
    isPSW(Obj.iHYKTYPE);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    $("#LB_BGDDDM").text(Obj.sBGDDDM);
    var boolFK = CheckBGDDQX($("#LB_BGDDDM").text(), iDJR);
    if (boolFK == "False") {
        art.dialog({ lock: true, content: "该操作员没有该保管地点的操作权限！", });
        $("#TB_CDNR").val("");
        $("#LB_HYKNO").text("");
        $("#LB_HYKTYPE").text("");
        $("#LB_HYKNAME").text("");
        $("#LB_BGDDDM").text("");
        return;
    }
    $("#LB_BGDDMC").text(Obj.sBGDDMC);
    $("#LB_YXQ").text(Obj.dYXQ);
    $("#HF_MDID").val(Obj.iMDID);

    vProcStatus = cPS_ADD;
    SetControlBaseState();
}

function GetGKDAData2(sSJHM) {
    sjson = "{'sSJHM':'" + sSJHM + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetGKDA2",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), },
        success: function (data) {
            result = data;
        },
        error: function (data) {
            result = "";
        }
    });
    return result;
}

function ToCDB(str) {
    var tmp = "";
    for (var i = 0; i < str.length; i++) {
        if (str.charCodeAt(i) > 65248 && str.charCodeAt(i) < 65375) {
            tmp += String.fromCharCode(str.charCodeAt(i) - 65248);
        }
        else {
            tmp += String.fromCharCode(str.charCodeAt(i));
        }
    }
    return tmp;
}

function isPSW(hyktype) {
    var data = CheckPSW(hyktype);
    if (data) {
        data = JSON.parse(data);
        pPSW = data.iBJ_PSW;
    }
}

function getGKDA(sfzhm, sjhm) {
    var str = GetGKDAData(0, "", sjhm);
    if (str == "null" || str == "") {
        BK = true;
        return;
    }
    $("#LB_GKXX").text("老顾客");
    var Obj = JSON.parse(str);
    var t = CheckBK(Obj.iGKID);
    var b = JSON.parse(t);
    if (b.iBJ_CHECK == true) {
        ShowMessage("此手机号已办过会员卡！", 4);
        $("#TB_SJHM").val("");
        return;
    }
    else
        BK = true;



}