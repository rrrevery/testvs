<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmArt_TreeArt.aspx.cs" Inherits="BF.CrmWeb.CrmArt.TreeArt.CrmArt_TreeArt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_WebArt%>
    <script>
        vPageMsgID = <%=CM_CRMART_TREE%>;
    </script>
    <script src="CrmArt_TreeArt.js"></script>
</head>
<body style="height: auto; overflow-y: hidden">
    <div id="MainPanel" class="bfbox zero_padding">
        <%=V_ArtToolBar%>
        <div id="TreePanel" class="zTreeArt">
            <ul id="treeDemo" class="ztree"></ul>
        </div>
    </div>
</body>
</html>
