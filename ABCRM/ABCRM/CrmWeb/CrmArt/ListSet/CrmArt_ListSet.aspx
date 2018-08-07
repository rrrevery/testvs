<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmArt_ListSet.aspx.cs" Inherits="BF.CrmWeb.CrmArt.ListSet.CrmArt_ListSet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_WebArt%>
    <script src="../../../Js/datagrid-dnd.js"></script>
    <script src="CrmArt_ListSet.js"></script>
</head>
<body style="height: auto; overflow-y: hidden">
    <div id="MainPanel" class="bfbox" style="top: 0px; margin-top: 0px">
        <div id="btn-toolbar" class="common_menu_tit" style="text-align: right; width: 97%">
            <div style="text-align: left; width: 30%; float: left">表格格式设置</div>
        </div>
        <table id="list"></table>
    </div>
</body>
</html>
