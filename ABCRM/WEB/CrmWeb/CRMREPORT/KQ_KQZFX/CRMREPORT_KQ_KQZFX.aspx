<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_KQ_KQZFX.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.KQ_KQZFX.CRMREPORT_KQ_KQZFX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_GetHYXX.js"></script>
    <script src="CRMREPORT_KQ_KQZFX.js"></script>
    <script src="../../CrmLib/CrmLib_FillSHBM.js"></script>
    <script type="text/javascript">
        vPageMsgID = <%=CM_CRMREPORT_KQ_KQZFX%>;
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
                        <li class="active"><a href="#tab1">客户群销售对比</a></li>
                    </ul>
                    <div id="tab1" class="tabcontent">
                        客群组级别
                        <select id="cbJB">
                            <option value="0">总部</option>
                            <option value="1">事业部</option>
                            <option value="2">门店</option>
                        </select>

                        所属门店
                        <select id="cbMD">
                            <option />
                        </select>

                        客群组　
                        <input id="TB_HYZMC" type="text" />
                        <input id="HF_HYZ" type="hidden" />
                        <input id="zHF_HYZ" type="hidden" />

                        <br />

                        部门级次
                        <select id="cbJC" onchange="SHChange()">
                            <option value="1">一级</option>
                            <option value="3">三级</option>
                            <option value="5">五级</option>
                        </select>

                        消费门店
                        <%--                        <select id="cbXFMD">
                            <option />
                        </select>--%>
                        <input id="TB_XFMD" type="text" />
                        <input id="HF_XFMD" type="hidden" />
                        <input id="zHF_XFMD" type="hidden" />

                        部门
                        <input id="TB_BMMC" type="text" />
                        <input id="HF_BMDM" type="hidden" />

                        <br />

                        日期类型
                        <select id="cbRQLX">
                            <option value="0">日期</option>
                            <option value="1">月</option>
                            <option value="2">季</option>
                        </select>
                        日期
                        <input id="edRQ1" type="text" class="Wdate"  />
                        ----------
                        <input id="edRQ2" type="text" class="Wdate"  />
                        <br />
                        同期
                        <input id="tdRQ1" type="text" class="Wdate"  />
                        ----------
                        <input id="tdRQ2" type="text" class="Wdate"  />
                        <input id="btnSrch1" type="button" value="查询" onclick="btnSrch1Click()" />
                        <iframe class="iFrame" id="fr1" frameborder="no" style="height: 500px; width: 960px"></iframe>
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
