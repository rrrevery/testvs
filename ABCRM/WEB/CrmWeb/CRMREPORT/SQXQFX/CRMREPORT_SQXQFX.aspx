<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_SQXQFX.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.SQXQFX.CRMREPORT_SQXQFX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
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
    <script src="../../CrmLib/CrmLib_BillList.js"></script>
    <script>vPageMsgID = '<%=CM_CRMREPORT_SQXQFX%>';</script>
    <script src="CRMREPORT_SQXQFX.js"></script>
</head>
<body>
    <div id="panelwrap">
        <div class="center_content">
            <div id="right_wrap">
                <div id="right_content">
                    <h2>商圈小区分析</h2>
                    <div id="btn-toolbar"></div>
                    <ul id="tabsmenu" class="tabsmenu">
                        <li class="active"><a href="#tab1">查询条件</a></li>
                        <li><a href="#tab2">查询结果</a></li>
                    </ul>
                    <div id="tab1" class="tabcontent">
                        <div class="form">
                            <div class="form_row">
                                <div class="div1">
                                    <div class="dv_sub_left">门店名称</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_MDMC" type="text" />
                                        <input id="HF_MDID" type="hidden" />
                                        <input id="zHF_MDID" type="hidden" />
                                    </div>
                                </div>
                                <div class="div2">
                                    <div class="dv_sub_left">商圈名称</div>
                                    <div class="dv_sub_right">
                                        <input type="text" id="TB_SQMC" />
                                        <input type="hidden" id="HF_SQID" />
                                        <input type="hidden" id="zHF_SQID" />
                                    </div>
                                </div>

                            </div>
                            <div class="form_row">
                                <div class="div1">
                                    <div class="dv_sub_left">小区名称</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_XQMC" type="text" />
                                        <input id="HF_XQID" type="hidden" />
                                        <input id="zHF_SXQID" type="hidden" />
                                    </div>
                                </div>
                                <div class="div1">
                                    <div class="dv_sub_left">无商圈小区</div>
                                    <div class="dv_sub_right">
                                        <input id="CB_WSQXQ" type="checkbox" />

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="clear"></div>
                        <div class="toogle_wrap">
                            <div class="trigger"><a href="#">更多条件</a></div>
                            <div class="toggle_container">
                                <div class="form_more">
                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                    <div class="form_row">
                                        <input type="button" class="form_button" value="+" onclick="ZSel_MoreCondition_Add();" />
                                    </div>
                                    <div class="clear"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clear"></div>
                    <div id="tab2" class="tabcontent">
                        <h3>
                            <label id="Label2"></label>
                        </h3>
                        <table id="list"></table>
                        <div id="pager"></div>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
</body>
</html>
