<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_QMQGZDYD_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.QMQGZDYD.GTPT_QMQGZDYD_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_GTPT_QMQGZDYD%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_QMQGZDYD_LR %>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_QMQGZDYD_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_QMQGZDYD_QD%>');
        bCanStart = CheckMenuPermit(iDJR, '<%=CM_GTPT_QMQGZDYD_ZZ%>');
        bCanStop = CheckMenuPermit(iDJR, '<%=CM_GTPT_QMQGZDYD_CX%>');
    </script>
          <script src="../../CrmLib/CrmLib_GetData.js"></script>
     <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="GTPT_QMQGZDYD_Srch.js"></script>
  
</head>
<body>
      <%=V_SearchBodyBegin %>

                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">记录编号</div>
                                    <div class="bffld_right">
                                        <input id="TB_JLBH" type="text" tabindex="1" />
                                    </div>
                                </div>
                                <div class="bffld">
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">开始日期</div>
                                    <div class="bffld_right">
                                        <input id="TB_KSRQ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_DJSJ2\')}'} )" />
                                    </div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">----------</div>
                                    <div class="bffld_right">
                                        <input id="TB_KSRQ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" />
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">结束日期</div>
                                    <div class="bffld_right">
                                        <input id="TB_JSRQ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_DJSJ2\')}'} )" />
                                    </div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">----------</div>
                                    <div class="bffld_right">
                                        <input id="TB_JSRQ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" />
                                    </div>
                                </div>
                            </div>

                             <div class="bffld">
        <div class="bffld_left">单据状态</div>
        <div class="bffld_right">
            <input class="magic-checkbox" type="checkbox" name="CB_STATUS" id="C_WSH" value="0" />
            <label for="C_WSH">未审核</label>
            <input class="magic-checkbox" type="checkbox" name="CB_STATUS" id="C_YSH" value="1" />
            <label for="C_YSH">已审核</label>
            <input class="magic-checkbox" type="checkbox" name="CB_STATUS" id="C_YQD" value="2" />
            <label for="C_YQD">已启动</label>
            <input class="magic-checkbox" type="checkbox" name="CB_STATUS" id="C_YZZ" value="-1" />
            <label for="C_YZZ">已终止</label>
        </div>
        <input type="hidden" id="HF_STATUS" />
    </div>
     <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记人</div>
            <div class="bffld_right">
                <input id="TB_DJRMC" type="text" />
                <input id="HF_DJR" type="hidden" />
                <input id="zHF_DJR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">登记时间</div>
            <div class="bffld_right">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">执行人</div>
            <div class="bffld_right">
                <input id="TB_ZXRMC" type="text" />
                <input id="HF_ZXR" type="hidden" />
                <input id="zHF_ZXR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">执行日期</div>
            <div class="bffld_right">
                <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
                          


        <%=V_SearchBodyEnd %>

                           
</body>
</html>
