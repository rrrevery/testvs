<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXGroup.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXGroup.GTPT_WXGroup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXGROUP%>';
    </script>
    <script src="GTPT_WXGroup.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">分组编号</div>
            <div class="bffld_right">
                <input type="text" id="TB_JLBH" disabled="disabled" style="background-color: bisque;" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">分组名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_GROUPNAME" />
            </div>
        </div>
        <div class="bffld" style="display: none;">
            <div id="jlbh"></div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">备注</div>
            <div class="bffld_right">
                <input type="text" id="TB_ZY" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
