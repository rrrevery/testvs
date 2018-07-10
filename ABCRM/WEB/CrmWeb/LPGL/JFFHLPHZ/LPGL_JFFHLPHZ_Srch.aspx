<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LPGL_JFFHLPHZ_Srch.aspx.cs" Inherits="BF.CrmWeb.LPGL.JFFHLPHZ.LPGL_JFFHLPHZ_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>vPageMsgID = '<%=CM_LPGL_LPFFHZ%>';</script>
    <script src="LPGL_JFFHLPHZ_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="zHF_BGDDDM" type="hidden" />
            </div>

        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">礼品名称</div>
            <div class="bffld_right">
                <input id="TB_LPMC" type="text" />
                <input id="HF_LPID" type="hidden" />
                <input id="zHF_LPID" type="hidden" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">统计日期</div>
            <div class="bffld_right">
                <input id="TB_FFRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_FFRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
