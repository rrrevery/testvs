<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXBQDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXBQDY.GTPT_WXBQDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_BQDY%>';
    </script>


     <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXBQDY.js"></script>

</head>
<body>
   
     <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">标签名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_BQMC" />
               <input type="hidden" id="TB_TAGID" />

            </div>
        </div>
       
    </div>
       <div class="bfrow">      
         <div class="bffld" style="width: 900px;">
            <font color='red'> 注:点完“保存”，“修改”，“删除”后请点击发布</font>
        </div>
    </div>
    <div class="bfrow bfrow_table" id="a">
        <table id="list" style="border: thin"></table>
    </div>

    <%=V_InputBodyEnd %>
  
</body>
</html>
