<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_LQXQDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.LQXQDY.CRMGL_LQXQDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_XQDY%>';
    </script>
    <script src="CRMGL_LQXQDY.js"></script>
<%--    <script src="../../CrmLib/CrmLib_GetData.js"></script>--%>
    <script src="../../CrmLib/CrmLib_FillHYQY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">小区名称</div>
            <div class="bffld_right">
                <input id="TB_XQMC" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">所属区域</div>
            <div class="bffld_right">
                <input id="TB_QYMC" type="text" />
                <input id="HF_QYID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">小区代码</div>
            <div class="bffld_right">
                <input id="TB_XQDM" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">小区户数</div>
            <div class="bffld_right">
                <input id="TB_XQHS" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">所属商圈</div>
            <div class="bffld_right">
                <input id="TB_SQMC" type="text" />
                <input id="HF_SQID" type="hidden" />
                <input id="zHF_SQID" type="hidden" />

            </div>
        </div>
    </div>

    <div class="bfrow">

        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" name="CB_BJTY" id="CB_BJ_TY" value="" />
                <label for="CB_BJ_TY"></label>
            </div>
        </div>

        <div class="bffld">
            <div class="bffld_left">复制标记</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" name="CB_BJCOPY" id="CB_BJ_COPY" value="" />
                <label for="CB_BJ_COPY"></label>
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>

    <div id="menuContent" class="menuContent">
        <ul id="TreeQY" class="ztree"></ul>
    </div>

</body>

</html>
