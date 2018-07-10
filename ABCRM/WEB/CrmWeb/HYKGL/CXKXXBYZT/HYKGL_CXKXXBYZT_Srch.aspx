<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_CXKXXBYZT_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.CXKXXBYZT.HYKGL_CXKXXBYZT_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_SrchKXXByZT%>';
    </script>
    <script src="HYKGL_CXKXXBYZT_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡属性</div>
            <div class="bffld_right" style="white-space: nowrap;">
                <input class="magic-radio" type="radio" name="CB_TABNAME" id="C_GKK" value="HYK_HYXX" checked="checked" />
                <label for="C_GKK">顾客卡</label>
                <input class="magic-radio" type="radio" name="CB_TABNAME" id="C_KCK" value="HYKCARD" />
                <label for="C_KCK">库存卡</label>
                <input type="hidden" id="HF_TABNAME" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>

        <div class="bffld">
            <div class="bffld_left">有效期</div>
            <div class="bffld_right">
                <input id="TB_YXQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_YXQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡状态</div>
            <div class="bffld_right" style="white-space: nowrap;">
                <input class="magic-radio" type="radio" name="CB_STATUS" id="C_A" value="" checked="checked" />
                <label for="C_A">全部卡</label>
                <input class="magic-radio" type="radio" name="CB_STATUS" id="C_YX" value="0" />
                <label for="C_YX">有效卡</label>
                <input class="magic-radio" type="radio" name="CB_STATUS" id="C_SM" value="3" />
                <label for="C_SM">睡眠卡</label>
                <input class="magic-radio" type="radio" name="CB_STATUS" id="C_YC" value="4" />
                <label for="C_YC">异常卡</label>
                <input class="magic-radio" type="radio" name="CB_STATUS" id="C_WX" value="-1" />
                <label for="C_WX">无效卡</label>
                <input class="magic-radio" type="radio" name="CB_STATUS" id="C_TY" value="-4" />
                <label for="C_TY">停用卡</label>
                <input type="hidden" id="HF_STATUS" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
