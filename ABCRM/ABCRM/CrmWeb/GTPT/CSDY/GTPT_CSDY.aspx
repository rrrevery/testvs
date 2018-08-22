<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_CSDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.CSDY.GTPT_CSDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script>
        vPageMsgID = '<%=CM_GTPT_CSDEF%>';
    </script>
    <script src="GTPT_CSDY.js"></script>
</head>
<body>
    <%=V_InputListBegin %>
        <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">城市名称</div>
            <div class="bffld_right">
                <input id="TB_CSMC" type="text" />
            </div>
        </div>
    </div>
    <%=V_InputListEnd %>
</body>
</html>
