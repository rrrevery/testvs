vUrl = "../GTPT.ashx";
var rowDataYHQ;
var rowDataKLX;
var vDialogName;
function InitGrid() {
    vColumnNames = ['序号', '标题', '缩略图标识', '缩略图标题', '作者', '图文消息摘要', '是否显示封面', '内容', '原文地址'];
    vColumnModel = [
             { name: 'iINX', width: 100, },
             { name: 'sTITLE', width: 100, },
             { name: 'sTHUMB_MEDIA_ID', width: 100, },
             { name: 'sTHUMB_TITLE', width: 100, },
             { name: 'sAUTHOR', width: 100, },
             { name: 'sDIGEST', width: 100, },
             { name: 'iBJ_COVER', width: 100, },
             { name: 'sCONTNET', width: 100, },
             { name: 'sYWURL', width: 100, },

    ];
}


function AddCustomerButton() {
    AddToolButtons("发布", "B_POST");
    AddToolButtons("删除发布", "B_DelPOST");

}

$(document).ready(function () {
    document.getElementById("B_POST").onclick = postToWX;
    document.getElementById("B_DelPOST").onclick = delPostToWX;

    BFButtonClick("TB_THUMB_TITLE", function () {
        var condData = new Object();
        condData["iTYPE"] = 8;
        condData["iPUBLICID"] = iWXPID;
        SelectMEDIA("TB_THUMB_TITLE", "TB_THUMB_MEDIA_ID", "zTB_THUMB_MEDIA_ID", false, condData);
    });

    $("#AddItem").click(function () {
        $('#list').datagrid('appendRow', {
            iINX: $("#list").datagrid("getRows").length + 1,
            sTITLE: $("#TB_TITLE").val(),
            sTHUMB_MEDIA_ID: $("#TB_THUMB_MEDIA_ID").val(),
            sTHUMB_TITLE: $("#TB_THUMB_TITLE").val(),
            sAUTHOR: $("#TB_AUTHOR").val(),
            sDIGEST: $("#TB_DIGEST").val(),
            iBJ_COVER: $("#CB_BJ_COVER")[0].checked ? "1" : "0",
            sCONTNET: editor.html(),
            sYWURL: $("#TB_YWURL").val(),
        });

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
    $.ajax({
        type: "post",
        url: "../GTPT_WX.ashx?requestType=news&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
        data: { postData: JSON.stringify(obj) },
        success: function (data) {
            if (data != "" && data.indexOf("错误") == "-1" && data.indexOf("error") == "-1") {
                document.getElementById("B_POST").disabled = true;
                ShowMessage("请求成功");
            } else {
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
    var vTYPE = $("#DDL_TYPE").find("option:selected").val()
    if (vTYPE != 4 && vTYPE != 6) {
        ShowMessage("只能删除图文消息和视频消息");
        return;
    }
    var obj = new Object();
    obj.media_id = $("#TB_MEDIA_ID").val();
    $.ajax({
        url: "../GTPT_WX.ashx?requestType=news&mode=deletenews&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
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
}



function SaveData() {

    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iTYPE = 6;
    Obj.sNAME = $("#TB_NAME").val();
    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    Obj.iLoginPUBLICID = iWXPID;

    return Obj;
}




function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_NAME").val(Obj.sNAME);
    $("#TB_MEDIA_ID").val(Obj.sMEDIA_ID);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID);

    $('#list').datagrid("loadData", { total: 0, rows: [] });
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    if (Obj.sMEDIA_ID != "") {
        document.getElementById("B_POST").disabled = true;
    }

}


function IsValidData() {

    if ($("#TB_NAME").val() == "") {
        ShowMessage("请输入名称", 3);
        return false;
    }
    return true;
}






