<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_YHQSYGZ.aspx.cs" Inherits="BF.CrmWeb.YHQGL.YHQSYGZ.YHQGL_YHQSYGZ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>vPageMsgID = <%=CM_YHQGL_YHQDEFSYGZ %>;</script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="YHQGL_YHQSYGZ.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">规则名称</div>
            <div class="bffld_right">
                <input id="TB_GZMC" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">使用上限</div>
            <div class="bffld_right">
                <input id="TB_SYSX" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld" style="width: 600px; display: none">
            <div class="bffld_left">配比标记</div>
            <input id="Radio3" type="radio" name="BJ_PB" value="0" />不抵销售
            <input id="Radio4" type="radio" name="BJ_PB" value="1" />配比金额抵销售
            <input id="Radio5" type="radio" name="BJ_PB" value="2" />配比金额抵消销售减券
            <input id="Radio1" type="radio" name="BJ_PB" value="3" />配比金额抵销售加档
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input id="CB_BJ_TY" type="checkbox" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>


    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">规则列表</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加规则</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除规则</button>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>


