<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DHT_YHQ.aspx.cs" Inherits="page2.YHQCX" %>

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
            <span>消费返券</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_CRMGL_YHQDY%>">优惠券定义(类型券)</div>
            <div class="nav_fld" menuid="<%=CM_YHQGL_YHQDEFFFGZ%>">定义优惠券发放规则</div>
            <div class="nav_fld" menuid="<%=CM_CRMGL_DEFYHQCXHD%>">促销活动优惠券定义</div>
            <div class="nav_fld" menuid="<%=CM_CRMGL_YHQDY%>">会员卡优惠券发放定义单</div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>银行刷卡送券</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_CRMGL_YHQDY%>">优惠券定义(发券类型按支付送)</div>
            <div class="nav_fld" menuid="<%=CM_YHQGL_YHQDEFFFGZ%>">定义优惠券发放规则</div>
            <div class="nav_fld" menuid="<%=CM_CRMGL_DEFYHQCXHD%>">促销活动优惠券定义</div>
            <div class="nav_fld" menuid="<%=CM_CRMGL_YHXXDY%>">定义银行信息</div>
            <div class="nav_fld">银行卡号段设定</div>
            <div class="nav_fld">会员卡支付送券定义单</div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>消费送电子纸券</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_CRMGL_YHQDY%>">优惠券定义(不勾电子优惠券标记)</div>
            <div class="nav_fld" menuid="<%=CM_YHQGL_YHQDEFFFGZ%>">定义优惠券发放规则</div>
            <div class="nav_fld" menuid="<%=CM_CRMGL_DEFYHQCXHD%>">促销活动优惠券定义</div>
            <div class="nav_fld">会员卡优惠券发放定义单</div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>银行刷卡送电子纸券</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld nav_wrap" menuid="<%=CM_CRMGL_YHQDY%>">优惠券定义(不勾电子优惠券标记,发券类型按支付送)</div>
            <div class="nav_fld" menuid="<%=CM_YHQGL_YHQDEFFFGZ%>">定义优惠券发放规则</div>
            <div class="nav_fld" menuid="<%=CM_CRMGL_DEFYHQCXHD%>">促销活动优惠券定义</div>
            <div class="nav_fld" menuid="<%=CM_CRMGL_YHXXDY%>">定义银行信息</div>
            <div class="nav_fld">银行卡号段设定</div>
            <div class="nav_fld">会员卡优惠券发放定义单</div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>后台发券</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_CRMGL_YHQDY%>">优惠券定义(不勾电子优惠券标记)</div>
            <div class="nav_fld" menuid="<%=CM_YHQGL_YHQDEFFFGZ%>">定义优惠券发放规则</div>
            <div class="nav_fld" menuid="<%=CM_CRMGL_DEFYHQCXHD%>">促销活动优惠券定义</div>
            <div class="nav_fld nav_wrap">
                会员卡优惠券发放定义单<br />
                (勾后台发放券标记)
            </div>
            <div class="nav_fld" menuid="<%=CM_YHQGL_MZDEF%>">优惠券面值定义</div>
            <div class="nav_fld">后台发放处理</div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>预存增值</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_YHQGL_YHQYCZZ%>">预存增值规则定义</div>
        </div>
    </div>
    <%=V_DHTBodyEnd %>
    <script type="text/javascript">
        $("#bftitle").html("优惠券导航图");
        $(document).ready(function () {
            $(".nav_right .nav_fld").click(function () {
                var tp_filename = "";
                var title = $(this)[0].innerText;//.substr(2, 99);
                var tabid = $(this).attr("menuid");
                switch (tabid) {
                    case "<%=CM_CRMGL_YHQDY%>":
                        tp_filename = "CrmWeb/CRMGL/YHQDY/CRMGL_YHQDY.aspx?action=add";
                        break;
                    case "<%=CM_YHQGL_YHQDEFFFGZ%>":
                        tp_filename = "CrmWeb/YHQGL/YHQFFGZ/YHQGL_YHQFFGZ_Srch.aspx";
                        break;
                    case "<%=CM_CRMGL_DEFYHQCXHD%>":
                        tp_filename = "CrmWeb/CRMGL/CXYHQDY/CRMGL_CXYHQDY_Srch.aspx";
                        break;
                    case "<%=CM_YHQGL_MZDEF%>":
                        tp_filename = "CrmWeb/YHQGL/MZDY/YHQGL_MZDY.aspx?action=add";
                        break;
                    case "<%=CM_YHQGL_YHQYCZZ%>":
                        tp_filename = "CrmWeb/YHQGL/YHQCKGZ/YHQGL_YHQCKGZ.aspx?action=add";
                        break;
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
