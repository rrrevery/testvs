<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_HYKJFBDCLJL_Srch.aspx.cs" Inherits="BF.CrmWeb.HYXF.HYKJFBDCLJL.HYXF_HYKJFBDCLJL_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_HYXF_HYKJFBDJCLCX%>';
    </script>
    <script src="HYXF_HYKJFBDCLJL_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">处理日期</div>
            <div class="bffld_right">
                <input id="TB_CLRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_CLRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">本期积分</div>
            <div class="bffld_right">
                <input id="TB_BQJF" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap">本期变动积分</div>
            <div class="bffld_right">
                <input id="TB_BQBDJF" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">未处理积分</div>
            <div class="bffld_right">
                <input id="TB_WCLJF" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap">未处理积分变动</div>
            <div class="bffld_right">
                <input id="TB_WCLBDJF" type="text" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
