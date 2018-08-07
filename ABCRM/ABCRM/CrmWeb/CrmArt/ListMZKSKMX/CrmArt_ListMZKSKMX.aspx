<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmArt_ListMZKSKMX.aspx.cs" Inherits="BF.CrmWeb.CrmArt.ListMZKSKMX.CrmArt_ListMZKSKMX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_WebArtList%>
    <script>
        vPageMsgID = <%=CM_MZKGL_FSNEW%>;
    </script>
    <script src="CrmArt_ListMZKSKMX.js"></script>
</head>
<body style="height: auto; overflow-y: hidden">
    <div id="MainPanel" class="bfbox zero_padding">
        <%=V_ArtToolBar%>
        <table id="list"></table>
    </div>
</body>
</html>
