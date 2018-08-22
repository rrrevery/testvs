<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_HQBQXFS.aspx.cs" Inherits="BF.CrmWeb.GTPT.HQBQXFS.GTPT_HQBQXFS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_HQBQXFS%>';
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

    <script src="GTPT_HQBQXFS.js"></script>
</head>
<body>
  
      <%=V_InputBodyBegin %>
    <div class="bfrow" id="hh">
        <div class="bffld">
            <div id="jlbh" ></div>
        </div>
    </div>
   
     <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">选择标签</div>
            <div class="bffld_right">
               <input type="text" id="TB_BQMC" />
               <input type="hidden" id="TB_TAGID" />
               <input type="hidden" id="zTB_TAGID" />
            </div>
        </div>
   
             
    </div>
     <div class="bfrow">
        <div class="bffld" style="width: 900px;">
            <font color='red'> 注:获取用户基本信息。最多支持一次拉取100条,查找更多用户，请点击“下一次”按钮</font>
        </div>
         
    </div>
      <div class="bfrow">      
         <div class="bffld" style="width: 900px;">
            <font color='red'> 注:备注直接在表格里"备注"列修改，修改完成后，点击”确认修改备注”按钮即可</font>
        </div>
    </div>
      <div class="bfrow">      
         <div class="bffld" style="width: 900px;">
            <font color='red'> 注:按"昵称"查询,只是查询表格里已经获取到的用户</font>
        </div>
    </div>
      <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left"></div>
            <div class="bffld_right">
        <button id="XZBQHQFS" type='button' class="item_addtoolbar">获取标签粉丝</button>
         <button id="NEXTBQYHXX" type='button' class="item_addtoolbar">选择标签的下一次</button>

            </div>
        </div>
     <div class="bffld">
            <div class="bffld_left"></div>
            <div class="bffld_right">
        <button id="HQQBFS" type='button' class="item_addtoolbar">获取所有粉丝</button>
        <button id="NEXTYHXX" type='button' class="item_addtoolbar">所有粉丝的下一次</button>

           </div>
        </div>
             
    </div>
  
       <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left"></div>
            <div class="bffld_right">
              <button id="PLDBQ" type='button' class="item_addtoolbar">批量打标签</button>
              <button id="PLQXBQ" type='button' class="item_addtoolbar">批量取消标签</button>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left"></div>
            <div class="bffld_right">
              <button id="XGBZ" type='button' class="item_addtoolbar">确认修改备注</button>
              <button id="ADDHMD" type='button' class="item_addtoolbar">加入黑名单</button>
            </div>
        </div>
    </div>
   
 <%--  <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">昵称</div>
            <div class="bffld_right">
               <input type="text" id="TB_NC" />            
            </div>
        </div>
         <button id="CXNC" type='button' >查询</button>

             
    </div>--%>


    <div class="bfrow bfrow_table" id="a">
        <table id="list" style="border: thin"></table>
    
    </div>
    <%=V_InputBodyEnd %>



</body>
</html>
