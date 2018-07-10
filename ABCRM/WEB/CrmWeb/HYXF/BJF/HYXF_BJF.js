vUrl = "../HYXF.ashx";
var HYKNO = GetUrlParam("HYKNO");

$(document).ready(function () {
    FillBGDDTree("TreeBGDD", "TB_BGDDMC");

    ;
    $("#TB_XFJE").blur(function () {
        CalcJFBL();
    });



    $("#TB_MC").click(function () {
        SelectJFGZ("TB_MC", "HF_ID", "zHF_ID", true);

    })

    $("#TB_HYK_NO").bind('keypress', function (event) {//#TB_CDNR,
        if (event.keyCode == "13") {
            GetHYXX();
        }
    });

    $("#TB_XFJE").bind('keypress', function (event) {//#TB_CDNR,
        if (event.keyCode == "13") {
            CalcJFBL();
        }
    });



});




function GetHYXX() {
    if ($("#TB_HYK_NO").val() != "") {
        var str = GetHYXXData(0, $("#TB_HYK_NO").val(), "CRMDB", $("#TB_CDNR").val());
        $("#HF_HYID").val(0);
        if (str == "null" || str == "") {
            art.dialog({ lock: true, content: "没有找到卡号" });
            return;
        }
        var Obj = JSON.parse(str);
        if (Obj.iSTATUS == -1) {
            art.dialog({ lock: true, content: "该卡已作废,不可以补积分" });
            return;
        }
        $("#HF_HYID").val(Obj.iHYID);
        $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
        $("#LB_HYKNAME").text(Obj.sHYKNAME);
        //$("#LB_JE").text(Obj.fCZJE);
        if ($("#TB_CDNR").val() != "") {
            $("#TB_HYK_NO").val(Obj.sHYK_NO);
        }


    }
}






function SetControlState() {

    //$("#status-bar").hide();
}

function CalcJFBL() {
    if ($("#TB_MC").val() != "" && $("#TB_XFJE").val() != "") {
        var data = GetJFBL($("#HF_ID").val());
        if (data) {
            var obj = JSON.parse(data);
            $("#HF_JF").val(obj.fJF);
            $("#HF_JE").val(obj.fJE);
            $("#LB_JF").text((Math.floor($("#TB_XFJE").val() / $("#HF_JE").val()) * $("#HF_JF").val()));//四舍五入
        }
    }
}



function IsValidData() {
    if ($("#HF_HYID").val() == "") {
        art.dialog({ content: "请选择会员", lock: true, time: 2 });
        return false;
    }
    if ($("#TB_XFJE").val() == "") {
        art.dialog({ content: "请输入消费金额", lock: true, time: 2 });
        return false;
    }

    if ($("#HF_BGDDDM").val() == "") {
        art.dialog({ content: "请选择保管地点", lock: true, time: 2 });
        return false;
    }
    return true;
}
function TreeNodeClickCustom(event, treeId, treeNode) {
    switch (treeId) {
        case "TreeBGDD": $("#HF_BGDDDM").val(treeNode.sBGDDDM); break;
    }
}
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";

    Obj.sMC = $("#TB_MC").val();

    Obj.iID = $("#HF_ID").val();
    Obj.fJE = $("#TB_XFJE").val();
    Obj.fJF = $("#LB_JF").text();
    Obj.iHYID = $("#HF_HYID").val();
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);

    $("#HF_ID").val(Obj.iID);
    $("#TB_MC").val(Obj.sMC);

    $("#TB_XFJE").val(Obj.fJE);
    $("#LB_JF").text(Obj.fJF);
    $("#HF_HYID").val(Obj.iHYID);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TB_HYK_NO").val(Obj.sHYK_NO);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);

}