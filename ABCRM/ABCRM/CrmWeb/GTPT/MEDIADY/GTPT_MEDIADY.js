vUrl = "../GTPT.ashx";

$(document).ready(function () {
    $(".rule_ask").hide();

    $('#DDL_TYPE').combobox({
        onSelect: function (record) {
            selectValueChange(record.value);
        }
    });
    $('#upload_img').click(function () {
        if (!/\.(bmp|png|jpeg|jpg|gif)$/.test($("#file_image").val())) {
            ShowMessage("图片类型必须是.bmp,.gif,jpeg,jpg,png中的一种")
            return;
        }
        UploadPictureToWXServer("form1", "HF_MID", "image");
    });
    $('#upload_voice').click(function () {
        if (!/\.(AMR|MP3|mp3|amr)$/.test($("#file_voice").val())) {
            ShowMessage("语音类型必须是.mp3,.amr中的一种")
            return;
        }
        UploadPictureToWXServer("form2", "HF_MID", "voice");
    });
    $('#upload_video').click(function () {
        if (!/\.(MP4|mp4)$/.test($("#file_video").val())) {
            ShowMessage("视频类型必须是.mp4")
            return;
        }
        UploadPictureToWXServer("form3", "HF_MID", "video");
    });
    $('#upload_thumb').click(function () {
        if (!/\.(JPG|jpg)$/.test($("#file_thumb").val())) {
            ShowMessage("缩略图类型必须是.jpg");
            return;
        }
        UploadPictureToWXServer("form4", "HF_MID", "thumb");
    });

});

function SetControlState() {
    //$("#B_Exec").hide();
    //$("#status-bar").hide();

}
function selectValueChange(ruleId, treeNode) {
    $(".rule_ask").hide();
    switch (parseInt(ruleId)) {
        case 2:
            $("#image").show();
            if (arguments.length > 1) {
                $("#TB_ImgTITLE").val(treeNode.sTITLE);
                $("#TB_DESCRIPTION").val(treeNode.sDESCRIPTION);
            }
            break;
        case 3:
            $("#voice").show();
            if (arguments.length > 1) {
                $("#TB_YYTITLE").val(treeNode.sTITLE);
                $("#TB_YYDESCRIPTION").val(treeNode.sDESCRIPTION);
            }
            break;
        case 4:
            $("#video").show();
            if (arguments.length > 1) {
                $("#TB_SPTLTLE").val(treeNode.sTITLE);
                $("#TB_SPDESCRIPTION").val(treeNode.sDESCRIPTION);
            }
            break;
        case 8:
            $("#thumb").show();
            if (arguments.length > 1) {
                $("#TB_SLTTITLE").val(treeNode.sTITLE);
                $("#TB_SLTDESCRIPTION").val(treeNode.sDESCRIPTION);
            }
            break;
        default:
            break;

    }
}
function IsValidData() {
    if (isNaN($('#DDL_TYPE').combobox('getValue'))) {
        ShowMessage("请选择类型", 3);
        return false;
    }
    switch (parseInt($('#DDL_TYPE').combobox('getValue'))) {
        case 2:
            if ($("#HF_MID").val().length == 0) {
                ShowMessage("请上传图片！", 3);
                return false;
            }
            if ($("#TB_ImgTITLE").val().length == 0) {
                ShowMessage("请填写图片标题", 3);
                return false;
            }
            if ($("#TB_DESCRIPTION").val().length == 0) {
                ShowMessage("请填写图片描述", 3);
                return false;
            }
            break;
        case 3:
            if ($("#HF_MID").val().length == 0) {
                ShowMessage("请上传语音文件！", 3);
                return false;
            }
            if ($("#TB_YYTITLE").val().length == 0) {
                ShowMessage("请填写语音标题", 3);
                return false;
            }
            if ($("#TB_YYDESCRIPTION").val().length == 0) {
                ShowMessage("请填写语音描述", 3);
                return false;
            }
            break;
        case 4:
            if ($("#HF_MID").val().length == 0) {
                ShowMessage("请上传视频文件！", 3);
                return false;
            }
            if ($("#TB_SPTLTLE").val().length == 0) {
                ShowMessage("请填写视频标题", 3);
                return false;
            }
            if ($("#TB_SPDESCRIPTION").val().length == 0) {
                ShowMessage("请填写视频描述", 3);
                return false;
            }
            break;
        case 8:
            if ($("#HF_MID").val().length == 0) {
                ShowMessage("请上传缩略图文件！", 3);
                return false;
            }
            if ($("#TB_SLTTITLE").val().length == 0) {
                ShowMessage("请填写缩略图标题", 3);
                return false;
            }
            if ($("#TB_SLTDESCRIPTION").val().length == 0) {
                ShowMessage("请填写缩略图描述", 3);
                return false;
            }
            break;
        default:
            break;
    }
    return true;
}


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iTYPE =$('#DDL_TYPE').combobox('getValue'); 
    Obj.sMEDIA_ID = $("#HF_MID").val();
    Obj.sURL = $("#TB_TWTPURL").val();
    switch (parseInt( GetSelectValue("DDL_TYPE"))) {

        case 2:
            Obj.sTITLE = $("#TB_ImgTITLE").val();
            Obj.sDESCRIPTION = $("#TB_DESCRIPTION").val();              
            break;
        case 3:
            Obj.sTITLE = $("#TB_YYTITLE").val();
            Obj.sDESCRIPTION = $("#TB_YYDESCRIPTION").val();
            break;
        case 4:
            Obj.sTITLE = $("#TB_SPTTITLE").val();
            Obj.sDESCRIPTION = $("#TB_SPDESCRIPTION").val();
            break;
        case 8:
            Obj.sTITLE = $("#TB_SLTTITLE").val();
            Obj.sDESCRIPTION = $("#TB_SLTDESCRIPTION").val();
            break;
        default: break;
    }

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    Obj.iLoginPUBLICID = iWXPID;
    

    return Obj;
}
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);   
    $("#DDL_TYPE").combobox("setValue", Obj.iTYPE); 
    $("#HF_MID").val(Obj.sMEDIA_ID);
    selectValueChange(Obj.iTYPE, Obj);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID);
}

