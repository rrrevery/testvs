<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DHT_MD.aspx.cs" Inherits="page2.CXMD" %>

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
            <span>促销满抵</span>
        </div>
        <div class="nav_right">
            <div class="nav_fld" menuid="<%=CM_YHQGL_MBJZGZ%>">定义满百减折规则</div>
            <div class="nav_fld"   menuid="<%=CM_YHQGL_CXMBJZDYD%>" >促销满抵定义单</div>
        </div>
    </div>
    <%=V_DHTBodyEnd %>
    <script type="text/javascript">
        $("#bftitle").html("促销满抵导航图");
        $(document).ready(function () {
            $(".nav_right .nav_fld").click(function () {
                var tp_filename = "";
                var title = $(this)[0].innerText;//.substr(2, 99);
                var tabid = $(this).attr("menuid");
                switch (tabid) {
                    case "<%=CM_YHQGL_MBJZGZ%>":
                        tp_filename = "CrmWeb/YHQGL/MBJZGZ/YHQGL_MBJZGZ.aspx";
                        break;
                }

                switch (tabid) {
                    case "<%=CM_YHQGL_CXMBJZDYD%>":
                          tp_filename = "CrmWeb/HYXF/CXMBJZDYD/HYXF_CXMBJZDYD.aspx?mjjq=0";
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
