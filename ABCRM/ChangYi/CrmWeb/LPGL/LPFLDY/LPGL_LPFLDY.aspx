<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LPGL_LPFLDY.aspx.cs" Inherits="BF.CrmWeb.LPGL.LPFLDY.LPGL_LPFLDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Tree %>
    <script>vPageMsgID = '<%=CM_LPGL_LPFLDY%>';</script>
    <script src="LPGL_LPFLDY.js"></script>
</head>
<body>
    <%=V_TreeBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="mjbj">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="CB_BJ_TY" value="" />
                <label for="CB_BJ_TY"></label>
            </div>
        </div>
    </div>
    <%=V_TreeBodyEnd %>
</body>
</html>

