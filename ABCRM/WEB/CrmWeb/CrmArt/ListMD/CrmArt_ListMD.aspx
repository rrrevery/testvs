<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmArt_ListMD.aspx.cs" Inherits="BF.CrmWeb.CrmArt.ListMD.CrmArt_ListMD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_WebArtList%>  
    <script>
        vPageMsgID = <%=CM_CRMGL_MDDEF%>;
    </script>
    <script src="CrmArt_ListMD.js"></script>
</head>
<body style="height: auto; overflow-y: hidden">
    <div id="MainPanel" class="bfbox zero_padding">
        <%=V_ArtToolBar%>
        <div class="bfrow">
            <div class="bffld_art">
                <div class="bffld_left">门店代码</div>
                <div class="bffld_right">
                    <input id="TB_MDDM" type="text" />

                </div>
            </div>
            <div class="bffld_art">
                <div class="bffld_left">门店名称</div>
                <div class="bffld_right">
                    <input id="TB_MDMC" type="text" />
                </div>
            </div>
        </div>
        <table id="list"></table>
    </div>
</body>
</html>
