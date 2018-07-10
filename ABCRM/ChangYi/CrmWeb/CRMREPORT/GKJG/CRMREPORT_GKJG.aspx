<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_GKJG.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.GKJG.CRMREPORT_GKJG" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="CRMREPORT_GKJG.js"></script>
    <script type="text/javascript">
        var $ = jQuery.noConflict();
        $(function () {
            $('#tabsmenu').tabify();
            $(".toggle_container").hide();
            $(".trigger").click(function () {
                $(this).toggleClass("active").next().slideToggle("slow");
                return false;
            });
        });
    </script>
</head>
<body>
    <div id="panelwrap">
        <div class="center_content">
            <div id="right_wrap">
                <div id="right_content">
                    <div id="btn-toolbar">
                    </div>
                    <ul id="tabsmenu" class="tabsmenu">
                        <li class="active"><a href="#tab1">按卡类型分析会员结构组成</a></li>
                        <li><a href="#tab2">按卡类型分析会员结构组成</a></li>
                    </ul>
                    <div id="tab1" class="tabcontent">
                        <select id="cbRQLX">
                            <option value="0">日期</option>
                            <option value="1">月</option>
                            <option value="2">季</option>
                            <option value="3">年</option>
                        </select>
                        <input id="edRQ1" type="text" />
                        ----------
                        <input id="edRQ2" type="text" />
                        <input id="btnSrch2" type="button" value="查询" onclick="btnSrch2Click()" />
                        <iframe class="iFrame" id="fr2" frameborder="no" style="height: 500px; width: 800px" src="http://localhost:8075/WebReport/ReportServer?reportlet=CR_HYK_XF_R.cpt&op=view"></iframe>
                    </div>
                    <div id="tab2" class="tabcontent">
                        <iframe class="iFrame" id="fr1" frameborder="no" style="height: 500px; width: 800px" src="http://localhost:8075/WebReport/ReportServer?reportlet=%5B4ea4%5D%5B53c9%5D%5B8868%5D.cpt&op=view"></iframe>
                    </div>
                    <div class="clear"></div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</body>
</html>
