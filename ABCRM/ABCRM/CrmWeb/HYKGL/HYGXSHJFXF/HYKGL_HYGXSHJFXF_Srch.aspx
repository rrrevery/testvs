<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYGXSHJFXF_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYGXSHJFXF.HYKGL_HYGXSHJFXF_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_GXSHJFMX%>';
    </script>
    <script src="HYKGL_HYGXSHJFXF_Srch.js"></script>
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
                        <div class="bffld" style ="display:none">
                            <div class="bffld_left">卡号</div>
                            <div class="bffld_right">
                                <input type="text" id="TB_HYK_NO" />

                            </div>
                        </div>
                        <div class="bffld">
                            <div class="bffld_left">消费时间</div>
                            <div class="bffld_right">
                                <input id="TB_JSRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                                <span class="Wdate_span">至</span>
                                <input id="TB_JSRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />

                            </div>
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>

        <div id="SearchResult" class="bfbox">
            <div id="GXSHJF" class="common_menu_tit slide_down_title">
                <span>管辖商户积分</span>
            </div>
            <div id="GXSHJF_Hidden" class="maininput">
                <table id="list"></table>
            </div>
            <div id="SHMDJF" class="common_menu_tit slide_down_title">
                <span>管辖商户门店积分</span>
            </div>
            <div id="SHMDJF_Hidden" class="maininput">
                <table id="list_SHMDJF"></table>
            </div>
            <div id="MDXFMX" class="common_menu_tit slide_down_title">
                <span>会员各门店消费明细</span>
            </div>
            <div id="MDXFMX_Hidden" class="maininput">
                <table id="list_MDXFMX"></table>
            </div>
            <div id="SPXFMX" class="common_menu_tit slide_down_title">
                <span>商品消费明细</span>
            </div>
            <div id="SPXFMX_Hidden" class="maininput">
                <table id="list_SPXFMX"></table>
            </div>
        </div>
    </div>
</body>
</html>
