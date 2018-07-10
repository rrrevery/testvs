vUrl = "../GTPT.ashx";
sendToWX = false;//是否需要发送消息到微信
var selectId = -1;

function InitGrid() {
    vColumnNames = ['序号', '标题', '描述', '消息链接', '图片链接'];
    vColumnModel = [
                { name: 'iINX', width: 60, editor:'text' },
                { name: 'sTITLE', width: 120, editor: 'text' },
                { name: 'sDESCRIPTION', width: 150, editor: 'text' },
                { name: 'sURL', width: 150, editor: 'text' },
                { name: 'sPIC_URL', width: 150, editor: 'text' },
    ];
}
function AddCustomerButton() {
    AddToolButtons("发布", "B_POST");
}

$(document).ready(function () {
    document.getElementById("B_POST").onclick = function () {
        if ($("#selectPublicID").combobox("getValue") == "选择公众号") {
            ShowMessage("请选择公众号");
            return;
        }

        var url = "../GTPTLib.ashx?requestType=postToWX&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF + "&func=";
        if (vTYPE == "2") {
            url += "SetUserAttention";
        }
        if (vTYPE == "3") {
            url += "SetUserAttention";
        }
        if (vTYPE == "0") {
            url += "SetUserAttention";
        }
        if (vTYPE == "1") {
            url += "SetUserAttention";
        }

        $.ajax({
            type: "post",
            url: url,
            success: function (data) {
                if (data == "ok") {
                    ShowMessage("请求成功");
                }
                else if (data == "") {
                    ShowMessage("请求失败");
                } else {
                    ShowMessage(data);
                }
            },
            error: function (data) {
                ShowMessage(data.responseText)
            }
        });
    }
    $(".rule_ask").hide();

    $("#selectPublicID").combobox({
        onSelect: function (record) {
            iWXPID = record.value;
            sWXPIF = record.pif;
            $("#DDL_WT").empty();
           FillWT_new("DDL_WT", vTYPE, '', iWXPID);


        }
    });
    $.parser.parse("#WXPublicID");
    FillPublicID($("#selectPublicID"));





    FillGZ($("#DDL_GZ"));

    $('#DDL_GZ').combobox({
        onSelect: function (record) {
            selectValueChange(record.value);
        }
    });

    $('#upload').click(function () {
        if (!/\.(gif|jpg|jpeg|png|GIF|JPG|PNG)$/.test($("#files").val())) {
            ShowMessage("图片类型必须是.gif,jpeg,jpg,png中的一种")
            return;
        }
        UploadPicture("form", "TB_IMG", "TPHF");
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


    $("#AddItem").click(function () {
        var rowidarr = $("#list").datagrid("getRows");
        if (rowidarr.length >= 10) {
            ShowMessage("图文消息最多为10条！");
            return;
        }
        if (!IsNumber($("#TB_XH").val())) {
            ShowMessage("序号不合法！");
            return;
        }
        var arrRow = new Array();
        for (var j = 0; j < rowidarr.length  ; j++) {
            var rowData = rowidarr[j];
            arrRow.push(rowData.iINX);
        }
        if (arrRow.indexOf($("#TB_XH").val()) >= 0) {
            ShowMessage("序号重复！");
            return;
        }
        if (IsEmpty($("#TB_TITLE").val())) {
            ShowMessage("标题不能为空！");
            return;
        }
        if (IsEmpty($("#TB_URL").val())) {
            ShowMessage("消息链接不能为空！");
            return;
        } else {
            if (!IsURL($("#TB_URL").val())) {
                ShowMessage("消息链接URL填写不正确！");
                return;
            }
        }
        if (IsEmpty($("#TB_IMG").val())) {
            ShowMessage("还未上传图片！");
            return;
        }
        $("#list").datagrid("appendRow", {
                iINX: $("#TB_XH").val(),
                sTITLE: $("#TB_TITLE").val(),
                sURL: $("#TB_URL").val(),
                sPIC_URL: $("#TB_IMG").val(),
                sDESCRIPTION: $("#TB_DESCRIBE").val(),
        });
    });
    $("#DelItem").click(function () {
        DeleteRows("list");
    });

 



});
function changewt() {
    if ($("#selectPublicID").combobox("getValue") == "选择公众号") {
        ShowMessage("请选择公众号");
        return;
    }


}

function SetControlState() {
    $("#B_Exec").hide();
    $("#status-bar").hide();

}
function selectValueChange(ruleId, treeNode) {
    $("#list").datagrid("loadData", { total: 0, rows: [] });
    $("#HF_MID").val("");
    $(".rule_ask").hide();
    switch (parseInt(ruleId)) {
        case 1:
            $("#text").show();
            if (arguments.length > 1)
                $("#TA_WZHF").val(treeNode.sANSWER);
            break;
        case 2:
            $("#image").show();
            if (arguments.length > 1) {
                $("#TB_ImgTITLE").val(treeNode.sTITLE);
                $("#TB_DESCRIPTION").val(treeNode.sDESCRIPTION);
                $("#HF_MID").val(treeNode.mid);
            }
            break;
        case 3:
            $("#voice").show();
            if (arguments.length > 1) {
                $("#TB_YYTITLE").val(treeNode.sTITLE);
                $("#TB_YYDESCRIPTION").val(treeNode.sDESCRIPTION);
                $("#HF_MID").val(treeNode.mid);
            }
            break;
        case 4:
            $("#video").show();
            if (arguments.length > 1) {
                $("#TB_SPTLTLE").val(treeNode.sTITLE);
                $("#TB_SPDESCRIPTION").val(treeNode.sDESCRIPTION);
                $("#HF_MID").val(treeNode.mid);
            }
            break;
        case 5:
            $("#music").show();
            break;
        case 6:
            $("#news").show();
            if (arguments.length > 1) {
                $("#list").datagrid("loadData", treeNode.itemTable, "json");
                $("#list").datagrid("loaded");
            }
            break;
        default:
            break;

    }
}
function IsValidData() {
    if ($("#DDL_WT").val() == "") {
        ShowMessage("请选择问题/关键字", 3);
        return false;
    }
    var vGZ = $('#DDL_GZ').combobox('getValue');
    if (isNaN(vGZ)) {
        ShowMessage("请选择回复规则", 3);
        return false;
    }
    switch (parseInt(vGZ)) {
        case 1:
            if ($("#TA_WZHF").val().length == 0) {
                ShowMessage("请填写回复文字内容", 3);
                return false;
            }
            if ($("#TA_WZHF").val().length > 200) {
                ShowMessage("请回复文字内容不能超过200字", 3);
                return false;
            }
            break;
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
        case 5:
            ShowMessage("腾讯目前不再支持音乐回复，请选择其他类型");
            return false
            break;
        case 6:
            var rows = $("#list").datagrid("getData").rows;
            if (bNeedItemData && rows.length <= 0) {
                ShowMessage("请先添加图文信息", 3);
                return false;
            }
            if ($(".datagrid-editable-input").length > 0) {
                ShowMessage("请先按回车保存图文数据", 3);
                return false;
            }

            break;
        default:
            break;
    }
    return true;
}

function SaveDataBase() {
    var Obj = new Object();
    Obj = SaveData(Obj);
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    //Obj.iLoginPUBLICID = iPID;
    Obj.sPTToken = GetUrlParam("ck");
    Obj.iLoginPUBLICID = iWXPID;

    return Obj;
};
function SaveData() {
    var Obj = new Object();
    Obj.iTYPE = vTYPE;
    Obj.iGZJLBH = $('#DDL_GZ').combobox('getValue');
    
    if (Obj.iGZJLBH == null)
        Obj.iGZJLBH = "0";
    Obj.iJLBH = GetSelectValue("DDL_WT");
    Obj.sWX_MID = $("#HF_MID").val();
    switch (parseInt(Obj.iGZJLBH)) { 
        case 1:
            Obj.sANSWER = $("#TA_WZHF").val();
            break;
        case 2:
            Obj.sTITLE = $("#TB_ImgTITLE").val();
            Obj.sDESCRIPTION = $("#TB_DESCRIPTION").val();
            break;
        case 3:
            Obj.sTITLE = $("#TB_YYTITLE").val();
            Obj.sDESCRIPTION = $("#TB_YYDESCRIPTION").val();
            break;
        case 4:
            Obj.sTITLE = $("#TB_SPTLTLE").val();
            Obj.sDESCRIPTION = $("#TB_SPDESCRIPTION").val();
            break;
        case 6:
            var lst = new Array();
            lst = $("#list").datagrid("getRows");
            Obj.itemTable = lst;
            break;
        default: break;
    }

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#DDL_WT").val(Obj.iJLBH);
    $("#HF_MID").val(Obj.sWX_MID);
    $("#DDL_GZ").combobox("setValue", Obj.iGZJLBH);
    selectValueChange(Obj.iGZJLBH, Obj)
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID)





}

function UploadPictureToWXServer(formID, tbName, type) {

    if ($("#selectPublicID").combobox("getValue") == "选择公众号") {
        ShowMessage("请选择公众号");
        return;
    }
    $("#" + formID).ajaxSubmit({
        url: '../UpLoadPictureToWXServer.ashx?type=' + type + "&PUBLICIF=" + sWXPIF,
        dataType: "json",
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




