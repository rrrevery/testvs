<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_YHQFFGZ_Srch.aspx.cs" Inherits="BF.CrmWeb.YHQGL.YHQFFGZ.YHQGL_YHQFFGZ_Srch" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_YHQGL_YHQDEFFFGZ%>';
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="YHQGL_YHQFFGZ_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">规则编号</div>
            <div class="bffld_right">
                <input id="TB_GZDM" type="text" />
                <input id="HF_BJ_SF" value="0" type="hidden" />
                <%-- BJ_SF=0 表示是该条记录是发放规则 --%>
            </div>
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
            <div class="bffld_left">门店</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">发放限额</div>
            <div class="bffld_right">
                <input id="TB_FFXE" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">起点金额</div>
            <div class="bffld_right">
                <input id="TB_QDJE" type="text" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>

