<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_SYDHDY_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXSYDHDY.GTPT_SYDHDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = <%=CM_GTPT_WXSYDHDY%>;
    </script>
    <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_SYDHDY_Srch.js?ts=1"></script>


</head>
<body>

    <%=V_SearchBodyBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">导航编号</div>
            <div class="bffld_right">
                <input id="TB_DHID" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">导航名称</div>
            <div class="bffld_right">
                <input id="TB_DHMC" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" name="CB_TY" id="C_N" value="0" />
                <label for="C_N">否</label>
                <input class="magic-checkbox" type="checkbox" name="CB_TY" id="C_Y" value="1" />
                <label for="C_Y">是</label>
                <input id="DH_DHTY" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">导航类型</div>
            <div class="bffld_right">
                <select id="DDL_DHLX">
                    <option></option>
                    <option value="0">首页导航</option>
                    <option value="1">会员专区页导航</option>
                </select>
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
