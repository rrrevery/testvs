vUrl = "../JKPT.ashx";

function GetHYXX() {
    if ($("#TB_HYKNO").val() != "") {
        var str = GetHYXXData(0, $("#TB_HYKNO").val(), "CRMDB");
        $("#HF_HYID").val(0);
        if (str == "null" || str == "") {
            art.dialog({ lock: true, content: "没有找到卡号" });
            return;
        }
        if (str.indexOf("错误") >= 0) {
            art.dialog({ lock: true, content: str });
            return;
        }
        var Obj = JSON.parse(str);
        $("#HF_HYID").val(Obj.iHYID);

    }
}

$(document).ready(function () {
    $("#TB_HYKNO").bind('keypress', function (event) {//#TB_CDNR,
        if (event.keyCode == "13") {
            GetHYXX();
        }
    });
    $("#TB_HYKNO").change(function () {
        GetHYXX();
    });
    BFButtonClick("TB_MDMC", function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });
});

function IsValidData() {
    if ($("#TB_HYKNO").val() == "") {
        art.dialog({ lock: true, content: "请输入卡号" });
        return false;
    }
    if ($("#HF_HYID").val() == "" || $("#HF_HYID").val() == undefined || $("#HF_HYID").val() == null) {
        art.dialog({ lock: true, content: "会员不存在！请重新录入信息" });
        return false;
    }
    //if ($("#TB_XFRQ").val() == "") {
    //    art.dialog({ lock: true, content: "请输入可疑消费日期"});
    //    return false;
    //}
    if ($("#HF_MDID").val() == "") {
        art.dialog({ lock: true, content: "请选择门店" });
        return false;
    }
    if ($("#rd_FKY")[0].checked == false) {
        if ($("#rd_KY")[0].checked == false) {
            art.dialog({ lock: true, content: '请选择修改的状态' });
            return false;
        }
    }

    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iHYID = $("#HF_HYID").val();
    Obj.iMDID = $("#HF_MDID").val();
    Obj.sBZ = $("#TB_BZ").val();
    if ($("#TB_XFNY").val() != "")
        Obj.iXFNY = $("#TB_XFNY").val();
    Obj.dXFRQ = $("#TB_XFRQ").val();
    Obj.iBJ_KY = $("[name='status']:checked").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#HF_HYID").val(Obj.iHYID);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_HYKNO").val(Obj.sHYK_NO);
    $("#TB_BZ").val(Obj.sBZ);
    $("#TB_XFRQ").val(Obj.dXFRQ);
    $("#TB_XFNY").val(Obj.iXFNY);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    if (Obj.iBJ_KY == "0") {
        $("#rd_FKY")[0].checked = true;
    }
    if (Obj.iBJ_KY == "1") {
        $("#rd_KY")[0].checked = true;
    }
}