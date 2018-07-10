<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXPPSBDY_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXPPSBDY.GTPT_WXPPSBDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = <%=CM_GTPT_WXPPSB%>;
    </script>
    <script src="GTPT_WXPPSBDY_Srch.js?ts=1"></script>
    <script src="../../CrmLib/CrmLib_BillWX.js"></script>
     <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">优先级</div>
            <div class="bffld_right">
                <input id="TB_INX" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">电话</div>
            <div class="bffld_right">
                <input id="TB_PHONE" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">网址</div>
            <div class="bffld_right">
                <input id="TB_IP" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">分类名称</div>
            <div class="bffld_right">
                <input id="TB_FLMC" type="text" />
                <input id="HF_FLID" type="hidden" />
                <input id="zHF_FLID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">商标名称</div>
            <div class="bffld_right">
                <input id="TB_SBMC" type="text" />
                <input id="HF_SBID" type="hidden" />
                <input id="zHF_SBID" type="hidden" />
            </div>

        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
