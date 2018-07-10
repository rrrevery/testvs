vUrl = "../GTPT.ashx";

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();

    BFUploadClick("TB_IMG", "HF_IMAGEURL", "FtpImg/WXFXDY");


});
function IsValidData() {
    if ($("#TB_NAME").val() == "") {
        ShowMessage("请输入名称");
        return false;
    }
    if ($("#TB_TITLE").val() == "") {
        ShowMessage("请输入分享标题");
        return false;
    }
    if ($("#TB_DESCRIBE").val() == "") {
        ShowMessage("请输入分享描述");
        return false;
    }
    if ($("#TB_URL").val() != "") {
        if (!IsURL($("#TB_URL").val())) {
            ShowMessage( "请输入正确的URL");
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
    Obj.sNAME = $("#TB_NAME").val();
    Obj.sDESCRIBE = $("#TB_DESCRIBE").val();
    Obj.sIMG = $("#TB_IMG").val();
    Obj.sURL = $("#TB_URL").val();
    Obj.sTITLE = $("#TB_TITLE").val();
    Obj.iBJ_SHARE = $("#CB_BJ_SHARE")[0].checked ? "1" : "0";
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_NAME").val(Obj.sNAME);
    $("#TB_DESCRIBE").val(Obj.sDESCRIBE);
    $("#TB_IMG").val(Obj.sIMG);
    $("#TB_URL").val(Obj.sURL);
    $("#TB_TITLE").val(Obj.sTITLE);
    $("#CB_BJ_SHARE").prop("checked", Obj.iBJ_SHARE == "1" ? true : false);
}

