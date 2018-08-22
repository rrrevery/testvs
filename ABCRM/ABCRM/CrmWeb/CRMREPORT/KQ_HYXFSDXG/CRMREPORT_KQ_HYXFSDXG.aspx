<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_KQ_HYXFSDXG.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.KQ_HYXFSDXG.CRMREPORT_KQ_HYXFSDXG" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_GetHYXX.js"></script>
    <script src="CRMREPORT_KQ_HYXFSDXG.js"></script>
    <script type="text/javascript">
        vPageMsgID = <%=CM_CRMREPORT_KQ_XFSDXG%>;
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
                        <li class="active"><a href="#tab1">按客群分析消费时段习惯</a></li>
                    </ul>
                    <div id="tab1" class="tabcontent">
                        日期类型
                        <select id="cbRQLX" onchange="cbRQLXChange()">
                            <option value="0">日</option>
                            <option value="1">月</option>
                        </select>
                        <input id="RQ1" type="text" class="Wdate" />
                        ----------
                        <input id="RQ2" type="text" class="Wdate" />
                        商户名称
                        <input type="text" id="TB_SHMC" />
                        <input type="hidden" id="HF_SHDM" />
                        <input type="hidden" id="zHF_SHDM" />
                        <br />

                        门店
                        <input id="TB_MDMC" type="text" />
                        <input id="HF_MDID" type="hidden" />
                        <input id="zHF_MDID" type="hidden" />
                        客群门店
                        <input id="TB_KQMDMC" type="text" />                     
                        <input id="HF_KQMDID" type="hidden" />
                        <input id="zHF_KQMDID" type="hidden" />

                        客群组级别
                        <select id="cbJB">
                            <option value="0">总部</option>
                            <option value="1">事业部</option>
                            <option value="2">门店</option>
                        </select>
                        客群组　
                        <input id="TB_HYZMC" type="text" />
                        <input id="HF_HYZ" type="hidden" />
                        <input id="zHF_HYZ" type="hidden" />
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
