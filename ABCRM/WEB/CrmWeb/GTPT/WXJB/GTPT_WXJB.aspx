<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXJB.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXJB.GTPT_WXJB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXJB%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXJB_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXJB_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXJB_CX%>');
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
     <script src="../../CrmLib/CrmLib_BillWX.js"></script>

    <script src="GTPT_WXJB.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" />
                <input id="HF_HYID" type="hidden" />
                <input id="HF_CODE" type="hidden" />
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
            <div class="bffld_left">类型</div>
            <div class="bffld_right">
                <label id="LB_LX" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">绑定时间</div>
            <div class="bffld_right">
                <label id="LB_BDSJ" style="text-align: left;" runat="server" />
            </div>
        </div>
          <div class="bffld">
                <div class="bffld_left">&nbsp;</div>
                <div class="bffld_right">
                    <input class="magic-checkbox" type="checkbox" name="WXKBJB" id="C_WXKBJB" value="" />
                    <label for="C_WXKBJB">同时解绑微信卡包会员卡</label>

                </div>
            </div>
        <div class="bffld" style="display: none;">
            <div class="bffld_left">微信号</div>
            <div class="bffld_right">
                <label id="LB_OPENID" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow" style="display: none;">
        <div class="bffld">
            <div class="bffld_left">联合ID</div>
            <div class="bffld_right">
                <label id="LB_LH" style="text-align: left;" runat="server" />
            </div>
        </div>
        <div class="bffld" style="display: none;">
            <div class="bffld_left">公共ID</div>
            <div class="bffld_right">
                <label id="LB_PID" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
