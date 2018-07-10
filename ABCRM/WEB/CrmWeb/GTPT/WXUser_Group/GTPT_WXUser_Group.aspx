<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXUser_Group.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXUser_Group.GTPT_WXUser_Group" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXUSER_GROUP%>';
    </script>
    <script src="GTPT_WXUser_Group.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">分组记录名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_GROUPJL_NAME" />
            </div>
        </div>
    </div>
    <div class="bfrow">
       <div class="bffld">
            <div class="bffld_left">分组</div>
            <div class="bffld_right">
                <input id="TB_GRPMC" type="text" />
                <input id="HF_GRPID" type="hidden" />
                <input id="zHF_GRPID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">备注</div>
            <div class="bffld_right">
                <input type="text" id="TB_ZY" />
            </div>
        </div>
    </div>
     <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">微信会员</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
