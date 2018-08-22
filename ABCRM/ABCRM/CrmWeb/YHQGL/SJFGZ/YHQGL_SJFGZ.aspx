<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_SJFGZ.aspx.cs" Inherits="BF.CrmWeb.YHQGL.FFGZ.YHQGL_FFGZ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_YHQGL_SJFGZ%>';
    </script>
    <script src="YHQGL_SJFGZ.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" style="display: none;">
            <div id="jlbh"></div>
        </div>

        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">发放规则代码</div>
            <div class="bffld_right">
                <input type="text" id="TB_FFGZID" readonly="true" disabled="disabled" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">发放规则名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_FFGZMC" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">发放限额</div>
            <div class="bffld_right">
                <input type="text" id="TB_FFXE" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">起点金额</div>
            <div class="bffld_right">
                <input type="text" id="TB_FFQDJE" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input type="checkbox" id="CB_BJ_TY" />停用
                                        <input type="hidden" id="HF_BJ_TY" value="0" />
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
    <div id="menuContent" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
</body>
</html>
