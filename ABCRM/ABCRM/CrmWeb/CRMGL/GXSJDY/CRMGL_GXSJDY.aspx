<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_GXSJDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.GXSJDY.GXSJDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = <%=CM_CRMGL_GXSJDY%>;
    </script>
    <script src="CRMGL_GXSJDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">区域名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_QYMC" />
            </div>
        </div>
    </div>

    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left; font-size: 14px">门店信息</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加门店</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除门店</button>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
