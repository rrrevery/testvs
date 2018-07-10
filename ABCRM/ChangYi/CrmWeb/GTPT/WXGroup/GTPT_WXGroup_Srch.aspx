<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXGroup_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXGroup.GTPT_WXGroup_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXGROUP%>';
    </script>
    <script src="GTPT_WXGroup_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">分组编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">分组名称</div>
            <div class="bffld_right">
                <input id="TB_GROUP_NAME" type="text" tabindex="2" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">状态</div>
            <div class="bffld_right">
                <input type="checkbox" name="CB_STATUS" value="" checked="checked" class="magic-checkbox" id="C_A"/>
                <label for="C_A">全部</label>
                <input type="checkbox" name="CB_STATUS" value="0" class="magic-checkbox" id="C_Y"/>
                <label for="C_Y">有效</label>
                <input type="checkbox" name="CB_STATUS" value="-1" class="magic-checkbox" id="C_N"/>
                <label for="C_N">无效</label>
                <input type="hidden" id="HF_STATUS" value="" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">备注</div>
            <div class="bffld_right">
                <input type="text" id="TB_ZY" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
