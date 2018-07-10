<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKJEPLZC.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKJEPLZC.MZKGL_MZKJEPLZC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_MZKGL_MZKJEPLZC%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKJEPLZC_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKJEPLZC_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKJEPLZC_CX%>');
    </script>
    <script src="MZKGL_MZKJEPLZC.js"></script>
    <%-- <script src="../../CrmLib/CrmLib_FillBGDD.js"></script>--%>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld"></div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="1" />
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">操作门店</div>
            <div class="bffld_right">
                <input id="TB_CZMD" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">转出金额</div>
            <div class="bffld_right">
                <label id="LB_ZCJE" runat="server" style="text-align: left" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">转入金额</div>
            <div class="bffld_right">
                <label id="LB_ZRJE" runat="server" style="text-align: left" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">转出数量</div>
            <div class="bffld_right">
                <label id="LB_ZCSL" runat="server" style="text-align: left" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">转入数量</div>
            <div class="bffld_right">
                <label id="LB_ZRSL" runat="server" style="text-align: left" />
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
    <div class="clear"></div>
    <div id="zMP7" class="common_menu_tit slide_down_title">
        <span>转入转出卡列表</span>
    </div>
    <div id="zMP7_Hidden">
        <div style="width: 50%; float: left" id="zcklist">
            <div id="tb_zck" class="item_toolbar">
                <span style="float: left">转出卡列表</span>
                <button id="AddOutCard" type='button' class="item_addtoolbar">添加卡</button>
                <button id="DelOutCard" type='button' class="item_deltoolbar">删除卡</button>
            </div>
            <div>
                <table id="List_OutCard"></table>
            </div>

        </div>
        <div style="width: 50%; float: left" id="zrklist">
            <div id="tb_zrk" class="item_toolbar">
                <span style="float: left">转入卡列表</span>
                <button id="AddInCard" type='button' class="item_addtoolbar">添加卡</button>
                <button id="DelInCard" type='button' class="item_deltoolbar">删除卡</button>
            </div>
            <div>
                <table id="List_InCard"></table>
            </div>

        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
