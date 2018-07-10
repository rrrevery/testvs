<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JKPT_YJGZDEF.aspx.cs" Inherits="BF.CrmWeb.JKPT.YJGZDEF.JKPT_YJGZDEF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
<%--    <script src="../../CrmLib/CrmLib_BillInput.js"></script>--%>
    <script type="text/javascript">
        vPageMsgID = '<%=CM_JKPT_YJGZDY%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_JKPT_YJGZDY%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_JKPT_YJGZDY%>');
    </script>
    <script src="JKPT_YJGZDEF.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">预警类型</div>
            <div class="bffld_right">
                <select id="DDL_YJLX" name="yjlx">
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">周期类型</div>
            <div class="bffld_right">
                <select id="DDL_ZQLX" name="zqlx">
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">指标类型</div>
            <div class="bffld_right">
                <select id="DDL_ZBLX" name="zblx">
                    <%--                    <option></option>
                    <option value="1" selected="selected">积分</option>
                    <option value="2">消费次数</option>--%>
                </select>
            </div>
        </div>
    </div>
    <%--    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">日类型</div>
            <div class="bffld_right">
                <select id="DDL_TYPE" name="type">
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">预警级别</div>
            <div class="bffld_right">
                <select id="DDL_YJJB" name="yjjb">
                </select>
            </div>
        </div>
    </div>--%>
    <%--    <div class="bfrow">
        <div class="bffld">
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_ZBS2" type="text" tabindex="2" />
            </div>
        </div>
    </div>--%>

    <div class="bfrow">
        <div class="bffld">
            <%--            <div class="bffld_left">最大预警会员</div>
            <div class="bffld_right">
                <input id="TB_HYSL" type="text" tabindex="3" />
            </div>--%>
            <div class="bffld_left">指标数</div>
            <div class="bffld_right">
                <input id="TB_ZBS" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input type="checkbox" id="CB_BJ_TY" class="magic-checkbox" />
                <label for="CB_BJ_TY"></label>
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
