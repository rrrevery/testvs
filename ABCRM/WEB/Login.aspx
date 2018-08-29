<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BF.Login" %>

<!DOCTYPE html>

<html lang="en" style="height:100%">
<head>
    <meta charset="utf-8">
    <title>登录 商友CRM后台管理系统</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="长益科技|CRM">
    <link href="Css/magic-check.css" rel="stylesheet" />
    <link href="Css/BF.login.css" rel="stylesheet" />
</head>
<body style="background: linear-gradient(to bottom right, #bdbdbd , #cecece)">
    <div class="loginc_r">
        <div class="logincr_c">
            <input id="username" type="text" placeholder="请输入用户名" class="login_txt1" onkeydown="SetEnterSumPwd();" value="99999" /><b class="b1"></b>
            <input id="password" type="password" placeholder="请输入密码" class="login_txt2" onkeydown="SetEnterSumEnt();" value="1" /><b class="b2"></b>
            <p style="margin: 25px 0 0 45px;">
                <input class="magic-checkbox" type="checkbox" id="cb_jz" value="1" />
                <label for="cb_jz">记住用户名密码</label>
            </p>
            <input id="B_Sure" type="button" value="登录" class="login_btn" onclick="loginFun()" />
            <%--<input type="button" value="no" onclick="nima()" style="width: 60px" />
                    <input type="button" value="yes" onclick="niba()" style="width: 60px" />
                    <input type="button" value="show" onclick="nimei()" style="width: 60px" />--%>
        </div>
    </div>

    <div class="login_b">
    </div>

    <script src="Js/jquery.js"></script>
    <script src="Js/Login.js"></script>
</body>

</html>
