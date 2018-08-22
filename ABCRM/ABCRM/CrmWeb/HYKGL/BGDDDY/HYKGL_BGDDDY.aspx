<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_BGDDDY.aspx.cs" Inherits="BF.CrmWeb.HYKGL.BGDDDY.HYKGL_BGDDDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%=V_Head_Tree %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID=<%=CM_HYKGL_HYKBGDDDY%>;
    </script>
    <script src="HYKGL_BGDDDY.js"></script>
</head>
<body>
    <%=V_TreeBodyBegin %>
    <%--<div>
        <div id="TopPanel" class="topbox">
            <div id="location" style="width: 400px; height: 40px; display: block; float: left; line-height: 40px;">
                <div id="switchspace" style="width: 50px; height: 40px; display: block; float: left">
                </div>
            </div>
            <div id="btn-toolbar" style="float: right">
                <div id="more" style="float: right; height: 40px; line-height: 40px; margin-right: 8px; padding-left: 20px;">
                    <i class="fa fa-list-ul fa-lg" aria-hidden="true" style="color: rgb(140,151,157)"></i>
                </div>
            </div>
        </div>
        <div class="bfbox">
            <div class="common_menu_tit">
                <span>会员卡建卡</span>
            </div>
            <div class="maininput2">
                <div id="TreePanel" class="bfblock_left">
                </div>
                <div id="MainPanel" class="bfblock_right">--%>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店</div>
            <div class="bffld_right">
                <input type="text" id="TB_MDMC" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">制卡标记</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="CK_BJ_ZK" value="0" />
                <label for="CK_BJ_ZK"></label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">销售标记</div>
            <div class="bffld_right">
                <input type="checkbox" class="magic-checkbox" id="CK_BJ_XS" value="0" />
                <label for="CK_BJ_XS"></label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input type="checkbox" class="magic-checkbox" id="CB_TY" value="0" />
                <label for="CB_TY"></label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld" id="mjbj">
        </div>
    </div>
    <%--</div>
            </div>
        </div>
    </div>--%>
    <%=V_TreeBodyEnd %>
</body>
</html>
