<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXYYFWJLCX_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXYYFWJLCX.GTPT_WXYYFWJLCX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXYYFWJLCX%>';
    </script>
    <script src="GTPT_WXYYFWJLCX_Srch.js"></script>
    <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">手机号码</div>
            <div class="bffld_right">
                <input id="TB_SJHM" type="text" />
            </div>
        </div>
    </div>
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
                <input id="TB_RQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_RQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">预约状态</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="STATUS" id="R_ALL" value="all" checked="checked" />
                <label for="R_ALL">全部</label>
                <input class="magic-radio" type="radio" name="STATUS" id="R_YY" value="1" />
                <label for="R_YY">已预约</label>
                <input class="magic-radio" type="radio" name="STATUS" id="R_QX" value="0" />
                <label for="R_QX">已取消</label>
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
