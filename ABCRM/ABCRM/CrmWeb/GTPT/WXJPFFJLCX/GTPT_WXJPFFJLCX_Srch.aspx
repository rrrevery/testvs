<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXJPFFJLCX_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXJPFFJLCX.GTPT_WXJPFFJLCX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXJPFFJLCX%>';
    </script>
    <script src="GTPT_WXJPFFJLCX_Srch.js"></script>
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
            <div class="bffld_left">状态</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="ZT" id="R_ALLZT" value="all" />
                <label for="R_ALLZT">全部</label>
                <input class="magic-radio" type="radio" name="ZT" id="R_WLQ" value="0" />
                <label for="R_WLQ">未领取</label>
                <input class="magic-radio" type="radio" name="ZT" id="R_YLQ" value="1" />
                <label for="R_YLQ">已领取</label>
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
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">类型</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="DJLX" id="R_ALL" value="all" />
                <label for="R_ALL">全部</label>
                <input class="magic-radio" type="radio" name="DJLX" id="R_QHB" value="1" />
                <label for="R_QHB">抢红包</label>
                <input class="magic-radio" type="radio" name="DJLX" id="R_CJ" value="2" />
                <label for="R_CJ">抽奖</label>
                <input class="magic-radio" type="radio" name="DJLX" id="R_GGK" value="3" />
                <label for="R_GGK">刮刮卡</label>
            </div>
        </div>

    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
