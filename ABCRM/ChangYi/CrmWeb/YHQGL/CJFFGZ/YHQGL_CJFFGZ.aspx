<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_CJFFGZ.aspx.cs" Inherits="BF.CrmWeb.YHQGL.CJFFGZ.YHQGL_CJFFGZ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_YHQGL_YHQDEFFFGZ_CJ%>';
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="YHQGL_CJFFGZ.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">发放规则名称</div>
            <div class="bffld_right">
                <input id="TB_FFGZMC" type="text" tabindex="2" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">抽奖次数上限</div>
            <div class="bffld_right">
                <input id="TB_CJCSSX" type="text" tabindex="2" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">起点金额</div>
            <div class="bffld_right">
                <input id="TB_QDJE" type="text" tabindex="1" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" name="CB_BJTY" id="CB_BJ_TY" value="" />
                <label for="CB_BJ_TY"></label>
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
