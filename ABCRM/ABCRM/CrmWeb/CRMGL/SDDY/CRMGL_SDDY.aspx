<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_SDDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.SDDY.CRMGL_SDDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script>
        vPageMsgID = '<%=CM_CRMGL_DefSD%>';
    </script>
    <script src="CRMGL_SDDY.js"></script>
</head>
<body>
    <%=V_InputListBegin %>

    <div class="bfrow">
       <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">标题</div>
            <div class="bffld_right">
                <input id="TB_BT" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始时间</div>
            <div class="bffld_right">
                <input id="TB_KSSJ" type="text" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束时间</div>
            <div class="bffld_right">
                <input id="TB_JSSJ" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left"></div>
            <span style="color:red">时间格式：如10点则输入1000</span>
        </div>
    </div>

    <%=V_InputListEnd %>
</body>
</html>
