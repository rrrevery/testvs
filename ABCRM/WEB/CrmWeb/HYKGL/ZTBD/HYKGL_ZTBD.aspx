<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_ZTBD.aspx.cs" Inherits="BF.CrmWeb.HYKGL.ZTBD.HYKGL_ZTBD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vCZK = IsNullValue(GetUrlParam("czk"), 0);
        if (vCZK == "0") {
            vPageMsgID = '<%= CM_HYKGL_HYKZTBD%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYKGL_HYKZTBD_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYKGL_HYKZTBD_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_HYKGL_HYKZTBD_CX%>');
        }
        else {
            vPageMsgID = '<%= CM_HYKGL_MZKZTBD%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYKGL_MZKZTBD_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYKGL_MZKZTBD_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_HYKGL_MZKZTBD_CX%>');
        }
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="HYKGL_ZTBD.js"></script>
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
            <div class="bffld_left">工本费</div>
            <div class="bffld_right">
                <input id="TB_GBF" type="text" name="s" onblur="checkvalue(this)" />
                <script>
                    function checkvalue(obj) {
                        if (!/^[+|-]?\d+\.?\d*$/.test(obj.value) && obj.value != '') {
                            alert('请输入数字！');
                            obj.select();
                        }
                    }
                </script>
            </div>
        </div>
        <div class="bffld_l">
            <div class="bffld_left">新状态</div>
            <input class="magic-radio" type="radio" name="status" id="rd_TY" value="-4" />
            <label for="rd_TY">停用卡</label>
            <input class="magic-radio" type="radio" name="status" id="rd_FSK" value="0" />
            <label for="rd_FSK">发售卡</label>
            <input class="magic-radio" type="radio" name="status" id="rd_YXFK" value="1" />
            <label for="rd_YXFK">已消费卡</label>
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
