<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXYYFWJLCL.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXYYFWJLCL.GTPT_WXYYFWJLCL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input%>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXYYFWJLCL%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXYYFWJLCL_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXYYFWJLCL_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXYYFWJLCL_CX%>');
    </script>
    <script src="GTPT_WXYYFWJLCL.js"></script>
    <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">备注</div>
            <div class="bffld_right">
                <input id="TB_BZ" type="text" />
            </div>
        </div>
    </div>

        <div style="clear: both;"></div>
    <div id="zMP1" class="common_menu_tit slide_down_title">
        <span>参加服务预约</span>
    </div>
    <div id="zMP1_Hidden" class="maininput bfborder_bottom">

        <div class="bfrow bfrow_table">
            <table id="list" style="border: thin"></table>
        </div>

        <div id="tb" class="item_toolbar">
            <span style="float: left"></span>
            <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
            <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
        </div>
    </div>

        <div style="clear: both;"></div>
    <div id="zMP2" class="common_menu_tit slide_down_title">
        <span>不参加服务预约</span>
    </div>
    <div id="zMP2_Hidden" class="maininput bfborder_bottom">

        <div class="bfrow bfrow_table">
            <table id="listBYY" style="border: thin"></table>
        </div>
        <div id="listBYY_tb" class="item_toolbar">
            <span style="float: left"></span>
            <button id="AddBYY" type='button' class="item_addtoolbar">添加</button>
            <button id="DelBYY" type='button' class="item_deltoolbar">删除</button>
        </div>
    </div>
<%--    <fieldset style="width: 850px;" id="news" class="rule_ask">
        <legend>参加服务预约</legend>


         <div class="bfrow bfrow_table">
        <table id="Table1" style="border: thin"></table>
    </div>
    <div id="tb1" class="item_toolbar">
        <span style="float: left"></span>
        <button id="md_Add1" type='button' class="item_addtoolbar">添加</button>
        <button id="md_Del1" type='button' class="item_deltoolbar">删除</button>
    </div>
    </fieldset>--%>

<%--    <fieldset style="width: 850px;" id="Fieldset1" class="rule_ask">
        <legend>不参加服务预约</legend>
         <div class="bfrow bfrow_table">
        <table id="Table2" style="border: thin"></table>
    </div>
    <div id="tb2" class="item_toolbar">
        <span style="float: left"></span>
        <button id="md_Add2" type='button' class="item_addtoolbar">添加</button>
        <button id="md_Del2" type='button' class="item_deltoolbar">删除</button>
    </div>
    </fieldset>--%>


    <%=V_InputBodyEnd %>
</body>
</html>
