<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKSJJGZDY_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKSJJGZDY.HYKGL_HYKSJJGZDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        var vSJ = GetUrlParam("sj");
        if (vSJ == '1')
            vPageMsgID = '<%=CM_HYKGL_HYKDJSQGZDY%>';
        else
            vPageMsgID = '<%=CM_HYKGL_HYKJJGZDY%>';
    </script>
    <script src="HYKGL_HYKSJJGZDY_Srch.js"></script>

</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">原卡类型</div>
            <div class="bffld_right">
                <input type="text" id="TB_HYKNAME_OLD" />
                <input type="hidden" id="HF_HYKTYPE_OLD" />
                <input type="hidden" id="zHF_HYKTYPE_OLD" />
                <input id="HF_BJ_SJ" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">新卡类型</div>
            <div class="bffld_right">
                <input type="text" id="TB_HYKNAME_NEW" />
                <input type="hidden" id="HF_HYKTYPE_NEW" />
                <input type="hidden" id="zHF_HYKTYPE_NEW" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">判断标准</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="CB_BJ_XFJE" id="R_ALL" value="all" checked="checked" />
                <label for="R_ALL">全部</label>
                <input class="magic-radio" type="radio" name="CB_BJ_XFJE" id="R_JF" value="0" />
                <label for="R_JF">积分</label>
                <input class="magic-radio" type="radio" name="CB_BJ_XFJE" id="R_JE" value="1" />
                <label for="R_JE">消费金额</label>
            </div>
        </div>
    </div>
    <%--    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">变级起点积分</div>
            <div class="bffld_right">
                <input type="text" id="TB_QDJF" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">门店</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
    </div>--%>
    <%=V_SearchBodyEnd %>
</body>
</html>
