<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmArt_ListWXLPFFGZ.aspx.cs" Inherits="BF.CrmWeb.CrmArt.ListWXLPFFGZ.CrmArt_ListWXLPFFGZ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <%=V_Head_WebArtList%>  
    <script>
        vPageMsgID = <%=CM_GTPT_WXLBFFGZ%>;
    </script>

    <script src="CrmArt_ListWXLPFFGZ.js"></script>
</head>
<body style="height: auto; overflow-y: hidden">
    <div id="MainPanel" class="bfbox zero_padding">
        <%=V_ArtToolBar%>
        <div class="bfrow">
            <div class="bffld_art">
                <div class="bffld_left">规则ID</div>
                <div class="bffld_right">
                    <input id="TB_GZID" type="text" />

                </div>
            </div>
            <div class="bffld_art">
                <div class="bffld_left">规则名称</div>
                <div class="bffld_right">
                    <input id="TB_GZMC" type="text" />
                </div>
            </div>
        </div>
        <table id="list"></table>
    </div>
</body></html>
