<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_JJR_RQ_DEF.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.JJR_RQ_DEF.CRMREPORT_JJR_RQ_DEF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_BillInput.js"></script>
    <script src="../../CrmLib/CrmLib_GetHYXX.js"></script>
    <script>
        vPageMsgID = <%=CM_CRMREPORT_JJR_RQ_DEF%>;
    </script>
    <script src="CRMREPORT_JJR_RQ_DEF.js"></script>

</head>
<body>
   
    <div id="panelwrap">
        <div class="center_content">
            <div class="right_wrap">
                <div id="right_content">
                    <h2>节假日日期定义</h2>
                    <div style="width: 800px;" id="btn-toolbar"></div>
                    <div></div>
                    <div id="tab1" class="tabcontent">
                        <div id="MainPanel" class="form">

                            <div class="form_row" id="A">
                                <div class="div1" id="jlbh">
                                </div>
                                <div class="div2">
                                   
                                </div>

                            </div>
                             
                            <div class="form_row">
                                <div class="div1">
                                    <div class="dv_sub_left">节假日名称 </div>
                                    <div class="dv_sub_right">
                                         <select id="S_JJRMC" onchange="change()">
                                            <option></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="div2">
                                    <div class="dv_sub_left">年度</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_ND" type="text"/>
                                    </div>
                                </div>

                            </div>

                              <div class="form_row">
                                <div class="div1">
                                    <div class="dv_sub_left">开始日期</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_DJSJ2\')}'} )" />
                                    </div>
                                </div>
                                <div class="div2">
                                    <div class="dv_sub_left">结束日期</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" />
                                    </div>
                                </div>
                            </div>



                            <div style="padding:0px;width:800px;">
                   <div id="div1" style="float:left;width:400px;">
                <div id="tab_1" class="tabcontent" style="border:0px;" >
                    <table id="list"></table>
                    <div id="pager"></div>
                </div>
                       </div>
                  <div id="div2" style="float:left;width:400px;">
                <div id="tab_2" class="tabcontent" style="border:0px;">
                    <table id="list2"></table>
                    <div id="pager2"></div>
                </div>
                       </div>
               </div>

                        </div>
                        <div id="status-bar"></div>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>


</body>
</html>
