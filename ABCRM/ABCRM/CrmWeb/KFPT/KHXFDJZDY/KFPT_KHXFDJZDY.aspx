<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_KHXFDJZDY.aspx.cs" Inherits="BF.CrmWeb.KFPT.KHXFDJZDY.KFPT_KHXFDJZDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script>
        vPageMsgID = '<%=CM_KFPT_KHXFDJZDY%>';
    </script>
    <script src="KFPT_KHXFDJZDY.js"></script>
</head>
<body>
    <%=V_InputListBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">客户组名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_KFDJZMC" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">累计消费金额</div>
            <div class="bffld_right">
                <input type="text" id="TB_XFJE_BEGIN" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input type="text" id="TB_XFJE_END" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">备注</div>
            <div class="bffld_right">
                <input type="text" id="TB_BZ" />
            </div>
        </div>
    </div>
    <%=V_InputListEnd %>
</body>
</html>
