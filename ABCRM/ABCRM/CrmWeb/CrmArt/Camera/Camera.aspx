<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Camera.aspx.cs" Inherits="BF.CrmWeb.CrmArt.Camera.Camera" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_WebArt %>
</head>
<body>
    <div id="FalshDiv" style="text-align: center; display: none;">
        <object style="z-index: 100" id="My_Cam" align="middle" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000"
            codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0"
            height="400" viewastext="在线拍照" width="500">
            <param name="allowScriptAccess" value="sameDomain" />
            <param name="movie" value="../js/MyCamera.swf" />
            <param name="quality" value="high" />
            <param name="bgcolor" value="#ffffff" />
            <param name="wmode" value="transparent" />
            <embed style="z-index: 100" align="middle" allowscriptaccess="sameDomain" bgcolor="#ffffff" height="400"
                name="My_Cam" pluginspage="http://www.macromedia.com/go/getflashplayer" quality="high" wmode="transparent"
                src="MyCamera.swf" type="application/x-shockwave-flash" width="500" />
        </object>
    </div>
    <div align="center">
        <input id="takePhoto" type="button" class="bfbut bfblue" value="拍照" onclick="photo()" />
    </div>
    <img id="SavedPhoto"/>
</body>
<script type="text/javascript">
    $(function () {
        //按钮样式
        $("#takePhoto").click(function (event) {
            event.preventDefault();
        });
        $("#FalshDiv").show();
    });
    function photo() {
        var MyCan = thisMovie("My_Cam");
        var base64Data = MyCan.GetBase64Code();
        PostToCrmlib("SavePhoto", { sData: base64Data, sDir: "test", sFileName: "test" }, function (data) {
            $("#SavedPhoto").attr('src', data);
        });
    }
    function thisMovie(movieName) {
        if (navigator.appName.indexOf("Microsoft") != -1) {
            return document[movieName];
        }
        else {
            return document[movieName];
        }
    }
</script>
</html>
