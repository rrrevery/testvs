<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmArt_ListYHQZH.aspx.cs" Inherits="BF.CrmWeb.CrmArt.ListYHQZH.CrmArt_ListYHQZH" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_WebArtList%>
    <script>
        vPageMsgID = <%=CM_CRMART_YHQZHCX%>;
    </script>
    <script src="CrmArt_ListYHQZH.js"></script>
</head>
<body style="height: auto; overflow-y: hidden">
    <div id="MainPanel" class="bfbox zero_padding">
        <%=V_ArtToolBar%>
        <div class="bfrow">
            <div class="bffld_art">
                <div class="bffld_left">开始卡号</div>
                <div class="bffld_right">
                    <input id="TB_KSKH" type="text" />

                </div>
            </div>
            <div class="bffld_art">
                <div class="bffld_left">结束卡号</div>
                <div class="bffld_right">
                    <input id="TB_JSKH" type="text" />
                </div>
            </div>
        </div>
        <table id="list"></table>
    </div>
</body>
</html>
