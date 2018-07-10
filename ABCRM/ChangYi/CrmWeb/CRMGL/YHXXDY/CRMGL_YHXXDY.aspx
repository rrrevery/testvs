<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_YHXXDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.YHXXDY.YHXXDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <style type="text/css">
        button {
            margin: 0px 5px 5px 0px;
            border-radius: 5px;
            border: currentColor;
            border-image: none;
            width: 82px;
            height: 28px;
            font-size: 15px;
            background-color: rgb(60, 148, 210);
        }
    </style>
    <script>
        vPageMsgID = '<%=CM_CRMGL_YHXXDY%>';
    </script>
    <script src="CRMGL_YHXXDY.js"></script>
</head>
<body>
    <%=V_InputListBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">银行名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_YHMC" />
            </div>
        </div>
    </div>

    <%=V_InputListEnd %>
</body>
</html>
