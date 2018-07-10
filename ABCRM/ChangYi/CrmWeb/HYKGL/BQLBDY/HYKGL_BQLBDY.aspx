<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_BQLBDY.aspx.cs" Inherits="BF.CrmWeb.HYKGL.BQLBDY.HYKGL_BQLBDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_BQLBDY%>'
    </script>
    <script src="HYKGL_BQLBDY.js"></script>
</head>
<body>
    <%=V_InputListBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">标签类别名称</div>
            <div class="bffld_right">
                <input id="TB_BQMC" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" name="CB_TY" id="CB_TY" value="" />
                <label for="CB_TY"></label>
            </div>
        </div>
    </div>
    <%=V_InputListEnd %>
</body>
</html>
