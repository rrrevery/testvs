<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXLCSBDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXLCSBDY.GTPT_WXLCSBDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = <%=CM_GTPT_WXLCSB%>;
    </script>
    <script src="GTPT_WXLCSBDY.js"></script>
      <script src="../../CrmLib/CrmLib_BillWX.js"></script>
     <script src="../../CrmLib/CrmLib_GetData.js"></script>

</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
    </div>

        <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />

            </div>
        </div>       
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">楼层名称</div>
            <div class="bffld_right">
                <input id="TB_LCMC" type="text" />
                <input type="hidden" id="HF_LCID" />
                <input type="hidden" id="zHF_LCID" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">商标名称</div>
            <div class="bffld_right">
                <input id="TB_SBMC" type="text" />
                <input type="hidden" id="HF_SBID" />
                <input type="hidden" id="zHF_SBID" />
            </div>
        </div>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>
