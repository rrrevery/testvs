<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_HYSJYJ.aspx.cs" Inherits="BF.CrmWeb.KFPT.HYSJ.KFPT_HYSJYJ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_BillList.js"></script>
    <script>
        vPageMsgID = '<%=CM_KFPT_HYSJYJ%>';
    </script>
    <script src="KFPT_HYSJYJ.js"></script>
    
</head>
<body>
    <div id="panelwrap">
        <div class="center_content">
            <div id="right_wrap">
                <div id="right_content">
                    <h2>会员升级预警</h2>
                    <div id="btn-toolbar"></div>
<%--                    <div id="bffld" class="tabcontent">
                    <div class="bfrow">
                       <div class="bffld">
                           <div id="jlbh"></div>
                        </div>                                                           
                     </div>
                    </div>--%>
                    <ul id="tabsmenu" class="tabsmenu">
                        <li class="active"><a id="dzlx0">升级会员预警</a></li>
                        <li><a id="dzlx1">积分升级会员预警</a></li>
                        <li><a id="dzlx2">异常卡预警</a></li>
                       
                    </ul>    
                        <div id="tab1" class="tabcontent">                           
                        <div class="form">
                          <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">会员卡类型</div>
                                    <div class="bffld_right">
                                        <input id="TB_HYKNAME" type="text" tabindex="2" />
                                    </div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">会员卡号</div>
                                    <div class="bffld_right">
                                        <input id="TB_HYK_NO" type="text" tabindex="3" />
                                    </div>
                                </div>
                            </div>
                             <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">升级日期段</div>
                                    <div class="bffld_right">
                                        <input id="TB_KSRQ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_KSRQ2\')}'} )" />
                                    </div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">--------</div>
                                    <div class="bffld_right">
                                        <input id="TB_KSRQ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" />
                                    </div>
                                </div>
                            </div>
                               <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">手机号</div>
                                    <div class="bffld_right">
                                        <input id="TB_SJHM" type="text" tabindex="2" />
                                    </div>
                                </div>                             
                                </div>
                            </div>
                        </div>               
                    <div class="clear"></div>
                    <div id="tab2" class="tabcontent">
                        <div id="tablelist0">
                            <table id="list0"></table>
                            <div id="pager0"></div>
                        </div>
                        <div id="tablelist1">
                            <table id="list1"></table>
                            <div id="pager1"></div>
                        </div>
                        <div id="tablelist2">
                            <table id="list2"></table>
                            <div id="pager2"></div>
                        </div>
                     
                        
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
</body>
</html>
