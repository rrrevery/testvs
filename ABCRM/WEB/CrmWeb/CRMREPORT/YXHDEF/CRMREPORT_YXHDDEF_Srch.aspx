<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_YXHDDEF_Srch.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.YXHDEF.CRMREPORT_YXHDDEF_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_BillList.js"></script>
    <script>
      
       
                vPageMsgID = <%=CM_CRMREPORT_YXHHDEF%>;
             
          
          
        
    </script>
    <script src="CRMREPORT_YXHDDEF_Srch.js"></script>

    <style type="text/css">
        input[type='text'] {
            text-align: left;
        }
    </style>
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
                    <h2>营销活动定义</h2>
                    <div style="width: 800px;" id="btn-toolbar"></div>
                    <ul id="tabsmenu" class="tabsmenu">
                        <li class="active"><a href="#tab1">查询条件</a></li>
                        <li><a href="#tab2">查询结果</a></li>
                    </ul>

                    <div id="tab1" class="tabcontent">
                        <div id="MainPanel" class="form">
                            <div class="form_row">
                                <div class="div1">
                                    <div class="dv_sub_left">记录编号</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_JLBH" type="text" tabindex="1" />
                                    </div>
                                </div>
                                <div class="div2">
                                    <div class="dv_sub_left">年度</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_ND" type="text" tabindex="2" />
                                    </div>
                                </div>
                            </div>
                          

                            
                            <div class="form_row">
                                <div class="div1">
                                    <div class="dv_sub_left">开始日期</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_DJSJ2\')}'} )" tabindex="8" />
                                    </div>
                                </div>
                                <div class="div2">
                                    <div class="dv_sub_left">结束日期</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" tabindex="9" />
                                    </div>
                                </div>
                            </div>



                           
                            <div class="clear"></div>
                        </div>
                        <div class="toogle_wrap">
                            <div class="trigger"><a href="#">更多条件</a></div>
                            <div class="toggle_container">
                                <div class="form">
                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                    <div class="form_row">
                                        <input type="button" class="form_button" value="+" onclick="ZSel_MoreCondition_Add();" />
                                        <%--  <input type="submit" class="form_submit" value="查询" onclick="brandSearch();" />--%>
                                    </div>
                                    <div class="clear"></div>
                                </div>
                            </div>
                        </div>
                    </div>
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
