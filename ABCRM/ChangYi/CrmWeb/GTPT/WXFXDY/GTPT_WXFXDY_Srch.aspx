<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXFXDY_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXFXDY.GTPT_WXFXDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script src="GTPT_WXFXDY_Srch.js"></script>
    <script>vPageMsgID = '<%=CM_GTPT_WXFXDY%>';</script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">分享ID</div>
            <div class="bffld_right">
                <input id="TB_ID" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">名称</div>
            <div class="bffld_right">
                <input id="TB_NAME" type="text" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
