<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_HYHDLDPS_Srch.aspx.cs" Inherits="BF.CrmWeb.KFPT.HYHDLDPS.KFPT_HYHDLDPS_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script src="KFPT_HYHDLDPS_Srch.js"></script>
    <script>
        vPageMsgID = '<%=CM_KFPT_HYHDLDPS%>';
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_KFPT_HYHDLDPS_CX%>');
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">是否评述</div>
            <div class="bffld_right">
                <%--style="width: auto"--%>
                <input class="magic-radio" type="radio" name="sfps" id="R_ALL" value="all" checked="checked" />
                <label for="R_ALL">全部</label>
                <input class="magic-radio" type="radio" name="sfps" id="R_YPS" value="1" />
                <label for="R_YPS">已评述</label>
                <input class="magic-radio" type="radio" name="sfps" id="R_WPS" value="2" />
                <label for="R_WPS">未评述</label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">活动名称</div>
            <div class="bffld_right">
                <select id="S_HDID">
                    <option></option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">评述时间</div>
            <div class="bffld_right twodate">
                <input id="TB_PSSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_PSSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
