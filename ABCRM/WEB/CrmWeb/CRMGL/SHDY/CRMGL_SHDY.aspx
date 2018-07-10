<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_SHDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.SHDY.CRMGL_SHDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script>
        vPageMsgID = <%=CM_CRMGL_SHDEF%>;
    </script>
    <script src="CRMGL_SHDY.js"></script>
</head>
<body>
    <%=V_InputListBegin %>
    <%--<div>
        <div id="TopPanel" class="topbox">
            <div id="location" style="width: 400px; height: 40px; display: block; float: left; line-height: 40px;">
                <div id="switchspace" style="width: 50px; height: 40px; display: block; float: left">
                </div>
            </div>
            <div id="btn-toolbar" style="float: right">
                <div id="more" style="float: right; height: 40px; line-height: 40px; margin-right: 8px; padding-left: 20px;">
                    <i class="fa fa-list-ul fa-lg" aria-hidden="true" style="color: rgb(140,151,157)"></i>
                </div>
            </div>
        </div>
        <div id="MainPanel" class="bfbox">
            <div class="common_menu_tit">
                <span>会员卡建卡</span>
            </div>
            <div class="maininput">--%>
    <div class="bfrow">
        <div class="bffld" id="jlbh" style="display: none">
        </div>
        <div class="bffld">
            <div class="bffld_left">商户代码</div>
            <div class="bffld_right">
                <input id="TB_SHDM" type="text" maxlength="4" data-easyform="char-normal;" />
                <input id="HF_SHDM" type="hidden" data-autobind="sSHDM" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">商户名称</div>
            <div class="bffld_right">
                <input id="TB_SHMC" type="text" data-autobind="sSHMC" />
            </div>
        </div>
    </div>
    <%--    <div class="clear"></div>
                <table id="list"></table>
                <div id="pager"></div>
            </div>
        </div>
    </div>--%>
    <%=V_InputListEnd %>
</body>
</html>
