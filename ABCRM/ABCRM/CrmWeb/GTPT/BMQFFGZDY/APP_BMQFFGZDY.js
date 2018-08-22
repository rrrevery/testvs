vUrl = "../GTPT.ashx";

$(document).ready(function () {
    $("#B_Exec").show();

});

function IsValidData() {
    if ($("#TB_GZMC").val() == "") {
        ShowMessage("请输入规则名称");

        return false;
    }
    if ($("#TB_XZCS").val() == "") {
        ShowMessage("请输入总限制次数");

        return false;
    }
    if ($("#TB_XZTS").val() == "") {
        ShowMessage("请输入总限制提示");

        return false;
    }
    if ($("#TB_XZCS_HY_T").val() == "") {
        ShowMessage("请输入会员每日限制次数");

        return false;
    }
    if ($("#TB_XZTS_HY_R").val() == "") {
        ShowMessage("请输入会员每日限制提示");

        return false;
    }
    if ($("#TB_CZCS_HY").val() == "") {
        ShowMessage("请输入会员限制次数");

        return false;
    }
    if ($("#TB_XZTS_HY").val() == "") {
        ShowMessage("请输入会员限制提示");

        return false;
    }
    if ($("#TB_XZCS_R").val() == "") {
        ShowMessage("请输入单日限制次数");

        return false;
    }
    if ($("#TB_XZTS_R").val() == "") {
        ShowMessage("请输入单日限制提示");

        return false;
    }
    return true;
}


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sGZMC = $("#TB_GZMC").val();
    Obj.iXZCS = $("#TB_XZCS").val();
    Obj.sXZTS = $("#TB_XZTS").val();
    Obj.iXZCS_HY_T =  $("#TB_XZCS_R").val();
    Obj.sXZTS_HY_T = $("#TB_XZTS_R").val();
    Obj.iXZCS_HY = $("#TB_CZCS_HY").val();
    Obj.sXZTS_HY = $("#TB_XZTS_HY").val();
    Obj.iXZCS_R = $("#TB_XZCS_HY_T").val();
    Obj.sXZTS_R =  $("#TB_XZTS_HY_R").val();

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iBMQFFGZID);
    $("#TB_GZMC").val(Obj.sGZMC);
    $("#TB_XZCS").val(Obj.iXZCS);
    $("#TB_XZTS").val(Obj.sXZTS);
    $("#TB_XZCS_HY_T").val(Obj.iXZCS_R);
    $("#TB_XZTS_HY_R").val(Obj.sXZTS_R);
    $("#TB_CZCS_HY").val(Obj.iXZCS_HY);
    $("#TB_XZTS_HY").val(Obj.sXZTS_HY);
    $("#TB_XZCS_R").val(Obj.iXZCS_HY_T);
    $("#TB_XZTS_R").val(Obj.sXZTS_HY_T);

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);

}



