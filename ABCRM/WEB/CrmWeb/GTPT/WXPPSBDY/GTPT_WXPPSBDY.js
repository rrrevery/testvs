vUrl = "../GTPT.ashx";


$(document).ready(function () {

    BFUploadClick("TB_LOGO", "HF_IMAGEURL", "FtpImg/PPSBLOGO");
    BFUploadClick("TB_IMG", "HF_IMAGEURL", "FtpImg/PPSBIMG");


    $("#TB_FLMC").click(function () {
        SelectFLMC("TB_FLMC", "HF_FLID", "zHF_FLID", true);
    });
});

function SetControlState() {
    $("#status-bar").hide();
    $("#B_Exec").hide();
}

function IsValidData() {

    if ($("#TB_SBMC").val() == "") {
        ShowMessage("商标名称能为空", 3);
        return false;
    }
    if ($("#TB_FLMC").val() == "") {
        ShowMessage("分类名称不能为空", 3);
        return false;
    }
    if ($("#TB_INX").val() == "") {
        ShowMessage("请输入优先级", 3);
        return false;
    }
    if ($("#TB_YWM").val() == "") {
        ShowMessage("英文名称不能为空", 3);
        return false;
    }

    if ($("#TB_PHONE").val() == "") {
        ShowMessage("请填写电话", 3);
        return false;
    }

    //if ($("#TB_IP").val() == "") {
    //    ShowMessage("请填写网址", 3);
    //    return false;
    //}

    if ($("#TB_DZ").val() == "") {
        ShowMessage("请填写地址", 3);
        return false;
    }

    if ($("#TB_IP").val() != "") {
        if (!IsURL($("#TB_IP").val())) {
            ShowMessage("请输入正确的网址", 3);
            return false;

        }
    }
    if ($("#TB_IMG").val() == "") {
        ShowMessage("请上传商标主图片", 3);
        return false;
    }
    if ($("#TB_LOGO").val() == "") {
        ShowMessage("请上传商标logo", 3);
        return false;
    }
    return true;
}



function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sSBMC = $("#TB_SBMC").val();
    Obj.iFLID = $("#HF_FLID").val();
    Obj.sPHONE = $("#TB_PHONE").val();
    Obj.sDZ = $("#TB_DZ").val();
    Obj.sIP = $("#TB_IP").val();
    Obj.iINX = $("#TB_INX").val();
    Obj.sIMG = $("#TB_IMG").val();
    Obj.sLOGO = $("#TB_LOGO").val();
    Obj.sYWM = $("#TB_YWM").val();
    Obj.sCONTENT = editor.html();

    return Obj;
}



function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_SBMC").val(Obj.sSBMC);
    $("#TB_FLMC").val(Obj.sFLMC);
    $("#HF_FLID").val(Obj.iFLID);
    $("#TB_PHONE").val(Obj.sPHONE);
    $("#TB_IP").val(Obj.sIP);
    $("#TB_DZ").val(Obj.sDZ);
    $("#TB_INX").val(Obj.iINX);
    $("#TB_IMG").val(Obj.sIMG);
    $("#TB_LOGO").val(Obj.sLOGO);
    $("#TB_YWM").val(Obj.sYWM);    
    editor.html(decodeURIComponent(Obj.sCONTENT));
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID);

}
function UpdateClick() {
    vProcStatus = cPS_MODIFY;
    SetControlBaseState();
    UpdateClickCustom();
    document.getElementById("TB_SBMC").disabled = true;

};

