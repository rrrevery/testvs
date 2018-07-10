<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKGS.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKGS.MZKGL_MZKGS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=V_Head_Input %>
    <script>
        vHF = GetUrlParam("hf");
        if (vHF == 0) {
            vPageMsgID = '<%=CM_MZKGL_MZKGS%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKGS_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKGS_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKGS_CX%>');
        }
        else {
            vPageMsgID = '<%=CM_MZKGL_MZKGSHF%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKGSHF_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKGSHF_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKGSHF_CX%>');
        }
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="MZKGL_MZKGS.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" onblur="GetMZKXX();" />
                <input id="HF_HYID" type="hidden" />
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
            <div class="bffld_left">
                金额
            </div>
            <div class="bffld_right">
                <label id="LB_JE" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">
                卡原状态
            </div>
            <div class="bffld_right">
                <label id="LB_YZT" style="text-align: left;" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">
                卡新状态
            </div>
            <div class="bffld_right">
                <label id="LB_XZT" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
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
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" type="text" />
            </div>
        </div>
    </div>


    <%=V_InputBodyEnd %>
</body>
</html>
