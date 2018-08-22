<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_XSYHHYQS.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.XSYHHYQS.CRMREPORT_XSYHHYQS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <%=V_Head_Report %>
    <script src="CRMREPORT_XSYHHYQS.js"></script>
    <title></title>
</head>
<body>
    <%=V_JKPTBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">日期</div>
            <div class="bffld_right twodate">
                <input id="edRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="edRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld" style="width: 10%">
            <div class="bffld_left"></div>
            <div class="bffld_right">
                <select id="S_RQLX" class="easyui-combobox">
                </select>
            </div>
        </div>
        <div class="bffld" style="width: 10%">
            <div class="bffld_left"></div>
            <div class="bffld_right">
                <select id="S_CHANNEL" class="easyui-combobox">
                </select>
            </div>
        </div>
    </div>
    <iframe class="iFrame" id="fr1" frameborder="no" style="height: 0; width: 100%"></iframe>
    <%=V_InputBodyEnd %>
</body>
</html>
