<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKZF.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKZF.HYKGL_HYKZF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%= CM_HYKGL_HYKZF%>';
        bCanEdit = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKZF_LR%>);
        bCanExec = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKZF_SH%>);
        bCanSrch = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKZF_CX%>);
    </script>
    <script src="HYKGL_HYKZF.js"></script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
        </div>
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
            <div class="bffld_left">操作地点 </div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" runat="server" />
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
        <span style="float: left">卡号列表</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加卡</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除卡</button>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
