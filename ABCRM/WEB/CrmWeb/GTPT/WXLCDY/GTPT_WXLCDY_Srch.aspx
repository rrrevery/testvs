<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXLCDY_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXLCDY.GTPT_WXLCDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <%--<script src="../../CrmLib/CrmLib_FillWXGroup.js"></script>--%>
    <script src="GTPT_WXLCDY_Srch.js"></script>
    <script>vPageMsgID = '<%=CM_GTPT_WXLCDY%>';</script>

</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input id="TB_WXMDMC" type="text" />
                <input id="HF_WXMDID" type="hidden" />
                <input id="zHF_WXMDID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">楼层名称</div>
            <div class="bffld_right">
                <input id="TB_NAME" type="text" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
