
vUrl = "../GTPT.ashx";

function InitGrid() {
    vColumnNames = ['会员ID', '会员卡号', '会员姓名', '用户ID'];
    vColumnModel = [
             { name: 'iHYID', width: 100, hidden: true },
             { name: 'sHYK_NO', width: 100, },
             { name: 'sHY_NAME', width: 100, },
             { name: 'sOPENID', width: 100, },

    ];
}


function AddCustomerButton() {
    AddToolButtons("发布", "B_POST");
    AddToolButtons("清理", "B_DelPOST");
    AddToolButtons("预览", "B_PREVIEW");
}

$(document).ready(function () {
    $("#div_tag").hide();
    $("#div_yhlist").hide();
    $("#div_content").hide();
    $("#div_media").hide();
    $("#div_kbid").hide();
    FillGZ($("#DDL_TYPE"));

    BFButtonClick("TB_TAGMC", function () {
        SelectWXBQ("TB_TAGMC", "HF_TAGID", "zHF_TAGID", iWXPID, sWXPIF, false);
    });
    $('#DDL_TYPE').combobox({
        onSelect: function (record) {
            selectTypeChange(record.value);
        }
    });
    $('#DDL_QFDX').combobox({
        onSelect: function (record) {
            selectQFDXChange(record.value);
        }
    });

    BFButtonClick("TB_MEDIA_TITLE", function () {
        var vTYPE = $('#DDL_TYPE').combobox('getValue');
        if (vTYPE == "") {
            ShowMessage("请先选择消息类型");
            return false;
        }
        var condData = new Object();
        condData["iTYPE"] = vTYPE;
        condData["iPUBLICID"] = iWXPID;
        SelectMEDIA("TB_MEDIA_TITLE", "TB_MEDIA_ID", "zTB_MEDIA_ID", false, condData);
    });

    document.getElementById("B_POST").onclick = postToWX;
    document.getElementById("B_DelPOST").onclick = delPostToWX;
    document.getElementById("B_PREVIEW").onclick = previewPostToWX;
    $("#AddItem").click(function () {
        var DataArry = new Object();
        DataArry["vZT"] = 2;
        DataArry["iPUBLICID"] = iWXPID;
        SelectWXHYK('list', DataArry, 'iHYID');
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });

})

function postToWX() {
    if ($("#selectPublicID").combobox("getValue") == "选择公众号") {
        ShowMessage("请选择公众号");
        return;
    }
    var obj = new Object();
    obj.iJLBH = $("#TB_JLBH").val();
    obj.sMSGTYPE = getMsgType();
    obj.iQFDX = $('#DDL_QFDX').combobox('getValue');
    $.ajax({
        type: "post",
        url: "../GTPT_WX.ashx?requestType=groupmessage&mode=search&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
        data: { postData: JSON.stringify(obj) },
        success: function (data) {
            if (data == "") {
                document.getElementById("B_POST").disabled = true;
                ShowMessage("请求成功");
            }
            else {
                ShowMessage(data);
            }
        },
        error: function (data) {
            ShowMessage(data)
        }
    });
}

function delPostToWX() { //只能删除图文消息和视频消息，其他类型的消息一经发送，无法删除。
    if ($("#selectPublicID").combobox("getValue") == "选择公众号") {
        ShowMessage("请选择公众号");
        return;
    }
    var vTYPE = $('#DDL_TYPE').combobox('getValue');
    if (vTYPE != 4 && vTYPE != 6) {
        ShowMessage("只能删除图文消息和视频消息");
        return;
    }
    var obj = new Object();
    obj.iJLBH = $("#TB_JLBH").val();//要删除的文章在图文消息中的位置还不知道怎么弄
    $.ajax({
        type: "post",
        url: "../GTPT_WX.ashx?requestType=groupmessage&mode=delete&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
        data: { postData: JSON.stringify(obj) },
        success: function (data) {
            if (data == "") {
                PageDate_Clear();
                vProcStatus = cPS_BROWSE;
                SetControlBaseState();
                document.getElementById("B_POST").disabled = true;
                ShowMessage("删除成功");
            }
            else {
                ShowMessage(data);
            }
        },
        error: function (data) {
            ShowMessage(data)
        }
    });
}

