<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_KZDY_Srch.aspx.cs" Inherits="BF.CrmWeb.CRMGL.KZDY.CRMGL_KZDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <%=V_Head_Search %>
    <script>
        vCZK = GetUrlParam("czk");
        if (vCZK == "0")
            vPageMsgID = <%=CM_CRMGL_HYKKZDY%>;
        else
            vPageMsgID = <%=CM_CRMGL_MZKKZDY%>;
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="CRMGL_KZDY_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡种编号</div>
            <div class="bffld_right">
                <input id="TB_HYKKZID" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡种名称</div>
            <div class="bffld_right">
                <input id="TB_HYKKZNAME" type="text" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
