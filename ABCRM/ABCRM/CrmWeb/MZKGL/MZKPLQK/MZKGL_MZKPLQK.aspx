<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKPLQK.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKPLQK.MZKGL_MZKPLQK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_MZKGL_TSCK%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_MZKGL_TSCK_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_MZKGL_TSCK_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_MZKGL_TSCK_CX%>');
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="MZKGL_MZKPLQK.js"></script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" />
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
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
        <div class="bffld">
            <div class="bffld_left">取款总金额</div>
            <div class="bffld_right">
                <label id="LB_QKJE" runat="server" style="text-align: left"></label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">取款数量</div>
            <div class="bffld_right">
                <label id="LB_QKSL" runat="server" style="text-align: left"></label>
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

    <div style="clear: both;"></div>
    <div id="zMP1" class="common_menu_tit slide_down_title bfborder_top">
        <span>面值卡信息</span>
    </div>
    <div id="zMP1_Hidden" class="maininput">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">取款金额</div>
                <div class="bffld_right">
                    <input id="TB_QKJE" type="text" tabindex="1" />
                </div>
            </div>
        </div>

        <div class="bfrow bfrow_table">
            <table id="list" style="border: thin"></table>
        </div>

        <div id="tb" class="item_toolbar">
            <span style="float: left"></span>
            <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
            <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
        </div>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>
