<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_XTCSDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.XTCSDY.CRMGL_XTCSDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .DLL {
            background-color: gray;
        }
    </style>
    <%=V_Head_InputList %>
    <script>
        vPageMsgID = '<%=CM_CRMGL_XTCSDY%>';

    </script>
    <script src="CRMGL_XTCSDY.js"></script>
</head>
<body>
    <%=V_InputListBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">参数名称</div>
            <div class="bffld_right">
                <select id="DDL_CSLX" no_control="true">
                    <option no_control="true" value="510" selected="selected">CRM系统</option>
                    <option no_control="true" value="0">系统框架</option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow" style="display: none">
        <div class="bffld" id="jlbh">
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">参数名称</div>
            <div class="bffld_right">
                <label id="LB_CSMC"></label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">当前值</div>
            <div class="bffld_right">
                <input id="TB_CURVAL" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow" style="display: none;">
        <div class="bffld">
            <div class="bffld_left">最小值</div>
            <div class="bffld_right">
                <input id="TB_MINVAL" type="text" readonly="true" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">最大值</div>
            <div class="bffld_right">
                <input id="TB_MAXVAL" type="text" readonly="readonly" />
            </div>
        </div>
    </div>

    <div class="bfrow" style="display: none;">
        <div class="bffld">
            <div class="bffld_left">缺省值</div>
            <div class="bffld_right">
                <input id="TB_DEFVAL" type="text" readonly="true" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">数据类型</div>
            <div class="bffld_right">
                <input id="TB_SJLX" type="text" readonly="readonly" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <label id="LB_ZY"></label>
            </div>
        </div>
    </div>
    <%=V_InputListEnd %>
</body>
</html>
