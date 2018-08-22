<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXGJCCFJLCX_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXGJCCFJLCX.GTPT_WXGJCCFJLCX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXGJCCFJLCX%>';
    </script>
    <script src="GTPT_WXGJCCFJLCX_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">微信号</div>
            <div class="bffld_right">
                <input id="TB_WX_NO" type="text" tabindex="4" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">关键字</div>
            <div class="bffld_right">
                <input id="TB_GJZ" type="text" tabindex="4" />

            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right">
                <input id="TB_RQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_RQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
