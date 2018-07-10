<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKJFRBB_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKJFRBB.HYKGL_HYKJFRBB_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>

    <script>vPageMsgID = '<%=CM_HYKGL_SrchHYKJFRBB%>';</script>
    <script src="HYKGL_HYKJFRBB_Srch.js"></script>

</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="4" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">日期</div>
            <div class="bffld_right">
                <input id="TB_RQ" type="text" class="Wdate" onfocus="WdatePicker()" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_left" hidden="hidden">报表内容</div>
        <div class="bffld_right">
            <input id="RD_BBNR" type="radio" tabindex="4" hidden="hidden" />
        </div>
        <div class="bffld">
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
