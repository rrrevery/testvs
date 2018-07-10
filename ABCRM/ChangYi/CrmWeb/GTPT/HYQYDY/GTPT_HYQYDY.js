vUrl = "../GTPT.ashx";

$(document).ready(function () {

    $("#status-bar").hide();
    $("#B_Exec").hide();
    //$('#upload').click(function () {

    //    if (!/\.(gif|jpg|jpeg|png|GIF|JPG|PNG)$/.test($("#files").val())) {
    //        ShowMessage("图片类型必须是.gif,jpeg,jpg,png中的一种");
    //        return;
    //    }
    //    UploadPicture("form2", "TB_IMG", "HYQYZHUTU");
    //});
    //$('#upload1').click(function () {

    //    if (!/\.(gif|jpg|jpeg|png|GIF|JPG|PNG)$/.test($("#files1").val())) {
    //        ShowMessage("图片类型必须是.gif,jpeg,jpg,png中的一种");
    //        return;
    //    }
    //    UploadPicture("form1", "TB_LOGO", "HYQYLOGO");
    //});

    BFUploadClick("TB_IMG", "HF_IMAGEURL", "FtpImg/HYQYZHUTU");
    BFUploadClick("TB_LOGO", "HF_IMAGEURL", "FtpImg/HYQYLOGO");

    RefreshButtonSep();
})


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sNAME = $("#TB_NAME").val();
    Obj.sHEAD = $("#TB_HEAD").val();
    Obj.sIMG = $("#TB_IMG").val();
    Obj.sLOGO = $("#TB_LOGO").val();
    Obj.sQYNR = editor.html();
    return Obj;
}


function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);

    $("#TB_NAME").val(Obj.sNAME);
    $("#TB_HEAD").val(Obj.sHEAD);
    $("#TB_IMG").val(Obj.sIMG);
    $("#TB_LOGO").val(Obj.sLOGO);
    editor.html(decodeURIComponent(Obj.sQYNR));
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID)


}


function IsValidData() {

    return true;
}
