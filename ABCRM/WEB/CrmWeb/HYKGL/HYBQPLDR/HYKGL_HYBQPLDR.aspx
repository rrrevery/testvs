<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYBQPLDR.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYBQPLDR.HYKGL_HYBQPLDR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../../Js/plupload.full.min.js"></script>
    <script src="../../../Js/zh_CN.js"></script>
    <script>
        vPageMsgID = '<%=CM_HYKGL_HYBQPLDR%>'
    </script>
    <script src="../../CrmLib/CrmLib_BaseImport.js"></script>
    <script src="HYKGL_HYBQPLDR.js"></script>

</head>
<body>

    <%=V_InputBodyBegin %>
    <div class="bfrow" style="display: none">
        <div class="bffld" id="jlbh">
        </div>
    </div>


    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left"></span>
        <button id="B_Import" type='button' class="item_addtoolbar">导入</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
