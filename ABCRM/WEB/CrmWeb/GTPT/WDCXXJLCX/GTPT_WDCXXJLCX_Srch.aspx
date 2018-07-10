<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WDCXXJLCX_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WDCXXJLCX.GTPT_WDCXXJLCX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>vPageMsgID = '<%=CM_GTPT_WDCMXCX%>';</script>
    <script src="GTPT_WDCXXJLCX_Srch.js"></script>
</head>
<body>
    <div>
        <div id="TopPanel" class="topbox">
            <div id="location" style="width: 400px; height: 40px; display: block; float: left; line-height: 40px;">
                <div id="switchspace" style="width: 50px; height: 40px; display: block; float: left">
                </div>
            </div>
            <div id="btn-toolbar" style="float: right">
                <div id="more" style="float: right; height: 40px; line-height: 40px; margin-right: 8px; padding-left: 20px;">
                    <i class="fa fa-list-ul fa-lg" aria-hidden="true" style="color: rgb(140,151,157)"></i>
                </div>
            </div>
        </div>
        <div id="MainPanel" class="bfbox">
            <div id="SearchPanel" class="common_menu_tit slide_down_title">
                <span>查询条件</span>
            </div>
            <div id="SearchPanel_Hidden" class="maininput">
                <div class="form">
                    <div class="bfrow">
                        <div class="bffld">
                            <div class="bffld_left">记录编号</div>
                            <div class="bffld_right">
                                <input type="text" id="TB_JLBH" />
                            </div>
                        </div>

                        <div class="bffld">
                        </div>
                    </div>

                    <div class="bfrow">
                        <div class="bffld">
                            <div class="bffld_left">调查主题</div>
                            <div class="bffld_right">
                                <input id="TB_DCZT" type="text" />
                            </div>
                        </div>
                        <div class="bffld">
                            <div class="bffld_left">微信号</div>
                            <div class="bffld_right">
                                <input type="text" id="TB_OPENID" />
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

                </div>
                <div class="clear"></div>
            </div>
        </div>


        <div id="SearchResult" class="bfbox">
            <div id="WDCXXJL" class="common_menu_tit slide_down_title">
                <span>微调查信息记录</span>

            </div>
            <div id="WDCXXJL_Hidden" class="maininput">
                <table id="list"></table>
            </div>
            <div id="WDCXXJLMX" class="common_menu_tit slide_down_title">
                <span>微调查信息记录明细</span>

            </div>
            <div id="WDCXXJLMX_Hidden" class="maininput">
                <table id="list_1"></table>
            </div>
        </div>
    </div>



</body>
</html>
