<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXLJD.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXLJD.GTPT_WXLJD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXLJD%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXLJD_LR %>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXLJD_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXLJD_CX%>');
    </script>
       <script src="../../CrmLib/CrmLib_BillWX.js"></script>
     <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXLJD.js"></script>
    <script src="../../CrmLib/CrmLib_FillBGDD.js"></script>
</head>
<body>
<%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYK_NO" type="text" />
                <input id="HF_HYID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">操作地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">礼品列表</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加礼品</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除礼品</button>
    </div>
    <%=V_InputBodyEnd %>
    <div id="menuContent" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
</body>
</html>
