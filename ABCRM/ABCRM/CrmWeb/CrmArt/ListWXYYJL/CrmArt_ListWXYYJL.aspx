<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmArt_ListWXYYJL.aspx.cs" Inherits="BF.CrmWeb.CrmArt.ListWXYYJL.CrmArt_ListWXYYJL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <%=V_Head_WebArtList%>  
    <script>
        vPageMsgID = <%=CM_GTPT_WXYYFWJLCX%>;
    </script>
    <script src="CrmArt_ListWXYYJL.js"></script>
</head>
<body style="height: auto; overflow-y: hidden">
    <div id="MainPanel" class="bfbox zero_padding">
        <%=V_ArtToolBar%>
        <div class="bfrow">
            <div class="bffld_art">
                <div class="bffld_left">记录编号</div>
                <div class="bffld_right">
                    <input id="TB_JLBH" type="text" />

                </div>
            </div>
            <div class="bffld_art">
                <div class="bffld_left">顾客姓名</div>
                <div class="bffld_right">
                    <input id="TB_GKXM" type="text" />
                </div>
            </div>
        </div>

        <table id="list"></table>
    </div>
</body>
</html>
