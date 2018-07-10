<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_SQLXDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.SQLXDY.CRMGL_SQLXDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_CRMGL_SQLXDY%>';
    </script>
    <script src="CRMGL_SQLXDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap">商圈类型名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_LXMC" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_BZ" type="text" />
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
