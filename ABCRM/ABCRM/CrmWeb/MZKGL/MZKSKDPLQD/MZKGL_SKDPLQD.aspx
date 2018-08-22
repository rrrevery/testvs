<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_SKDPLQD.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKSKDPLQD.MZKGL_SKDPLQD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_MZKGL_SKDPLQD%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_MZKGL_SKDPLQD_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_MZKGL_SKDPLQD_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_MZKGL_SKDPLQD_CX%>');
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="MZKGL_SKDPLQD.js"></script>

</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">操作地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" type="text" />
            </div>
        </div>

    </div>
    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">售卡单列表</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
