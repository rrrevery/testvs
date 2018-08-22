<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKYHQXFTJMD_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKYHQXFTJMD.HYKGL_HYKYHQXFTJMD_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <%=V_Head_Search %>
    <script>vPageMsgID = '<%=CM_HYKGL_YHQ_XFTJByMD%>';</script>
    <script src="HYKGL_HYKYHQXFTJMD_Srch.js"></script>
</head>
<body>
    <div id='reg-form'>
        <div id='TopPanel' class='topbox'>
            <div id='location'>
                <div id='switchspace'></div>
            </div>
            <div id='btn-toolbar'>
                <div id='morebuttons'><i class='fa fa-list-ul fa-lg' aria-hidden='true' style='color: rgb(140,151,157)'></i></div>
            </div>
        </div>
        <div id='MainPanel' class='bfbox'>
            <div id='SearchPanel' class='common_menu_tit slide_down_title'><span>查询条件</span></div>
            <div id='SearchPanel_Hidden' class='maininput'>
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
                        <div class="bffld_left">门店名称</div>
                        <div class="bffld_right">
                            <input type="text" id="TB_MDMC" />
                            <input type="hidden" id="HF_MDID" />
                            <input type="hidden" id="zHF_MDID" />
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

            <div id='zMP3' class='common_menu_tit slide_down_title'><span>优惠券门店款台消费明细</span></div>
            <div id='zMP3_Hidden' class='maininput'>
                <table id='list2'></table>
            </div>
        </div>
    </div>
</body>
</html>
