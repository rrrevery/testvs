<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_JFZCD.aspx.cs" Inherits="BF.CrmWeb.HYXF.JFZCD.HYXF_JFZCD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = <%=CM_HYXF_JFZC%>;
        bCanEdit = CheckMenuPermit(iDJR, <%=CM_HYXF_JFZC_LR%>);
        bCanExec = CheckMenuPermit(iDJR, <%=CM_HYXF_JFZC_SH%>);
        bCanSrch = CheckMenuPermit(iDJR, <%=CM_HYXF_JFZC_CX%>);
    </script>
    <script src="HYXF_JFZCD.js"></script>
    <script src="../../CrmLib/CrmLib_FillBGDD.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">转入卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" onblur="GetHYXX();" />
                <input type="hidden" id="HF_HYKTYPE" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">操作地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" runat="server" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">转入会员姓名</div>
            <div class="bffld_right">
                <label id="LB_HY_NAME" runat="server"></label>
                <input type="hidden" id="HF_HYID_ZR" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">有效期</div>
            <div class="bffld_right">
                <label id="LB_YXQ" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">未处理积分</div>
            <div class="bffld_right">
                <label id="LB_WCLJF" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">累计积分</div>
            <div class="bffld_right">
                <label id="LB_LJJF" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">转入积分</div>
            <div class="bffld_right">
                <label id="LB_ZRJF" runat="server" style="text-align: left"></label>
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
    <div id="menuContent" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
</body>
</html>
