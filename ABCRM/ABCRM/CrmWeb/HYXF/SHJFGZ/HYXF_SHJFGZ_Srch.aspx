<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_SHJFGZ_Srch.aspx.cs" Inherits="BF.CrmWeb.HYXF.SHJFGZ.HYXF_SHJFGZ_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_HYXF_SHJFGZ%>';
    </script>
    <script src="HYXF_SHJFGZ_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" />
            </div>

        </div>
        <div class="bffld">
            <div class="bffld_left">规则名称</div>
            <div class="bffld_right">
                <input id="TB_MC" type="text" tabindex="2" />
            </div>
        </div>
    </div>
   
    <%=V_SearchBodyEnd %>
</body>
</html>
