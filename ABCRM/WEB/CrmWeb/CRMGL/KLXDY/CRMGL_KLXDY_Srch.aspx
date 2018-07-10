<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_KLXDY_Srch.aspx.cs" Inherits="BF.CrmWeb.CRMGL.KLXDY.CRMGL_KLXDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vCZK = GetUrlParam("czk");
        if (vCZK == "0")
            vPageMsgID = <%=CM_CRMGL_HYKLXDY%>;
        else
            vPageMsgID = <%=CM_CRMGL_MZKLXDY%>;
    </script>
    <script src="CRMGL_KLXDY_Srch.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型代码</div>
            <div class="bffld_right">
                <input id="TB_HYKTYPE" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型名称</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡种</div>
            <div class="bffld_right">
                <select id="S_KZ">
                     <option></option>
                </select>
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
