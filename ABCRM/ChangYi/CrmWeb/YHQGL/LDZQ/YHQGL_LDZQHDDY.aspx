<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_LDZQHDDY.aspx.cs" Inherits="BF.CrmWeb.YHQGL.LDZQ.YHQGL_LDZQHDDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script>
        vPageMsgID = <%=CM_YHQGL_LDZQHDDY%>;
    </script>
    <script src="YHQGL_LDZQHDDY.js"></script>
</head>
<body>
    <%=V_InputListBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">活动主题</div>
            <div class="bffld_right">
                <input id="TB_CXZT" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">活动内容</div>
            <div class="bffld_right">
                <input id="TB_CXNR" class="long" type="text" />
            </div>
        </div>
    </div>
    <%=V_InputListEnd %>
</body>
</html>
