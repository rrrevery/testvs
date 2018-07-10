<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_CXHDDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.CXHDDY.GTPT_CXHDDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXCXHD%>';
    </script>
    <script src="GTPT_CXHDDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">促销主题</div>
            <div class="bffld_right">
                <input type="text" id="TB_CXZT" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">内容</div>
            <div class="bffld_right">
                <input type="text" id="TB_CXNR" />
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
    <%=V_InputBodyEnd %>
</body>
</html>
