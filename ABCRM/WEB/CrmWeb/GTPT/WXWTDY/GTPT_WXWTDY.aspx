<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXWTDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXWTDY.GTPT_WXWTDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
        <script src="../../CrmLib/CrmLib_BillWX.js"></script>
     <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXWTDY.js"></script>
    <script>vPageMsgID = '<%=CM_GTPT_WXWTDY%>';</script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">标记</div>
            <div class="bffld_right">
                <input type="radio" value="0" name="TYPE" class="magic-radio" id="gjchf" />
                <label for='gjchf'>关键词回复</label>
                <input type="radio" value="1" name="TYPE" class="magic-radio" id="cdts" />
                <label for='cdts'>菜单推送</label>
                <input type="radio" value="2" name="TYPE" class="magic-radio" id="gzhf" />
                <label for='gzhf'>关注回复</label>
                <input type="radio" value="3" name="TYPE" class="magic-radio" id="mrhf" />
                <label for='mrhf'>默认回复</label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">状态</div>
            <div class="bffld_right">
                <input type="radio" value="1" name="status" class="magic-radio" id="zc" />
                <label for='zc'>正常</label>
                <input type="radio" value="2" name="status" class="magic-radio" id="ty" />
                <label for='ty'>停用</label>
            </div>
        </div>
    </div>
    <div class="bfrow" style="height: 50px">
        <div class="bffld_l">
            <div class="bffld_left">问题 </div>
            <div class="bffld_right">
                <textarea cols="84" rows="2" id="TT_ASK"></textarea>
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
