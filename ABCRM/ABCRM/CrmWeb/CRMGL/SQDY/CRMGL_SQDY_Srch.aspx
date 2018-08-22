<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_SQDY_Srch.aspx.cs" Inherits="BF.CrmWeb.CRMGL.SQDY.CRMGL_SQDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_CRMGL_SQDY%>';
    </script>
    <script src="CRMGL_SQDY_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">商圈编号</div>
            <div class="bffld_right">
                <input type="text" id="TB_JLBH" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">商圈名称</div>
            <div class="bffld_right">
                <input id="TB_SQMC" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
