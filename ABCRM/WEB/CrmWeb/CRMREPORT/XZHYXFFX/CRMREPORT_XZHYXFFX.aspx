<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_XZHYXFFX.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.HYXZLS.CRMREPORT_HYXZLS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_GetHYXX.js"></script>
    <script src="CRMREPORT_XZHYXFFX.js"></script>
    <script type="text/javascript">
        vPageMsgID = <%=CM_CRMREPORT_XZHYXFFX%>;
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
                        <li class="active"><a href="#tab1">新增会员消费分析</a></li>
                    </ul>
                    <div id="tab1" class="tabcontent">
                        日期类型
                        <select id="cbRQLX" onchange="ClearDate('edRQ1','edRQ2')">
                            <%--<option value="0">日期</option>--%>
                            <option value="1">月</option>
                            <option value="2">季</option>
                        </select>
                        <input id="edRQ1" type="text" class="Wdate" />
                        ----------
                        <input id="edRQ2" type="text" class="Wdate" />

                        商户
                        <select id="cbSH2" onchange="SH2Change()">
                            <option></option>
                        </select>
                        门店
                        <input id="TB_MDMC" type="text" />
                        <input id="HF_MDID" type="hidden" />
                        <input id="zHF_MDID" type="hidden" />

                        <input id="btnSrch1" type="button" value="查询" onclick="btnSrch1Click()" />
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
