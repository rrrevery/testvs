vHF = GetUrlParam("hf");
vCZK = GetUrlParam("czk");
vUrl = vCZK == "0" ? "../HYKGL.ashx" : "../../MZKGL/MZKGL.ashx";
vDBName = vCZK == "0" ? "CRMDB" : "CRMDBMZK";
var HYKNO = GetUrlParam("HYKNO");
var HYK_NO = "";
function GetHYXX() {
    if ($("#TB_HYKNO").val() != "") {
        var str = GetHYXXData(0, $("#TB_HYKNO").val(), vDBName);
        if (str) {
            if (str.indexOf("错误") >= 0) {
                ShowMessage(str, 3);
                return;
            }

            var Obj = JSON.parse(str);

            if (Obj.sHYK_NO == "") {
                ShowMessage("没有找到卡号", 3);
                return;
            }
            if (Obj.iBJ_GS != 1) {
                ShowMessage("该卡类型不允许挂失", 3);
                return;

            }
            if (Obj.iSTATUS < 0 && vHF == "0") {
                ShowMessage("此卡为无效卡，无需挂失", 3);
                return;
            }
            if (Obj.iSTATUS != -3 && vHF == "1") {
                ShowMessage("此卡非挂失状态，不能恢复", 3);
                return;
            }
            $("#HF_HYID").val(Obj.iHYID);
            $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
            $("#LB_HYNAME").text(Obj.sHY_NAME);
            $("#LB_HYKNAME").text(Obj.sHYKNAME);
            $("#HF_FXDWDM").val(Obj.sFXDWDM);
            $("#LB_FXDWMC").text(Obj.sFXDWMC);
            $("#HF_BJ_CHILD").val(Obj.iBJ_CHILD);

            //if (Obj.iBJ_CHILD == 0) {//挂失的为主卡

            //    if (Obj.iHYID != "") {
            //         HYK_NO = GetZK(Obj.iHYID);
            //        if (HYK_NO != "") {
            //            $("#A").show();
            //        }
            //        else
            //            $("#A").hide();
            //    }

            //}
            //else
            //    $("#A").hide();


            //if (Obj.iHYID != "") {
            //    var gxshjf = GetGXSHJF(Obj.iHYID, iDJR);
            //    if (gxshjf != "") {
            //        $("#LB_JF").text(gxshjf);
            //    }
            //}

            $("#LB_JF").text(Obj.fWCLJF);
            $("#LB_JE").text(Obj.fCZJE);
            if (vHF == 0) {
                $("#LB_YZT").text("正常有效");
                $("#LB_XZT").text("挂失");

            }
            else {
                $("#LB_YZT").text("挂失");
                $("#LB_XZT").text("正常有效");
            }
        }
    }
}

function IsValidData() {
    if ($("#HF_HYID").val() == "" || $("#HF_HYID").val() == undefined || $("#HF_HYID").val() == null) {
        ShowMessage("会员不存在！请重新录入信息", 3);
        return false;
    }
    if ($("#HF_BGDDDM").val() == "") {
        ShowMessage('请选择操作地点!', 3);
        return false;
    }

    //if (HYK_NO != "" && $("#TB_HYKHM_C").val()=="") {
    //    ShowMessage('请选择一张子卡', 3);
    //    return false;
    //}


    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sHYKNO = $("#TB_HYKNO").val();
    Obj.iHYID = $("#HF_HYID").val();
    Obj.sFXDWDM = $("#HF_FXDWDM").val();
    Obj.sFXDWMC = $("#LB_FXDWMC").text();
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.iBJ_CHILD = $("#HF_BJ_CHILD").val();
    Obj.fJF = $("#LB_JF").text();
    Obj.fJE = $("#LB_JE").text();
    Obj.sZY = $("#TB_ZY").val();
    Obj.iLX = vHF;
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    Obj.sHYK_NOC = $("#TB_HYKHM_C").val();
    return Obj;
}

$(document).ready(function () {
    $("#A").hide();

    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");
    if (HYKNO != "") {
        $("#TB_HYKNO").val(HYKNO);
        GetHYXX();
        vProcStatus = cPS_ADD;
        SetControlBaseState();
        $("#TB_HYKNO").attr("readonly", "readonly");
    }

    $("#TB_HYKNO").bind('keypress', function (event) {//#TB_CDNR,
        if (event.keyCode == "13") {
            GetHYXX();
        }
    });

    //$("#TB_HYKNO").change(function () {

    //    GetHYXX();
    //});

    //FillFXDWTree("TreeFXDW", "TB_FXDWMC", "menuContent");


    $("#btn_HYKHM").click(function () {
        HYID = $("#HF_HYID").val();

        $.dialog.open("../../WUC/HYKC/WUC_HYKC.aspx?iHYID=" + HYID, {
            lock: true, width: 400, height: 450, cancel: false
                    , close: function () {
                        WUC_HYKC_Return();

                    }
        }, false);
    });
})


function WUC_HYKC_Return() {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";

            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                if (tp_return_ChoiceOne) {
                    tp_mc += contractValues[i].Name;
                    tp_hf += contractValues[i].Id;
                } else {
                    tp_mc += contractValues[i].Name;
                }
            }
            $("#TB_HYKHM_C").val(tp_mc);
            //$("#HF_HYZ").val(tp_hf);
            //$("#zHF_HYZ").val(jsonString);
        }
    }
}
//function onFXDWClick(e, treeId, treeNode) {
//    $("#TB_FXDWMC").val(treeNode.name);
//    $("#HF_FXDWDM").val(treeNode.id);
//    hideMenu("menuContent");
//}

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
    $("#HF_FXDWDM").val(Obj.sFXDWDM);
    $("#LB_FXDWMC").text(Obj.sFXDWMC);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#LB_HYNAME").text(Obj.sHY_NAME);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    $("#LB_JF").text(Obj.fJF);
    $("#LB_JE").text(Obj.fJE);
    $("#TB_ZY").val(Obj.sZY);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#HF_BJ_CHILD").val(Obj.iBJ_CHILD);

    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);

    $("#TB_HYKHM_C").val(Obj.sHYK_NOC);

    if (vHF == 0) {
        $("#LB_YZT").text("正常有效");
        $("#LB_XZT").text("挂失");

    }
    else {
        $("#LB_YZT").text("挂失");
        $("#LB_XZT").text("正常有效");
    }
}

