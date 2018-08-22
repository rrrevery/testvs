<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmArt_ListYHQ.aspx.cs" Inherits="BF.CrmWeb.CrmArt.ListYHQ.CrmArt_ListYHQ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_WebArtList%>
    <script>
        vPageMsgID = <%=CM_CRMGL_YHQDY%>;
    </script>
    <script src="CrmArt_ListYHQ.js"></script>
</head>
<body style="height: auto; overflow-y: hidden">
    <div id="MainPanel" class="bfbox zero_padding">
        <%=V_ArtToolBar%>
        <div class="bfrow">
            <div class="bffld_art">
                <div class="bffld_left">优惠券ID</div>
                <div class="bffld_right">
                    <input id="TB_YHQID" type="text" />

                </div>
            </div>
            <div class="bffld_art">
                <div class="bffld_left">优惠券名称</div>
                <div class="bffld_right">
                    <input id="TB_YHQMC" type="text" />
                </div>
            </div>
        </div>
        <table id="list"></table>
    </div>
</body>
</html>
