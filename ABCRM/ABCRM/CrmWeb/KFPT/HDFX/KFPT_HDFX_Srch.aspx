<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_HDFX_Srch.aspx.cs" Inherits="BF.CrmWeb.KFPT.HDFX.KFPT_HDFX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script src="KFPT_HDFX_Srch.js"></script>
    <script>
        vPageMsgID = '<%=CM_KFPT_HDFX%>';
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>

</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">活动名称</div>
            <div class="bffld_right">
                <select id="DDL_HDID">
                    <option></option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始时间</div>
            <div class="bffld_right twodate">
                <input id="TB_KSSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_KSSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束时间</div>
            <div class="bffld_right twodate">
                <input id="TB_JSSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_JSSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>


    <%=V_SearchBodyEnd %>
</body>
</html>
