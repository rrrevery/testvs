<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKHS.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKHS.MZKGL_MZKHS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_MZKGL_MZKHS%>';
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="MZKGL_MZKHS.js"></script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" />
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" readonly="true" />
                <input id="HF_BGDDDM" type="hidden" />

            </div>
        </div>
        <div class="bffld" style="display:none">
            <div class="bffld_left">发行单位</div>
            <div class="bffld_right">
                <input id="TB_FXDWMC" type="text" tabindex="3" />
                <input id="HF_FXDWDM" type="hidden" />
                <input id="HF_FXDWID" type="hidden" />
            </div>

        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">保管人</div>
            <div class="bffld_right">
                <input id="HF_BGR" type="hidden" />
                <input id="TB_BGRMC" type="text" tabindex="2" readonly="true" />
                <input id="zHF_BGR" type="hidden" />
                <!-- 放大镜按钮 -->
                <input type="button" class="bfbtn btn_query" id="B_BGR" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">退卡数量</div>
            <div class="bffld_right">
                <input type="text" id="TB_TKSL" readonly="true" />
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
