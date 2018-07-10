<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JKPT_MDXFCSJKY_Srch.aspx.cs" Inherits="BF.CrmWeb.JKPT.MDXFCSJKY.JKPT_MDXFCSJKY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=V_Head_JKPT %>
    <script>
        vPageMsgID = '<%=CM_JKPT_MDXFY%>';
        var vPageMsgID_XFCSJK = '<%=CM_JKPT_XFCSJK%>';
    </script>
    <script src="JKPT_MDXFCSJKY_Srch.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_JKPTBodyBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">年月</div>
            <div class="bffld_right twodate">
                <input id="TB_YEARMONTH1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt:'yyyyMM'})" />
                <span class="Wdate_span">至</span>
                <input id="TB_YEARMONTH2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt:'yyyyMM'})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">门店</div>
            <div class="bffld_right">
                <input type="text" id="TB_MDMC" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
            </div>
        </div>
    </div>
    <div class="bffld">
        <div class="bffld_left">图示极限</div>
        <div class="bffld_right">
            <input id="TB_TSJX" type="text" value="100" />
        </div>
    </div>
    <div id="zMP1" class="inpage_tit slide_down_title">监控图表</div>
    <div id="zMP1_Hidden" class="bfrow">
        <div id="ContinarChart"></div>
    </div>
    <div id="zMP2" class="inpage_tit slide_down_title">监控数据</div>
    <div id="zMP2_Hidden" class="bfrow">
        <table id="list"></table>
    </div>
    <%=V_JKPTBodyEnd %>
</body>
</html>
