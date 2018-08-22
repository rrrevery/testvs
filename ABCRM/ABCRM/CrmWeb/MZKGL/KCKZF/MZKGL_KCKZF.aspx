<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_KCKZF.aspx.cs" Inherits="BF.CrmWeb.MZKGL.KCKZF.MZKGL_KCKZF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        var vHF = GetUrlParam("hf");
        if (vHF == "0") {
            vPageMsgID = '<%=CM_MZKGL_KCKZF%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_MZKGL_KCKZF_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_MZKGL_KCKZF_SH%>');
        }
        if (vHF == "1") {
            vPageMsgID = '<%=CM_MZKGL_KCKZFHF%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_MZKGL_KCKZFHF_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_MZKGL_KCKZFHF_SH%>');
        }
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="MZKGL_KCKZF.js"></script>
    <script src="../../CrmLib/CrmLib_FillBGDD.js"></script>
    <script src="../../CrmLib/CrmLib_FillHYKTYPE.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="1" />
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" id="dv_je"></div>
            <div class="bffld_right">
                <label id="LB_ZFKJE" runat="server"></label>
            </div>
        </div>

    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" id="dv_sl"></div>
            <div class="bffld_right">
                <label id="LB_ZFKSL" runat="server"></label>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" id="dv_yy"></div>
            <div class="bffld_right">
                <input id="TB_ZFKYY" type="text" class="long" />
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
    <div id="menuContentHYKTYPE" class="menuContent">
        <ul id="TreeHYKTYPE" class="ztree"></ul>
    </div>
    <div id="menuContent" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
</body>
</html>
