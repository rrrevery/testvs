<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DHT_ZK.aspx.cs" Inherits="page2.HYZK" %>

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
            <span>商品折扣</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_HYXF_XFZKL%>">普通折扣单</div>
            <div class="nav_fld" menuid="<%=CM_HYXF_XFZKL%>">优先折扣单</div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>会员组特定折扣</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_HYXF_HYZDY%>">会员组定义</div>
            <div class="nav_fld" menuid="<%=CM_HYXF_HYTDZKDYD%>">会员组特定折扣定义单</div>
        </div>
    </div>
    <%=V_DHTBodyEnd %>
    <script type="text/javascript">
        $("#bftitle").html("会员折扣导航图");
        $(document).ready(function () {
            $(".nav_right .nav_fld").click(function () {
                var tp_filename = "";
                var title = $(this)[0].innerText;//.substr(2, 99);
                var tabid = $(this).attr("menuid");
                switch (tabid) {
                    case "<%=CM_HYXF_HYZDY%>":
                        tp_filename = "CrmWeb/HYXF/HYZDY/HYXF_HYZDY.aspx?status=0";
                        break;
                    case "<%=CM_HYXF_HYTDZKDYD%>":
                        tp_filename = "CrmWeb/HYXF/TDJFZKD/HYXF_TDJFZKD.aspx?lx=1";
                        break;
                    case "<%=CM_HYXF_XFZKL%>":
                        tp_filename = "CrmWeb/HYXF/ZKDYD/HYXF_ZKDYD.aspx";
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
