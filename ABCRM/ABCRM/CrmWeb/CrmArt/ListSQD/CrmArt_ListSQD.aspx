<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmArt_ListSQD.aspx.cs" Inherits="BF.CrmWeb.CrmArt.ListSQD.CrmArt_ListSQD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <%=V_Head_WebArtList%>  
    <script>
        var vCZK = IsNullValue(GetUrlParam("vCZK"), "0");
        if (vCZK == "0")
            vPageMsgID = <%=CM_HYKGL_HYKLYSQ%>;
        else
            vPageMsgID = <%=CM_MZKGL_MZKLYSQ%>;
    </script>
    <script src="CrmArt_ListSQD.js"></script>
</head>
<body style="height: auto; overflow-y: hidden">
    <div id="MainPanel" class="bfbox zero_padding">
        <%=V_ArtToolBar%>
        <div class="bfrow">
            <div class="bffld_art">
                <div class="bffld_left">申请单编号</div>
                <div class="bffld_right">
                    <input id="TB_JLBH" type="text" />
                </div>
            </div>
        </div>
        <table id="list"></table>
    </div>
</body>
</html>
