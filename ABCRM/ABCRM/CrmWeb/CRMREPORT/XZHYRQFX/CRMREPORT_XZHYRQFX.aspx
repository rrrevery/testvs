<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_XZHYRQFX.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.XZHYRQFX.CRMREPORT_XZHYRQFX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_GetHYXX.js"></script>
    <script src="CRMREPORT_XZHYRQFX.js"></script>
    <script type="text/javascript">
        vPageMsgID = <%=CM_CRMREPORT_XZHYFX_RQ%>;
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
                        <li class="active"><a href="#tab1">新增会员日期分析</a></li>
                    </ul>
                    <div id="tab1" class="tabcontent">
                        日期类型
                        <select id="cbRQLX" onchange="cbRQLXChange()" >
                            <option value="0">日期</option>
                            <option value="1">月</option>
                        </select>
                        <br />
                        日期
                        <input id="TB_RQ1" type="text" class="Wdate"  />
                        ----------
                        <input id="TB_RQ2" type="text" class="Wdate"  />
                        同期
                        <input id="TB_TQ1" type="text" class="Wdate"  />
                        ----------
                        <input id="TB_TQ2" type="text" class="Wdate"  />
                        <input id="btnSrch1" type="button" value="查询" onclick="btnSrch1Click()" />
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
