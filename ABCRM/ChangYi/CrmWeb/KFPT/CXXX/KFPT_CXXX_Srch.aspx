<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_CXXX_Srch.aspx.cs" Inherits="BF.CrmWeb.KFPT.CXXX.KFPT_CXXX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=V_Head_Search%>
    <script>
        vPageMsgID = '<%=CM_KFPT_CXXX%>';
    </script>
    <script src="KFPT_CXXX_Srch.js"></script>
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
                        <div class="bffld_l">
                            <div class="bffld_left">单据类型</div>
                            <div class="bffld_right">
                                <input type="radio" value="0" name="djlx" checked="checked" class="magic-radio" id="JFD" />
                                <label for="JFD">积分单据</label>
                                <input type="radio" value="1" name="djlx" class="magic-radio" id="TDJFD" />
                                <label for="TDJFD">特定会员积分单据</label>
                                <input type="radio" value="2" name="djlx" class="magic-radio" id="ZKD" />
                                <label for="ZKD">折扣单据</label>
                                <input type="radio" value="3" name="djlx" class="magic-radio" id="MJD" />
                                <label for="MJD">满减单据</label>
                                <input type="radio" value="4" name="djlx" class="magic-radio" id="FQD" />
                                <label for="FQD">发券单据</label>
                                <input type="radio" value="5" name="djlx" class="magic-radio" id="YQD" />
                                <label for="YQD">用券单据</label>
                                <%--                <input type="radio" value="6" name="djlx" class="magic-radio" id="JFFLGZ" />
                <label for="JFFLGZ">积分返利规则</label>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>

        <div id="SearchResult" class="bfbox">
            <div id="ZX" class="common_menu_tit slide_down_title">
                <span>正在执行单据</span>
            </div>
            <div id="ZX_Hidden" class="maininput">
                <table id="list"></table>
            </div>
            <div id="JJQD" class="common_menu_tit slide_down_title">
                <span>即将启动单据</span>
            </div>
            <div id="JJQD_Hidden" class="maininput">
                <table id="list_JJQD"></table>
            </div>
            <div id="WSH" class="common_menu_tit slide_down_title">
                <span>未审核单据</span>
            </div>
            <div id="WSH_Hidden" class="maininput">
                <table id="list_WSH"></table>
            </div>
            <div id="ZZ" class="common_menu_tit slide_down_title">
                <span>已终止单据</span>
            </div>
            <div id="ZZ_Hidden" class="maininput">
                <table id="list_ZZ"></table>
            </div>
            <div id="JFFL" class="common_menu_tit slide_down_title">
                <span>积分返利规则</span>
            </div>
            <div id="JFFL_Hidden" class="maininput">
                <table id="list_JFFL"></table>
            </div>
        </div>
    </div>
</body>
</html>
