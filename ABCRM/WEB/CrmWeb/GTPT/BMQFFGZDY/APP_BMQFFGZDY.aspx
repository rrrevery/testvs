<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="APP_BMQFFGZDY.aspx.cs" Inherits="BF.CrmWeb.APP.BMQFFGZDY.APP_BMQFFGZDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script type="text/javascript">
        vPageMsgID = '<%=CM_LMSHGL_BMQFFGZDY%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_LMSHGL_BMQFFGZDY_LR%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_LMSHGL_BMQFFGZDY_CX%>');

    </script>
    <script src="APP_BMQFFGZDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">规则名称</div>
            <div class="bffld_right">
                <input id="TB_GZMC" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">总限制次数</div>
            <div class="bffld_right">
                <input id="TB_XZCS" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">总限制提示</div>
            <div class="bffld_right">
                <input id="TB_XZTS" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" style="font-size: 10px">每日限制次数</div>
            <div class="bffld_right">
                <input id="TB_XZCS_HY_T" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="font-size: 10px">每日限制提示</div>
            <div class="bffld_right">
                <input id="TB_XZTS_HY_R" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld" style="width: 900px;">
            <div class="bffld_left" style="width: 200px;">会员活动期内限制次数 </div>
            <div class="bffld_right">
                <input id="TB_CZCS_HY" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld" style="width: 900px;">
            <div class="bffld_left" style="width: 200px;">会员活动期内限制提示</div>
            <div class="bffld_right">
                <input id="TB_XZTS_HY" type="text" />
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld" style="width: 900px;">
            <div class="bffld_left" style="width: 200px;">会员单日限制次数 </div>
            <div class="bffld_right">
                <input id="TB_XZCS_R" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld" style="width: 900px;">
            <div class="bffld_left" style="width: 200px;">会员单日限制提示</div>
            <div class="bffld_right">
                <input id="TB_XZTS_R" type="text" />
            </div>
        </div>

    </div>


    <%=V_InputBodyEnd %>
</body>
</html>