function UploadPictureToWXServer(formID, tbName, type) {

    if ($("#selectPublicID").combobox("getValue") == "选择公众号") {
        ShowMessage("请选择公众号");
        return;
    }
    var obj = new Object();
    obj.title = $("#TB_SPTLTLE").val();
    obj.introduction = $("#TB_SPDESCRIPTION").val();

    $("#" + formID).ajaxSubmit({
        url: "../UpLoadMediaToWXServer.ashx?type=" + type + "&PUBLICIF=" + sWXPIF,
        dataType: "json",
        data:{postData:JSON.stringify(obj)},
        success: function (data) {
            if (data.errCode == 0) {
                ShowMessage("上传成功！");
                $("#" + tbName).val(data.result);
            } else {
                ShowMessage(data.errMessage);
            }
        },
        error: function (data) {
            ShowMessage("失败:" + data.responsetext);
        }
    });

}


function IsValidInputData() {
    if (!IsValidData())
        return false;
    return true;
}


function DeleteClick() {
    ShowYesNoMessage("是否删除？", function () {
        var obj = new Object();
        obj.media_id = $("#HF_MID").val();
        $.ajax({
            url: "../GTPT_WX.ashx?requestType=news&mode=delete&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
            dataType: "text", //返回数据的类型
            data: { postData: JSON.stringify(obj) },
            success: function (data) {
                if (data == "") {
                    PageDate_Clear();
                    vProcStatus = cPS_BROWSE;
                    SetControlBaseState();
                    ShowMessage("删除成功");
                }
                else {
                    ShowMessage(data);
                }
            },
            error: function (data) {
                ShowMessage(data);
            }
        });
    });
};




