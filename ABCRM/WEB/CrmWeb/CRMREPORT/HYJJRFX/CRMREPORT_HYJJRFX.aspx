<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_HYJJRFX.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.HYJJRFX.CRMREPORT_HYJJRFX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="CRMREPORT_HYJJRFX.js"></script>
    <script type="text/javascript">
        vPageMsgID = '<%=CM_CRMREPORT_HYJJRFX%>';
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
                        <li class="active"><a href="#tab1">会员节假日分析（年）</a></li>
                    </ul>
                    <div id="tab1" class="tabcontent">
                        日期
                        <input id="RQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt: 'yyyy'})" />
                        ----------
                        <input id="RQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt: 'yyyy'})" />
                        <br />
                        商户名称
                        <input type="text" id="TB_SHMC" />
                        <input type="hidden" id="HF_SHDM" />
                        <input type="hidden" id="zHF_SHDM" />
                        门店
                        <input id="TB_MDMC" type="text" />
                        <input id="HF_MDID" type="hidden" />
                        <input id="zHF_MDID" type="hidden" />
                        卡类型
                        <input id="TB_HYKNAME" type="text" />
                        <input id="HF_HYKNAME" type="hidden" />
                        <input id="zHF_HYKNAME" type="hidden" />
                        <br />
<%--                        商品分类
                        <input type="text" id="TB_SPFLMC" />
                        <input type="hidden" id="HF_SPFLDM" />
                        <input type="hidden" id="HF_SPFLID" />
                        <input type="hidden" id="zHF_SPFLDM" />
                        商户品牌
                        <input id="TB_SBMC" type="text" />
                        <input id="HF_SBID" type="hidden" />
                        <input id="zHF_SBID" type="hidden" />--%>
                        节假日
                        <input id="TB_JJRMC" type="text" />
                        <input id="HF_JJRID" type="hidden" />
                        <input id="zHF_JJRID" type="hidden" />
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
