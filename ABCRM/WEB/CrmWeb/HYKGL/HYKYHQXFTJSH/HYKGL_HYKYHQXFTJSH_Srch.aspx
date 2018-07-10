<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKYHQXFTJSH_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKYHQXFTJSH.HYKGL_HYKYHQXFTJSH_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <%=V_Head_Search %>
    <script>vPageMsgID = '<%=CM_HYKGL_YHQ_XFTJBySH%>';</script>
    <script src="HYKGL_HYKYHQXFTJSH_Srch.js"></script>
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
                        <div class="bffld_left">商户</div>
                        <div class="bffld_right">
                            <input type="text" id="TB_SHMC" />
                            <input type="hidden" id="HF_SHDM" />
                            <input type="hidden" id="zHF_SHDM" />
                        </div>
                    </div>
                </div>

                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">卡类型</div>
                        <div class="bffld_right">
                            <input type="text" id="TB_HYKNAME" />
                            <input type="hidden" id="HF_HYKTYPE" />
                            <input type="hidden" id="zHF_HYKTYPE" />
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left">统计日期</div>
                        <div class="bffld_right">
                            <input id="TB_XFRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                            <span class="Wdate_span">至</span>
                            <input id="TB_XFRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
        <div id='SearchResult' class='bfbox'>
            <div id='zMP1' class='common_menu_tit slide_down_title'><span>优惠券门店消费日报</span></div>
            <div id='zMP1_Hidden' class='maininput'>
                <table id='list'></table>
            </div>

            <div id='zMP2' class='common_menu_tit slide_down_title'><span>优惠券门店款台消费日报</span></div>
            <div id='zMP2_Hidden' class='maininput'>
                <table id='list1'></table>
            </div>

            <div id='zMP3' class='common_menu_tit slide_down_title'><span>优惠券门店收款员消费明细</span></div>
            <div id='zMP3_Hidden' class='maininput'>
                <table id='list2'></table>
            </div>
        </div>
    </div>
</body>
</html>
