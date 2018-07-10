<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_CZKZJFGZDY.aspx.cs" Inherits="BF.CrmWeb.MZKGL.CZKZJFGZDY.MZKGL_CZKZJFGZDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script>
        vPageMsgID = '<%=CM_MZKDKHGL_CZKZJFGZ%>';
    </script>
    <script src="MZKGL_CZKZJFGZDY.js"></script>
</head>
<body>
    <%=V_InputListBegin %>
    <div class="bfrow" style="display: none">
        <div class="bffld" id="jlbh">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">销售金额</div>
            <div class="bffld_right">
                <input id="TB_QDJE" type="text" />
                <input id="HF_QDJE" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">-----</div>
            <div class="bffld_right">
                <input id="TB_ZDJE" type="text" />
                <input id="HF_ZDJE" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">赠送比例</div>
            <div class="bffld_right">
                <input id="TB_ZSBL" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">赠送积分</div>
            <div class="bffld_right">
                <input id="TB_YXQTS" type="text" />
            </div>
        </div>
    </div>
    <%=V_InputListEnd %>
</body>
</html>
