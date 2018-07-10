<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_KQ_XFPHFX.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.KQ_XFPHFX.CRMREPORT_KQ_XFPHFX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_GetHYXX.js"></script>
    <script src="CRMREPORT_KQ_XFPHFX.js"></script>
    <script type="text/javascript">
        vPageMsgID = <%=CM_CRMREPORT_KQ_XFPHFX%>;
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
                        <li class="active"><a href="#tab1">客户群消费偏好分析</a></li>
                    </ul>
                    <div id="tab1" class="tabcontent">
                        客群组级别
                        <select id="cbJB">
                            <option value="0">总部</option>
                            <option value="1">事业部</option>
                            <option value="2">门店</option>
                        </select>
                        客群门店
                        <select id="cbMD">
                            <option />
                        </select>
                        客群组　
                        <input id="TB_HYZMC" type="text" />
                        <input id="HF_HYZ" type="hidden" />
                        <input id="zHF_HYZ" type="hidden" />
                        <br />

                        <%--                        消费门店

                        <input id="TB_XFMD" type="text" />
                        <input id="HF_XFMD" type="hidden" />
                        <input id="zHF_XFMD" type="hidden" />--%>
                        季度
                        <input id="jdRQ1" type="text" class="Wdate" onfocus="WdatePicker({ dateFmt: 'yyyyM', isQuarter: true, disabledDates: ['....-0[5-9]-..', '....-1[0-2]-..'], startDate: '%y-01-01' })" />

                        <%--                  <input id="jdRQ2" type="text" class="Wdate" onfocus="WdatePicker({ dateFmt: 'yyyyM', isQuarter: true, disabledDates: ['....-0[5-9]-..', '....-1[0-2]-..'], startDate: '%y-01-01' })" />--%>
                        分析维度
                        <select id="cbWDLX">
                            <option value="1">消费金额</option>
                            <option value="2">消费次数</option>
                        </select>
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
