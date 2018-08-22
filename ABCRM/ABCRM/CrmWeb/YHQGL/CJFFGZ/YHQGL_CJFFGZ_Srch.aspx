<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_CJFFGZ_Srch.aspx.cs" Inherits="BF.CrmWeb.YHQGL.CJFFGZ.YHQGL_CJFFGZ_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_YHQGL_YHQDEFFFGZ_CJ%>';
    </script>
    <script src="YHQGL_CJFFGZ_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">发放规则代码</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" tabindex="1" />
                <input type="hidden" id="HF_BJ_SF" value="3" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">发放规则名称</div>
            <div class="bffld_right">
                <input id="TB_FFGZMC" type="text" tabindex="2" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">抽奖次数上限</div>
            <div class="bffld_right">
                <input id="TB_CJCSSX" type="text" tabindex="2" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">起点金额</div>
            <div class="bffld_right">
                <input id="TB_QDJE" type="text" tabindex="1" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input type="checkbox" name="CB_BJ_TY" value="0" class ="magic-checkbox" id="c_n"/>
                <label for="c_n">否</label>
                <input type="checkbox" name="CB_BJ_TY" value="1" class ="magic-checkbox" id="c_y"/>
                <label for="c_y">是</label>
                <input type="hidden" id="HF_BJ_TY" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
