<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXQDSJFGZDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXQDSJFGZDY.GTPT_WXQDSJFGZDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = <%=CM_GTPT_WXQDGZ%>
        bCanEdit = CheckMenuPermit(iDJR, <%=CM_GTPT_WXQDGZ_LR %>);
        bCanExec = CheckMenuPermit(iDJR, <%=CM_GTPT_WXQDGZ_SH%>);
        bCanSrch = CheckMenuPermit(iDJR, <%=CM_GTPT_WXQDGZ_CX%>);
        bCanStart = CheckMenuPermit(iDJR, <%=CM_GTPT_WXQDGZ_QD%>);
        bCanStop =  CheckMenuPermit(iDJR, <%=CM_GTPT_WXQDGZ_ZZ%>);
    </script>
    <script src="GTPT_WXQDSJFGZDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>

    </div>
    <div class="bfrow">

        <div class="bffld">
            <div class="bffld_left">开始时间</div>
            <div class="bffld_right">
                <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束时间</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
    </div>

    <div class="bfrow">

        <div class="bffld">
            <div class="bffld_left">连续签到天数</div>
            <div class="bffld_right">
                <input id="TB_COUNTS" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">签到提示</div>
            <div class="bffld_right">
                <input id="TB_CONTENT" type="text" />
            </div>
        </div>
    </div>

 <%--   <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left"></div>
            <div class="bffld_right">
                <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
                <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left"></div>
            <div class="bffld_right">
                <button id="Add" type='button' class="item_addtoolbar">添连续</button>
                <button id="Del" type='button' class="item_deltoolbar">删连续</button>

            </div>
        </div>
    </div>--%>
   <%-- <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left"></div>
            <div class="bffld_right">
                <table id="list"></table>
            </div>
        </div>
        <div class="bffld_l" id="tab_2">
            <div class="bffld_left"></div>
            <div class="bffld_right">
                <table id="list2"></table>
            </div>
        </div>
    </div>--%>


                       <div>
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left"></span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
    </div>


     <div style="clear: both;"></div>


     

    <div id="tb_KLX" class="item_toolbar">
        <span style="float: left">卡类型</span>
        <button id="Add" type='button' class="item_addtoolbar">添加</button>
        <button id="Del" type='button' class="item_deltoolbar">删除</button>
    </div>
          <div class="bfrow bfrow_table">
        <table id="list2" style="border: thin"></table>
    </div>  
    <%=V_InputBodyEnd %>
</body>
</html>
