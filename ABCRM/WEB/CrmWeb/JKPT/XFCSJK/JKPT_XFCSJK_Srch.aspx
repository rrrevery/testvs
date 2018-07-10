<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JKPT_XFCSJK_Srch.aspx.cs" Inherits="BF.CrmWeb.JKPT.XFCSJK.JKPT_XFCSJK_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_JKPT %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = '<%=CM_JKPT_XFCSJK%>';
        vPageMsgID_DYHYFX = '<%=CM_KFPT_DYHYFX%>';
    </script>
    <script src="JKPT_XFCSJK_Srch.js"></script>
</head>
<body>
    <%=V_JKPTBodyBegin %>
    <div id="zMP1" class="inpage_tit slide_down_title">会员信息</div>
    <div id="zMP1_Hidden" class="bfrow">
        <table id="list"></table>
    </div>
    <div id="zMP2" class="inpage_tit slide_down_title">消费明细</div>
    <div id="zMP2_Hidden" class="bfrow">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">日期</div>
                <div class="bffld_right twodate">
                    <input id="TB_XFRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                    <span class="Wdate_span">至</span>
                    <input id="TB_XFRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                </div>
            </div>
        </div>
        <div class="clear"></div>
        <table id="list_XF"></table>
    </div>
    <%=V_JKPTBodyEnd %>
</body>
</html>
