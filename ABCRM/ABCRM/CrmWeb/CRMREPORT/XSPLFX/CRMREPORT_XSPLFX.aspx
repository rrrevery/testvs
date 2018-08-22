<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_XSPLFX.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.XSPLFX.CRMREPORT_XSPLFX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_GetHYXX.js"></script>
    <script src="CRMREPORT_XSPLFX.js"></script>
    <script type="text/javascript">
        vPageMsgID = <%=CM_CRMREPORT_XSPLFX%>;
    </script>
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
                        <li class="active"><a href="#tab1">电商销售品类分析</a></li>
                        <%--<li><a href="#tab2">客群品类频率分析</a></li>--%>
                    </ul>
                    <div id="tab1" class="tabcontent">                   
                        <select id="cbRQLX4" onchange="cbRQLX4Change()" style="display: none">
                            <option value="0">日期</option>
                            <option value="1">月</option>
                            <option value="2">季</option>
                        </select>
                        日期
                        <input id="TB_RQ1" type="text" class="Wdate" />
                        ----------
                        <input id="TB_RQ2" type="text" class="Wdate" />

                        <input id="btnSrch1" type="button" value="查询" onclick="btnSrch4Click()" />
                        <br />
                        <iframe class="iFrame" id="fr1" frameborder="no" style="height: 500px; width: 960px"></iframe>
                    </div>
                    <div class="clear"></div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</body>
</html>

