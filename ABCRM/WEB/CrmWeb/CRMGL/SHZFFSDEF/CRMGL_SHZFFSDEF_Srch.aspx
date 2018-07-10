<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_SHZFFSDEF_Srch.aspx.cs" Inherits="BF.CrmWeb.CRMGL.SHZFFSDEF.CRMGL_SHZFFSDEF_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = <%=CM_CRMGL_SHZFFSDY%>;
    </script>
    <script src="CRMGL_SHZFFSDEF_Srch.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">商户</div>
            <div class="bffld_right">
                <select id="S_SH" class="easyui-combobox"></select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">支付方式代码</div>
            <div class="bffld_right">
                <input id="TB_ZFFSDM" type="text" tabindex="2" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">支付方式名称</div>
            <div class="bffld_right">
                <input id="TB_ZFFSMC" type="text" tabindex="3" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <select id="DDL_YHQ" class="easyui-combobox"></select>
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
