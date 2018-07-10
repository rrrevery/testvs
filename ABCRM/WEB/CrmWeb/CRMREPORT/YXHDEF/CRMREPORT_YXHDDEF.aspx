<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_YXHDDEF.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.YXHDEF.CRMREPORT_YXHDDEF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_BillInput.js"></script>
    <script src="../../CrmLib/CrmLib_GetHYXX.js"></script>
    <script src="../../CrmLib/CrmLib_GetKCKXX.js"></script>
    <script>
     
           
                vPageMsgID = <%=CM_CRMREPORT_YXHHDEF%>;
              
    </script>
    <script src="CRMREPORT_YXHDDEF.js"></script>

</head>
<body>
   

      <div id="panelwrap">
        <div class="center_content">
            <div id="right_wrap">
                <div id="right_content">
                    <h2></h2>
                    <div style="width: 800px;" id="btn-toolbar"></div>
                    <div id="tab1" class="tabcontent">
                        <div id="MainPanel" class="form">
                            <div class="form_row">
                                <div class="div1" id="jlbh">
                                </div>
                                <div class="div2">
                                    <div class="dv_sub_left">年度</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_ND" type="text" />
                                       
                                    </div>
                                </div>
                            </div>
                         


                              <div class="form_row">

                            <div class="div1">
                                <div class="dv_sub_left">开始时间</div>
                                <div class="dv_sub_right">
                                    <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker( )" />
                                </div>
                            </div>
                            <div class="div2">
                                <div class="dv_sub_left">结束时间</div>
                                <div class="dv_sub_right">
                                    <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker( )" />
                                </div>
                            </div>
                        </div>

                         
                            <div class="form_row">
                                <div class="div1" style="width: 800px;">
                                    <div class="dv_sub_left">活动主题</div>
                                    <input id="TB_HDZT" type="text" class="form_input" />
                                </div>
                            </div>
                             <div class="form_row">
                                <div class="div1" style="width: 800px;">
                                    <div class="dv_sub_left">活动内容</div>
                                    <input id="TB_HDNR" type="text" class="form_input" />
                                </div>
                            </div>
                            <div id="status-bar" style="width: 800px;">
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
   
</body>
</html>
