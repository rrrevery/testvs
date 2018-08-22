<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DHT_CJ.aspx.cs" Inherits="page2.CJCX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_None %>
</head>
<body>
    <%=V_DHTBodyBegin %>
    <div class="bfrow">
        <div class="nav_left">
            <span>消费抽奖</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld">抽奖券定义(类型:抽奖)</div>
            <div class="nav_fld">定义促销抽奖发放规则</div>
            <div class="nav_fld">促销活动抽奖定义</div>
            <div class="nav_fld">促销抽奖发放定义单</div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>银行刷卡消费抽奖</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld">抽奖券定义(发券类型:按支付送)</div>
            <div class="nav_fld">定义促销抽奖发放规则</div>
            <div class="nav_fld">促销活动抽奖定义</div>
            <div class="nav_fld" menuid="<%=CM_CRMGL_YHXXDY%>">定义银行信息</div>
            <div class="nav_fld">银行号段设定</div>
            <div class="nav_fld">促销抽奖发放定义单</div>
        </div>
    </div>
    <%=V_DHTBodyEnd %>
    <script type="text/javascript">
        $("#bftitle").html("抽奖促销导航图");
        $(document).ready(function () {
            $(".nav_right .nav_fld").click(function () {
                var tp_filename = "";
                var title = $(this)[0].innerText;//.substr(2, 99);
                var tabid = $(this).attr("menuid");
                switch (tabid) {
                    case "<%=CM_CRMGL_YHXXDY%>":
                        tp_filename = "CrmWeb/CRMGL/YHXXDY/CRMGL_YHXXDY.aspx";
                        break;
                }
                if (tp_filename) {
                    MakeNewTab(pturl + tp_filename, title, tabid);
                }
            });
        });
    </script>
</body>
</html>
