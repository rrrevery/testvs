<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_LPTPSC_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.LPTPSC.GTPT_LPTPSC_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_GTPT_LPTPSC%>';
    </script>
    <script src="GTPT_LPTPSC_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">礼品</div>
            <div class="bffld_right">

               <input id="TB_LPMC" type="text" />
                <input id="HF_LPID" type="hidden" />
                <input id="zHF_LPID" type="hidden" />
            </div>
        </div>
      
    </div>
   
  
    <%=V_SearchBodyEnd %> 


</body>
</html>
