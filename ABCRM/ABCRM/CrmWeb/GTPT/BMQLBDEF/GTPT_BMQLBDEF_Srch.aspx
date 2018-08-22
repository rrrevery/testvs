<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_BMQLBDEF_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.BMQLBDEF.GTPT_BMQLBDEF_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>

    <script>
        vPageMsgID = <%=CM_GTPT_BMQLBDEF%>;
    </script>


    <script src="GTPT_BMQLBDEF_Srch.js"></script>


</head>
<body>

    <%=V_SearchBodyBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">礼包id</div>
            <div class="bffld_right">
                <input id="TB_JLBH" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">礼包名称</div>
            <div class="bffld_right">
                <input id="TB_LBMC" />
            </div>
        </div>

    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
