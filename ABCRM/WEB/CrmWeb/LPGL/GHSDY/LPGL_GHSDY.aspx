<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LPGL_GHSDY.aspx.cs" Inherits="BF.CrmWeb.LPGL.GHSDY.GHSDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script> 
        vPageMsgID = <%=CM_LPGL_GHSDY%>;
    </script>
    <script src="LPGL_GHSDY.js"></script>

</head>
<body>
    <%=V_InputListBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_GHSMC" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">地址</div>
            <div class="bffld_right">
                <input type="text" id="TB_GHSDZ" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">电话</div>
            <div class="bffld_right">
                <input type="text" id="TB_DHHM" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">主营项目</div>
            <div class="bffld_right">
                <input type="text" id="TB_ZYXM" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="CB_BJ_TY" value="" />
                <label for="CB_BJ_TY"></label>
                <input type="hidden" id="HF_BJ_TY" />
            </div>
        </div>
    </div>
    <%=V_InputListEnd %>
</body>
</html>
