<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JKPT_BMXFCSJKR_Srch.aspx.cs" Inherits="BF.CrmWeb.JKPT.BMXFCSJKR.JKPT_BMXFCSJKR_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_JKPT %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="JKPT_BMXFCSJKR_Srch.js"></script>
    <script>vPageMsgID = '<%=CM_JKPT_BMXFR%>';</script>
    <script src="../../CrmLib/CrmLib_FillSHBM.js"></script>

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
            <div class="bffld_left">商户</div>
            <div class="bffld_right" onchange="SHChange()">
                <select id="S_SH">
                    <option></option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">部门</div>
            <div class="bffld_right">
                <input id="TB_BMMC" type="text" />
                <input id="HF_BMDM" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">图示极限</div>
            <div class="bffld_right">
                <input id="TB_TSJX" type="text" value="50" />
            </div>
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
    <div id="menuContent" class="menuContent">
        <ul id="TreeSHBM" class="ztree"></ul>
    </div>
</body>
</html>
