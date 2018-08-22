<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_HQHMD.aspx.cs" Inherits="BF.CrmWeb.GTPT.HQHMD.GTPT_HQHMD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_HQHMD%>';
    </script>

     <style>
         /*.datagrid-header-rownumber,*/
         .datagrid-cell-rownumber {
             margin: 0;
             padding: 0 4px;
             white-space: nowrap;
             word-wrap: normal;
             overflow: hidden;
             height: 130px;
             line-height: 130px;
             font-size: 12px;
         }

         .datagrid-cell-c1-itemid {
            height: 130px;
         }


    </style>
     <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_HQHMD.js"></script>

</head>
<body>
   

      <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="hh">
            <div id="jlbh"></div>
        </div>
    </div>
    
          <div class="bfrow">

        <div class="bffld">
   
              <button id="SHOWHMD" type='button' class="item_addtoolbar">获取黑名单列表</button>
              <button id="DELHMD" type='button' class="item_addtoolbar">移出黑名单</button>
              <button id="XGBZ" type='button' class="item_addtoolbar">确认修改备注</button>

        </div>
    </div>

   



    <div class="bfrow bfrow_table" id="a">
        <table id="list" style="border: thin"></table>
    </div>

    <%=V_InputBodyEnd %>



</body>
</html>
