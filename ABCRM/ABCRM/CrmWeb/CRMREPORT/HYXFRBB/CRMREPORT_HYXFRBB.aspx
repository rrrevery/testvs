﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_HYXFRBB.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.HYXFRBB.CRMREPORT_HYXFRBB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="CRMREPORT_HYXFRBB.js"></script>
    <script type="text/javascript">
        vPageMsgID = '<%=CM_CRMREPORT_HYFLXFRBB%>';
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
                        <li class="active"><a href="#tab1">门店会员消费日报表</a></li>
                    </ul>
                    <div id="tab1" class="tabcontent">
                        商户名称
                        <input type="text" id="TB_SHMC" />
                        <input type="hidden" id="HF_SHDM" />
                        <input type="hidden" id="zHF_SHDM" />
                        门店名称
                        <input type="text" id="TB_MDMC" />
                        <input type="hidden" id="HF_MDID" />
                        <input type="hidden" id="zHF_MDID" />
                        <br />

                        日期
                        <input id="RQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                        ----------
                        <input id="RQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                        <br />
                        同期
                        <input id="TQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                        ----------
                        <input id="TQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />

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
