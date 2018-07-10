<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_DTHYZDBQ.aspx.cs" Inherits="BF.CrmWeb.GTPT.DTHYKDBQ.GTPT_DTHYZDBQ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_HYDBQ%>';
    </script>
     <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>

    <script src="GTPT_HYPLDBQ.js"></script>
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
            <div class="bffld_left">会员组</div>
            <div class="bffld_right">
               <input type="text" id="TB_HYKMC" />
               <input type="hidden" id="HF_HYKID" />
               <input type="hidden" id="HF_HYZID"/>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">选择标签</div>
            <div class="bffld_right">
               <input type="text" id="TB_BQMC" />
               <input type="hidden" id="TB_TAGID" />
               <input type="hidden" id="zTB_TAGID" />
            </div>
        </div>
   
             
    </div>
      
     <%--  <div class="bfrow">
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
              <button id="ADDHMD" type='button' class="item_addtoolbar">加入黑名单</button>
            </div>
        </div>
    </div>--%>
   

 <%-- <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">卡列表</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加卡</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除卡</button>
    </div>--%>
    <%=V_InputBodyEnd %>







</body>
</html>
