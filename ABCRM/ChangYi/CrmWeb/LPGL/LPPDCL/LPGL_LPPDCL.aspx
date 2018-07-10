<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LPGL_LPPDCL.aspx.cs" Inherits="BF.CrmWeb.LPGL.LPPDCL.LPGL_LPPDCL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>vPageMsgID = '<%=CM_LPGL_LPPDCL%>';</script>
    <script src="LPGL_LPPDCL.js"></script>
    <script src="../../CrmLib/CrmLib_FillBGDD.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">操作地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" runat="server" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>

        <div class="bffld">
            <div class="bffld_left">盘点日期</div>
            <div class="bffld_right">
                <input id="TB_PDRQ" class="Wdate" onfocus="WdatePicker()" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" type="text"  />
            </div>
        </div>
    </div>
    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">商品列表</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加商品</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除商品</button>
    </div>
    <%=V_InputBodyEnd %>
    <div id="menuContent" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
</body>
</html>

