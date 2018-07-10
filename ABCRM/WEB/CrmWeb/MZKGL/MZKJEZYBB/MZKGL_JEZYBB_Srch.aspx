<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_JEZYBB_Srch.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKJEZYBB.MZKGL_JEZYBB_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>vPageMsgID = '<%=CM_MZKGL_JEZYBB%>';</script>
    <script src="MZKGL_JEZYBB_Srch.js"></script>
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
            <div class="bffld_left">年月</div>
            <div class="bffld_right">
                <input id="TB_NY" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt:'yyyyMM'})"/>
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
