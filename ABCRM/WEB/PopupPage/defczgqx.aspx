<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="defczgqx.aspx.cs" Inherits="BF.PopupPage.defczgqx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../Js/CommonFunctionCrm.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <iframe id="ccc" src="" height="700" width="800"></iframe>
        <div>
        </div>
    </form>
</body>
<script type="text/javascript">
    var personid = GetUrlParam("personid");
    var ck = GetUrlParam("ck");
    //window.location.href = "../CrmWeb/CRMGL/CRMCZQX/CRMGL_CRMCZQX.aspx?personid=" + personid;
    document.getElementById("ccc").src = "../CrmWeb/CRMGL/CRMCZQX/CRMGL_CRMCZQX.aspx?czy=0&personid=" + personid + "&ck=" + ck;
</script>

</html>
