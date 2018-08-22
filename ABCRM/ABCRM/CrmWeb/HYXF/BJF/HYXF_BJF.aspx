<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_BJF.aspx.cs" Inherits="BF.CrmWeb.HYXF.BJF.HYXF_BJF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_HYXF_BJF%>';
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="HYXF_BJF.js"></script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYK_NO" type="text" onblur="GetHYXX();" />
                <input id="HF_HYID" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <label id="LB_HYKNAME"></label>
                <input type="hidden" id="HF_HYKTYPE" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">操作地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>


    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">积分规则</div>
            <div class="bffld_right">
                <input id="TB_MC" type="text" tabindex="4" />
                <input id="HF_ID" type="hidden" />
                <input id="HF_zID" type="hidden" />
                <input id="HF_JF" type="hidden" />
                <input id="HF_JE" type="hidden" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">消费金额</div>
            <div class="bffld_right">
                <input id="TB_XFJE" type="text" tabindex="4" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">积分</div>
            <div class="bffld_right">
                <label id="LB_JF" style="text-align: left;" />
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
