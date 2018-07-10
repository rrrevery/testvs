<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmArt_ListWXUSER.aspx.cs" Inherits="BF.CrmWeb.CrmArt.ListWXUSER.CrmArt_ListWXUSER" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_WebArtList%>
    <script>
        vPageMsgID = <%=CM_HYKGL_HYKCX%>;
    </script>
    <script src="CrmArt_ListWXUSER.js"></script>
</head>
<body style="height: auto; overflow-y: hidden">
    <div id="MainPanel" class="bfbox zero_padding">
        <%=V_ArtToolBar%>
        <div class="bfrow">
            <div class="bffld_art">
                <div class="bffld_left">关注时间(起始)</div>
                <div class="bffld_right">
                    <input id="TB_DJSJ1" type="text" onfocus="WdatePicker({maxDate:'#F{ $dp.$D(\'TB_DJSJ2\') }'})"  />
                </div>
            </div>
            <div class="bffld_art">
                <div class="bffld_left">关注时间(终止)</div>
                <div class="bffld_right">
                    <input id="TB_DJSJ2" type="text" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'TB_DJSJ1\')}',maxDate:'%y-%M-{%d+1}'})" />
                </div>
            </div>
        </div>

        <div class="bfrow">
            <div class="bffld_art">
                <div class="bffld_left">微粉号</div>
                <div class="bffld_right">
                    <input id="TB_WX_NO" type="text" />
                </div>
            </div>
            <div class="bffld_art">
                 <div class="bffld_left">用户昵称</div>
                <div class="bffld_right">
                    <input id="TB_NICKNAME" type="text" />
                </div>
            </div>
        </div>
        <table id="list"></table>
    </div>
</body>
</html>
