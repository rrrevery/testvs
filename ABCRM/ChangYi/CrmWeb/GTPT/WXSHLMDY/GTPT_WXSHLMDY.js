vUrl = "../GTPT.ashx";

$(document).ready(function () {


    $("#status-bar").hide();
    $("#B_Exec").hide();
    BFButtonClick("TB_LMSHLXMC", function () {
        SelectLMSHLX("TB_LMSHLXMC", "HF_LMSHLXID", "zHF_LMSHLXID", true);
    });


    //$('#B_LOGO').click(function () {
    //    if (!/\.(gif|jpg|jpeg|png|GIF|JPG|PNG)$/.test($("#files1").val())) {
    //        ShowMessage("图片类型必须是.gif,jpeg,jpg,png中的一种");
    //        return;
    //    }
    //    UploadPicture("form1", "TB_LOGO", "LMSHDYLOGO");
    //});
    //$('#B_IMG').click(function () {
    //    if (!/\.(gif|jpg|jpeg|png|GIF|JPG|PNG)$/.test($("#files").val())) {
    //        ShowMessage("图片类型必须是.gif,jpeg,jpg,png中的一种");
    //        return;
    //    }
    //    UploadPicture("form2", "TB_IMG", "LMSHDY");
    //});
    BFUploadClick("TB_LOGO", "HF_IMAGEURL", "FtpImg/LMSHDYLOGO");
    BFUploadClick("TB_IMG", "HF_IMAGEURL", "FtpImg/LMSHDY");


});



function IsValidData() {
    if ($("#TB_LMSHMC").val() == "") {
       ShowMessage("请输入联盟商户名称");
       return false;
    }

    if ($("#TB_LMSHLXMC").val() == "") {
        ShowMessage("请选择类型名称");
        return false;
    }


    if ($("#TB_SCRS").val() != "") {
        if (isNaN($("#TB_SCRS").val())) {
            ShowMessage("收藏人数请输入数字");
            return false;
        }
    }

    if ($("#TB_INX").val() != "") {
        if (isNaN($("#TB_INX").val())) {
            ShowMessage("显示顺序请输入数字");
            return false;
        }
    }

    if ($("#TB_IMG").val() == "") {
        ShowMessage("请上传联盟商户图片");
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sLMSHMC = $("#TB_LMSHMC").val();
    Obj.iLXID = $("#HF_LMSHLXID").val();
    Obj.sRJDJ = $("#TB_RJDJ").val();
    Obj.sBZ = $("#TB_BZ").val
    Obj.sLOGO = $("#TB_LOGO").val();
    Obj.sIMG = $("#TB_IMG").val();
    Obj.sNR = $("#TB_NR").val();
    Obj.sYHJS = $("#TB_YHJS").val();
    Obj.sADDRESS = $("#TB_ADDRESS").val();
    Obj.sTIME = $("#TB_YJSJ").val();
    Obj.sPHONE = $("#TB_PHONE").val();
    if ($("#TB_LAT").val() != "") {
        Obj.sLAT = $("#TB_LAT").val();
    } else {
        Obj.sLAT = 0;
    }

    if ($("#TB_LEN").val() != "") {
        Obj.sLEN = $("#TB_LEN").val();
    } else {
        Obj.sLEN = 0;
    }

    Obj.sTITLE = $("#TB_TITLE").val();
    Obj.sCONTENT = $("#TB_CONTENT").val();

    if ($("#TB_SCRS").val() != null && $("#TB_SCRS").val() != "")
        Obj.iSCRS = $("#TB_SCRS").val();

    if ($("#TB_INX").val() != null && $("#TB_INX").val() != "")
        Obj.iINX = $("#TB_INX").val();

   
    Obj.iBJ_TY = $("#CK_iBJ_TY")[0].checked ? 1 : 0;

    Obj.iCHANNELID = $('#DDL_CHANNELID').combobox('getValue');


    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_LMSHMC").val(Obj.sLMSHMC);
    $("#HF_LMSHLXID").val(Obj.iLXID);
    $("#TB_LMSHLXMC").val(Obj.sLXMC);
    $("#TB_RJDJ").val(Obj.sRJDJ);
    $("#TB_IMG").val(Obj.sIMG);
    $("#TB_LOGO").val(Obj.sLOGO);
    $("#TB_BZ").val(Obj.sBZ);
    $("#TB_NR").val(Obj.sNR);
    $("#TB_YHJS").val(Obj.sYHJS);
    $("#TB_ADDRESS").val(Obj.sADDRESS);
    $("#TB_YJSJ").val(Obj.sTIME);
    $("#TB_PHONE").val(Obj.sPHONE);
    $("#TB_LAT").val(Obj.sLAT);
    $("#TB_LEN").val(Obj.sLEN);
    $("#TB_TITLE").val(Obj.sTITLE);
    $("#TB_CONTENT").val(Obj.sCONTENT);
    $("#TB_SCRS").val(Obj.iSCRS);
    $("#TB_INX").val(Obj.iINX);
    $("#CK_iBJ_TY")[0].checked = Obj.iBJ_TY == "1" ? true : false;
    $('#DDL_CHANNELID').combobox("setValue", Obj.iCHANNELID);
}



function InsertClickCustom() {
    var str = GetSelfInx("MOBILE_LMSHDY", "INX");
    if (str != "" && str != null) {
        var Obj = JSON.parse(str);
        $("#TB_INX").val(Obj.iINX + 1);
    }
};


