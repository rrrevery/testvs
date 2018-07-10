<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DHT_JF.aspx.cs" Inherits="BF.CrmWeb.CRMDHT.JFDHT" %>

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
            <span>商品积分定义单</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_HYXF_JFGZ%>">
                <span>普通积分单</span>
            </div>
            <div class="nav_fld" menuid="<%=CM_HYXF_JFGZ%>">
                <span>优先定义单</span>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>会员组特定积分</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_HYXF_HYZDY%>">
                <span>会员组定义</span>
            </div>
            <div class="nav_fld" menuid="<%=CM_HYXF_HYTDZKDYD%>">
                <span>会员组特定积分定义单</span>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>消费送积分</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_YHQGL_SJFGZ%>">
                <span>定义消费送积分规则</span>
            </div>
            <div class="nav_fld" menuid="<%=CM_YHQGL_YHQFFDYD_JF%>">
                <span>会员卡赠送积分定义单</span>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>银行刷卡送积分</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_YHQGL_SJFGZ%>">
                <span>定义消费送积分规则</span>
            </div>
            <div class="nav_fld" menuid="<%=CM_YHQGL_YHQFFDYD_JF_ZFS%>">
                <span>会员卡赠送积分定义单</span>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>积分抵现</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_CRMGL_YHQDY%>">
                <span>优惠券定义（类型选择积分）</span>
            </div>
            <div class="nav_fld" menuid="<%=CM_YHQGL_DEFJFDXBL%>">
                <span>会员卡类型积分抵现比例</span>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>开卡送积分</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_YHQGL_SJFGZ%>">
                <span>定义消费送积分规则</span>
            </div>
            <div class="nav_fld" menuid="<%=CM_YHQGL_YHQFFDYD_JF_KK%>">
                <span>会员卡开卡积分促销定义单</span>
            </div>
        </div>
    </div>
    <%-- <div class="bfrow">
        <div class="nav_left">
            <span>消费积分倍增</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld">
                <span>定义消费积分规则</span>
            </div>
            <div class="nav_fld">
                <span>会员卡积分规则设定</span>
            </div>
        </div>
    </div>--%>
    <div class="bfrow">
        <div class="nav_left">
            <span>促销积分（特定返利）</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_CRMGL_YHQDY%>">
                <span>优惠券定义（类型选择促销积分）</span>
            </div>
            <div class="nav_fld" menuid="<%=CM_YHQGL_YHQDEFFFGZ%>">
                <span>定义优惠券发放规则</span>
            </div>

            <div class="nav_fld" menuid="<%=CM_YHQGL_YHQFFDYD%>">
                <span>会员卡促销积分</span>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>银行刷卡促销积分</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_CRMGL_YHXXDY%>">
                <span>定义银行信息</span>
            </div>
            <div class="nav_fld">
                <span>银行卡号段设定</span>
            </div>
            <div class="nav_fld" menuid="<%=CM_CRMGL_YHQDY%>">
                <span>优惠券定义（类型选择）</span>
            </div>
            <div class="nav_fld" menuid="<%=CM_YHQGL_YHQFFDYD%>">
                <span>会员卡促销积分定义单</span>
            </div>
            <div class="nav_fld" menuid="<%=CM_YHQGL_YHQDEFFFGZ%>">
                <span>定义优惠券发放规则</span>
            </div>
        </div>
    </div>
    <%=V_DHTBodyEnd %>
    <script type="text/javascript">
        $("#bftitle").html("积分导航图");
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
                        tp_filename = "CrmWeb/YHQGL/YHQFFGZ/YHQGL_YHQFFGZ.aspx";
                        break;

                    case "<%=CM_YHQGL_YHQFFDYD%>":
                        tp_filename = "CrmWeb/HYXF/FQDYD/HYXF_FQDYD.aspx?djlx=0&lx=4";
                        break;
                    case "<%=CM_CRMGL_YHXXDY%>":
                        tp_filename = "CrmWeb/CRMGL/YHXXDY/CRMGL_YHXXDY.aspx";
                        break;
                    case "<%=CM_HYXF_HYTDZKDYD%>":
                        tp_filename = "CrmWeb/HYXF/TDJFZKD/HYXF_TDJFZKD.aspx?lx=0&action=add";
                        break;
                    case "<%=CM_HYXF_HYZDY%>":
                        tp_filename = "CrmWeb/HYXF/HYZDY/HYXF_HYZDY.aspx?action=add&status=0";
                        break;
                    case "<%=CM_HYXF_JFGZ%>":
                        if (this.innerText.trim() == "普通积分单")
                            tp_filename = "CrmWeb/HYXF/JFDYD/HYXF_JFDYD.aspx";
                        else
                            tp_filename = "CrmWeb/HYXF/JFDYD/HYXF_JFDYD.aspx?yx=1";
                        break;
                    case "<%=CM_YHQGL_SJFGZ%>":
                        tp_filename = "CrmWeb/YHQGL/SJFGZ/YHQGL_SJFGZ.aspx?lx=3";
                        break;
                    case "<%=CM_YHQGL_YHQFFDYD_JF%>":
                        tp_filename = "CrmWeb/HYXF/FQDYD/HYXF_FQDYD.aspx?djlx=0&lx=3";
                        break;
                    case "<%=CM_YHQGL_YHQFFDYD_JF_ZFS%>":
                        tp_filename = "CrmWeb/HYXF/FQDYD/HYXF_FQDYD.aspx?djlx=2&lx=3";
                        break;
                    case "<%=CM_YHQGL_YHQFFDYD_JF_KK%>":
                        tp_filename = "CrmWeb/HYXF/FQDYD/HYXF_FQDYD.aspx?djlx=3&lx=3";
                        break;
                    case "<%=CM_YHQGL_DEFJFDXBL%>":
                        tp_filename = "CrmWeb/YHQGL/JFDXBL/YHQGL_JFDXBL.aspx";
                        break;
                    <%--case "<%=%>":
                        tp_filename = "";
                        break;
                    case "<%=%>":
                        tp_filename = "";
                        break;--%>

                }
                if (tp_filename) {
                    MakeNewTab(pturl + tp_filename, title, tabid);
                }
            });
        });
    </script>
</body>
</html>
