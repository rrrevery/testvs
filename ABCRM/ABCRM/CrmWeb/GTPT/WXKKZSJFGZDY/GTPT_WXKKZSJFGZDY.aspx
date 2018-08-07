<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXKKZSJFGZDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXKKZSJFGZDY.GTPT_WXKKZSJFGZDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vGZLX = GetUrlParam("gzlx");
        if (vGZLX == "1") {
            vPageMsgID = '<%=CM_GTPT_WXKKSFGZ%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXKKSFGZ_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXKKSFGZ_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXKKSFGZ_CX%>');
            bCanStart = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXKKSFGZ_QD%>');
            bCanStop = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXKKSFGZ_ZZ%>');
            vCaption = "微信开卡赠送积分规则定义";
        }
        else {
            vPageMsgID = '<%=CM_GTPT_WXBKSFGZ%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXBKSFGZ_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXBKSFGZ_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXBKSFGZ_CX%>');
            bCanStart = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXBKSFGZ_QD%>');
            bCanStop = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXBKSFGZ_ZZ%>');
            vCaption = "微信绑卡赠送积分规则定义";
        }
    </script>

     <script src="../../CrmLib/CrmLib_BillWX.js"></script>
     <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXKKZSJFGZDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %> 
                            <div class="bfrow">
                                <div class="bffld" id="jlbh">
                                </div>
                          
                            </div>

                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">开始日期</div>
                                    <div class="bffld_right">
                                        <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                                    </div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">结束日期</div>
                                    <div class="bffld_right">
                                        <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">限制数量</div>
                                    <div class="bffld_right">
                                        <input id="TB_XZSL" type="text" />
                                    </div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">领取有效期</div>
                                    <div class="bffld_right">
                                        <input id="TB_YXQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">                              
                                <div class="bffld">
                                    <div class="bffld_left">微信摘要</div>
                                    <div class="bffld_right">
                                        <input id="TB_WXZY" type="text" />
                                    </div>
                                </div>
                            </div>


         
     <div>
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">列表</span>
        <button id="AddKLX" type='button' class="item_addtoolbar">添加</button>
        <button id="DelKLX" type='button' class="item_deltoolbar">删除</button>
    </div>

     <div style="clear: both;"></div>

    <div id="tb_KLX" class="item_toolbar">
        <span style="float: left">列表</span>
        <button id="AddLB" type='button' class="item_addtoolbar">添加</button>
        <button id="DelLB" type='button' class="item_deltoolbar">删除</button>
    </div>


   <div class="bfrow bfrow_table">
        <table id="list_KLX" style="border: thin"></table>
    </div>  
                         
  
   
        <%=V_InputBodyEnd %>

</body>
</html>
