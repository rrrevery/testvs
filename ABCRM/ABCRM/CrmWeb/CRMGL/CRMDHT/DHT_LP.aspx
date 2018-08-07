<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DHT_LP.aspx.cs" Inherits="page2.LPCX" %>

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
            <span>促销礼品发放</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_LPGL_JFFHLPDY%>">礼品信息定义</div>
            <div class="nav_fld" menuid="<%=CM_LPGL_LPFFGZDEF%>">定义促销礼品发放规则</div>
            <div class="nav_fld" menuid="<%=CM_YHQGL_YHQFFDYD_LP%>">促销礼品发放定义单</div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>银行刷卡促销礼品发放</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_LPGL_JFFHLPDY%>">礼品信息定义</div>
            <div class="nav_fld" menuid="<%=CM_LPGL_LPFFGZDEF%>">定义促销礼品发放规则</div>
            <div class="nav_fld" menuid="<%=CM_CRMGL_YHXXDY%>">定义银行信息</div>
            <div class="nav_fld">银行卡号段设定</div>
            <div class="nav_fld">促销礼品发放定义单</div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>生日礼</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_LPGL_JFFHLPDY%>">礼品信息定义</div>
            <div class="nav_fld nav_wrap" menuid="<%=CM_LPGL_LPFFGZDEF%>" id="sr">
                礼品发放规则定义<br />
                (规则类型生日礼)
            </div>
            <div class="nav_fld" menuid="<%=CM_LPGL_LPFF_SR%>">生日礼发放</div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>首刷礼</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_LPGL_JFFHLPDY%>">礼品信息定义</div>
            <div class="nav_fld nav_wrap" menuid="<%=CM_LPGL_LPFFGZDEF%>" id="ss">
                礼品发放规则定义<br />
                (规则类型首刷礼)
            </div>
            <div class="nav_fld" menuid="<%=CM_LPGL_LPFF_SS%>">首刷礼发放</div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>办卡礼</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_LPGL_JFFHLPDY%>">礼品信息定义</div>
            <div class="nav_fld nav_wrap" menuid="<%=CM_LPGL_LPFFGZDEF%>" id="bk">
                礼品发放规则定义<br />
                (规则类型办卡礼)
            </div>
            <div class="nav_fld" menuid="<%=CM_LPGL_LPFF_BK%>">办卡礼发放</div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>积分返礼</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_LPGL_JFFHLPDY%>">礼品信息定义</div>
            <div class="nav_fld nav_wrap" menuid="<%=CM_LPGL_LPFFGZDEF%>" id="jffl">
                礼品发放规则定义<br />
                (规则类型类型积分返礼)
            </div>
            <div class="nav_fld" menuid="<%=CM_LPGL_LPFF_JFFL%>">积分返礼发放</div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>来店礼</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_LPGL_JFFHLPDY%>">礼品信息定义</div>
            <div class="nav_fld nav_wrap" menuid="<%=CM_LPGL_LPFFGZDEF%>" id="ld">
                礼品发放规则定义<br />
                (规则类型来店礼)
            </div>
            <div class="nav_fld" menuid="<%=CM_LPGL_LPFF_LD%>">来店礼发放</div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>满额赠礼</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_LPGL_JFFHLPDY%>">礼品信息定义</div>
            <div class="nav_fld" menuid="<%=CM_LPGL_JSZLGZ%>">>满额赠礼规则定义</div>
            <div class="nav_fld">满额赠礼定义单</div>
        </div>
    </div>
    <%=V_DHTBodyEnd %>
    <script type="text/javascript">
        $("#bftitle").html("礼品促销导航图");
        $(document).ready(function () {
            $(".nav_right .nav_fld").click(function () {
                var tp_filename = "";
                var title = $(this)[0].innerText;//.substr(2, 99);
                var tabid = $(this).attr("menuid");
                switch (tabid) {
                    case "<%=CM_LPGL_JFFHLPDY%>":
                        tp_filename = "CrmWeb/LPGL/LPDY/LPGL_LPDY.aspx";
                        break;//
                    case "<%=CM_CRMGL_YHXXDY%>":
                        tp_filename = "CrmWeb/CRMGL/YHXXDY/CRMGL_YHXXDY.aspx";
                        break;
                    case "<%=CM_LPGL_LPFFGZDEF%>":
                        if (this.id == "sr")
                            tp_filename = "CrmWeb/LPGL/LPFFGZ/LPGL_LPFFGZ.aspx?GZLX=5";
                        else if (this.id == "ss")
                            tp_filename = "CrmWeb/LPGL/LPFFGZ/LPGL_LPFFGZ.aspx?GZLX=1";
                        else if (this.id == "bk")
                            tp_filename = "CrmWeb/LPGL/LPFFGZ/LPGL_LPFFGZ.aspx?GZLX=2";
                        else if (this.id == "jffl")
                            tp_filename = "CrmWeb/LPGL/LPFFGZ/LPGL_LPFFGZ.aspx?GZLX=3";
                        else if (this.id == "ld")
                            tp_filename = "CrmWeb/LPGL/LPFFGZ/LPGL_LPFFGZ.aspx?GZLX=4";
                        else
                            tp_filename = "CrmWeb/LPGL/LPFFGZ/LPGL_LPFFGZ.aspx";
                        break;
                    case "<%=CM_LPGL_LPFF_SR%>":
                        tp_filename = "CrmWeb/LPGL/LPFF/LPGL_LPFF.aspx?GZLX=5";
                        break;
                    case "<%=CM_LPGL_LPFF_SS%>":
                        tp_filename = "CrmWeb/LPGL/LPFF/LPGL_LPFF.aspx?GZLX=1";
                        break;
                    case "<%=CM_LPGL_LPFF_BK%>":
                        tp_filename = "CrmWeb/LPGL/LPFF/LPGL_LPFF.aspx?GZLX=2";
                        break;
                    case "<%=CM_LPGL_LPFF_JFFL%>":
                        tp_filename = "CrmWeb/LPGL/LPFF/LPGL_LPFF.aspx?GZLX=3";
                        break;
                    case "<%=CM_LPGL_LPFF_LD%>":
                        tp_filename = "CrmWeb/LPGL/LPFF/LPGL_LPFF.aspx?GZLX=4";
                        break;
                    case "<%=CM_LPGL_LPFFHDDY%>":
                        tp_filename = "CrmWeb/LPGL/HDLPFF/LPGL_HDLPFF.aspx";
                        break;
                    case "<%=CM_LPGL_JSZLGZ%>":
                        tp_filename = "CrmWeb/LPGL/LPZLGZ/LPGL_LPZLGZ.aspx";
                        break;
                    case "<%=CM_YHQGL_YHQFFDYD_LP%>":
                        tp_filename = "CrmWeb/HYXF/FQDYD/HYXF_FQDYD.aspx?djlx=0&lx=1";
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
