<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_BMKLXXFHZ_Srch.aspx.cs" Inherits="BF.CrmWeb.HYXF.BMKLXXFHZ.HYXF_BMKLXXFHZ_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>vPageMsgID = '<%=CM_HYXF_BMKLXXF%>';</script>
    <script src="HYXF_BMKLXXFHZ_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input type="text" id="TB_HYKNAME" />
                <input type="hidden" id="HF_HYKTYPE" />
                <input type="hidden" id="zHF_HYKTYPE" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">商户名称</div>
            <div class="bffld_right">
                <input id="TB_SHMC" type="text" />
                <input id="HF_SHDM" type="hidden" />
                <input id="zHF_SHDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">限定部门</div>
            <div class="bffld_right">
                <input id="TB_SHBMMC" type="text" />
                <input id="HF_SHBMDM" type="hidden" />
                <input id="zHF_SHBMDM" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">统计日期</div>
            <div class="bffld_right">
                <input id="TB_XFRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_XFRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
