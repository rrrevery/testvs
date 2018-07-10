<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXYYFWDY_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXYYFWDY.GTPT_WXYYFWDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXYYFWDY%>';
    </script>
      <script src="../../CrmLib/CrmLib_BillWX.js"></script>
     <script src="../../CrmLib/CrmLib_GetData.js"></script>

    <script src="GTPT_WXYYFWDY_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin%>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店</div>
            <div class="bffld_right">
                <input type="text" id="TB_MDMC" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">序号</div>
            <div class="bffld_right">
                <input id="TB_INX" type="text" tabindex="1" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">预约主题</div>
            <div class="bffld_right">
                <input id="TB_YYZT" type="text" class="form_input" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd%>
</body>
</html>
