<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXLBDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXLBDY.GTPT_WXLBDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>vPageMsgID = '<%=CM_GTPT_WXLBDY%>';</script>
    <script src="GTPT_WXLBDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">礼包名称</div>
            <div class="bffld_right">
                <input id="TB_LBMC" type="text" />

            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <font color='red'> 注:类型为优惠券填写结束日期和金额,日期格式:2015-10-26</font>
        </div>
        <div class="bffld">
            <font color='red'> 类型为积分，需填写积分值</font>
        </div>
    </div>


    <div id="tb" class="item_toolbar">
        <button id="Add1" type='button' class="item_addtoolbar">添加礼品</button>
        <button id="Add2" type='button' class="item_addtoolbar">添加优惠券</button>
        <button id="Add3" type='button' class="item_addtoolbar">添加积分</button>
        <button id="Del" type='button' class="item_deltoolbar">删除</button>
    </div>

    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>


    <%=V_InputBodyEnd %>
</body>
</html>
