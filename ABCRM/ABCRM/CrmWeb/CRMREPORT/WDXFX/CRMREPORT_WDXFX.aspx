<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_WDXFX.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.WDXFX.CRMREPORT_WDXFX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_GetHYXX.js"></script>
    <script src="CRMREPORT_WDXFX.js"></script>
    <script type="text/javascript">
        vPageMsgID = <%=CM_CRMREPORT_RFMWDXFX%>;
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
                        <li class="active"><a href="#tab1">客群消费稳定性分析</a></li>
                        <%--<li><a href="#tab2">客群品类频率分析</a></li>--%>
                    </ul>
                    <div id="tab1" class="tabcontent">
                        门店
                        <select id="cbMD">
                            <option/>
                        </select>
                        R
                        <select id="cbR">
                            <option value=""></option>
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                            <option value="5">5</option>
                        </select>
                        F
                        <select id="cbF">
                            <option value=""></option>
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                            <option value="5">5</option>
                        </select>
                        M
                        <select id="cbM">
                            <option value=""></option>
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                            <option value="5">5</option>
                        </select>
                        <input id="btnSrch1" type="button" value="查询" onclick="btnSrch1Click()" />
                        <iframe class="iFrame" id="fr1" frameborder="no" style="height: 500px; width: 960px"></iframe>
                    </div>
                    <%--<div id="tab2" class="tabcontent">
                        日期类型
                        <select id="cbRQLX2">
                            <option value="0">日期</option>
                            <option value="1">月</option>
                            <option value="2">季</option>
                        </select>
                        <input id="edPLRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                        ----------
                        <input id="edPLRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />                        
                        部门级次
                        <select id="cbBMJC2">
                            <option value="2">1级</option>
                            <option value="4">2级</option>
                            <option value="6">3级</option>
                            <option value="8">4级</option>
                            <option value="13">5级</option>
                        </select>
                        门店
                        <select id="cbMD2">
                            <option/>
                        </select>
                        <input id="btnSrch2" type="button" value="查询" onclick="btnSrch2Click()" />
                        <iframe class="iFrame" id="fr2" frameborder="no" style="height: 500px; width: 960px"></iframe>
                    </div>--%>
                    <div class="clear"></div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</body>
</html>
