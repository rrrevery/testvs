<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WDCYHCX_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WDCYHCX.GTPT_WDCYHCX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
   <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WDCYHCX%>';
    </script>
    <script src="GTPT_WDCYHCX_Srch.js"></script>

</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">微信号</div>
            <div class="bffld_right">
                <input id="TB_WX_NO" type="text" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记时间</div>
            <div class="bffld_right">
                <input id="TB_RQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_RQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
