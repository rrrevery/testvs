<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_KCKBB_Srch.aspx.cs" Inherits="BF.CrmWeb.MZKGL.KCKBB.MZKGL_KCKBB_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_MZKGL_MZKKCBB%>';
    </script>
    <script src="MZKGL_KCKBB_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">面值卡卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="4" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="zHF_BGDDDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">保管人</div>
            <div class="bffld_right">
                <input id="TB_BGRMC" type="text" />
                <input id="HF_BGR" type="hidden" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
