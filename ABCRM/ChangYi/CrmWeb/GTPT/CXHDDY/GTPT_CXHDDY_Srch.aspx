<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_CXHDDY_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.CXHDDY.GTPT_CXHDDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXCXHD%>';
    </script>
    <script src="GTPT_CXHDDY_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">促销ID</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">主题</div>
            <div class="bffld_right">
                <input id="TB_CXZT" type="text" maxlength="4" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right">
                <input id="TB_START_RQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_END_RQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">内容</div>
            <div class="bffld_right">
                <input id="TB_CXNR" type="text" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
