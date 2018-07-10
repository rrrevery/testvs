<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXBQDY_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXBQDY.GTPT_WXBQDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_GTPT_BQDY%>';
    </script>
    <script src="GTPT_WXBQDY_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
       
        <div class="bffld">
            <div class="bffld_left">标签名称</div>
            <div class="bffld_right">
                <input id="TB_BQMC" type="text" maxlength="4" />
            </div>
        </div>
    </div>
   
   
    <%=V_SearchBodyEnd %>


</body>
</html>
