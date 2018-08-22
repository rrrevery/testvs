<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportHYDA.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYDALR_NEW.ImportHYDA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_BillInput.js"></script>
    <script>
        $(document).ready(function () {
            //$("#status-bar").hide();
            $("#status-bar .bffld").hide();

        });

        function SetControlState() {
            PageControl(true, false);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="panelwrap">
        <div class="center_content">
            <div class="right_wrap">
                <div id="right_content">
                    <h2>会员档案导入</h2>
                    <div style="display:none;" id="btn-toolbar"></div>
                    <h3>注：（大小不要超过4M）导入文件第一行为标题；第一列 卡号，第二列 姓名，第三列 身份证号，第四列 性别，第五列 手机号码，第六列 地址</h3>
                    <div id="tab1" class="tabcontent">
                        <div id="MainPanel" class="form">
                            <div class="bfrow" style="display:none;">
                                <div class="bffld" id="jlbh">
                                </div>
                                <div class="bffld">

                                </div>

                            </div>

            <table style="width: 800px; border: 0;">
                <tr>
                    <td style="width: 120px;">Excel版本选择</td>
                    <td class="dt2">
                        <asp:RadioButton ID="RB_1" runat="server" Text="Excel2003" Checked="True" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <%--<tr>
    <td class="dt1" >门店</td>
    <td class="dt2">
        <asp:DropDownList ID="DDL_MD" runat="server">
        </asp:DropDownList>
                    </td>
    <td>&nbsp;</td>
  </tr>--%>

                <tr>
                    <td class="dt1">上传路径</td>
                    <td class="dt2">
                        <asp:FileUpload ID="FileUp" runat="server" />
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="上传" />
                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Style="height: 21px" Text="导入" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td></td>
                    <td>导入总行数：<asp:Label ID="LB_GV_ROW" runat="server" Text="" Font-Size="Larger" Font-Bold="true"></asp:Label>
                        <%--导入总金额：<asp:Label ID="LB_GV_JE" runat="server" Text="" Font-Size="Larger" Font-Bold="true"></asp:Label>--%>

                    </td>
                    <td>&nbsp;</td>

                </tr>
                <tr>
                    <td class="dt1">&nbsp;</td>
                    <td>


                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging"
                            Width="700px" AllowPaging="True">
                            <Columns>
                                <asp:BoundField DataField="HYK_NO" HeaderText="会员卡号">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HY_NAME" HeaderText="姓名">
                                    <%--默认值 Center--%>
                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SFZBH" HeaderText="身份证">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SEX" HeaderText="性别">
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                </asp:BoundField>
<%--                                <asp:BoundField DataField="CSRQ" HeaderText="出生日期">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="SJHM" HeaderText="手机号码">
                                    <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TXDZ" HeaderText="通讯地址">

                                    <HeaderStyle Width="200px" />
                                </asp:BoundField>

<%--                                <asp:BoundField DataField="YZBM" HeaderText="邮政编码" />
                                <asp:BoundField DataField="E_MAIL" HeaderText="E_MAIL" />--%>
                            </Columns>
                            <HeaderStyle BackColor="#33CCCC" />
                        </asp:GridView>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3"></td>
                </tr>
            </table>

                        </div>
                        <div id="status-bar"></div>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    </form>
</body>
</html>
