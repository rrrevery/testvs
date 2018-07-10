<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXSQGZDYD.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXSQGZDYD.GTPT_WXSQGZDYD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXSQDYD%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXSQDYD_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXSQDYD_SH%>');
        bCanStart = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXSQDYD_QD%>');
        bCanStop = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXSQDYD_ZZ%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXSQDYD_CX%>');

    </script>
      <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXSQGZDYD.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">优惠券规则</div>
            <div class="bffld_right">
                <input id="TB_GZMC" type="text" />
                <input type="hidden" id="HF_GZID" />
                <input type="hidden" id="zHF_GZID" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right">
                <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
