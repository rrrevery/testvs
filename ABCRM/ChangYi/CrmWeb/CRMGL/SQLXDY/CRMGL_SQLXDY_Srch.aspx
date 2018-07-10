<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_SQLXDY_Srch.aspx.cs" Inherits="BF.CrmWeb.CRMGL.SQLXDY.CRMGL_SQLXDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_CRMGL_SQLXDY%>';
    </script>
    <script src="CRMGL_SQLXDY_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">类型编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">类型名称</div>
            <div class="bffld_right">
                <input id="TB_LXMC" type="text" tabindex="2" />
                <input id="HF_LXMC" type="hidden" tabindex="2" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_BZ" type="text" />
                <input id="HF_BZ" type="hidden" tabindex="2" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
