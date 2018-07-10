<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_KFJLDY_Srch.aspx.cs" Inherits="BF.CrmWeb.KFPT.KFJLDY.KFPT_KFJLDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script src="KFPT_KFJLDY_Srch.js"></script>
    <script>vPageMsgID = '<%=CM_KFPT_KFJLDY%>';</script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bffld">
        <div class="bffld_left">记录编号</div>
        <div class="bffld_right">
            <input id="TB_JLBH" type="text" tabindex="1" />
        </div>
    </div>
    <div class="bffld">
        <div class="bffld_left">客服经理</div>
        <div class="bffld_right">
            <input id="HF_KFJL" type="hidden" />
            <input id="TB_KFJLMC" type="text" />
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l" style="width: 100%; height: 60px;">
            <div class="bffld_left" style="width: 8.3%">备注</div>
            <div class="bffld_right">
                <textarea id="TB_BZ" cols="20" rows="3" style="width: 65.7%; border: 1px #90A9B7 solid; background-color: #F4F6F7; border-radius: 4px; font-size: inherit;"></textarea>
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
