<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKXFCX_Srch.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKXFCX.MZKGL_MZKXFCX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>vPageMsgID = '<%=CM_MZKGL_XFCX%>';</script>
    <script src="MZKGL_MZKXFCX_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO1" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_HYKNO2" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="4" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>

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
            <div class="bffld_left">收款台号</div>
            <div class="bffld_right">
                <input id="TB_SKTNO" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">借方金额</div>
            <div class="bffld_right">
                <input id="TB_JFJE" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">贷方金额</div>
            <div class="bffld_right">
                <input id="TB_DFJE" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
                <div class="bffld">
            <div class="bffld_left">账户余额</div>
            <div class="bffld_right">
                <input id="TB_ZHYE" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">处理日期</div>
            <div class="bffld_right twodate">
                <input id="TB_CLRQ1" type="text" class="Wdate" onfocus="WdatePicker()" />
                <span class="Wdate_span">至</span>
                <input id="TB_CLRQ2" type="text" class="Wdate" onfocus="WdatePicker()" />
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

    <%=V_SearchBodyEnd %>
</body>
</html>
