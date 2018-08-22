<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYPLDBQ.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYPLDBQ.HYKGL_HYPLDBQ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = '<%=CM_HYKGL_HYPLDBQ%>';
    </script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="HYKGL_HYPLDBQ.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow" style="display: none">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">标签类别</div>
            <div class="bffld_right">
                <select id="DDL_BQLB" onchange="BQLBChange()">
                    <option></option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">标签项目</div>
            <div class="bffld_right">
                <input id="TB_BQXMMC" type="text" />
                <input id="HF_BQXMID" type="hidden" />
            </div>
        </div>
    </div>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">标签值</div>
            <div class="bffld_right">
                <select id="DDL_BQZ">
                    <option></option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">有效期</div>
            <div class="bffld_right">
                <input id="TB_YXQ" type="text" class="Wdate" onfocus="WdatePicker()" />
            </div>
        </div>
    </div>

    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">卡列表</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加卡</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除卡</button>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
