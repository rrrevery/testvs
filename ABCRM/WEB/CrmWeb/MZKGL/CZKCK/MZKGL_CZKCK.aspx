<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_CZKCK.aspx.cs" Inherits="BF.CrmWeb.MZKGL.CZKCK.MZKGL_CZKCK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_MZKGL_CKCL%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_MZKGL_CKCL_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_MZKGL_CKCL_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_MZKGL_CKCL_CX%>');
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="MZKGL_CZKCK.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">操作门店</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" onblur="GetMZKXX();" />
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
            <div class="bffld_left">存款金额</div>
            <div class="bffld_right">
                <label id="LB_CKJE" runat="server" />
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
    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>
    <div id="tb" class="item_toolbar">
        <span style="float: left">支付方式</span>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
