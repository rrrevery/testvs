<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXSHLMLXDY_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXSHLMLXDY.GTPT_WXSHLMLXDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_GTPT_LMSHLXDY%>';
    </script>
    <script src="GTPT_WXSHLMLXDY_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">类型ID</div>
            <div class="bffld_right">
                 <input id="TB_LXID" type="text"  />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">类型名称</div>
            <div class="bffld_right">
                 <input id="TB_LXMC" type="text" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
