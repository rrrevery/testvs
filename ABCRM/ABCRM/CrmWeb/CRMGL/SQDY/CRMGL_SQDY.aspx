<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_SQDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.SQDY.CRMGL_SQDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_CRMGL_SQDY%>';
    </script>
    <script src="CRMGL_SQDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">商圈名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_SQMC" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店</div>
            <div class="bffld_right">
                <input type="text" id="TB_MDMC" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">商圈描述</div>
            <div class="bffld_right">
                <input id="TB_SQMS" type="text" />
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
