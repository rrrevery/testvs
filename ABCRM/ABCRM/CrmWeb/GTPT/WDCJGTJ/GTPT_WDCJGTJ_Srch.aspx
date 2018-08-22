<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WDCJGTJ_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WDCJGTJ.GTPT_WDCJGTJ_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <%=V_Head_Search %>
    <script>vPageMsgID = '<%=CM_GTPT_WDCTJCX%>';</script>
    <script src="GTPT_WDCJGTJ_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">调查主题</div>
                                    <div class="bffld_right">
                                        <input id="TB_DCZT" type="text" />
                                    </div>
                                </div>
                            </div>

                        
                         <div class="bfrow">
                        <div class="bffld">
                            <div class="bffld_left">调查日期</div>
                            <div class="bffld_right">
                                <input id="TB_DCRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                                <span class="Wdate_span">至</span>
                                <input id="TB_DCRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                            </div>
                        </div>
                    </div>                           
        <%=V_SearchBodyEnd %>

</body>
</html>
