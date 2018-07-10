<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_YHPOSXX.aspx.cs" Inherits="BF.CrmWeb.CRMGL.YHPOSXX.CRMGL_YHPOSXX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <%=V_Head_InputList %>
    <script>  vPageMsgID = '<%=CM_CRMGL_YHPOSXX%>'; </script>
    <script src="CRMGL_YHPOSXX.js"></script>
    <style>
        .ui-jqgrid-btable .ui-state-highlight {
            background: yellow;
        }
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
        <div class="bffld" id="jlbh">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">收款台号</div>
            <div class="bffld_right">
                <input id="TB_SKTNO" type="text" />
                <input id="HF_SKTNO" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">序列号</div>
            <div class="bffld_right">
                <input id="TB_XLH" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">IP地址</div>
            <div class="bffld_right">
                <input id="TB_IPDZ" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">密钥</div>
            <div class="bffld_right">
                <input id="TB_MY" type="password" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_left">加密标记</div>
        <div class="bffld_right">
            <input id="BJ_JM" type="checkbox" />
        </div>
    </div>
    <%=V_InputListEnd %>
</body>
</html>
