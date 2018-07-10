<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_JFBSGZ_Srch.aspx.cs" Inherits="BF.CrmWeb.YHQGL.JFBSGZ.YHQGL_JFBSGZ_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_YHQGL_JFBSGZ%>';
    </script>
    <script src="YHQGL_JFBSGZ_Srch.js"></script>

</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">规则代码</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">规则名称</div>
            <div class="bffld_right">
                <input id="TB_GZMC" type="text" tabindex="2" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
