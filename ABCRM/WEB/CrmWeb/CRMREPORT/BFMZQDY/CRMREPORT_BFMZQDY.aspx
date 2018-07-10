<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_BFMZQDY.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.BFMZQDY.CRMREPORT_BFMZQDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_BillInput.js"></script>
    <script>
        vPageMsgID = <%=CM_CRMREPORT_RFMQZ%>;
    </script>
    <a href="../../CrmLib/CrmLib.ashx">../../CrmLib/CrmLib.ashx</a>
    <script src="FillSHMC.js"></script>

    <script src="CRMREPORT_BFMZQDY.js"></script>
    <style>
        .ui-jqgrid-btable .ui-state-highlight { background: yellow; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="panelwrap">
            <div class="center_content">
                <div id="right_wrap">
                    <div id="right_content">
                        <h2>BFM权重定义</h2>
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
                                               <input id="HF_ND" type="hidden" />
                                        </div>
                                    </div>
                                    <div class="div2">
                                        
                                    </div>
                                </div>
                                <div class="form_row">
                                    <div class="div1">
                                         <div class="dv_sub_left">商户名称</div>
                                        <div class="dv_sub_right">
                                         <select id="TB_SHMC">
                                                <option></option>
                                            </select>
                                      <input type="hidden" id="HF_SHDM" />
                                        <input type="hidden" id="HF_SHDM1" />
                                        </div>
                                    </div>
                                    <div class="div2">
                                        <div class="dv_sub_left">R权重</div>
                                        <div class="dv_sub_right">
                                            <input id="TB_R_QZ" type="text" />
                                        </div>
                                    </div>
                                </div>
                                  <div class="form_row">
                                    <div class="div1">
                                         <div class="dv_sub_left">F权重</div>
                                        <div class="dv_sub_right">
                                            <input id="TB_F_QZ" type="text" />
                                        </div>
                                    </div>
                                    <div class="div2">
                                        <div class="dv_sub_left">M权重</div>
                                        <div class="dv_sub_right">
                                            <input id="TB_M_QZ" type="text" />
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
