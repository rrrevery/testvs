vUrl = "../GTPT.ashx";
var time = setInterval(function () { $("#TB_GROUPNAME").focus(); }, 1000);
$(document).ready(function () {
    $("input[type='checkbox'][name='CB_STATUS']").click(function () {
        if (this.checked) {
            $(this).prop("checked", this.checked).siblings().prop("checked", !this.checked);
            $("#HF_STATUS").val($(this).val());
        }
        else {
            $(this).prop("checked", this.checked);
            $("#HF_STATUS").val("");
        }
    });


    $(document).on("click", "input,button", function (event) {
        if ($(event.target).attr("id") != "TB_GROUPNAME") {

        }
        if (event.target.id != "TB_GROUPNAME") {
            window.clearInterval(time);
            $(this).focus();
        }


    });

});

function SetControlState() {
    $("#B_Delete").hide();
    $("#B_Exec").hide();
}

function IsValidData() {
    //名称不为空
    //名称长度不能超过30
    if ($("#TB_JLBH").val() == "0") {
        art.dialog({ content: "未分组 不可修改<br/>2秒后自动关闭", lock: true, time: 2 });
        return false;
    }
    if ($("#TB_JLBH").val() == "1") {
        art.dialog({ content: "黑名单 分组不可修改<br/>2秒后自动关闭", lock: true, time: 2 });
        return false;
    }
    return true;
}
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";

    //修改分组 删除分组 待做
    //先执行分组相关的操作：1、创建分组。2、移动用户到指定分组。3、记录分组信息在本地数据库
    //主表数据
    Obj.sGROUP_NAME = $("#TB_GROUPNAME").val();
    Obj.sZY = $("#TB_ZY").val();
    Obj.iSTATUS = $("#HF_STATUS").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    //主表数据
    $("#TB_GROUPNAME").val(Obj.sGROUP_NAME);
    $("#TB_ZY").val(Obj.sZY);
    $("[name='CB_STATUS']").each(function () {
        if ($(this).val() == Obj.iSTATUS) {
            $(this).prop("checked", true).siblings().prop("checked", false);
        }
    });
}
//微信分组操作
function WXGroupsOperation(mode, method) {
    var result = "";
    var obj = new Object();
    obj.group = new Object();
    obj.group.id = $("#TB_JLBH").val();
    obj.group.name = $.trim($("#TB_GROUPNAME").val());
    obj.mode = mode;
    obj.method = method;
    $.ajax({
        url: "../GTPT_WX.ashx?requestType=groups",
        data: { postData: JSON.stringify(obj) },
        async: false
    }).then(function (responseText, state, jqxhr) {
        if (responseText != "" && responseText.indexOf("错误") == "-1" && responseText.indexOf("error") == "-1") {
            data = JSON.parse(responseText);
            result = data.group.id;
        }
        else {
            art.dialog({ content: responseText, lock: true });
            result = "fail";
        }

    }).fail(function (data, xhr, readystate, status) {
        art.dialog({ content: "微信请求失败", lock: true });
        result = "fail";
    });
    return result;
}
//创建分组


//创建分组
function createGroups() {
    var result = "";
    var obj = new Object();
    obj.group = new Object();
    obj.group.name = $("#TB_GROUPNAME").val();
    obj.mode = "insert";
    obj.method = "post";
    $.ajax({
        url: "../GTPT_WX.ashx?requestType=groups",
        data: { postData: JSON.stringify(obj) },
        async: false
    }).then(function (responseText, state, jqxhr) {
        if (responseText != "" && responseText.indexOf("错误") == "-1" && responseText.indexOf("error") == "-1") {
            data = JSON.parse(responseText);
            result = data.group.id;
        }
        else {
            art.dialog({ content: responseText, lock: true });
            result = "fail";
        }

    }).fail(function (data, xhr, readystate, status) {
        art.dialog({ content: "微信请求失败", lock: true });
        result = "fail";
    });
    return result;
}


//将所有微信移动到未分组 当中
function deleteGroups() {
    var result = "";
    var obj = new Object();
    obj.group = new Object();
    obj.group.name = $("#TB_GROUPNAME").val();
    obj.mode = "insert";
    obj.method = "post";
    var json = '{"group":{"name":"' + $("#TB_GROUPNAME").val() + '"}}';
    $.ajax({
        url: "../GTPT_WX.ashx?requestType=groups",
        data: { postData: JSON.stringify(obj) },
        async: false
    }).then(function (responseText, state, jqxhr) {
        if (responseText != "" && responseText.indexOf("错误") == "-1" && responseText.indexOf("error") == "-1") {
            data = JSON.parse(responseText);
            result = data.group.id;
        }
        else {
            art.dialog({ content: responseText, lock: true });
            result = "fail";
        }

    }).fail(function (data, xhr, readystate, status) {
        art.dialog({ content: "微信请求失败", lock: true });
        result = "fail";
    });
    return result;
}



function posttosever(str_data, str_url, str_mode) {

    var result = "";
    switch (str_mode) {
        case "Insert":
            result = WXGroupsOperation("Insert", "POST");
            break;
        case "Update"://修改
            result = WXGroupsOperation("Update", "POST");
            break;
        case "Delete"://删除
            //result = WXGroupsOperation("delete", "GET");//没有提供此功能
            break;

    }
    if (result == "fail") {
        return "";
    }
    str_data.iGROUPID = result;
    var canBeClose = false;
    var myDialog = art.dialog({
        lock: true, content: '正在提交数据,请等待......'
        , close: function () {
            if (canBeClose) {
                return true;
            }
            return false;
        }
    });
    if (str_data == "") {
        canBeClose = true;
        myDialog.close();
        return;
    }
    $.ajax({
        type: "post",
        url: str_url + "?mode=" + str_mode + "&func=" + vPageMsgID + "&old=" + vOLDDB,
        async: false,
        data: { json: JSON.stringify(str_data), titles: 'cecece' },
        success: function (data) {
            canBeClose = true;
            myDialog.close();
            if (data.indexOf("错误") >= 0 || data.indexOf("error") >= 0) {
                art.dialog({ lock: true, content: data });
                canBeClose = false;
            }
            else {
                art.dialog({ lock: true, content: "操作成功(2秒后自动关闭)", time: 2 });
                canBeClose = true;
                vJLBH = GetValueRegExp(data, "jlbh")
                if (vJLBH != "")
                    ShowDataBase(vJLBH);
            }
        },
        error: function (data) {
            canBeClose = false;
            myDialog.close();
            art.dialog({ lock: true, content: "保存失败：" + data });
        }
    });
    return canBeClose;
}