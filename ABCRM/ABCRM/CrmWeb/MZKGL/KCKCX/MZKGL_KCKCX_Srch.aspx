<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_KCKCX_Srch.aspx.cs" Inherits="BF.CrmWeb.MZKGL.KCKCX.MZKGL_KCKCX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_MZKGL_MZKBGKC_CX%>';
    </script>
    <script src="MZKGL_KCKCX_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">库存卡号</div>
            <div class="bffld_right">
                <input id="TB_KCKHM" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">售卡编号</div>
            <div class="bffld_right">
                <input id="TB_SKBH" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">保管人</div>
            <div class="bffld_right">
                <input id="TB_BGRMC" type="text" />
                <input id="HF_BGR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">状态</div>
            <div class="bffld_right">
                <input id="rd_JK" type="radio" tabindex="3" name="status" style="width: 40px" value="0" />建卡
                                        <input id="rd_LY" type="radio" tabindex="3" name="status" style="width: 40px" value="1" />领用
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">是否验卡</div>
            <div class="bffld_right">
                <input id="rd_WYK" type="radio" tabindex="3" name="bj_yk" style="width: 25px; text-wrap: none" value="0" />未验卡
                <input id="rd_YYK" type="radio" tabindex="3" name="bj_yk" style="width: 25px; text-wrap: none" value="1" />已验卡
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="1" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDDM" type="text" tabindex="2" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="zHF_BGDDDM" type="hidden" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
