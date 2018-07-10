<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreviewDialog.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXQFXXDY.PreviewDialog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_WebArt%>
</head>
<body style="height: auto; overflow-y: hidden">
    <div id="MainPanel" class="bfbox zero_padding">
        <%--        <%=V_ArtToolBar%>--%>
        <div class="bfrow">
            <div class="bffld_art">
                <div class="bffld_left">会员卡号</div>
                <div class="bffld_right">
                    <input id="TB_HYKNO" type="text" />
                </div>
            </div>
            <div class="bffld_art">
                <div class="bffld_left"></div>
                <div class="bffld_right">
                    <input type="button" id="B_Confirm" value="确定" class="bfbut bfblue" />
                </div>
            </div>
        </div>
    </div>


</body>

<script type="text/javascript">
    $("#B_Confirm").click(function () {
        var wxno = $("#TB_HYKNO").val();
        if (wxno == "") {
            ShowMessage("不能为空");
        }
        else {
            $.dialog.data("dialogValue", $("#TB_HYKNO").val());
            art.dialog.close();
        }
    })
</script>
</html>
