﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmArt_ListLP.aspx.cs" Inherits="BF.CrmWeb.CrmArt.ListLP.CrmArt_ListLP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title></title>
    <%=V_Head_WebArtList%>
    <script>
        vPageMsgID = <%=CM_LPGL_JFFHLPDY%>;
    </script>
    <script src="CrmArt_ListLP.js"></script>
</head>
<body style="height: auto; overflow-y: hidden">
    <div id="MainPanel" class="bfbox zero_padding">
        <%=V_ArtToolBar%>
        <div class="bfrow">
            <div class="bffld_art">
                <div class="bffld_left">礼品代码</div>
                <div class="bffld_right">
                    <input id="TB_LPDM" type="text" />

                </div>
            </div>
            <div class="bffld_art">
                <div class="bffld_left">礼品名称</div>
                <div class="bffld_right">
                    <input id="TB_LPMC" type="text" />
                </div>
            </div>
        </div>
        <table id="list"></table>
    </div>
</body></html>
