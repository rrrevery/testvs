<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_SHJFGZ.aspx.cs" Inherits="BF.CrmWeb.HYXF.SHJFGZ.HYXF_SHJFGZ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_HYXF_SHJFGZ%>';
    </script>

    <script src="HYXF_SHJFGZ.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap">名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_MC" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">金额</div>
            <div class="bffld_right">
                <input type="text" id="TB_JE" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">积分</div>
            <div class="bffld_right">
                <input type="text" id="TB_JF" />
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>

