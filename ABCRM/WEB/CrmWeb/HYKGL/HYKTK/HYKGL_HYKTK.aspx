<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKTK.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKTK.HYKGL_HYKTK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%=V_Head_Input %>
    <script>
        vPageMsgID = <%=CM_HYKGL_HYKTK%>
        bCanEdit = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKTK_LR%>);
        bCanExec = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKTK_SH%>);
        bCanSrch = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKTK_CX%>);
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="HYKGL_HYKTK.js"></script>
    <script src="../../CrmLib/CrmLib_FillBGDD.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" runat="server" />
                <input id="HF_HYID" type="hidden" />
                <input id="HF_YHQYE" type="hidden" />
                <input id="HF_JF" type="hidden" />
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
        <div class="bffld" style="display: none">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" />
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡费金额</div>
            <div class="bffld_right">
                <input id="TB_KFJE" type="text" readonly="true" />
            </div>
        </div>
    </div>

    <div class="bfrow">

        <div class="bffld">
            <div class="bffld_left">已用卡费金额</div>
            <div class="bffld_right">
                <input id="TB_YYKFJE" type="text" readonly="true" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">退回卡费金额</div>
            <div class="bffld_right">
                <input id="TB_THKFJE" type="text" readonly="true" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">已用天数</div>
            <div class="bffld_right">
                <input id="TB_YYTS" type="text" readonly="true" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">退还余额</div>
            <div class="bffld_right">
                <input id="TB_THYE" type="text" readonly="true" />
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
    </div>
    <%=V_InputBodyEnd %>
    <div id="menuContent" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
    <div id="menuContentHYKTYPE" class="menuContent">
        <ul id="TreeHYKTYPE" class="ztree"></ul>
    </div>
</body>
</html>
