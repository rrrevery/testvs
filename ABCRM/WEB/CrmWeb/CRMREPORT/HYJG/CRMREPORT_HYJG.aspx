<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_HYJG.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.HYJG.CRMREPORT_HYJG" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_GetHYXX.js"></script>
    <script src="CRMREPORT_HYJG.js"></script>
    <script type="text/javascript">
        vPageMsgID = <%=CM_CRMREPORT_HYJG%>;
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
                        <li class="active"><a href="#tab1">会员总量分析</a></li>
                        <li><a href="#tab2">会员结构分析</a></li>
                        <li><a href="#tab3">会员日期总量分析</a></li>
                        <li><a href="#tab4">会员等级结构分析</a></li>
                        <li><a href="#tab5">商圈会员结构分析</a></li>
                    </ul>
                    <div id="tab1" class="tabcontent">
                        日期
                        <input id="edRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />

                        商户
                        <select id="cbSH2" onchange="SH2Change()">
                            <option></option>
                        </select>
                        门店
                        <input id="TB_MDMC" type="text" />
                        <input id="HF_MDID" type="hidden" />
                        <input id="zHF_MDID" type="hidden" />

                        <input id="btnSrch1" type="button" value="查询" onclick="btnSrch1Click()" />
                        <br />
                        <iframe class="iFrame" id="fr1" frameborder="no" style="height: 500px; width: 960px"></iframe>
                    </div>

                    <div id="tab2" class="tabcontent">
                        分析维度
                        <select id="cbFXWD">
                            <option value="0">会员数量</option>
                            <option value="1">消费金额</option>
                            <option value="2">客单价</option>
                        </select>
                        日期类型
                        <select id="cbRQLX2">
                            <%--<option value="0">日期</option>--%>
                            <option value="1">月</option>
                        </select>
                        <input id="edBMRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt:'yyyyMM'})" />
                        ----------
                        <input id="edBMRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt:'yyyyMM'})" />
                        <input id="btnSrch2" type="button" value="查询" onclick="btnSrch2Click()" />
                        <br />
                        <iframe class="iFrame" id="fr2" frameborder="no" style="height: 500px; width: 960px"></iframe>
                    </div>

                    <div id="tab3" class="tabcontent">
                        日期
                        <input id="TB_RQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt: 'yyyyMM'})" />
                        ----------
                        <input id="TB_RQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt: 'yyyyMM'})" />
                        <br />
                        同期
                        <input id="TB_TQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt: 'yyyyMM'})" />
                        ----------
                        <input id="TB_TQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt: 'yyyyMM'})" />
                        <br />
                        商户
                        <select id="cbSH" onchange="SHChange()">
                            <option></option>
                        </select>
                        门店名称
                        <input type="text" id="TB_MD" />
                        <input type="hidden" id="HF_MD" />
                        <input type="hidden" id="zHF_MD" />

                        <input id="btnSrch3" type="button" value="查询" onclick="btnSrch3Click()" />
                        <iframe class="iFrame" id="fr3" frameborder="no" style="height: 500px; width: 960px"></iframe>
                    </div>

                    <div id="tab4" class="tabcontent">
                        日期
                        <input id="TB_YF" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt: 'yyyyMM'})" />
                        商户
                        <select id="cbSH3" onchange="SH3Change()">
                            <option></option>
                        </select>
                        门店名称
                        <input type="text" id="TB_MD2" />
                        <input type="hidden" id="HF_MD2" />
                        <input type="hidden" id="zHF_MD2" />
                        统计维度
                        <select id="cbTJWD">
                            <option value="0">会员有效卡数量</option>
                            <option value="1">活跃会员人数</option>
                            <option value="2">新增会员人数</option>
                        </select>
                        活跃度
                        <select id="cbHYD">
                            <option></option>
                            <option value="30">30</option>
                            <option value="60">60</option>
                            <option value="90">90</option>
                            <option value="180">180</option>
                            <option value="365">365</option>
                        </select>
                        <input id="btnSrch4" type="button" value="查询" onclick="btnSrch4Click()" />
                        <iframe class="iFrame" id="fr4" frameborder="no" style="height: 500px; width: 960px"></iframe>
                    </div>

                    <div id="tab5" class="tabcontent">
                        商户
                        <select id="cbSH5" onchange="SH5Change()">
                            <option></option>
                        </select>
                        门店
                        <input id="TB_MD5" type="text" />
                        <input id="HF_MD5" type="hidden" />
                        <input id="zHF_MD5" type="hidden" />

                        <input id="btnSrch5" type="button" value="查询" onclick="btnSrch5Click()" />
                        <br />
                        <iframe class="iFrame" id="fr5" frameborder="no" style="height: 500px; width: 960px"></iframe>
                    </div>

                    <div class="clear"></div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</body>
</html>
