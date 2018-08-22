vUrl = "../GTPT.ashx";
$(document).ready(function () {
    document.getElementById("zxr1").innerHTML = "处理人";
    document.getElementById("hfjg").style.display = "none";
    RefreshButtonSep();
});


function SetControlState() {
    $("#B_Start").hide();
    $("#B_Insert").hide();
    $("#B_Delete").hide();
    $("#B_Update").hide();
    $("#B_Exec").hide();

}


function IsValidData() {
    if ($("#TB_CLYJ").val() == "") {
        ShowMessage("处理意见不能为空");
        return false;
    }

    return true;
}

function SaveData() {
    var Obj = new Object();        
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iSTATUS = $("#HF_STATUS").val();
    Obj.iTSLX = $("#HF_TSLX").val();
    Obj.dTSRQ = $("#LB_TSRQ").text();
    Obj.sGKXM = $("#LB_GKXM").text();
    Obj.sLXDH = $("#LB_LXDH").text();
    Obj.sMDMC = $("#LB_MDMC").text();
    Obj.iMDID = $("#HF_MDID").val();
    Obj.sHYK_NO = $("#LB_HYK_NO").text();
    Obj.iHYID = $("#HF_HYID").val();
    Obj.sOPENID = $("#HF_OPENID").val();
    Obj.sTSNR = $("#LB_TSNR").text();
    Obj.sCLYJ = $("#TB_CLYJ").val();
    Obj.sFKXX = $("#TB_FKXX").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#LB_STATUS").text(Obj.sSTATUSMC);
    $("#HF_STATUS").val(Obj.iSTATUS);

    if (Obj.iSTATUS >= 1) {
        $("#TB_CLYJ").disable = true;
        if (Obj.dHFRQ != "") {
            if (Obj.iHFJG == 1) {
                $("#LB_HFJG").text("非常满意");
            }
            if (Obj.iHFJG == 2) {
                $("#LB_HFJG").text("满意");
            }
            if (Obj.iHFJG == 3) {
                $("#LB_HFJG").text("一般");
            }
            if (Obj.iHFJG == 4) {
                $("#LB_HFJG").text("不满意");
            }
        }


        document.getElementById("hfjg").style.display = "block";
        $("#LB_ZXRMC").text(Obj.sZXRMC);
        $("#HF_ZXR").val(Obj.iZXR);
        $("#LB_ZXRQ").text(Obj.dZXRQ);
        //$("#LB_QDRMC").text(Obj.sHFRMC);
        //$("#HF_QDR").val(Obj.iHFR);
        $("#LB_QDSJ").text(Obj.dHFRQ);
    }

    $("#LB_TSLX").text(Obj.sTS);
    $("#HF_TSLX").val(Obj.iTSLX);
    $("#LB_TSRQ").text(Obj.dTSRQ);
    $("#LB_GKXM").text(Obj.sGKXM);
    $("#LB_LXDH").text(Obj.sLXDH);
    $("#LB_MDMC").text(Obj.sMDMC);
    $("#HF_MDID").val(Obj.iMDID);
    $("#LB_HYK_NO").text(Obj.sHYK_NO);
    $("#HF_HYID").val(Obj.iHYID);
    $("#HF_OPENID").val(Obj.sOPENID);
    $("#LB_TSNR").text(Obj.sTSNR);
    $("#TB_CLYJ").val(Obj.sCLYJ);
    $("#TB_FKXX").val(Obj.sFKXX);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);


}