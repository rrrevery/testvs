<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_QTJFDHJL_Srch.aspx.cs" Inherits="BF.CrmWeb.YHQGL.QTJFDHJL.YHQGL_QTJFDHJL_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>vPageMsgID = '<%=CM_YHQGL_QTJFDHJL%>';</script>
    <script src="YHQGL_QTJFDHJL_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input id="TB_YHQMC" type="text" />
                <input id="HF_YHQID" type="hidden" />
                <input id="zHF_YHQID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">兑换金额</div>
            <div class="bffld_right">
                <input id="TB_DHJE" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">变动积分</div>
            <div class="bffld_right">
                <input id="TB_BDJF" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">处理时间</div>
            <div class="bffld_right twodate">
                <input id="TB_CLSJ1" type="text" class="Wdate" onfocus="WdatePicker()" />
                <span class="Wdate_span">至</span>
                <input id="TB_CLSJ2" type="text" class="Wdate" onfocus="WdatePicker()" />

            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
