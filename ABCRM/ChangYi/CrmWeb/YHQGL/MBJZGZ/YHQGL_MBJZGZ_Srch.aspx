<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_MBJZGZ_Srch.aspx.cs" Inherits="BF.CrmWeb.YHQGL.MBJZGZ.YHQGL_MBJZGZ_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>

    <script>vPageMsgID = '<%=CM_YHQGL_MBJZGZ%>';</script>
    <script src="YHQGL_MBJZGZ_Srch.js"></script>

</head>
<body>

    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">规则编号</div>
            <div class="bffld_right">
                <input id="TB_GZDM" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">规则名称</div>
            <div class="bffld_right">
                <input id="TB_GZMC" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">减折限额</div>
            <div class="bffld_right">
                <input id="TB_JZXE" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">起点金额</div>
            <div class="bffld_right">
                <input id="TB_QDJE" type="text" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
