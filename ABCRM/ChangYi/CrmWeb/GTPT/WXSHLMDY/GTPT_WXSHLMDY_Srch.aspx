<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXSHLMDY_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXSHLMDY.GTPT_WXSHLMDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXSHLM%>';
    </script>
    <script src="GTPT_WXSHLMDY_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">联盟商户ID</div>
            <div class="bffld_right">
                <input id="TB_LMSHID" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">联盟商户名称</div>
            <div class="bffld_right">
                <input id="TB_LMSHMC" type="text" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
