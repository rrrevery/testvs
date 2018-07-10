<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_RFMJBDY.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.RFMJBDY.CRMREPORT_RFMJBDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_BillInput.js"></script>
    <script>
        vPageMsgID = <%=CM_CRMREPORT_RFMJB%>;
    </script>
    <script src="CRMREPORT_RFMJBDY.js"></script>
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
                        <h2>RFM级别定义</h2>
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
                                        <div class="dv_sub_left">级别</div>
                                        <div class="dv_sub_right">
                                            <input id="TB_JB" type="text" maxlength="4" />
                                            <input id="HF_JB" type="hidden" />
                                        </div>
                                    </div>
                                    <div class="div2">
                                        <div class="dv_sub_left">比例</div>
                                        <div class="dv_sub_right">
                                            <input id="TB_BL" type="text" />
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
