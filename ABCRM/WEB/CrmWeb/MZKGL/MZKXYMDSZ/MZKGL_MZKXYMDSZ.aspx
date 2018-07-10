<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKXYMDSZ.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKXYMDSZ.MZKGL_MZKXYMDSZ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script>
        vPageMsgID = '<%=CM_MZKGL_CZKXYMDDEF%>';
    </script>
    <script src="MZKGL_MZKXYMDSZ.js"></script>
</head>
<body>
    <%=V_InputListBegin %>
    <div class="bfrow" style="display: none">
        <div class="bffld" id="jlbh">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
                <input id="HF_OLDHYKTYPE" type="hidden" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">可用门店</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
                <input id="HF_OLDMDID" type="hidden" />
            </div>
        </div>
    </div>
    <%=V_InputListEnd %>
</body>
</html>
