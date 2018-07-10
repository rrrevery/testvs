<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="
    SelectKCKNoType.aspx.cs" Inherits="BF.CrmWeb.CrmLib.SelectKCK.SelectKCKNoType" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="SelectKCKNoType.js"></script>
    <style>
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
        <div>
            <div class="bfrow" style="margin-top: 10px;">
                <div class="bffld">
                    <div class="bffld_left">开始卡号</div>
                    <div class="bffld_right">
                        <input id="TB_KSKH" type="text" />
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">结束卡号</div>
                    <div class="bffld_right">
                        <input id="TB_JSKH" type="text" />
                    </div>
                </div>
            </div>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">数量</div>
                    <div class="bffld_right">
                        <input id="TB_SL" type="text" />
                    </div>
                </div>
                <div class="bffld">
                </div>
            </div>
            <div style="clear: both;"></div>
            <div class="btn-group">
                <input id="B_Search" type="button" value="查询" class="btn" />
                <input id="B_Confirm" type="button" value="确定" class="btn" />
                <input id="B_Cancel" type="button" value="取消" class="btn" />
                <%--                    <input id="B_AllSelect" type="button" value="全部选择" class="btn" />
                    <input id="B_AllCancel" type="button" value="全部取消" class="btn" />--%>
            </div>
            <table id="list"></table>
            <div id="pager"></div>
        </div>

        <div id="menuContentHYKTYPE" class="menuContent">
            <ul id="TreeHYKTYPE" class="ztree"></ul>
        </div>
    </form>
</body>
</html>
