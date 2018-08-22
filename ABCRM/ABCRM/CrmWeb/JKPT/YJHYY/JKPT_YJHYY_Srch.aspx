<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JKPT_YJHYY_Srch.aspx.cs" Inherits="BF.CrmWeb.JKPT.YJHYY.JKPT_YJHYY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=V_Head_JKPT %>
    <script>vPageMsgID = '<%=CM_JKPT_YJHYY%>';</script>
    <script src="JKPT_YJHYY_Srch.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_JKPTBodyBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">日期</div>
            <div class="bffld_right twodate">
                <input id="TB_YEARMONTH1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt:'yyyyMM'})" />
                <span class="Wdate_span">至</span>
                <input id="TB_YEARMONTH2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt:'yyyyMM'})" />
            </div>
        </div>
        <div class="bffld" id="md">
            <div class="bffld_left">门店</div>
            <div class="bffld_right">
                <input type="text" id="TB_MDMC" />
                <input type="hidden" id="HF_MDID" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">范围</div>
            <div class="bffld_right">
                <input type="radio" class="magic-radio" id="qbhy" name="kyhy" value="0" checked="checked" />
                <label for="qbhy">全部</label>
                <input type="radio" class="magic-radio" id="kyhy" name="kyhy" value="1" />
                <label for="kyhy">可疑会员</label>
                <input type="radio" class="magic-radio" id="fkyhy" name="kyhy" value="2" />
                <label for="fkyhy">非可疑会员</label>
            </div>
        </div>
    </div>
    <div id="zMP1" class="inpage_tit slide_down_title">预警类型人数对比表</div>
    <div id="zMP1_Hidden" class="bfrow">
        <div id="ContinarChart"></div>
    </div>
    <div id="zMP2" class="inpage_tit slide_down_title">预警会员数据</div>
    <div id="zMP2_Hidden" class="bfrow">
        <table id="list"></table>
    </div>
    <div id="zMP3" class="inpage_tit slide_down_title">会员预警明细描述</div>
    <div id="zMP3_Hidden" class="bfrow">
        <table id="list1"></table>
    </div>
    <%=V_JKPTBodyEnd %>
</body>
</html>
