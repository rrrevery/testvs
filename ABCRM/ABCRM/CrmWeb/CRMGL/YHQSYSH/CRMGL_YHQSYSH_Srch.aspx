<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_YHQSYSH_Srch.aspx.cs" Inherits="BF.CrmWeb.CRMGL.YHQSYSH.CRMGL_YHQSYSH_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_CRMGL_DEFYHQSYSH%>';
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="CRMGL_YHQSYSH_Srch.js"></script>
</head>
<body>

    <%=V_SearchBodyBegin %>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">商户名称</div>
            <div class="bffld_right">
                <select id="DDL_SHMC" class="easyui-combobox"></select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">优惠券名称</div>
            <div class="bffld_right">
                <select id="DDL_YHQMC" class="easyui-combobox"></select>
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
