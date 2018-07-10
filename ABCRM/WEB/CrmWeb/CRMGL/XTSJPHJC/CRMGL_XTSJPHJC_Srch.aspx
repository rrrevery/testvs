<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_XTSJPHJC_Srch.aspx.cs" Inherits="BF.CrmWeb.CRMGL.XTSJPHJC.CRMGL_XTSJPHJC_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_CRMGL_CheckXTSJ%>';
    </script>
    <script src="CRMGL_XTSJPHJC_Srch.js"></script>
</head>
<body>
    <%=V_NoneBodyBegin %>

        <div id="MainPanel" class="bfbox">
            <div id="SearchPanel" class="common_menu_tit slide_down_title">
                <span>查询条件</span>
            </div>
            <div id="SearchPanel_Hidden" class="maininput">
                <div class="form">
                    <div class="bfrow">
                        <div class="bffld">
                            <div class="bffld_left">对账日期</div>
                            <div class="bffld_right">
                                <input id="TB_RQ" class="Wdate" type="text" onfocus="WdatePicker({isShowWeek:true,dateFmt:'yyyy-MM-dd',})" />
                            </div>

                        </div>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>

        <div id="SearchResult" class="bfbox">
            <div id="ResultDeal" class="common_menu_tit slide_down_title">
                <span>日处理状态表</span>
            </div>
            <div id="ResultDeal_Hidden" class="maininput">
                <table id="list"></table>
            </div>

            <div id="ResultAccount" class="common_menu_tit slide_down_title" style="display: none">
                <span>储值卡对账表</span>
            </div>
            <div id="ResultAccount_Hidden" class="maininput" style="display: none">
                <table id="list_ResultAccount"></table>
            </div>


            <div id="ResultCoupon" class="common_menu_tit slide_down_title">
                <span>优惠券对账表</span>
            </div>
            <div class="maininput" id="ResultCoupon_Hidden">
                <table id="list_ResultCoupon"></table>
            </div>


            <div id="ResultStock" class="common_menu_tit slide_down_title">
                <span>库存卡对账表</span>
            </div>
            <div class="maininput" id="ResultStock_Hidden">
                <table id="list_ResultStock"></table>
            </div>

        </div>
    <%=V_NoneBodyEnd %>
</body>
</html>
