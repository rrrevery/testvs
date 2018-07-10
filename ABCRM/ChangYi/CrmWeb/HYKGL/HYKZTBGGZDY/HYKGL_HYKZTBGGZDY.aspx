<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKZTBGGZDY.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKZTBGGZDY.HYKGL_HYKZTBGGZDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script>vPageMsgID = '<%=CM_HYKGL_ZTBGGZDY%>';</script>
    <script src="HYKGL_HYKZTBGGZDY.js"></script>
</head>
<body>
    <%=V_InputListBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input type="text" id="TB_HYKNAME" />
                <input type="hidden" id="HF_HYKTYPE" />
                <input type="hidden" id="zHF_HYKTYPE" />
                <input type="hidden" id="HF_HYKTYPEOLD" />
            </div>
        </div>

        <div class="bffld">
            <div class="bffld_left">未用月数</div>
            <div class="bffld_right">
                <input type="text" id="TB_WYYS" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">规则类型</div>
            <div class="bffld_right" style="width: auto">
                <input id="Radio1" type="radio" name="cld" value="1" />睡眠
                <input id="Radio2" type="radio" name="cld" value="2" />呆滞
                <input id="Radio3" type="radio" name="cld" value="3" />终止
                <input id="Radio4" type="radio" name="cld" value="4" />作废
                <input id="HF_HYKStatus" type="hidden" />
            </div>
        </div>

        <div class="bffld" id="jlbh">
        </div>
    </div>
    <%=V_InputListEnd %>
</body>
</html>