function previewPostToWX() {
    if ($("#selectPublicID").combobox("getValue") == "选择公众号") {
        ShowMessage("请选择公众号");
        return;
    }
    $.dialog.open("PreviewDialog.aspx", {
        lock: true, width: 650, height: 70, cancel: false,
        drag: true, fixed: false,
        close: function () {

            var obj = new Object();
            obj.iJLBH = $("#TB_JLBH").val();
            obj.sHYK_NO = $.dialog.data('dialogValue');
            obj.sMSGTYPE = getMsgType(); // obj.sMSGTYPE = $("#DDL_TYPE").find("option:selected").attr("type");
            $.ajax({
                type: "post",
                url: "../GTPT_WX.ashx?requestType=groupmessage&mode=previewmassmsg&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
                data: { postData: JSON.stringify(obj) },
                success: function (data) {
                    if (data == "") {
                        ShowMessage("预览发送成功");
                    } else {
                        ShowMessage(data);
                    }
                },
                error: function (data) {
                    ShowMessage(data)
                }
            });



        }
    }, false);


}

function LoadData(value) {
    var v = value;
}
function selectTypeChange(typeId) {
    if (typeId == 1) {  //消息类型为文本时，素材选择框和卡包隐藏、文本框显示
        $("#div_content").show();
        $("#div_media").hide();
        $("#div_kbid").hide();
    }
    else if (typeId == 7) { //消息类型为卡包时，素材选择框和文本框隐藏、卡包显示
        $("#div_content").hide();
        $("#div_media").hide();
        $("#div_kbid").show();
        Getwxcard();
    }
    else {
        $("#div_content").hide();
        $("#div_media").show();
        $("#div_kbid").hide();
    }

}
function Getwxcard() {
    var str = GetWXCARDData();
    if (str == "null" || str == "") {
        return;
    }
    if (str.indexOf("错误") >= 0) {
        ShowMessage(str);
        return;
    }
    var Obj = JSON.parse(str);
    $("#LB_CARDID").text(Obj.sCARDID);
}
function selectQFDXChange(qfdxId) {
    if (qfdxId == 1) {//按标签
        $("#div_tag").show();
        $("#div_yhlist").hide();
    }
    else if (qfdxId == 2) {//按用户列表 {
        $("#div_tag").hide();
        $("#div_yhlist").show();
    }
    else {
        $("#div_tag").hide();
        $("#div_yhlist").hide();
    }

}

function getMsgType() {
    var value = $('#DDL_TYPE').combobox("getValue");
    var data = $('#DDL_TYPE').combobox("getData"); //获取控件中所有值
    for (var index in data) { //循环找索引
        if (data[index].value == value) {
            return data[index].type; //返回赋值type的值
        }
    }
    return "";
}
function SaveData() {

    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iQFDX = $('#DDL_QFDX').combobox('getValue');
    Obj.iMSGTYPE = $('#DDL_TYPE').combobox('getValue');
    if (Obj.iMSGTYPE == 7) {
        Obj.sMEDIA_ID = $("#LB_CARDID").text();
    }
    else {
        Obj.sMEDIA_ID = $("#TB_MEDIA_ID").val();
    }
    Obj.iTAGID = $("#HF_TAGID").val();
    Obj.sCONTENT = $("#TB_CONTENT").val();
    Obj.iBJ_ZZ = $("#CB_BJ_ZZ").prop("checked") ? 1 : 0;

    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}




function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#DDL_QFDX").combobox("setValue", Obj.iQFDX);
    selectQFDXChange(Obj.iQFDX);
    $("#DDL_TYPE").combobox("setValue", Obj.iMSGTYPE);
    selectTypeChange(Obj.iMSGTYPE);
    if (Obj.iMSGTYPE == 7) {
        $("#LB_CARDID").text(Obj.sMEDIA_ID);
    }
    else {
        $("#TB_MEDIA_ID").val(Obj.sMEDIA_ID);
    }
    $("#TB_MEDIA_TITLE").val(Obj.sMEDIA_TITLE);
    $("#HF_TAGID").val(Obj.iTAGID);
    $("#TB_TAGMC").val(Obj.sTAGMC);
    $("#TB_CONTENT").val(Obj.sCONTENT);
    $("#CB_BJ_ZZ").prop("checked", Obj.iBJ_ZZ == 1 ? true : false);
    $("#HF_MSG_ID").val(Obj.iMSG_ID);
    if (Obj.iSTATUS == 3) {
        document.getElementById("B_POST").disabled = true;
    }

    $('#list').datagrid("loadData", { total: 0, rows: [] });
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}


function IsValidData() {
    var vQFDX = $('#DDL_QFDX').combobox('getValue');
    if (vQFDX == "") {
        ShowMessage("请先选择群发对象");
        return false;
    }
    var vTYPE = $('#DDL_TYPE').combobox('getValue');
    if (vTYPE == "") {
        ShowMessage("请先选择消息类型");
        return false;
    }
    return true;
}


function IsValidInputData() {
    if (!IsValidData())
        return false;
    return true;
}





