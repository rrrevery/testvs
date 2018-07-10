<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXJFDHYHQ_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXJFDHYHQ.GTPT_WXJFDHYHQ_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>

      <script src="../../CrmLib/CrmLib_GetData.js"></script>
      <script src="../../CrmLib/CrmLib_BillWX.js"></script>

    <script>
        vPageMsgID = '<%=CM_GTPT_WXJFDHYHQ%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXJFDHYHQ_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXJFDHYHQ_SH%>');
        bCanStart = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXJFDHYHQ_QD%>');
        bCanStop = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXJFDHYHQ_ZZ%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXJFDHYHQ_CX%>');
    </script>
    <script src="GTPT_WXJFDHYHQ_Srch.js"></script>
</head>
<body>
  <%=V_SearchBodyBegin%>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" tabindex="1" />
            </div>
        </div>
         <div class="bffld_l">
            <div class="bffld_left">单据状态</div>
            <div class="bffld_right" style="width: 300px">
                <input type="checkbox" name="CB_STATUS" value="0" class="magic-checkbox" id="C_BC"/>
                <label for="C_BC">已保存</label>
                <input type="checkbox" name="CB_STATUS" value="1" class="magic-checkbox" id="C_SH"/> 
                <label for="C_SH">已审核</label>
                <input type="checkbox" name="CB_STATUS" value="2" class="magic-checkbox" id="C_QD"/>
                <label for="C_QD">已启动</label>
                <input type="checkbox" name="CB_STATUS" value="-1" class="magic-checkbox" id="C_ZZ"/>
                <label for="C_ZZ">已终止</label>
                <input type="hidden" id="HF_STATUS" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right twodate">
                <input id="TB_KSRQ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_KSRQ2\')}'} )" />
                <span class="Wdate_span">至</span>
                <input id="TB_KSRQ2" type="text" class="Wdate" onfocus="WdatePicker()" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right twodate">
                <input id="TB_JSRQ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_JSRQ2\')}'} )" />
                <span class="Wdate_span">至</span>
                <input id="TB_JSRQ2" type="text" class="Wdate" onfocus="WdatePicker()" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记人</div>
            <div class="bffld_right">
                <input id="TB_DJRMC" type="text" />
                <input id="HF_DJR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">审核人</div>
            <div class="bffld_right">
                <input id="TB_ZXRMC" type="text" />
                <input id="HF_ZXR" type="hidden" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
