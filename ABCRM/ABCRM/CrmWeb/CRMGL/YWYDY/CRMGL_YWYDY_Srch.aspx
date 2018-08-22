<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_YWYDY_Srch.aspx.cs" Inherits="BF.CrmWeb.CRMGL.YWYDY.CRMGL_YWYDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = <%=CM_CRMGL_YWYDY%>
        bCanEdit = CheckMenuPermit(iDJR, <%=CM_CRMGL_YWYDY_LR%>);
        bCanSrch = CheckMenuPermit(iDJR, <%=CM_CRMGL_YWYDY_CX%>);
    </script>
    <script src="CRMGL_YWYDY_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">业务员编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" tabindex="1" />
            </div>
        </div>

        <div class="bffld">
            <div class="bffld_left">业务员代码</div>
            <div class="bffld_right">
                <input id="TB_YWYDM" type="text" tabindex="1" />
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">所属门店</div>
            <div class="bffld_right">
                <input type="text" id="TB_MDMC" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">业务员名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_NAME" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
