<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_QMQTH_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.QMQTH.GTPT_QMQTH_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <%=V_Head_Search %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../CrmLib/CrmLib_BillWX.js"></script>

    <script>
        vPageMsgID = '<%=CM_GTPT_JFDHLPDYD%>';
      
    </script>

    <script src="GTPT_QMQTH_Srch.js"></script>
</head>
<body>
  
      <%=V_SearchBodyBegin %>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYK_NO" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>



 
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记人</div>
            <div class="bffld_right">
                <input id="TB_DJRMC" type="text" />
                <input id="HF_DJR" type="hidden" />
                <input id="zHF_DJR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">登记时间</div>
            <div class="bffld_right">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
  

 
  

    <div class="bfrow">
        <div class="bffld" style="width: 800px">
            <div class="bffld_left" style="width: 11%">状态</div>
            <div class="bffld_right" style="width: 89%">
                <input class="magic-radio" type="radio" name="STATUS" id="all" value="all" checked="checked"   />
                <label for='all'>全部</label>
                <input class="magic-radio" type="radio" name="STATUS" id="R_BC" value="0" />
                <label for="R_BC">成功</label>
                <input class="magic-radio" type="radio" name="STATUS" id="R_SH" value="1" />
                <label for="R_SH">失败</label>
                
            </div>
        </div>
    </div>


    <%=V_SearchBodyEnd %>


</body>
</html>
