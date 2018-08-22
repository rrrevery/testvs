<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="defczqx.aspx.cs" Inherits="BF.PopupPage.defczqx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../CrmWeb/CrmLib/CrmLib_Common.js"></script>
</head>
<body>
    <iframe id="ccc" src="" height="340" width="755" frameborder="0"></iframe>
</body>
<script type="text/javascript">
    var personid = GetUrlParam("personid");
    var ck = GetUrlParam("ck");
    //window.location.href = "../CrmWeb/CRMGL/CRMCZQX/CRMGL_CRMCZQX.aspx?personid=" + personid;
    document.getElementById("ccc").src = "../CrmWeb/CRMGL/CRMCZQX/CRMGL_CRMCZQX.aspx?czy=1&personid=" + personid + "&ck=" + ck;
</script>
</html>
