<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_NLDDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.NLDDY.CRMGL_NLDDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <%=V_Head_InputList %>
    <script>
        vPageMsgID = <%=CM_CRMGL_HYNLDDY%>;
    </script>
    <script src="CRMGL_NLDDY.js"></script>
</head>
<body>
    <%=V_InputListBegin %>

    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">名称</div>
            <div class="bffld_right">
                <input id="TB_NLDMC" type="text" />
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始年龄</div>
            <div class="bffld_right">
                <input id="TB_NL1" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束年龄</div>
            <div class="bffld_right">
                <input id="TB_NL2" type="text" />
            </div>
        </div>

    </div>

    <%=V_InputListEnd %>
</body>
</html>
