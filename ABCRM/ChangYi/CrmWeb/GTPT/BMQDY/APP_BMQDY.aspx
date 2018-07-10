﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="APP_BMQDY.aspx.cs" Inherits="BF.CrmWeb.APP.BMQDY.APP_BMQDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script type="text/javascript">
        vPageMsgID = '<%=CM_LMSHGL_BMQDY%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_LMSHGL_BMQDY_LR%>');
    </script>
    <script src="APP_BMQDY.js"></script>
</head>
<body>

    <%=V_InputBodyBegin %>

    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">编码券名称</div>
            <div class="bffld_right">
                <input id="TB_BMQMC" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input id="TB_YHQMC" type="text" />
                <input id="HF_YHQID" type="hidden" />
                <input id="zHF_YHQID" type="hidden" />
            </div>

        </div>
        <div class="bffld">
            <div class="bffld_left">描述</div>
            <div class="bffld_right">
                <input id="TB_MS" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input type="checkbox" id="DDL_TY" value="0" />
            </div>
        </div>

    </div>


    <%=V_InputBodyEnd %>
</body>
</html>
