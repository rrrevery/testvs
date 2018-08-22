<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKQKGX.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKQKGX.MZKGL_MZKQKGX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_MZKGL_MZKQKGX%>';
    </script>
    <script src="MZKGL_MZKQKGX.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">售卡单</div>
            <div class="bffld_right">
                <input id="TB_SKDJLBH" type="text" readonly="readonly" />
                <input id="HF_SKDJLBH" type="hidden" />
                <input id="zHF_SKDJLBH" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">待还金额</div>
            <div class="bffld_right">
                <label id="LB_DHKJE"></label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">还款金额</div>
            <div class="bffld_right">
                <input type="text" id="TB_HKJE" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" type="text" />
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
