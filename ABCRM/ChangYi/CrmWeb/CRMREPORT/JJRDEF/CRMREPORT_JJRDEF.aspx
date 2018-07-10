<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_JJRDEF.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.JJRDEF.CRMREPORT_JJRDEF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

      <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_BillInput.js"></script>
    <script>
        vPageMsgID = <%=CM_CRMREPORT_JJR_DEF%>;
    </script>
    <script src="CRMREPORT_JJRDEF.js"></script>

</head>
<body>
   
       <div id="panelwrap">
        <div class="center_content">
            <div class="right_wrap">
                <div id="right_content">
                    <h2>节假日定义</h2>
                    <div style="width: 800px;" id="btn-toolbar"></div>
                    <div></div>
                    <div id="tab1" class="tabcontent">
                        <div id="MainPanel" class="form">

                            <div class="form_row" id="A">
                                <div class="div1" id="jlbh">
                                </div>
                                <div class="div2">
                                   <div class="dv_sub_left">节假日名称 </div>
                                    <div class="dv_sub_right">
                                        <input id="TB_MC" type="text"/>
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
                <%--  <div id="div2" style="float:left;width:400px;">
                <div id="tab_2" class="tabcontent" style="border:0px;">
                    <table id="list2"></table>
                    <div id="pager2"></div>
                </div>
                       </div>--%>
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
