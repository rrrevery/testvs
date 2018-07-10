<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_DRXFJEYJDY_Srch.aspx.cs" Inherits="BF.CrmWeb.KFPT.DRXFJEYJDY.KFPT_DRXFJEYJDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script src="KFPT_DRXFJEYJDY_Srch.js"></script>
    <script>vPageMsgID = '<%=CM_KFPT_DRXFJEYJ%>';</script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="1" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">预警金额</div>
            <div class="bffld_right">
                <input type="text" id="TB_YJJE" tabindex="2" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
