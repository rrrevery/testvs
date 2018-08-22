<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LPGL_JFFHLPKC_Srch.aspx.cs" Inherits="BF.CrmWeb.LPGL.JFFHLPKC.LPGL_JFFHLPKC_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>vPageMsgID = <%=CM_LPGL_JFFHLPKC_CX%>;</script>
    <script src="LPGL_JFHLLPKC_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">礼品代码</div>
            <div class="bffld_right">
                <input id="TB_LPDM" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="text-wrap: none">礼品国际码</div>
            <div class="bffld_right">
                <input id="TB_LPGJM" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" name="" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="zHF_BGDDDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">礼品名称</div>
            <div class="bffld_right">
                <input id="TB_LPMC" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">礼品规格</div>
            <div class="bffld_right">
                <input id="TB_LPGG" type="text" name="" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">礼品单价</div>
            <div class="bffld_right">
                <input id="TB_LPDJ" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">礼品进价</div>
            <div class="bffld_right">
                <input id="TB_LPJJ" type="text" name="" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">礼品积分</div>
            <div class="bffld_right">
                <input id="TB_LPJF" type="text" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
