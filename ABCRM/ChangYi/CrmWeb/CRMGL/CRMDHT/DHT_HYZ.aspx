<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DHT_HYZ.aspx.cs" Inherits="page2.KQZGL" %>

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
            <span>客群组定义</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_HYXF_HYZLXDY%>">客群组类型定义</div>
            <div class="nav_fld" menuid="<%=CM_HYXF_HYZDY%>">客群组定义</div>
            <div class="nav_fld">查询导出客群组</div>
        </div>
    </div>
    <div class="bfrow">
        <div class="nav_left">
            <span>客群组使用</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld">客群组定义活动</div>
            <div class="nav_fld">客群组查询分析</div>
        </div>
    </div>
    <%=V_DHTBodyEnd %>
    <script type="text/javascript">
        $("#bftitle").html("客群组导航图");
        $(document).ready(function () {
            $(".nav_right .nav_fld").click(function () {
                var tp_filename = "";
                var title = $(this)[0].innerText;//.substr(2, 99);
                var tabid = $(this).attr("menuid");
                switch (tabid) {
                    case "<%=CM_HYXF_HYZLXDY%>":
                        tp_filename = "CrmWeb/HYXF/HYZLXDY/HYXF_HYZLXDY.aspx";
                        break;
                    case "<%=CM_HYXF_HYZDY%>":
                        tp_filename = "CrmWeb/HYXF/HYZDY/HYXF_HYZDY_Srch.aspx?status=0";
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
