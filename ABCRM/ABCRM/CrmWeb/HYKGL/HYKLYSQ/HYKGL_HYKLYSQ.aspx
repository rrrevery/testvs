<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKLYSQ.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKLYSQ.HYKGL_HYKLYSQ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vCZK = IsNullValue(GetUrlParam("czk"), 0);
        vDJLX = GetUrlParam("djlx");
        if (vCZK == "0") {
            if (vDJLX == "0") {
                vPageMsgID = '<%= CM_HYKGL_HYKLYSQ%>';
                bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYKGL_HYKLYSQ_LR%>');
                bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYKGL_HYKLYSQ_SH%>');
                bCanSrch = CheckMenuPermit(iDJR, '<%=CM_HYKGL_HYKLYSQ_CX%>');
            }

        }
        else {
            if (vDJLX == "0") {
                vPageMsgID = '<%= CM_MZKGL_MZKLYSQ%>';
                bCanEdit = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKLYSQ_LR%>');
                bCanExec = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKLYSQ_SH%>');
                bCanSrch = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKLYSQ_CX%>');
            }

        }
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="HYKGL_HYKLYSQ.js"></script>
    <script src="../../CrmLib/CrmLib_FillBGDD.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">拨入地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC_BR" runat="server" />
                <input id="HF_BGDDDM_BR" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">申请数量</div>
            <div class="bffld_right">
                <label id="LB_HYKSL" runat="server"></label>
                <%--<input id="TB_JKSL" type="text" readonly="readonly"  /><%--onblur="Checkjksl()"--%>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">备注</div>
            <div class="bffld_right">
                <input id="TB_BZ" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>
    <div id="tb" class="item_toolbar">
        <span style="float: left">卡类型列表</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
    </div>
    <%=V_InputBodyEnd %>
    <div id="menuContent" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
</body>
</html>
