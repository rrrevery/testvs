<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_ZYDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.ZYDY.CRMGL_ZYDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Tree %>
    <script>
        vPageMsgID = <%=CM_CRMGL_ZYDY%>;
    </script>
    <script src="CRMGL_ZYDY.js"></script>
</head>
<body>
    <%=V_TreeBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input type="checkbox" id="CB_TY" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld" id="mjbj">
        </div>
    </div>
    <%=V_TreeBodyEnd %>
</body>
</html>
