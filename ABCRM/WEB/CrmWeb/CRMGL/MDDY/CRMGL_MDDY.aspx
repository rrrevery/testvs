<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_MDDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.MDDY.CRMGL_MDDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script>
        vPageMsgID = <%=CM_CRMGL_MDDEF%>;
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="CRMGL_MDDY.js"></script>
</head>
<body>
    <%=V_InputListBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">商户</div>
            <div class="bffld_right">
                <select id="S_SH" class="easyui-combobox">
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店代码</div>
            <div class="bffld_right">
                <input id="TB_MDDM" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld" style="display: none;">
            <div class="bffld_left">管辖商户</div>
            <div class="bffld_right">
                <select id="S_GXSH" class="easyui-combobox">
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">进销存分店号</div>
            <div class="bffld_right">
                <input type="text" id="TB_FDBH_JXC" />
            </div>
        </div>
    </div>

    <%=V_InputListEnd %>
</body>
</html>
