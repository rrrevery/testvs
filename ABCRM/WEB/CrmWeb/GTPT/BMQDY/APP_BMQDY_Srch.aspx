<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="APP_BMQDY_Srch.aspx.cs" Inherits="BF.CrmWeb.APP.BMQDY.APP_BMQDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script src="APP_BMQDY_Srch.js"></script>

    <script>
        vPageMsgID = '<%=CM_LMSHGL_BMQDY%>';
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_LMSHGL_BMQDY_CX%>');
    </script>
</head>
<body>
    <%=V_SearchBodyBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">编码券ID</div>
            <div class="bffld_right">
                <input id="TB_BMQID" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">编码券名称</div>
            <div class="bffld_right">
                <input id="TB_BMQMC" type="text" tabindex="2" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input id="TB_YHQMC" type="text" />
                <input id="HF_YHQID" type="hidden" />
                <input id="zHF_YHQID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
