<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKXFTJ_Srch.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKXFTJ.MZKGL_MZKXFTJ_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>vPageMsgID = '<%=CM_MZKGL_JEZ_XFTJ%>';</script>
    <script src="MZKGL_MZKXFTJ_Srch.js"></script>
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
                            <div class="bffld_left">卡类型</div>
                            <div class="bffld_right">
                                <input id="TB_HYKNAME" type="text" />
                                <input id="HF_HYKTYPE" type="hidden" />
                                <input id="zHF_HYKTYPE" type="hidden" />
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
                </div>
                <div class="clear"></div>
            </div>
        </div>

        <div id="SearchResult" class="bfbox">
            <div id="MDXF" class="common_menu_tit slide_down_title">
                <span>门店消费</span>

            </div>
            <div id="MDXF_Hidden" class="maininput">
                <table id="list"></table>
            </div>
            <div id="MDSKTXF" class="common_menu_tit slide_down_title">
                <span>门店收款台消费</span>

            </div>
            <div id="MDSKTXF_Hidden" class="maininput">
                <table id="list_MDSKTXF"></table>
            </div>
            <div id="MDSKTXFMX" class="common_menu_tit slide_down_title">
                <span>门店收款台消费明细</span>

            </div>
            <div id="MDSKTXFMX_Hidden" class="maininput">
                <table id="list_MDSKTXFMX"></table>
            </div>
        </div>
    </div>
</body>
</html>
