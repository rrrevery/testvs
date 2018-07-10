<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_BQZKLXDY.aspx.cs" Inherits="BF.CrmWeb.HYKGL.BQZKLXDY.HYKGL_BQZKLXDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_BQZKLXDY%>';
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="HYKGL_BQZKLXDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow" style="display: none">
        <div class="bffld" id="jlbh">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">标签类别</div>
            <div class="bffld_right">
                <select id="DDL_BQLB" onchange="BQLBChange()">
                    <option></option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">卡类型</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>
