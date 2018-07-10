<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYTJ.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYTJ.HYKGL_HYTJ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>vPageMsgID = <%=CM_HYKGL_HYTJGZ%>;</script>
    <script src="HYKGL_HYTJ.js"></script>
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
                <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker()" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker()" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">奖励方式</div>
            <div class="bffld_right">
                <select id="DDL_JLFS">
                    <option selected="" value="0">--请选择--</option>
                    <option value="1">积分</option>
                    <option value="2">电子券</option>
                </select>
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">消费金额</div>
            <div class="bffld_right">
                <input type="text" id="TB_XFJE" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">消费次数</div>
            <div class="bffld_right">
                <input type="text" id="TB_XFCS" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">消费积分</div>
            <div class="bffld_right">
                <input id="TB_XFJF" type="text" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>

    <div style="clear: both;"></div>
    <div id="zMP1" class="common_menu_tit slide_down_title">
        <span>规则明细</span>
        <div id="div" class="btn_dropdown">
            <i class="fa fa-angle-down" aria-hidden="true" style="color: white"></i>
        </div>
    </div>
    <div id="zMP1_Hidden" class="maininput">
        <div id="tb" class="item_toolbar">
            <span style="float: left">规则列表</span>
            <button id="AddGZ" type='button' class="item_addtoolbar">添加规则</button>
            <button id="DelGZ" type='button' class="item_deltoolbar">删除规则</button>
        </div>
        <div class="bfrow bfrow_table">
            <table id="list" style="border: thin"></table>
        </div>
    </div>

        <div style="clear: both;"></div>
    <div id="zMP2" class="common_menu_tit slide_down_title">
        <span>客群明细</span>
        <div id="div2" class="btn_dropdown">
            <i class="fa fa-angle-down" aria-hidden="true" style="color: white"></i>
        </div>
    </div>
    <div id="zMP2_Hidden" class="maininput">
        <div id="Div4" class="item_toolbar">
            <span style="float: left">客群列表</span>
            <button id="AddKQ" type='button' class="item_addtoolbar">添加客群</button>
            <button id="DelKQ" type='button' class="item_deltoolbar">删除客群</button>
        </div>

        <div class="bfrow bfrow_table">
            <table id="listKQ" style="border: thin"></table>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
