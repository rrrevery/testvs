var yhm;
var pw;

$(document).ready(function () {
    //$("#username").focus();//尼玛自动焦点会导致placeholder不显示
    var jzmm = getCookie("jzmm");
    yhm = getCookie("yhm");
    pw = getCookie("pw");
    if (jzmm == "" || jzmm == "1")
        document.getElementById("cb_jz").checked = true;
    else
        document.getElementById("cb_jz").checked = false;
    if (jzmm == "1" && yhm != "" && pw != "") {
        $("#username").val(yhm);
        $("#password").val(pw);
    }
    document.getElementById("cb_jz").onclick = function () {
        setCookie("jzmm", document.getElementById("cb_jz").checked ? 1 : 0, 365);
    };
});

function nima() {
    document.getElementById("cb_jz").checked = false;
}
function niba() {
    document.getElementById("cb_jz").checked = true;
    //$("#cb_jz")[0].attr("checked", 'checked');
}
function nimei() {
    alert(document.getElementById("cb_jz").checked);
}
function loginFun() {
    //alert(1);
    setCookie("jzmm", document.getElementById("cb_jz").checked ? 1 : 0, 365);
    var name = $("#username").val();
    var word = $("#password").val();
    $.ajax({
        type: 'post',
        url: 'login.ashx',
        data: { username: name, password: word, titles: 'cecece' },
        dataType: "html",
        success: function (data) {
            if (document.getElementById("cb_jz").checked) {
                setCookie("yhm", name, 30);
                setCookie("pw", word, 30);
            }
            else {
                delCookie("yhm");
                delCookie("pw");
            }
            //alert("3"+data);
            if (data == 1 || data == 2)
                window.location.href = "Index2.aspx?id=" + name;
            else if (data.length > 1) {
                if (data == "session") {
                    window.location.href = "Login.aspx";
                } else {
                    alert(data);
                }
            }
        },
        error: function (data) {
            alert("4" + data.responseText);
        }
    }
   );
    //alert(2);
}

function SetEnterSumPwd() {
    if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {
        document.getElementById("password").focus();
    }
}

function SetEnterSumEnt() {
    if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {
        document.getElementById("B_Sure").focus();
    }
}
//cookie
function setCookie(c_name, value, expiredays) {
    var exdate = new Date()
    exdate.setDate(exdate.getDate() + expiredays)
    document.cookie = c_name + "=" + escape(value) +
    ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString())
}

function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return unescape(arr[2]);
    else
        return "";
}

function delCookie(name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null)
        document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}