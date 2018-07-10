vUrl = "../GTPT.ashx";

$(document).ready(function () {
    BFButtonClick("TB_LPMC", function () {
        SelectLPXX("TB_LPMC", "HF_LPID", "zHF_LPID", true);
    });


    BFUploadClick("TB_IMG", "HF_IMAGEURL", "FtpImg/wx_lptp");
    BFUploadClick("TB_PIC_URL", "HF_IMAGEURL", "FtpImg/wx_lptplogo");
});

function IsValidData() {
    if ($("#TB_WXDHJF").val() == "") {
        ShowMessage("请填写兑换积分", 3);
        return false;
    }
    return true;
}

function SetControlState() {
    $("#jlbh").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Exec").hide();
    $("#status-bar").hide();
    document.getElementById("TB_LPMC").disabled = true;
    RefreshButtonSep();

}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#HF_LPID").val();
    Obj.sLPMC = $("#TB_LPMC").val();
    Obj.sIMG = $("#TB_IMG").val();
    Obj.sPIC_URL = $("#TB_PIC_URL").val();
    Obj.sLPQC = $("#TB_LPQC").val();
    Obj.iDHJF = $("#TB_WXDHJF").val();
    Obj.iBJ_NORMAL = $("#DDL_LX").find("option:selected").val();
    Obj.sLPJS = editor.html();

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);

    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_LPID").val(Obj.iJLBH);
    $("#TB_LPMC").val(Obj.sLPMC);
    $("#TB_IMG").val(Obj.sIMG);
    $("#TB_PIC_URL").val(Obj.sPIC_URL);
    $("#TB_LPQC").val(Obj.sLPQC);
    $("#TB_WXDHJF").val(Obj.iDHJF);
    $("#DDL_LX").val(Obj.iBJ_NORMAL);
    editor.html(decodeURIComponent(Obj.sLPJS));
}







