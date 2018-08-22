<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_KHDALR_Srch.aspx.cs" Inherits="BF.CrmWeb.MZKGL.KHDALR.MZKGL_KHDALR_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_MZKGL_DKHQYKHDA%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_MZKGL_DKHQYKHDA_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_MZKGL_DKHQYKHDA_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_MZKGL_DKHQYKHDA_CX%>');
    </script>
    <script src="MZKGL_KHDALR_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" />
            </div>
        </div>
        <div class="bffld">
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
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记时间</div>
            <div class="bffld_right twodate">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_DJSJ2\')}'} )" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker()" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">审核日期</div>
            <div class="bffld_right twodate">
                <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_ZXRQ2\')}'} )" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker()" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
