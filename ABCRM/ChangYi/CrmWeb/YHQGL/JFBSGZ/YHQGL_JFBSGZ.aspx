<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_JFBSGZ.aspx.cs" Inherits="BF.CrmWeb.YHQGL.JFBSGZ.YHQGL_JFBSGZ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_YHQGL_JFBSGZ%>';
    </script>
    <script src="YHQGL_JFBSGZ.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">规则名称</div>
            <div class="bffld_right">
                <input id="TB_GZMC" type="text" tabindex="2" />
            </div>
        </div>
    </div>
    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">规则列表</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加规则</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除规则</button>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
