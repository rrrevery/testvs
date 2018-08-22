vUrl = "../GTPT.ashx";

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();
    BFButtonClick("TB_WXMDMC", function () {
        SelectWXMD("TB_WXMDMC", "HF_WXMDID", "zHF_WXMDID", false);
    });
    BFUploadClick("TB_IMG", "HF_IMAGEURL", "FtpImg/WXLCTP");

    //$('#upload').click(function () {  
    //    if (!/\.(gif|jpg|jpeg|png|GIF|JPG|PNG)$/.test($("#files").val())) {
    //        ShowMessage("图片类型必须是.gif,jpeg,jpg,png中的一种", 3);
    //        return;
    //    }
    //    UploadPicture("form1", "TB_IMG", "WXLCTP");

    //});
});
function IsValidData() {

    if ($("#HF_WXMDID").val() == "") {
        ShowMessage("请选择门店名称", 3);
        return false;
    }
    if ($("#TB_INX").val() == "") {
        ShowMessage("请输入序号", 3);
        return false;
    }
    if ($("#TB_NAME").val() == "") {
        ShowMessage("请输入楼层名称", 3);
        return false;
    }

    //if ($("#TB_IMG").val() == "") {
    //    ShowMessage("请上传楼层图片", 3);
    //    return false;
    //}
    return true;
}


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iWX_MDID = $("#HF_WXMDID").val();
    Obj.iINX = $("#TB_INX").val();
    Obj.sNAME = $("#TB_NAME").val();
    Obj.sIMG = $("#TB_IMG").val();

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_WXMDID").val(Obj.iWX_MDID);
    $("#TB_WXMDMC").val(Obj.sMDMC);
    $("#TB_INX").val(Obj.iINX);
    $("#TB_NAME").val(Obj.sNAME);
    $("#TB_IMG").val(Obj.sIMG);
}
