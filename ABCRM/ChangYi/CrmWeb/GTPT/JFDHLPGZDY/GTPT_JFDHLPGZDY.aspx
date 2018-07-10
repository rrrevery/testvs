<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_JFDHLPGZDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.JFDHLPGZDY.GTPT_JFDHLPGZDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_JFDHLPGZDY%>';
    </script>
    <script src="GTPT_JFDHLPGZDY.js"></script>
</head>
<body>
       <%=V_InputBodyBegin %>

                            <div class="bfrow">
                                <div class="bffld">
                                    <div id="jlbh"></div>
                                </div>
                                <div class="bffld">
                                     <div class="bffld_left">规则名称</div>
                                    <div class="bffld_right">
                                        <input id="TB_GZMC" type="text" />
                                    </div>
                                   
                                </div>
                            </div>
                           <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">礼品列表</span>
        <button id="Add_LP" type='button' class="item_addtoolbar">添加</button>
        <button id="Del_LP" type='button' class="item_deltoolbar">删除</button>
    </div>

        <div style="clear: both;"></div>


     

    <div id="tb_KLX" class="item_toolbar">
        <span style="float: left">卡类型列表</span>
        <button id="Add_KLX" type='button' class="item_addtoolbar">添加</button>
        <button id="Del_KLX" type='button' class="item_deltoolbar">删除</button>
    </div>
          <div class="bfrow bfrow_table">
        <table id="list2" style="border: thin"></table>
    </div>             
                        
                         

   <%=V_InputBodyEnd %>
                 
</body>
</html>
