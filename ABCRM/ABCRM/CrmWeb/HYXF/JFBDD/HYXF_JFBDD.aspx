<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_JFBDD.aspx.cs" Inherits="BF.CrmWeb.HYXF.JFBDD.HYXF_JFBDD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = <%=CM_HYXF_JFBD %>;
        bCanEdit = CheckMenuPermit(iDJR, <%=CM_HYXF_JFBD_LR%>);
        bCanExec = CheckMenuPermit(iDJR, <%=CM_HYXF_JFBD_SH%>);
        bCanSrch = CheckMenuPermit(iDJR, <%=CM_HYXF_JFBD_CX%>);
    </script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="HYXF_JFBDD.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">操作地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" runat="server" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">调整积分</div>
            <div class="bffld_right">
                <input id="TB_JF" type="text" />
                <input type="button" class="bfbtn btn_trim" id="BTN_TZJF" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">调整金额</div>
            <div class="bffld_right">
                <input id="TB_JE" type="text" />
                <input type="button" class="bfbtn btn_trim" id="BTN_TZJE" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">&nbsp;</div>
            <div class="bffld_right">
                <input id="CK_iBJ_CLWCLJF" type="checkbox" tabindex="1" class="magic-checkbox" />
                <label for="CK_iBJ_CLWCLJF">调整兑奖积分(未处理积分)</label>
                <input id="CK_iBJ_CLBQJF" type="checkbox" tabindex="1" class="magic-checkbox" />
                <label for="CK_iBJ_CLBQJF">调整升级积分(本期积分)</label>
                <input id="CK_iBJ_CLBNLJJF" type="checkbox" tabindex="1" class="magic-checkbox" />
                <label for="CK_iBJ_CLBNLJJF">调整本年积分(本年累计积分)</label>
                <input id="CK_iBJ_CLLJJF" type="checkbox" tabindex="1" class="magic-checkbox" />
                <label for="CK_iBJ_CLLJJF">调整累计积分</label>
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
        <span style="float: left">卡号列表</span>

        <button id="AddItem" type='button' class="item_addtoolbar">添加卡</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除卡</button>


    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
