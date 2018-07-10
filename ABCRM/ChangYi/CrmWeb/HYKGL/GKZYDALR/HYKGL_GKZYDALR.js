vUrl = "../HYKGL.ashx";
var vHYK_NO = GetUrlParam("HYK_NO");
var countdown = 60;

function SetControlState() {



}

$(document).ready(function () {
    $("#btn-toolbar div").hide();
    $("#status-bar .div2").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#B_Exec").hide();

    $("#btnYZM").click(function () {
        var iYZM = 10000 + Math.ceil(Math.random() * 9999);
        var sYZM = iYZM.toString().substr(1);
        var tInterval;
        if (SendSMS($("#TB_SJHM").val(), sYZM) == "0") {
            $("#HF_YZM").val(sYZM);
            $("#btnYZM").attr("disabled", "disabled");
            tInterval = setInterval(function () {
                settime()
            }, 1000)
        }
        $("#HF_SJHM").val($("#TB_SJHM").val());

        function settime() {
            if (countdown == 0) {
                $("#btnYZM").removeAttr("disabled");
                $("#btnYZM").val("验证码");
                countdown = 60;
                clearInterval(tInterval);
            } else {
                $("#btnYZM").attr("disabled", "disabled");
                $("#btnYZM").val("重发(" + countdown + ")");
                countdown--;
            }
        }
    });

    $("#TB_SFZBH").change(function () {
        if ($("#TB_SFZBH").val() == "") {
            ShowMessage("请输入证件号码", 3);
            return;
        }
        getGKDA($("#TB_SFZBH").val(), "", true);
        if ($("#TB_SFZBH").val() != "")
            showBirthday($("#TB_SFZBH").val());
    });

    $("#TB_SJHM").change(function () {
        if ($("#TB_SJHM").val() == "") {
            art.dialog({ content: "请输入手机号码", lock: true, time: 2 });
            return;
        }
        if ($("#TB_SFZBH").val() != "") {
            //修改手机号，查找是否重复
            getGKDA("", $("#TB_SJHM").val(), true);
            showBirthday($("#TB_SFZBH").val());
            return;
        }
        //根据手机号，查询客户信息，显示出来
        art.dialog({
            content: "证件号为空,是否根据手机信息查找顾客信息?",
            lock: true,
            ok: function () {
                getGKDA("", $("#TB_SJHM").val(), true);
                if ($("#TB_SFZBH").val() != "")
                    showBirthday($("#TB_SFZBH").val());
            }
            , cancel: true
        });
        document.getElementById("btnYZM").click();

    });

    //用于会员卡操作台,应该使用会员卡号查
    if (vHYK_NO != "") {
        var str = GetGKDAData(0, "", "", vHYK_NO);
        ShowGKDA(str);
        if ($("#TB_SFZBH").val() != "")
            showBirthday($("#TB_SFZBH").val());
    }
    RefreshButtonSep();

    $("#takePhoto").click(function () {
        TakePhoto("HeadPhoto", "HF_IMGURL");
    });
});

function showBirthday(val) {
    var birthdayValue;
    if (15 == val.length) { //15位身份证号码
        birthdayValue = val.charAt(6) + val.charAt(7);
        if (parseInt(birthdayValue) < 10) {
            birthdayValue = '20' + birthdayValue;
        }
        else {
            birthdayValue = '19' + birthdayValue;
        }
        birthdayValue = birthdayValue + '-' + val.charAt(8) + val.charAt(9) + '-' + val.charAt(10) + val.charAt(11);
        if (parseInt(val.charAt(14) / 2) * 2 != val.charAt(14))//看最后一位，奇数男偶数女
        {
            //man
            //$("#DDL_SEX").val("0");
            $("#Radio1").click();
        }
        else {  //sex = '女';
            //$("#DDL_SEX").val("1");
            $("#Radio2").click();
        }
    }
    if (18 == val.length) { //18位身份证号码
        birthdayValue = val.charAt(6) + val.charAt(7) + val.charAt(8) + val.charAt(9) + '-' + val.charAt(10) + val.charAt(11) + '-' + val.charAt(12) + val.charAt(13);

        if (parseInt(val.charAt(16) / 2) * 2 != val.charAt(16))//看倒数第二位，奇数男偶数女
        {
            //man
            //$("#DDL_SEX").val("0");
            $("#Radio1").click();
        }
        else {  //sex = '女';
            //$("#DDL_SEX").val("1");
            $("#Radio2").click();
        }
    }
    $("#TB_CSRQ").val(birthdayValue);
    getSRXXMORE();

}

