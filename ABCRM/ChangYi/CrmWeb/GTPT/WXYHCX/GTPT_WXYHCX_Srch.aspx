<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXYHCX_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXYHCX.GTPT_WXYHCX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXYHCX%>';
    </script>
    <script src="GTPT_WXYHCX_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">绑卡时间</div>
            <div class="bffld_right">
                <input id="TB_GZSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">------</div>
            <div class="bffld_right">
                <input id="TB_GZSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
<%--    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开卡时间</div>
            <div class="bffld_right">
                <input id="TB_KKSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">------</div>
            <div class="bffld_right">
                <input id="TB_KKSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">取消时间</div>
            <div class="bffld_right">
                <input id="TB_QXSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">------</div>
            <div class="bffld_right">
                <input id="TB_QXSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>--%>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO1" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">------</div>
            <div class="bffld_right">
                <input id="TB_HYKNO2" type="text" />
            </div>
        </div>
    </div>
     <%=V_SearchBodyEnd %>
</body>
</html>
