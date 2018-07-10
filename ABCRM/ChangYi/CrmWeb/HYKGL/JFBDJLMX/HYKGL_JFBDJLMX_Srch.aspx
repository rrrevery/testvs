<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_JFBDJLMX_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.JFBDJLMX.HYKGL_JFBDJLMX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>vPageMsgID = '<%= CM_HYKGL_JFBDMX%>';</script>
    <script src="HYKGL_JFBDJLMX_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input type="text" id="TB_HYK_NO" />

            </div>
        </div>

        <div class="bffld">
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">处理时间</div>
            <div class="bffld_right">
                <input id="TB_JSRQ1" type="text" class="Wdate" onfocus="WdatePicker()" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_JSRQ2" type="text" class="Wdate" onfocus="WdatePicker()" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
