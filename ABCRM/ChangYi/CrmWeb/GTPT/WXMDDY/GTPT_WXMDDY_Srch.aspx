<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXMDDY_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXMDDY.GTPT_WXMDDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
        <script src="../../CrmLib/CrmLib_GetData.js"></script>

     <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="GTPT_WXMDDY_Srch.js"></script>
    <script>vPageMsgID = '<%=CM_GTPT_WXMDDY%>';</script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店ID</div>
            <div class="bffld_right">
                <input id="TB_MDID" type="text" />
            </div>
        </div>

        <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
