<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebCamera.aspx.cs" Inherits="BF.CrmWeb.CrmArt.WebCamera.WebCamera" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_WebArt %>
    <script src="../../../Js/jquery.webcam.min.js"></script>
    <script src="WebCamera.js"></script>
    <style type="text/css">
        .double-border {
            border: 5px solid #ddd;
            padding: 5px;
            background: #fff;
        }
    </style>
</head>

<body style="height: auto; overflow-y: hidden">
    <div id="Div1" class="bfbox zero_padding">
        <%=V_ArtToolBar%>
        <div id="webcam" class="double-border"></div>
        <div class="double-border" id="showImage" style="display: none">
            <canvas id="resultPhoto" style="width: 320px; height: 240px"></canvas>
        </div>
        <div align="center">
            <input id="takePhoto" type="button" class="bfbut bfblue" value="拍照" />
            <input id="takeAgain" type="button" class="bfbut bfblue" value="重拍" disabled="disabled" />
        </div>
    </div>
</body>
</html>
