<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WDCNRTKDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WDCNRTKDY.GTPT_WDCNRTKDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = <%=CM_GTPT_WDCNRDY%>;
    </script>
    <script src="GTPT_WDCNRTKDY.js"></script>

 
</head>
<body>
       <%=V_InputBodyBegin %>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div id="jlbh"></div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">名称</div>
                                    <div class="bffld_right">
                                        <input id="TB_MC" type="text" />
                                        
                                    </div>
                                </div>
                            </div>
                          
    
      <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_BZ" type="text" />
            </div>
        </div>
    </div>
           
                         
                        
                   


     <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>
    <div id="tb" class="item_toolbar">
        <span style="float: left"></span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
    </div>

    <%=V_InputBodyEnd %>

</body>
</html>
