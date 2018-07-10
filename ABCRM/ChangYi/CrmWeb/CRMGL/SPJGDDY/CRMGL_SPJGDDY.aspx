<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_SPJGDDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.SPJGDDY.CRMGL_SPJGDDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script>
        vPageMsgID = <%=CM_CRMGL_SPJGDDY%>;
    </script>
    <script src="CRMGL_SPJGDDY.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <style>
        .ui-jqgrid-btable .ui-state-highlight {
            background: yellow;
        }
    </style>
</head>
<body>
    <%=V_InputListBegin %>

    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld" style="display: none">
            <div class="bffld_left">商户</div>
            <div class="bffld_right">
                <input id="TB_SHMC" type="text" />
                <input id="HF_SHDM" type="hidden" />
                <input id="zHF_SHDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">价格带名称</div>
            <div class="bffld_right">
                <input id="TB_JGDMC" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">最低价格</div>
            <div class="bffld_right">
                <input id="TB_LSDJ1" type="text" onkeyup="clearNoNum(this)" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">最高价格</div>
            <div class="bffld_right">
                <input id="TB_LSDJ2" type="text" onkeyup="clearNoNum(this)" />
            </div>
        </div>
    </div>
    <%=V_InputListEnd %>
</body>
</html>
