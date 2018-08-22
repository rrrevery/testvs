<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_GXSJDY_Srch.aspx.cs" Inherits="BF.CrmWeb.CRMGL.GXSJDY.GXSJDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search%>
    <script>
        vPageMsgID = <%=CM_CRMGL_GXSJDY%>;
    </script>
    <script src="CRMGL_GXSJDY_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" />
            </div>

        </div>
        <div class="bffld">
            <div class="bffld_left">名称</div>
            <div class="bffld_right">
                <input id="TB_QYMC" type="text" tabindex="2" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
