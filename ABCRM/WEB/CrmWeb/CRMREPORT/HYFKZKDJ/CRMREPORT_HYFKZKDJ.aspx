<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_HYFKZKDJ.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.HYFKZKDJ.CRMREPORT_HYFKZKDJ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_GetHYXX.js"></script>
    <script src="CRMREPORT_HYFKZKDJ.js"></script>
    <script type="text/javascript">
        vPageMsgID = <%=CM_CRMREPORT_HYFKZKDJ%>;
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
                        <li class="active"><a href="#tab1">会员分卡种客单价分析</a></li>
                        <li><a href="#tab2">会员分卡种日期客单价分析</a></li>
                    </ul>
                    <div id="tab1" class="tabcontent">
                        日期类型
                        <select id="cbRQLX">
                            <option value="0">日期</option>
                            <option value="1">月</option>
                            <option value="2">季</option>
                        </select>
                        <input id="edRQ1" type="text" class="Wdate"  />
                        ----------
                        <input id="edRQ2" type="text" class="Wdate"  />

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
                    <div id="tab2" class="tabcontent">
                        日期类型
                        <select id="cbRQLX2" onchange="cbRQLX2Change()" >
                            <option value="0">日期</option>
                            <option value="1">月</option>
                            <option value="2">季</option>
                        </select>
                        <br />
                        日期
                        <input id="TB_RQ1" type="text" class="Wdate"  />
                        ----------
                        <input id="TB_RQ2" type="text" class="Wdate"  />
                        <br />
                        同期
                        <input id="TB_TQ1" type="text" class="Wdate"  />
                        ----------
                        <input id="TB_TQ2" type="text" class="Wdate"  />
                        <br />
                        商户
                        <select id="cbSH" onchange="SHChange()">
                            <option></option>
                        </select>
                        门店
                        <input id="TB_MD" type="text" />
                        <input id="HF_MD" type="hidden" />
                        <input id="zHF_MD" type="hidden" />
                        <input id="btnSrch2" type="button" value="查询" onclick="btnSrch2Click()" />
                        <br />
                        <iframe class="iFrame" id="fr2" frameborder="no" style="height: 500px; width: 960px"></iframe>
                    </div>
                    <div class="clear"></div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</body>
</html>
