<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKXF.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKXF.HYKGL_HYKXF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="HYKGL_HYKXF.js"></script>
    <script>
        vPageMsgID = <%=CM_HYKGL_HYKXF%>
        bCanEdit = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKXF_LR%>);
        bCanExec = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKXF_SH%>);
        bCanSrch = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKXF_CX%>);
    </script>

</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">操作门店</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" />
                <input id="HF_HYID" type="hidden" />
                <input type="button" id="btn_HYKHM_OLD" class="bfbtn btn_search" />
            </div>
        </div>

        <div class="bffld">
            <div class="bffld_left">姓名</div>
            <div class="bffld_right">
                <label id="LB_HY_NAME" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <label id="LB_HYKNAME" style="text-align: left;" runat="server" />
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡年费</div>
            <div class="bffld_right">
                <label id="LB_GBF" style="text-align: left;" runat="server" />
            </div>
        </div>

    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" class="long" type="text" />
            </div>
        </div>
    </div>

    <div id="zMPSK" class="inpage_tit slide_down_title">
        <span>年费收款方式</span>
    </div>
    <div id="zMPSK_Hidden" class="inpageinput">
        <table id="list"></table>
    </div>

    <div id="zMPCK" class="inpage_tit slide_down_title">
        <span>从卡信息</span>
    </div>
    <div id="zMPCK_Hidden" class="inpageinput">
        <table id="list_ck" style="border: thin"></table>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
