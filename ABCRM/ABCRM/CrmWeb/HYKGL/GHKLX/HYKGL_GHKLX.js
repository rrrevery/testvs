vUrl = "../HYKGL.ashx";
var HYKNO = GetUrlParam("HYKNO");


$(document).ready(function () {
    if (HYKNO != "") {
        $("#TB_HYKHM_OLD").val(HYKNO);
        GetHYXX();
        vProcStatus = cPS_ADD;
        SetControlBaseState();
        $("#TB_HYKHM_OLD").attr("readonly", "readonly");
    }

    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME_NEW", "menuContentHYKTYPE");

  //  FillBGDDTreeSK("TreeBGDD", "TB_BGDDMC", "menuContent");
    //卡类型弹出框 
    //$("#TB_HYKNAME_NEW").click(function () {
    //    SelectKLX("TB_HYKNAME_NEW", "HF_HYKTYPE_NEW", "zHF_HYKTYPE_NEW", true);
    //});

    $("#btn_HYKHM_OLD").click(function () {

        var s = $("[name='LX']:checked").val();
        if ($("[name='LX']:checked").val() == undefined) {
            ShowMessage("请选择类型", 3);
            return false;

        }
        var conData = new Object();
        conData.iBJ_KCK = 0;
        SelectSK("TB_HYKHM_OLD", "HF_HYID", "", conData);
    });

    $("#btn_HYKHM_NEW").click(function () {
        var conData = new Object();
        conData.iBJ_KCK = 1;
        SelectSK("TB_HYKHM_NEW", "", "", conData);
    });


    $("#TB_HYKHM_OLD").bind('keypress', function (event) {//#TB_CDNR,
        if (event.keyCode == "13") {

            var s=$("[name='LX']:checked").val();
            if ($("[name='LX']:checked").val() == undefined) {
                    ShowMessage("请选择类型", 3);
                    return false;

            }
            GetHYXX();
        }
    });

    $("#TB_HYKHM_NEW").bind('keypress', function (event) {//#TB_CDNR,
        if (event.keyCode == "13") {
            GetKCKXX();
        }
    });


    $(function () {
        $('input:radio[name="LX"]').change(function () {
         



            if ($("[name='LX']:checked").val() == 1) {

                document.getElementById("TB_HYKNAME_NEW").disabled = false;
                document.getElementById("TB_HYKHM_NEW").disabled = true;

          
            }
            if ($("[name='LX']:checked").val() == 2) {
                document.getElementById("TB_HYKNAME_NEW").disabled = true;
                document.getElementById("TB_HYKHM_NEW").disabled = false;

            }
            

            

        });



    });

})

function onHYKClick(e, treeId, treeNode) {
    $("#TB_HYKNAME_NEW").val(treeNode.name);
    $("#HF_HYKTYPE_NEW").val(treeNode.id);
        hideMenu("menuContentHYKTYPE");   
};

function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContent");
}

function GetHYXX() {
    if ($("#TB_HYKHM_OLD").val() != "") {
        var str = GetHYXXData(0, $("#TB_HYKHM_OLD").val());
        if (str) {
            var Obj = JSON.parse(str);
            $("#HF_HYID").val(Obj.iHYID);
            $("#HF_HYKTYPE_OLD").val(Obj.iHYKTYPE);
            $("#LB_HY_NAME").text(Obj.sHY_NAME);
            $("#LB_HYKTYPE_OLD").text(Obj.sHYKNAME);
            $("#LB_JF").text(Obj.fWCLJF);
            $("#LB_JE").text(Obj.fCZJE);

            $("#LB_BGDDMC").text(Obj.sBGDDMC);
            $("#LB_BGDDDM").text(Obj.sBGDDDM);

            if ($("[name='LX']:checked").val() == 1) {
                $("#TB_HYKHM_NEW").val(Obj.sHYK_NO);

            }
        }
    }
}

