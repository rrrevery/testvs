<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmArt_ListBMQLB.aspx.cs" Inherits="BF.CrmWeb.CrmArt.ListBMQLB.CrmArt_ListBMQLB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_WebArtList%>  
    <script>
        vPageMsgID = <%=CM_GTPT_BMQLBDEF%>;
    </script>
    <script src="CrmArt_ListBMQLB.js"></script>
</head>
<body style="height: auto; overflow-y: hidden">
    <div id="MainPanel" class="bfbox zero_padding">
        <%=V_ArtToolBar%>
        <div class="bfrow">
            <div class="bffld_art">
                <div class="bffld_left">礼包id</div>
                <div class="bffld_right">
                    <input id="TB_LBID" type="text" />

                </div>
            </div>
            <div class="bffld_art">
                <div class="bffld_left">礼包名称</div>
                <div class="bffld_right">
                    <input id="TB_LBMC" type="text" />
                </div>
            </div>
        </div>
        <table id="list"></table>
    </div>
</body>
</html>
