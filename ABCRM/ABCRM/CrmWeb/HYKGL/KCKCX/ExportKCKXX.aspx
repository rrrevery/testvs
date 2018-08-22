<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExportKCKXX.aspx.cs" Inherits="BF.CrmWeb.DE.ExportKCKXX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script type="text/javascript">
        vDbName = GetUrlParam("old");
        //$("#HF_DbName").val(vDbName);//写在后面...

        function onHYKClick(e, treeId, treeNode) {
            $("#TB_HYKNAME").val(treeNode.name);
            $("#HF_HYKTYPE").val(treeNode.id);
            hideMenu("menuContentHYKTYPE");
        }
    </script>
    <script src="../../CrmLib/CrmLib_FillHYKTYPE.js"></script>

    <style>
        body {
            background-color: white;
        }

        .bfrow {
            width: 500px;
        }

        .bffld_right input[type=text] {
            width: 150px;
        }

        .bffld, .bffld {
            width: 250px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 20px;">
            <asp:HiddenField ID="HF_DbName" runat="server" Value="" />
        </div>
        <div>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">开始卡号</div>
                    <div class="bffld_right">
                        <%--<input id="TB_KSKH" type="text" />--%>
                        <asp:TextBox ID="TB_KSKH" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">结束卡号</div>
                    <div class="bffld_right">
                        <%--<input id="TB_JSKH" type="text" />--%>
                        <asp:TextBox ID="TB_JSKH" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">卡类型</div>
                    <div class="bffld_right">
                        <%--<input id="TB_HYKNAME" type="text" />--%>
                        <asp:TextBox ID="TB_HYKNAME" runat="server"></asp:TextBox>
                        <input id="HF_HYKTYPE" type="hidden" runat="server" />
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">面值</div>
                    <div class="bffld_right">
                        <%--<input id="TB_MZ" type="text" />--%>
                        <asp:TextBox ID="TB_MZ" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div style="clear: both;"></div>
            <div class="btn-group">
                <asp:Button ID="B_Export" runat="server" Text="导出" OnClick="B_Export_Click" CssClass="btn" />
                <%--        <asp:Button ID="Button1" runat="server" Text="导出2" OnClick="Button1_Click" />
        <input id="B_Export2" type="button" value="导出3"/>--%>
            </div>
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>

        <div id="menuContentHYKTYPE" class="menuContent">
            <ul id="TreeHYKTYPE" class="ztree"></ul>
        </div>

    </form>
</body>
<script type="text/javascript">
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", "menuContentHYKTYPE");
    if (vDbName == "0")
        $("#HF_DbName").val("CRMDB");
    else
        $("#HF_DbName").val("CRMDBOLD");
</script>
</html>
