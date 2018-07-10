<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXKKBKZLJLCX_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXKKBKZLJLCX.GTPT_WXKKBKZLJLCX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXKKBKZLJLCX%>';
    </script>
    <script src="GTPT_WXKKBKZLJLCX_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYK_NO" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">登记时间</div>
            <div class="bffld_right">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">类型</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="DJLX" id="R_KK" value="1" />
                <label for="R_KK">开卡</label>
                <input class="magic-radio" type="radio" name="DJLX" id="R_BK" value="2" />
                <label for="R_BK">绑卡</label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">状态</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="ZT" id="R_WLQ" value="0" />
                <label for="R_WLQ">未领取</label>
                <input class="magic-radio" type="radio" name="ZT" id="R_CR" value="1" />
                <label for="R_CR">存入账户</label>
                <input class="magic-radio" type="radio" name="ZT" id="R_YLQ" value="2" />
                <label for="R_YLQ">已领取</label>
            </div>
        </div>

    </div>



    <%=V_SearchBodyEnd %>
</body>
</html>
