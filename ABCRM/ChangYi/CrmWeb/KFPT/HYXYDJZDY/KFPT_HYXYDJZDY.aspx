<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_HYXYDJZDY.aspx.cs" Inherits="BF.CrmWeb.KFPT.HYXYDJZDY.KFPT_HYXYDJZDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script>
        vPageMsgID = '<%=CM_KFPT_HYXYDJZDY%>';
    </script>
    <script src="KFPT_HYXYDJZDY.js"></script>
</head>
<body>
    <%=V_InputListBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">信用等级名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_XYDJMC" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">信用等级颜色</div>
            <div class="bffld_right">
                <input type="text" id="TB_XYDJYS" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">分析周期</div>
            <div class="bffld_right">
                <input type="text" id="TB_FXZQ" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">退货次数</div>
            <div class="bffld_right">
                <input type="text" id="TB_THCS" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">同专柜消费次数</div>
            <div class="bffld_right">
                <input type="text" id="TB_XFCS_TGZ" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">同楼层消费品牌数量</div>
            <div class="bffld_right">
                <input type="text" id="TB_PPS_TLC" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">备注</div>
            <div class="bffld_right">
                <input type="text" id="TB_BZ" />
            </div>
        </div>
    </div>

    <%=V_InputListEnd %>
</body>
</html>
