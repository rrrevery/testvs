<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_CZKQK.aspx.cs" Inherits="BF.CrmWeb.MZKGL.CZKQK.MZKGL_CZKQK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_MZKGL_QKCL%>';
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="MZKGL_CZKQK.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">操作门店</div>
            <div class="bffld_right">
                <input id="TB_CZMD" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" onblur="GetHYXX();" />
                <input id="HF_HYID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <label id="LB_HYKNAME" runat="server" />
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">姓名</div>
            <div class="bffld_right">
                <label id="LB_HY_NAME" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">
                原余额
            </div>
            <div class="bffld_right">
                <label id="LB_YJE" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">取款金额</div>
            <div class="bffld_right">
                <input id="TB_QKJE" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left"></div>
            <div class="bffld_right">
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
