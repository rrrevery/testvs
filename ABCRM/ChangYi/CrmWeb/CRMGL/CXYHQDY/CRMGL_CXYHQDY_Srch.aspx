<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_CXYHQDY_Srch.aspx.cs" Inherits="BF.CrmWeb.CRMGL.CXYHQDY.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_CRMGL_DEFYHQCXHD%>';
    </script>
    <script src="CRMGL_CXYHQDY_Srch.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>


<body>

    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input type="text" id="TB_YHQMC" />
                <input type="hidden" id="HF_YHQID" />
                <input type="hidden" id="zHF_YHQID" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">商户名称</div>
            <div class="bffld_right">
                <input id="TB_SHMC" type="text" />
                <input id="HF_SHDM" type="hidden" />
                <input id="zHF_SHDM" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">活动编号</div>
            <div class="bffld_right">
                <input id="TB_CXHDBH" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">主题名称</div>
            <div class="bffld_right">
                <input id="TB_CXZTMC" type="text" name="" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">活动开始日期</div>
            <div class="bffld_right">
                <input id="TB_KSSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_KSSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">活动结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_JSSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">用券结束日期</div>
            <div class="bffld_right">
                <input id="TB_YHQJSRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_YHQJSRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>


    <%=V_SearchBodyEnd %>
</body>
</html>
