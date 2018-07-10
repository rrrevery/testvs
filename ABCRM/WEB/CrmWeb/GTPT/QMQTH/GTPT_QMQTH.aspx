<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_QMQTH.aspx.cs" Inherits="BF.CrmWeb.GTPT.QMQTH.GTPT_QMQTH" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
      <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script>
        vPageMsgID = '<%=CM_GTPT_QMQTH%>';
    </script>

    <script src="GTPT_QMQTH.js"></script>
</head>
<body>
   
     <%=V_InputBodyBegin %>
    <div class="bfrow" style="display: none;">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">订单号</div>
            <div class="bffld_right">
                <input type="text" id="TB_CODE" />
            </div>
        </div>
      
    </div>
     <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">金额</div>
            <div class="bffld_right">
                <input type="text" id="TB_JE" />
            </div>
        </div>
      
    </div>  
    
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input type="text" id="TB_HYK_NO" />
            </div>
        </div>
      
    </div>

      <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">返回信息</div>
            <div class="bffld_right">
          <label id="LB_ERRORM" style="text-align: left;" runat="server" />

            </div>
        </div>
      
    </div>
    <%=V_InputBodyEnd %>


</body>
</html>
