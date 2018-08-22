<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JKPT_SKTPMR_Srch.aspx.cs" Inherits="BF.CrmWeb.JKPT.SKTPMR.JKPT_SKTPMR_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_JKPT %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = '<%=CM_JKPT_SKTPMR%>';
    </script>
    <script src="JKPT_SKTPMR_Srch.js"></script>
</head>
<body>
    <%=V_JKPTBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">日期</div>
            <div class="bffld_right">
                <input id="TB_RQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
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
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">收款台号</div>
            <div class="bffld_right">
                <input id="TB_SKTNO" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">前</div>
            <div class="bffld_right">
                <input id="TB_ROWNUM" type="text" style="width: 40px;float:left" value="20" />
             <div class="bffld_left" style="text-align: left;padding-left:10px">名</div>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">排名方式</div>
            <div class="bffld_right_l">
                <input type="radio" value="1" name="djzt" id="jycs" checked="checked" class="magic-radio" />
                <label for="jycs">交易次数</label>
                <input type="radio" value="2" name="djzt" class="magic-radio" id="thcs" />
                <label for="thcs">退货次数</label>
                <input type="radio" value="3" name="djzt" class="magic-radio" id="jyje" />
                <label for="jyje">交易金额</label>
            </div>
        </div>
    </div>

    <div id="zMP2" class="inpage_tit slide_down_title">排名列表</div>
    <div id="zMP2_Hidden" class="bfrow">
        <table id="list"></table>
    </div>
    <%=V_JKPTBodyEnd %>
</body>
</html>
