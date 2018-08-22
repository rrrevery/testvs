<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_LQXQDY_Srch.aspx.cs" Inherits="BF.CrmWeb.CRMGL.LQXQDY.CRMGL_LQXQDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_XQDY%>';
    </script>
    <script src="CRMGL_LQXQDY_Srch.js"></script>

</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">小区编号</div>
            <div class="bffld_right">
                <input type="text" id="TB_JLBH" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">小区名称</div>
            <div class="bffld_right">
                <input id="TB_XQMC" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">所属商圈</div>
            <div class="bffld_right">
                <input id="TB_SQMC" type="text" />
                <input id="HF_SQID" type="hidden" />
                <input id="zHF_SQID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">小区代码</div>
            <div class="bffld_right">
                <input id="TB_XQDM" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">所属区域</div>
            <div class="bffld_right">
                <input id="TB_QYMC" type="text" />
                <input id="HF_QYDM" type="hidden" />
                 <input id="zHF_QYDM" type="hidden" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>

</body>

</html>
