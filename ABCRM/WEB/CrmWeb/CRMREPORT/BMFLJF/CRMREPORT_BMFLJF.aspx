<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_BMFLJF.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.BMFLJF.CRMREPORT_BMFLJF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_GetHYXX.js"></script>
    <script src="CRMREPORT_BMFLJF.js"></script>
    <script src="../../CrmLib/CrmLib_FillSHBM.js"></script>
    <script type="text/javascript">
        vPageMsgID = <%=CM_CRMREPORT_BMFLJF%>;
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
                        <li class="active"><a href="#tab1">按部门查询积分</a></li>
                        <li><a href="#tab2">按品牌查询积分</a></li>
                        <li><a href="#tab3">按日期查询积分</a></li>
                    </ul>
                    <div id="tab1" class="tabcontent">
                        日期类型
                        <select id="cbRQLX" onchange="cbRQLXChange()">
                            <option value="0">日期</option>
                            <option value="1">月</option>
                            <option value="2">季</option>
                        </select>
                        <input id="edRQ1" type="text" class="Wdate" />
                        ----------
                        <input id="edRQ2" type="text" class="Wdate" />
                        商户
                        <select id="cbSH" onchange="SHChange()">
                            <option></option>
                        </select>
                        部门
                        <input id="TB_BMMC" type="text" />
                        <input id="HF_BMDM" type="hidden" />
                        部门级次
                        <select id="cbBMJC">
                        </select>
                        <p></p>
                        消费门店
                        <select id="cbMD">
                            <option />
                        </select>
                        卡类型　
                        <input id="TB_HYKNAME" type="text" />
                        <input id="HF_HYKNAME" type="hidden" />
                        <input id="zHF_HYKNAME" type="hidden" />
                        <input id="btnSrch1" type="button" value="查询" onclick="btnSrch1Click()" />
                        <br />
                        <iframe class="iFrame" id="fr1" frameborder="no" style="height: 500px; width: 960px"></iframe>
                    </div>
                    <div id="tab2" class="tabcontent">
                        日期类型
                        <select id="cbRQLX2" onchange="cbRQLX2Change()" >
                            <option value="0">日期</option>
                            <option value="1">月</option>
                            <option value="2">季</option>
                        </select>
                        <input id="edBMRQ1" type="text" class="Wdate"  />
                        ----------
                        <input id="edBMRQ2" type="text" class="Wdate"  />
                        消费门店
                        <select id="cbMD2">
                            <option />
                        </select>
                        卡类型　
                        <input id="TB_HYKTYPE" type="text" />
                        <input id="HF_HYKTYPE" type="hidden" />
                        <input id="zHF_HYKTYPE" type="hidden" />
                        <input id="btnSrch2" type="button" value="查询" onclick="btnSrch2Click()" />
                        <br />
                        <iframe class="iFrame" id="fr2" frameborder="no" style="height: 500px; width: 960px"></iframe>
                    </div>
                    <div id="tab3" class="tabcontent">
                        日期类型
                        <select id="cbRQLX3" onchange="cbRQLX3Change()" >
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
                        <select id="cbSH2" onchange="SH2Change()">
                            <option></option>
                        </select>
                        门店
                        <input id="TB_MDMC" type="text" />
                        <input id="HF_MDID" type="hidden" />
                        <input id="zHF_MDID" type="hidden" />
                        卡类型　
                        <input id="TB_HYKLX" type="text" />
                        <input id="HF_HYKLX" type="hidden" />
                        <input id="zHF_HYKLX" type="hidden" />
                        <input id="btnSrch3" type="button" value="查询" onclick="btnSrch3Click()" />
                        <br />
                        <iframe class="iFrame" id="fr3" frameborder="no" style="height: 500px; width: 960px"></iframe>
                    </div>
                    <div class="clear"></div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <div id="menuContent" class="menuContent" style="display: none; position: absolute; background-color: white; overflow: auto;" />
    <ul id="TreeSHBM" class="ztree" style="margin-top: 0; width: 200px; height: 200px"></ul>
</body>
</html>
