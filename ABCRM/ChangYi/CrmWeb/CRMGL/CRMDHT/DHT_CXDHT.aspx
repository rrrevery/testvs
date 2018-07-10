<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DHT_CXDHT.aspx.cs" Inherits="BF.CrmWeb.CRMDHT.CXDHT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_None %>
    <style>
        .nav_list {
            border: 1px solid #f6f6f6;
            border-radius: 5px;
            background: #fff;
            text-align: center;
            padding: 25px 0;
            float: left;
            width: 180px;
            box-shadow: 0 3px 3px #e5e8ec;
            -webkit-box-shadow: 0 3px 3px #e5e8ec;
            -moz-box-shadow: 0 3px 3px #e5e8ec;
            margin: 0 15px 25px;
            /*cursor: wait;*/
        }

        .img_nav {
            width: 35px;
        }

        .nav_list p {
            font-size: 14px;
            line-height: 14px;
            color: #666;
            margin-top: 6px;
        }

        .nav_wall {
            width: 850px;
            margin: 0 auto;
        }
    </style>
</head>
<body style="background: #f1f4f8; min-width: 1149px; padding-top: 90px;">
    <div class="nav_wall">
        <div class="nav_list" menuid="<%=CM_CRMGL_JFDHT%>">
            <img src="../../../image/icon/icon_08.png" class="img_nav" />
            <p>会员积分</p>
        </div>
        <div class="nav_list" menuid="<%=CM_CRMGL_ZKDHT%>">
            <img src="../../../image/icon/icon_09.png" class="img_nav" />
            <p>会员折扣</p>
        </div>
        <div class="nav_list" menuid="<%=CM_CRMGL_CXMJDHT%>">
            <img src="../../../image/icon/icon_10.png" class="img_nav" />
            <p>促销满减</p>
        </div>
        <div class="nav_list" menuid="<%=CM_CRMGL_CXMDDHT%>">
            <img src="../../../image/icon/icon_11.png" class="img_nav" />
            <p>促销满抵</p>
        </div>
        <div class="nav_list" menuid="<%=CM_CRMGL_CXYHQDHT%>">
            <img src="../../../image/icon/icon_12.png" class="img_nav" />
            <p>优惠券促销</p>
        </div>
        <div class="nav_list" menuid="<%=CM_CRMGL_LPCXDHT%>">
            <img src="../../../image/icon/icon_13.png" class="img_nav" />
            <p>礼品促销</p>
        </div>
        <div class="nav_list" menuid="<%=CM_CRMGL_CJDHT%>">
            <img src="../../../image/icon/icon_14.png" class="img_nav" />
            <p>抽奖促销</p>
        </div>
    </div>
</body>
</html>

<script type="text/javascript">
    $(document).ready(function () {
        $(".nav_list").click(function () {
            var tp_filename = "";
            var title = $(this).children()[1].innerText;//.substr(2, 99);
            var tabid = $(this).attr("menuid");
            switch (tabid) {
                case "<%=CM_CRMGL_JFDHT%>":
                    tp_filename = "CrmWeb/CRMGL/CRMDHT/DHT_JF.aspx";
                    break;
                case "<%=CM_CRMGL_ZKDHT%>":
                    tp_filename = "CrmWeb/CRMGL/CRMDHT/DHT_ZK.aspx";
                    break;
                case "<%=CM_CRMGL_CXMJDHT%>":
                    tp_filename = "CrmWeb/CRMGL/CRMDHT/DHT_MD.aspx";
                    break;
                case "<%=CM_CRMGL_CXMDDHT%>":
                    tp_filename = "CrmWeb/CRMGL/CRMDHT/DHT_MJ.aspx";
                    break;
                case "<%=CM_CRMGL_CXYHQDHT%>":
                    tp_filename = "CrmWeb/CRMGL/CRMDHT/DHT_YHQ.aspx";
                    break;
                case "<%=CM_CRMGL_LPCXDHT%>":
                    tp_filename = "CrmWeb/CRMGL/CRMDHT/DHT_LP.aspx";
                    break;
                case "<%=CM_CRMGL_CJDHT%>":
                    tp_filename = "CrmWeb/CRMGL/CRMDHT/DHT_CJ.aspx";
                    break;
            }
            if (tp_filename) {
                MakeNewTab(pturl + tp_filename, title, tabid);
            }
        });
    });
</script>
