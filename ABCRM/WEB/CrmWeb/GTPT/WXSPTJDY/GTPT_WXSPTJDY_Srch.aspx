<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXSPTJDY_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXSPTJDY.GTPT_WXSPTJDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>

    <script>
        vPageMsgID = <%=CM_GTPT_WXSPTJ%>;
    </script>

    <script src="GTPT_WXSPTJDY_Srch.js"></script>

</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">商品名称</div>
            <div class="bffld_right">
                <input id="TB_SPMC" type="text" />
                <input id="HF_SPDM" type="hidden"/>
                <input id="zHF_SPDM" type="hidden"/>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">商户名称</div>
            <div class="bffld_right">
                <input id="TB_SHMC" type="text" />
                <input type="hidden" id="HF_SHDM" />
                <input type="hidden" id="zHF_SHDM" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
