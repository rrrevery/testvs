<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_HYHDDY.aspx.cs" Inherits="BF.CrmWeb.KFPT.HYHDDY.KFPT_HYHDDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = <%=CM_KFPT_HYHDDY%>;
    </script>
    <script src="KFPT_HYHDDY.js"></script>
    <style>
        .ui-jqgrid-btable .ui-state-highlight {
            background: yellow;
        }
    </style>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">活动名称</div>
            <div class="bffld_right">
                <input id="TB_HDMC" type="text" />
            </div>
        </div>
    </div>
<%--    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">活动名称</div>
            <div class="bffld_right">
                <input id="TB_HDMC" type="text" maxlength="4" />

            </div>
        </div>
        <div class="bffld">
        </div>
    </div>--%>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">活动内容</div>
            <div class="bffld_right">
                <input id="TB_HDNR" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">预计人数</div>
            <div class="bffld_right">
                <input id="TB_RS" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">活动时间</div>

            <div class="bffld_right">
                <input id="TB_KSSJ" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">--------</div>
            <div class="bffld_right">
                <input id="TB_JSSJ" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">报名时间</div>
            <div class="bffld_right">
                <input id="TB_BM_RQ1" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">--------</div>
            <div class="bffld_right">
                <input id="TB_BM_RQ2" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
    </div>
    <div class="bfrow" style="display: none">
        <div class="bffld">
            <div class="bffld_left">确认时间</div>
            <div class="bffld_right">
                <input id="TB_QR_RQ1" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">--------</div>
            <div class="bffld_right">
                <input id="TB_QR_RQ2" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">参加时间</div>
            <div class="bffld_right">
                <input id="TB_CJ_RQ1" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">--------</div>
            <div class="bffld_right">
                <input id="TB_CJ_RQ2" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">回访时间</div>
            <div class="bffld_right">
                <input id="TB_HF_RQ1" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">--------</div>
            <div class="bffld_right">
                <input id="TB_HF_RQ2" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">评述时间</div>
            <div class="bffld_right">
                <input id="TB_PS_RQ1" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">--------</div>
            <div class="bffld_right">
                <input id="TB_PS_RQ2" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
