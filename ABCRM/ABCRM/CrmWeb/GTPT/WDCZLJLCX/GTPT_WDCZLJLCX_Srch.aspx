<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WDCZLJLCX_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WDCZLJLCX.GTPT_WDCZLJLCX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
       <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WDCZLJLCX%>';
    </script>
    <script src="GTPT_WDCZLJLCX_Srch.js?ts=1"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>

        <div class="bfrow">
        <div class="bffld" >
            <div class="bffld_left">状态</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="STATUS" id="all" value="all" checked="checked"   />
                <label for='all'>全部</label>
                <input class="magic-radio" type="radio" name="STATUS" id="R_BC" value="0" />
                <label for="R_BC">未领取</label>
                <input class="magic-radio" type="radio" name="STATUS" id="R_SH" value="1" />
                <label for="R_SH">已领取</label>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记时间</div>
            <div class="bffld_right">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">领取时间</div>
            <div class="bffld_right">
                <input id="TB_LQSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_LQSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>


        <%=V_SearchBodyEnd %>

</body>
</html>