function GetKCKXX() {
    if ($("#TB_HYKHM_NEW").val() != "") {
        //if ($("#TB_HYKHM_NEW").val() == $("#TB_HYKHM_OLD").val()) {
        //    ShowMessage("该卡与原卡卡号一致，请重新选择", 3);
        //    $("#TB_HYKHM_NEW").val("");
        //    return;
        //}
        var str = GetKCKXXData($("#TB_HYKHM_NEW").val(), "");
        if (str == "null" || str == "") {
            ShowMessage("没有找到卡号", 3);
            $("#TB_HYKHM_NEW").val("");
            return;
        }
        var Obj = JSON.parse(str);
        if (Obj.sCZKHM == "") {
            ShowMessage("没有找到卡号", 3);
            $("#TB_HYKHM_NEW").val("");
            return;
        }
        if (Obj.iSTATUS != 1) {
            ShowMessage("该卡不是领用状态，请重新选择", 3);
            $("#TB_HYKHM_NEW").val("");
            return;
        }
        //var boolFK = CheckBGDDQX(Obj.sBGDDDM, iDJR);
        var boolFK = CheckBGDDQX(Obj.sBGDDDM, iDJR);
        if (boolFK.iBJ_CHECK == false) {
            ShowMessage("该操作员没有该保管地点的操作权限！", 3);
            $("#LB_BGDDMC").text("");
            $("#LB_BGDDDM").text("");
            $("#TB_HYKNAME_NEW").val("");
            $("#HF_HYKTYPE_NEW").val("");
            return;
        }
        else {
            $("#LB_BGDDMC").text(Obj.sBGDDMC);
            $("#LB_BGDDDM").text(Obj.sBGDDDM);
            $("#TB_HYKNAME_NEW").val(Obj.sHYKNAME);
            $("#HF_HYKTYPE_NEW").val(Obj.iHYKTYPE);
        }
        //if (boolFK == "False") {
        //    ShowMessage("该操作员没有该保管地点的操作权限！", 3);
        //    $("#LB_BGDDMC").text("");
        //    $("#LB_BGDDDM").text("");
        //    $("#LB_HYKNAME_NEW").text("");
        //    $("#LB_HYKTYPE_NEW").text("");
        //    return;
        //}
        //else {
        //    $("#LB_BGDDMC").text(Obj.sBGDDMC);
        //    $("#LB_BGDDDM").text(Obj.sBGDDDM);
        //    $("#LB_HYKNAME_NEW").text(Obj.sHYKNAME);
        //    $("#LB_HYKTYPE_NEW").text(Obj.iHYKTYPE);
        //}
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
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sHYKHM_OLD = $("#TB_HYKHM_OLD").val();
    Obj.iHYID = $("#HF_HYID").val();
    Obj.iHYKTYPE_OLD = $("#HF_HYKTYPE_OLD").val();
    Obj.fJF = $("#LB_JF").text();
    Obj.fJE = $("#LB_JE").text();
    Obj.sHYKHM_NEW = $("#TB_HYKHM_NEW").val();
    //Obj.iHYKTYPE_NEW = $("#HF_HYKTYPE_NEW").val(); 
    Obj.iHYKTYPE_NEW = $("#HF_HYKTYPE_NEW").val();
   // Obj.fBDJF = $("#TB_BDJF").val();
    Obj.sBGDDDM = $("#LB_BGDDDM").text();
    Obj.sZY = $("#TB_ZY").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}



function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_HYKHM_OLD").val(Obj.sHYKHM_OLD);
    $("#HF_HYID").val(Obj.iHYID);
    $("#LB_HY_NAME").text(Obj.sHY_NAME);
    $("#HF_HYKTYPE_OLD").val(Obj.iHYKTYPE_OLD);
    $("#LB_HYKTYPE_OLD").text(Obj.sHYKNAME_OLD);
    $("#TB_HYKHM_NEW").val(Obj.sHYKHM_NEW);
    $("#HF_HYKTYPE_NEW").val(Obj.iHYKTYPE_NEW);
    $("#TB_HYKNAME_NEW").val(Obj.sHYKNAME_NEW);
    
    $("#LB_JF").text(Obj.fJF);
    $("#LB_JE").text(Obj.fJE);
    $("#TB_BDJF").val(Obj.fBDJF);
    $("#HF_BDJF").val(Obj.fBDJF);
    $("#LB_BGDDMC").text(Obj.sBGDDMC);
    $("#LB_BGDDDM").text(Obj.sBGDDDM);
    $("#TB_ZY").val(Obj.sZY);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}

function MoseDialogCustomerReturn(dialogName, lst, showField) {
    if (dialogName == "DialogSK") {
        $("#" + showField).blur()
    }
}