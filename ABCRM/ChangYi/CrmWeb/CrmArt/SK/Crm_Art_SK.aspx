﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Crm_Art_SK.aspx.cs" Inherits="BF.CrmWeb.CrmArt.SK.Crm_Art_SK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_WebArt%>
    <script src="Crm_Art_SK.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body style="height: auto; overflow-y: hidden">
    <div id="MainPanel" class="bfbox zero_padding">
        <%=V_ArtToolBar%>
        <div class="bfrow" style="margin-top: 10%">
            <div class="bffld_art" style="width: 100%">
                <div class="bffld_right" style="width: 100%;">
                    <input id="TB_CDNR" type="text" placeholder="请刷卡……" style="font-size: 30px; height: 50px;" />
                </div>
            </div>
        </div>
    </div>
</body>
</html>
