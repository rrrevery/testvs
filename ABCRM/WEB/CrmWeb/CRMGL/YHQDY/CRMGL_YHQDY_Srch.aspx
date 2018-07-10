<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_YHQDY_Srch.aspx.cs" Inherits="BF.CrmWeb.CRMGL.YHQDY.CRMGL_YHQDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = <%=CM_CRMGL_YHQDY%>;
    </script>
    <script src="CRMGL_YHQDY_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">优惠券ID</div>
            <div class="bffld_right">
                <input id="TB_YHQID" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">名称</div>
            <div class="bffld_right">
                <input id="TB_YHQMC" type="text" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
