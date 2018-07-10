<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_MBJZGZ.aspx.cs" Inherits="BF.CrmWeb.YHQGL.MBJZGZ.YHQGL_MBJZGZ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>vPageMsgID = '<%=CM_YHQGL_MBJZGZ%>';</script>
    <script src="YHQGL_MBJZGZ.js"></script>
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
            <div class="bffld_left">发放限额</div>
            <div class="bffld_right">
                <input id="TB_FFXE" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">起点金额</div>
            <div class="bffld_right">
                <input id="TB_QDJE" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">

        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input id="CB_BJ_TY" type="checkbox" />
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


    <div style="clear: both; margin-top: 10px;">
        <pre>
　　满百减折规则描述：按消费金额由高到低匹配满减规则，当剩余不满足高档金额时按金额排序依次匹配。

　　例如：例如100减10元，200减30，500减100。

　　当消费金额为1300时，会取2次500，1次200，1次100的规则，共减240元。
        </pre>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>
