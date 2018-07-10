<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_HYQYDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.HYQYDY.CRMGL_HYQYDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%=V_Head_Tree %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = <%=CM_CRMGL_HYQYDY%>;
    </script>
    <script src="CRMGL_HYQYDY.js"></script>
</head>
<body>
    <%=V_TreeBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">邮政编码</div>
            <div class="bffld_right">
                <input id="TB_YZBM" type="text" />
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

