<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_JXCDB.aspx.cs" Inherits="BF.CrmWeb.CRMGL.JXCDB.CRMGL_JXCDB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script>
        vPageMsgID = <%=CM_CRMGL_CRMToJXC%>;
    </script>

    <script src="CRMGL_JXCDB.js"></script>
    <style type="text/css">
        input[type='password'] {
            border: 1px #90A9B7 solid;
            width: 300px;
            height: 24px;
            border-radius: 4px;
            -webkit-border-radius: 4px;
            border-radius: 4px;
            -moz-border-radius: 4px;
            background: #f1f4f8;
        }
    </style>
</head>
<body>
    <%=V_InputListBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">门店</div>
            <div class="bffld_right">
                <input type="text" id="TB_MDMC" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">用户名</div>
            <div class="bffld_right">
                <input type="text" id="TB_USERNAME" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">数据库服务名</div>
            <div class="bffld_right">
                <input type="text" id="TB_DBNAME" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">密码</div>
            <div class="bffld_right">
                <input type="password" id="TB_PASSWORD" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">IP地址</div>
            <div class="bffld_right">
                <input id="TB_IPDZ" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">端口号</div>
            <div class="bffld_right">
                <input id="TB_DKH" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">备注</div>
            <div class="bffld_right">
                <input id="TB_BZ" type="text" />
            </div>
        </div>
    </div>

    <%=V_InputListEnd %>
</body>
</html>
