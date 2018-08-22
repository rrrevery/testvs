<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BF.Login" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8">
    <title>登录 商友CRM后台管理系统</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="长益科技|CRM">
    <link href="Css/magic-check.css" rel="stylesheet" />
    <link href="Css/BF.login.css" rel="stylesheet" />
</head>
<body id="login">
    <div class="login_t">
        <a href="http://www.changyi.com/CN/" target="_blank">
            <img src="./image/login/login_logo.jpg" /></a>
    </div>
    <div class="login_c">
        <div class="wrap">
            <div class="loginc_l">
                <img src="image/login/login_left.png" />
            </div>
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
        </div>
    </div>

    <%--<div class="container clearfix">
        <div class="logo-area">
            <h1><span class="logo">长益科技<em>CRM</em></span></h1>
        </div>
        <form class="form-signin typocn">
            <h2 class="form-signin-heading">登录</h2>
            <span class="top-tab"></span>
            <input type="text" id="username" onclick="this.select();" onkeydown="SetEnterSumPwd();" class="input-block-level margin20t" placeholder="用户名" value="99999">
            <input type="password" id="password" onclick="this.select();" onkeydown="SetEnterSumEnt();" class="input-block-level margin15t" placeholder="密码" value="1">
            <label class="checkbox margin5t">
                <input type="checkbox" checked="checked" value="remember-me">记住密码
            </label>
            <a href="#" class="btn btn-link">忘记密码？</a>
            <div class="btn-group btn-block margin10t">
            </div>
            <br>
            <button class="btn btn-primary btn-block" type="button" id="B_Sure" onclick="loginFun()">登&nbsp;&nbsp;录</button>
        </form>

    </div>--%>
    <div class="login_b">
        <p>北京长京益康信息科技有限公司 版权所有 京ICP备09104860号 </p>
        <p>免费服务电话:800-810-1190 电话:8610-84982277 传真:8610-84990180</p>
    </div>

    <script src="Js/jquery.js"></script>
    <script src="Js/Login.js"></script>
</body>

</html>