function getGKDA(sfzbh, sjhm, isUpdate) {
    if (!sfzbh && !sjhm) {
        return;
    }
    var str = GetGKDAData(0, sfzbh, sjhm);
    if (str == "null" || str == "") {
        if (isUpdate) {
            if (sfzbh != "" && $("#HF_GKID").val() == "") {
                ShowMessage("顾客档案不存在", 3);
                $("#TB_SFZBH").val("");
            }
            if (sfzbh == "" && sjhm != "" && $("#HF_GKID").val() == "") {
                ShowMessage("顾客档案不存在", 3);
                $("#TB_SFZBH").val("");
                $("#TB_SJHM").val("");
            }
            return;
        }
        $("#TB_SFZBH").val(sfzbh || $("#TB_SFZBH").val());
        $("#TB_SJHM").val(sjhm || $("#TB_SJHM").val());
        return;
    }
    else {
        var Obj = JSON.parse(str);
        var t = CheckBK(Obj.iGKID);
        var b = JSON.parse(t);
        if (b.iHYID == 0) {
            ShowMessage("该档案未发卡", 3);
            $("#TB_SJHM").val("");
            $("#TB_SFZBH").val("");
            return;
        }

        if ($("#TB_SFZBH").val() != "" && sjhm != "" && isUpdate && $("#TB_SFZBH").val() != Obj.sSFZBH) {
            ShowMessage("手机号码重复", 3);
            $("#TB_SJHM").val("");
            return;
        }
        //检查身份证是否重复
        if (sjhm != "" && sfzbh != "" && isUpdate && $("#HF_GKID").val() != Obj.iGKID && $("#HF_GKID").val() != "") {
            $("#TB_SFZBH").val("");
            ShowMessage("证件号已重复", 3);
            return;
        }
        ShowGKDA(str);
    }


}
function getSRXXMORE() {
    if ($("#TB_CSRQ").val() != "") {
        var str = GetSRXX($("#TB_CSRQ").val());
        var Obj = JSON.parse(str);
        $("#TB_SX").val(Obj.sSX);
        $("#TB_XZ").val(Obj.sXZ);

    }
}
function ShowGKDA(str) {
    if (!str || str == "null") {
        return;
    }
    var Obj = JSON.parse(str);
    $("#HF_GKID").val(Obj.iGKID);
    $("#TB_SFZBH").val(Obj.sSFZBH);
    //  document.getElementById("TB_SFZBH").disabled = true;
    //$("#TB_SFZBH").attr("readonly", "readonly");
    if (Obj.iSEX == 0) {
        $("#Radio1").click();
    }
    else {
        $("#Radio2").click();
    }
    $("#HF_IMGURL").val(Obj.sIMGAGEURL);
    $("#HF_IMGURL_OLD").val(Obj.sIMGAGEURL);
    $("#HF_SFZBH").val(Obj.sSFZBH);
    $("#TB_CSRQ").val(Obj.dCSRQ);
    $("#TB_GKNAME").val(Obj.sGK_NAME);
    $("#HF_GKNAME").val(Obj.sGK_NAME);
    $("#TB_SJHM").val(Obj.sSJHM);
    $("#HF_SJHMOLD").val(Obj.sSJHM);
    $("#TB_WX").val(Obj.sWX);
    $("#HF_WXOLD").val(Obj.sWX);
    if (Obj.sIMGURL != "") {
        $("#HeadPhoto").attr("src", Obj.sIMGURL);
    }
}

