<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXSHLMLXDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXSHLMLXDY.GTPT_WXSHLMLXDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_LMSHLXDY%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_LMSHLXDY%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_LMSHLXDY%>');
    </script>
     <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXSHLMLXDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">类型名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_LXMC" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">顺序</div>
            <div class="bffld_right">
                 <input id="TB_INX" type="text" />
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
