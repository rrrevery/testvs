<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKJFYBB_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKJFYBB.HYKGL_HYKJFYBB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>

    <script>vPageMsgID = '<%=CM_HYKGL_SrchHYKJFYBB%>';</script>
    <script src="HYKGL_HYKJFYBB_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="4" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">年</div>
            <div class="bffld_right" style="width: 80px">
                <input id="TB_N" type="text" style="width: 70px" />
            </div>
            <div class="bffld_left" style="width: 20px">月</div>
            <div class="bffld_right" style="width: 80px">
                <input id="TB_Y" type="text" style="width: 70px" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
