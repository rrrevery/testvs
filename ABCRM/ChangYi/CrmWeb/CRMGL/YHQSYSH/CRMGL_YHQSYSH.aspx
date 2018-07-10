<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_YHQSYSH.aspx.cs" Inherits="BF.CrmWeb.CRMGL.YHQSYSH.CRMGL_YHQSYSH" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = <%=CM_CRMGL_DEFYHQSYSH%>;
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="CRMGL_YHQSYSH.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow" style="display: none;">
        <div class="bffld">
            <div id="jlbh">
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">商户名称</div>
            <div class="bffld_right">
                <select id="DDL_SHMC">
                    <option></option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">优惠券名称</div>
            <div class="bffld_right">
                <select id="DDL_YHQMC"class="easyui-combobox" >
                    <option></option>
                </select>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">使用单标记</div>
            <div class="bffld_right">
                <input id="BJ_SYD" class="magic-checkbox" type="checkbox" />
                <label for="BJ_SYD"></label>
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>
