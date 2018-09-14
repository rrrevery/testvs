<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LPGL_LPLQD.aspx.cs" Inherits="BF.CrmWeb.LPGL.LPLQD.LPGL_LPLQD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = '<%=CM_LPGL_JFFHLPLQD%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_LPGL_JFFHLPLQD_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_LPGL_JFFHLPLQD_SH%>');
    </script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="LPGL_LPLQD.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">领取人</div>
            <div class="bffld_right">
                <input id="TB_LQRMC" type="text" />
                <input id="HF_LQR" type="hidden" />
                <input id="zHF_LQR" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">拨出地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC_BC" runat="server" type="text" />
                <input id="HF_BGDDDM_BC" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">拨入地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" runat="server" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">总数量</div>
            <div class="bffld_right">
                <label id="LB_ZSL" runat="server" style="text-align: left">0</label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">总金额</div>
            <div class="bffld_right">
                <label id="LB_ZJE" runat="server" style="text-align: left">0</label>
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
        <span style="float: left">礼品列表</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加礼品</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除礼品</button>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>

