vUrl = "../GTPT.ashx";
$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();

    //$("#upload").click(function () {
    //    if (!/\.(gif|jpg|jpeg|png|GIF|JPG|PNG)$/.test($("#files").val())) {
    //        ShowMessage("图片类型必须是.gif,jpeg,jpg,png中的一种");
    //        return;
    //    }
    //    UploadPicture("form2", "TB_IMG", "FLXXIMG");
    //});


    BFUploadClick("TB_IMG", "HF_IMAGEURL", "FtpImg/FLXXIMG");

});


function IsValidData() {
    if ($("#TB_FLMC").val() == "") {
        ShowMessage("请输入分类名称", 3);
        return false;
    }
    if ($("#TB_INX").val() == "") {
        ShowMessage("请输入序号", 3);
        return false;
    }
    return true;
}
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
       Obj.iJLBH = "0";
    Obj.sFLMC = $("#TB_FLMC").val();
    Obj.iINX = $("#TB_INX").val();
    Obj.sIMG = $("#TB_IMG").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);   
    $("#TB_FLMC").val(Obj.sFLMC);
    $("#TB_INX").val(Obj.iINX);
    $("#TB_IMG").val(Obj.sIMG);
}