function ShowData(Data) {
    var data = JSON.parse(Data);
    $("#TB_JLBH").val(data.iJLBH);
    $("#HF_GKID").val(data.iGKID);
    $("#TB_SFZBH").val(data.sSFZBH);
    $("#TB_CSRQ").val(data.dCSRQ);
    $("#DDL_SEX").val(data.iSEX);
    $("#TB_SJHM").val(data.sNEW_SJHM);
    $("#HF_SJHMOLD").val(data.sOLD_SJHM);
    $("#HF_WXOLD").val(data.sOLD_WX);
    $("#TB_GKNAME").val(data.sGK_NAME);
    $("#HF_GKNAME").val(data.sOLD_GK_NAME);
    $("#HF_SFZBH").val(data.sOLD_SFZBH);
    $("#TB_XZ").val(data.sXZ);
    $("#TB_SX").val(data.sSX);
    $("#LB_DJRMC").text(data.sDJRMC);
    $("#HF_DJR").val(data.iDJR);
    $("#LB_DJSJ").text(data.dDJSJ);
    $("#HF_IMGURL").val(data.sNEW_IMGURL);
    $("#HF_IMGURL_OLD").val(data.sOLD_IMGURL);
    $("[name='sex'][value='" + data.iSEX + "']").attr("checked", true);
    if (data.iSEX == 0) {
        $("#Radio1").click();
    }
    else {
        $("#Radio2").click();
    }
    if (data.sNEW_IMGURL != "" || data.sOLD_IMGURL != "") {
        $("#HeadPhoto").attr("src", data.sIMGAGEURL == "" ? data.sOLD_IMGURL : data.sNEW_IMGURL);
    }

}


function GetHYXX() {
    if ($("#TB_CXHYKNO").val() != "") {
        var str = GetHYXXData(0, $("#TB_CXHYKNO").val());
        if (str == "null" || str == "") {
            art.dialog({ lock: true, content: "没有找到卡号" });
            $("#TB_CXHYKNO").val("");
            return;
        }

        var Obj = JSON.parse(str);
        $("#TB_JLBH").val(Obj.iHYID);
        $("#HF_HYID").val(Obj.iHYID);

        ShowDataBase(Obj.iHYID);
    }
}



function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "") {
        Obj.iJLBH = "0";
    }
    Obj.iGKID = $("#HF_GKID").val();
    Obj.sHYK_NO = vHYK_NO;
    Obj.sSFZBH = $("#TB_SFZBH").val();
    Obj.sOLD_SFZBH = $("#HF_SFZBH").val();
    Obj.dCSRQ = $("#TB_CSRQ").val();
    Obj.iSEX = $("[name='sex']:checked").val();
    Obj.sNEW_SJHM = $("#TB_SJHM").val();
    Obj.sNEW_WX = $("#TB_WX").val();
    Obj.sXZ = $("#TB_XZ").val();
    Obj.sSX = $("#TB_SX").val();
    Obj.sOLD_SJHM = $("#HF_SJHMOLD").val();
    Obj.sOLD_WX = $("#HF_WXOLD").val();
    Obj.sGK_NAME = $("#TB_GKNAME").val();
    Obj.sOLD_GK_NAME = $("#HF_GKNAME").val();
    Obj.sNEW_IMGURL = $("#HF_IMGURL").val();
    Obj.sOLD_IMGURL = $("#HF_IMGURL_OLD").val();
    if (Obj.sNEW_IMGURL == "") {
        Obj.sNEW_IMGURL = Obj.sOLD_IMGURL;
    }
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function IsValidData() {
    if ($("#TB_SJHM").val() == "") {
        ShowMessage('请输入手机号码', 3);
        return false;
    }
    //if ($("#TB_YZM").val() == "") {
    //    art.dialog({ content: '请输入验证码', lock: true });
    //    return false;
    //}
    //if ($("#HF_YZM").val() != $("#TB_YZM").val()) {
    //    art.dialog({ content: '验证码输入错误', lock: true });
    //    return false;
    //}
    //if ($("#HF_SJHM").val() != $("#TB_SJHM").val()) {
    //    ShowMessage("手机号和接受验证码的手机号不一致");
    //    return false;
    //}
    return true;
}

function ClearData() {
    $("#TB_JLBH").val("");
    $("#HF_GKID").val("");
    $("#TB_SFZBH").val("");
    $("#TB_CSRQ").val("");
    $("#DDL_SEX").val("");
    $("#TB_SJHM").val("");
    $("#TB_WX").val("");
    $("#TB_XZ").val("");
    $("#TB_SX").val("");
    $("#HF_IMGURL").val("");
    $("#HF_IMGURL_OLD").val("");
    $("#TB_GKNAME").val("");
    $("#HF_GKNAME").val("");

}

function InsertClickCustom() {
    document.getElementById("TB_SFZBH").disabled = false;
    $("#TB_SFZBH").removeAttr("readonly");
};