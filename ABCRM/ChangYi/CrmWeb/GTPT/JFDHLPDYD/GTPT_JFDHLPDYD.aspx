<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_JFDHLPDYD.aspx.cs" Inherits="BF.CrmWeb.GTPT.JFDHLPDYD.GTPT_JFDHLPDYD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
 <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script>
        vPageMsgID = '<%=CM_GTPT_JFDHLPDYD%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_JFDHLPDYD_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_JFDHLPDYD_SH%>');
        bCanStart = CheckMenuPermit(iDJR, '<%=CM_GTPT_JFDHLPDYD_QD%>');
        bCanStop = CheckMenuPermit(iDJR,'<%=CM_GTPT_JFDHLPDYD_ZZ%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_JFDHLPDYD_CX%>');
    </script>
    <script src="GTPT_JFDHLPDYD.js"></script>


</head>
<body>
        <%=V_InputBodyBegin %>

                            <div class="bfrow">
                                <div class="bffld">
                                    <div id="jlbh"></div>
                                </div>
                                <div class="bffld">
                                </div>
                            </div>
                            <div class ="bfrow">
                                <div class="bffld"> 
                                    <div class="bffld_left">选取规则</div>
                                    <div class="bffld_right">
                                        <input id="TB_GZMC" type="text" />
                                        <input type="hidden" id="HF_GZID" />
                                        <input type="hidden" id="zHF_GZID" />
                                    </div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">单据类型</div>
                                    <div class="bffld_right">
                                        <select id="DDL_LX">
                                            <option></option>
                                            <option value="1">常驻规则</option>
                                            <option value="2">活动规则</option>
                                        </select>
                                    </div>
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
                                    <div class="bffld_left">扣除门店</div>
                                    <div class="bffld_right">
                                        <input id="TB_WXMDMC" type="text" />
                                        <input id="HF_WXMDID" type="hidden" />
                                        <input id="zHF_WXMDID" type="hidden" />
                                    </div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">领奖有效期</div>
                                    <div class="bffld_right">
                                        <input id="TB_LJYXQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                                    </div>
                                </div>
                            </div>
                            <fieldset style="width: 900px; float: left;">
                                <legend>处理方式</legend>
                                <div class="bfrow"  style="width:900px;">
                                    <div class="bffld">
                                    <div class="bffld_left">处理方式</div>
                                    <div class="bffld_right" style="width: 900px">
                                        <input type="radio" value="0" name="CLFS" checked="checked" />单门店减积分
                                        <input type="radio" value="2" name="CLFS" />同商户门店分摊积分
                                        <input type="radio" value="3" name="CLFS" />全部门店分摊积分
                                        <input type="radio" value="4" name="CLFS" />管辖商户分摊
                                    </div>
                                </div>
                                </div>
                            </fieldset>
                     
                                   <div class="bfrow">                              
                                <div class="bffld">
                                    <div class="bffld_left">摘要</div>
                                    <div class="bffld_right">
                                        <input id="TB_BZ" type="text" />
                                    </div>
                                </div>
                            </div>
     
       <%=V_InputBodyEnd %>
         
</body>
</html>
