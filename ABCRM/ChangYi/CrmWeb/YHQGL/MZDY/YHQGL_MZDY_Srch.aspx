<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_MZDY_Srch.aspx.cs" Inherits="BF.CrmWeb.YHQGL.MZDY.YHQGL_MZDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_YHQGL_MZDEF%>';
    </script>
    <script src="YHQGL_MZDY_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">面值编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">面值名称</div>
            <div class="bffld_right">
                <input id="TB_NAME" type="text" tabindex="2" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input type="text" id="TB_YHQMC" />
                <input type="hidden" id="HF_YHQID" />
                <input type="hidden" id="zHF_YHQID" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">金额</div>
            <div class="bffld_right">
                <input id="TB_JE" type="text" tabindex="1" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">停用</div>
            <div class="bffld_right">
                <input type="checkbox" name="CB_BJ_TY" value="0" />未停用
                <input type="checkbox" name="CB_BJ_TY" value="1" />已停用
                <input type="hidden" id="HF_BJ_TY" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
