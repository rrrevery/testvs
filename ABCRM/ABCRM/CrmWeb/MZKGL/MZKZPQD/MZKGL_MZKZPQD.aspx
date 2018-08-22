<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKZPQD.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKZPQD.MZKGL_MZKZPQD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="MZKGL_MZKZPQD.js"></script>
    <script>
        vPageMsgID = '<%=CM_MZKGL_MZKZPQD%>'
    </script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">到账银行</div>
            <div class="bffld_right">
                <input id="TB_DZYH" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" style="text-wrap: none">付款人名称</div>
            <div class="bffld_right">
                <input id="TB_FKRMC" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">支票金额</div>
            <div class="bffld_right">
                <input id="TB_ZPJE" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">到账日期</div>
            <div class="bffld_right">
                <input id="TB_DZRQ" type="text" class="Wdate" onfocus="WdatePicker()" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="text-wrap: none">使用金额</div>
            <div class="bffld_right">
                <input id="TB_SYJE" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" type="text" />
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
