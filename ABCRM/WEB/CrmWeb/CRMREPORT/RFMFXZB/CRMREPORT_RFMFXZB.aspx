<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_RFMFXZB.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.RFMFXZB.CRMREPORT_RFMFXZB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_BillInput.js"></script>
    <script>
        var vLX = GetUrlParam("LX") || 0;
        var vCaption;
        if (vLX == "1") {
            vPageMsgID = '<%=CM_CRMREPORT_RFMFXZB %> ';
            vCaption = "商户RFM分析指标";
        }
        if (vLX == "2") {
            vPageMsgID = '<%=CM_CRMREPORT_RFMFXZB_MD %> ';
            vCaption = "门店RFM分析指标";
        }
    </script>
    <script src="CRMREPORT_RFMFXZB.js"></script>
    <script src="../../CrmLib/CrmLib_GetHYXX.js"></script>
    <style>
        .ui-jqgrid-btable .ui-state-highlight {
            background: yellow;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="panelwrap">
            <div class="center_content">
                <div id="right_wrap">
                    <div id="right_content">
                        <h2>商户/门店RFM分析指标定义</h2>
                        <div style="width: 800px;" id="btn-toolbar"></div>
                        <div id="tab1" class="tabcontent">
                            <div id="MainPanel" class="form">

                                <div class="form_row">
                                    <div class="div1" id="jlbh">
                                    </div>
                                    <div class="div2">
                                    </div>
                                </div>
                                <div class="form_row">
                                    <div class="div1">
                                        <div class="dv_sub_left">年度</div>
                                        <div class="dv_sub_right">
                                            <input id="TB_ND" type="text" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form_row">
                                    <div class="div1">
                                        <div class="dv_sub_left">级别</div>
                                        <div class="dv_sub_right">
                                            <input id="TB_JB" type="text" />
                                        </div>
                                    </div>
                                    <div class="div2">
                                        <div class="dv_sub_left" id="DV_DMLX">商户</div>
                                        <div class="dv_sub_right">
                                            <select id="S_SH">
                                                <option></option>
                                            </select>
                                        </div>
                                        <%-- <div class="dv_sub_left">商户代码</div>
                                        <div class="dv_sub_right">
                                            <input id="TB_SHDM" type="text" maxlength="4" />
                                            <input id="HF_SHDM" type="hidden" />
                                        </div>--%>
                                    </div>
                                </div>
                                <div class="form_row">
                                    <div class="div1">
                                        <div class="dv_sub_left">消费金额</div>
                                        <div class="dv_sub_right">
                                            <input id="TB_XFJE_BEGIN" type="text" />
                                        </div>
                                    </div>
                                    <div class="div2">
                                        <div class="dv_sub_left">----------</div>
                                        <div class="dv_sub_right">
                                            <input id="TB_XFJE_END" type="text" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form_row">
                                    <div class="div1">
                                        <div class="dv_sub_left">来店次数</div>
                                        <div class="dv_sub_right">
                                            <input id="TB_LDCS_BEGIN" type="text" />
                                        </div>
                                    </div>
                                    <div class="div2">
                                        <div class="dv_sub_left">----------</div>
                                        <div class="dv_sub_right">
                                            <input id="TB_LDCS_END" type="text" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form_row">
                                    <div class="div1">
                                        <div class="dv_sub_left">来店频率</div>
                                        <div class="dv_sub_right">
                                            <input id="TB_LDPL_BEGIN" type="text" />
                                        </div>
                                    </div>
                                    <div class="div2">
                                        <div class="dv_sub_left">----------</div>
                                        <div class="dv_sub_right">
                                            <input id="TB_LDPL_END" type="text" />
                                        </div>
                                    </div>
                                </div>
                                <div id="status-bar" style="width: 800px;">
                                </div>
                                <div class="clear"></div>
                                <table id="list"></table>
                                <div id="pager"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </form>
</body>
</html>
