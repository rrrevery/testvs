<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKASKFSCXSKXX_Srch.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKASKFSCXSKXX.MZKGL_MZKASKFSCXSKXX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=V_Head_Search %>
    <script>vPageMsgID = '<%=CM_MZKGL_MZKASKFSCXSKXX%>';</script>
    <script src="MZKGL_MZKASKFSCXSKXX_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">审核人</div>
            <div class="bffld_right">
                <input id="TB_ZXRMC" type="text" />
                <input id="HF_ZXR" type="hidden" />
                <input id="zHF_ZXR" type="hidden" />
            </div>
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
            <div class="bffld_left">统计日期</div>
            <div class="bffld_right">
                <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">查询范围</div>
            <div class="bffld_right">
                <input type="radio" name="RD_CXFW" value="0" checked="checked" class="magic-radio" id="r_all"/>
                <label for="r_all">全部</label>
                <input type="radio" name="RD_CXFW" value="1" class="magic-radio" id="r_sk"/>
                <label for="r_sk">售卡信息</label>
                <input type="radio" name="RD_CXFW" value="2" class="magic-radio" id="r_ck"/>
                <label for="r_ck">存款信息</label>
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
