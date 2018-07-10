<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_SHZFFSDEF.aspx.cs" Inherits="BF.CrmWeb.CRMGL.SHZFFSDEF.CRMGL_SHZFFSDEF" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script>
        vPageMsgID = '<%=CM_CRMGL_SHZFFSDY%>';
    </script>
    <script src="CRMGL_SHZFFSDEF.js"></script>
</head>
<body>
    <%=V_InputListBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh" style="display: none">
        </div>
        <div class="bffld">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">支付方式代码</div>
            <div class="bffld_right">
                <label id="TB_ZFFSDM" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">支付方式名称</div>
            <div class="bffld_right">
                <label id="TB_ZFFSMC" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">积分比例</div>
            <div class="bffld_right">
                <input id="TB_JFBL" type="text" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">满百减折标记</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="TB_BJ_MBJZ" value=""  />
                <label for="TB_BJ_MBJZ"></label>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">积分标记</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="TB_BJ_JF" value="" />
                <label for="TB_BJ_JF"></label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">返券标记</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="TB_BJ_FQ" value="" />
                <label for="TB_BJ_FQ"></label>
            </div>
        </div>
    </div>
    <%=V_InputListEnd %>
</body>
</html>
