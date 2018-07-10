<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_HYHDLDPS.aspx.cs" Inherits="BF.CrmWeb.KFPT.HYHDLDPS.KFPT_HYHDLDPS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script type="text/javascript">
        vPageMsgID = '<%=CM_KFPT_HYHDLDPS%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_KFPT_HYHDLDPS_LR%>');
    </script>
    <script src="KFPT_HYHDLDPS.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">活动名称</div>
            <div class="bffld_right">
                <label id="LB_HDMC" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">客服人员</div>
            <div class="bffld_right">
                <label id="LB_KFRYMC" style="text-align: left;" runat="server" />
                <input id="HF_KFRYID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">评分</div>
            <div class="bffld_right">
                <input id="TB_PF" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">领导评语</div>
            <div class="bffld_right">
                <input type="text" id="TB_LDPY" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">评述人</div>
            <div class="bffld_right">
                <label id="LB_PSRMC" class='djr'></label>
                <input id="HF_PSR" type="hidden" />
                <label id="LB_PSSJ" class='djsj' ></label>
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
