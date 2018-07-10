<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKSJJGZDY.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKSJJGZDY.HYKGL_HYKSJJGZDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        var vSJ = GetUrlParam("sj");
        if (vSJ == '1')
            vPageMsgID = '<%=CM_HYKGL_HYKDJSQGZDY%>';
        else
            vPageMsgID = '<%=CM_HYKGL_HYKJJGZDY%>';
    </script>
    <script src="HYKGL_HYKSJJGZDY.js"></script>
    <script src="../../CrmLib/CrmLib_FillHYKTYPE.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow" style="display: none;">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">原卡类型</div>
            <div class="bffld_right">
                <input type="text" id="TB_HYKNAME_OLD" />
                <input type="hidden" id="HF_HYKTYPE_OLD" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">新卡类型</div>
            <div class="bffld_right">
                <select id="S_HYKNAME_NEW"></select>
                <input type="text" id="TB_HYKNAME_NEW" />
                <input type="hidden" id="HF_HYKTYPE_NEW" />
            </div>
        </div>
    </div>

<%--    <div class="bfrow">
                <div class="bffld">
            <div class="bffld_left">操作门店</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
    </div>--%>



    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">判断标准</div>
            <div class="bffld_right"> 
                <input class="magic-radio" type="radio" name="CB_BJ_XFJE" id="R_JF" value="0" />
                <label for="R_JF">积分</label>
                <input class="magic-radio" type="radio" name="CB_BJ_XFJE" id="R_JE" value="1" />
                <label for="R_JE">消费金额</label>
            </div>
        </div>
        <div class="bffld" id="div_drxfje" style="display:none">
            <div class="bffld_left">起点消费金额</div>
            <div class="bffld_right">
                <input type="text" id="TB_DRXFJE" />
            </div>
        </div>
        <div class="bffld" id="div_qdjf" style="display:none">
            <div class="bffld_left" >起点积分</div>
            <div class="bffld_right">
                <input type="text" id="TB_QDJF" />
            </div>
        </div>
    </div>



    <%=V_InputBodyEnd %>
    <div id="menuContentHYKTYPE_OLD" class="menuContent">
        <ul id="TreeHYKTYPE_OLD" class="ztree"></ul>
    </div>

</body>
</html>
