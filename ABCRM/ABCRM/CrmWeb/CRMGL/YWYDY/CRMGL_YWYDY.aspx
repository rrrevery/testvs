<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_YWYDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.YWYDY.CRMGL_YWYDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = <%=CM_CRMGL_YWYDY%>
        bCanEdit = CheckMenuPermit(iDJR, <%=CM_CRMGL_YWYDY_LR%>);
        bCanSrch = CheckMenuPermit(iDJR, <%=CM_CRMGL_YWYDY_CX%>);
    </script>
    <script src="CRMGL_YWYDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">业务员代码</div>
            <div class="bffld_right">
                <input id="TB_YWYDM" type="text" tabindex="1" readonly="true" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">所属门店</div>
            <div class="bffld_right">
                <input type="text" id="TB_MDMC" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">业务员名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_NAME" />
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

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input type="checkbox" class="magic-checkbox" id="CB_TY" value="0" />
                <label for="CB_TY"></label>
            </div>
        </div>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>
