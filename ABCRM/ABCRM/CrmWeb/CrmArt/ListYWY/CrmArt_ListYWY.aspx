<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmArt_ListYWY.aspx.cs" Inherits="BF.CrmWeb.CrmArt.ListYWY.CrmArt_ListYWY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_WebArtList%>
    <script>
        vPageMsgID = <%=CM_CRMGL_YWYDY%>;
    </script>
    <script src="CrmArt_ListYWY.js"></script>
</head>
<body style="height: auto; overflow-y: hidden">
    <div id="MainPanel" class="bfbox zero_padding">
        <%=V_ArtToolBar%>
        <div class="bfrow">
            <div class="bffld_art">
                <div class="bffld_left">业务员代码</div>
                <div class="bffld_right">
                    <input id="TB_YWYDM" type="text" />

                </div>
            </div>
            <div class="bffld_art">
                <div class="bffld_left">业务员名称</div>
                <div class="bffld_right">
                    <input id="TB_YWYMC" type="text" />
                </div>
            </div>
        </div>
        <table id="list"></table>
    </div>
</body>
</html>
