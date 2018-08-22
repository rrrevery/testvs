<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_YHQCKGZ.aspx.cs" Inherits="BF.CrmWeb.YHQGL.YHQCKGZ.YHQGL_YHQCKGZ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_YHQGL_YHQYCZZ%>';
    </script>
    <script src="YHQGL_YHQCKGZ.js"></script>
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
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right">
                <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_JSRQ\')}'} )" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店</div>
            <div class="bffld_right">
                <input id="TB_MD" type="text" />
                <input id="HF_MD" type="hidden" />
                <input id="zHF_MD" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input id="TB_YHQ" type="text" />
                <input id="HF_YHQ" type="hidden" />
                <input id="zHF_YHQ" type="hidden" />
            </div>
        </div>

    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">使用结束日期</div>
            <div class="bffld_right">
                <input id="TB_SYJSRQ" type="text" class="Wdate" onfocus="WdatePicker()" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">现金优惠券</div>
            <div class="bffld_right">
                <input id="TB_XJYHQ" type="text" />
                <input id="HF_XJYHQ" type="hidden" />
                <input id="zHF_XJYHQ" type="hidden" />
            </div>
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
