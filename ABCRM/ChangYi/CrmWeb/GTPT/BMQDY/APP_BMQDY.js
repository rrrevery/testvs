vUrl = "../GTPT.ashx";

$(document).ready(function () {
   
    $("#B_Exec").show();
  //  $("#status-bar").hide();

    BFButtonClick("TB_YHQMC", function () {
        var conData = new Object();
        conData.iBJCODED = 1;
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", true, conData);
    });

});

function IsValidData() {
    if ($("#TB_BMQMC").val() == "") {
        ShowMessage("请输入编码券名称");

        return false;
    }
    if ($("#HF_YHQID").val() == "") {
        ShowMessage("请选择优惠券");

        return false;
    }
    return true;
}


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iYHQID = $("#HF_YHQID").val();
    Obj.sBMQMC = $("#TB_BMQMC").val();
    Obj.sSYSM = $("#TB_MS").val();
    Obj.iBJ_TY = $("#DDL_TY")[0].checked ? "1" : "0";
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
 
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iBMQID);
    $("#TB_BMQMC").val(Obj.sBMQMC);
    $("#HF_YHQID").val(Obj.iYHQID);
    $("#TB_YHQMC").val(Obj.sYHQMC);
    $("#TB_MS").val(Obj.sSYSM);
    $("#DDL_TY").prop("checked", Obj.iBJ_TY == "1" ? true : false);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);

}



