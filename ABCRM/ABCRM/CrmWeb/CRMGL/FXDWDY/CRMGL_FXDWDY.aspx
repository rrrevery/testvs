<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_FXDWDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.FXDWDY.CRMGL_FXDWDY" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%=V_Head_Tree %>
    <script>
        vPageMsgID = <%=CM_CRMGL_FXDW%>;
    </script>
    <script src="CRMGL_FXDWDY.js"></script>
</head>
<body>
    <%=V_TreeBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="mjbj">
        </div>
    </div>

    <div class="bfrow" style="display:none">
        <div class="bffld">
            <div class="bffld_left">线上门店</div>
            <div class="bffld_right">
                <input type="checkbox" class="magic-checkbox" id="CB_XSMD" value="0" />
                <label for="CB_XSMD"></label>
            </div>
        </div>
    </div>

    <div class="bfrow" style="display:none">
        <div class="bffld">
            <div class="bffld_left">默认标记</div>
            <div class="bffld_right">
                <input type="checkbox" class="magic-checkbox" id="CB_MR" value="0" />
                <label for="CB_MR"></label>
            </div>
        </div>
    </div>
    <%=V_TreeBodyEnd %>
</body>
</html>

