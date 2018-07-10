<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYMMXG.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYMMXG.HYKGL_HYMMXG" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        var TYPE = GetUrlParam("iTYPE");
        if (TYPE == 0)
            vPageMsgID = '<%=CM_HYKGL_XGPASSWORD %>';
        else
            vPageMsgID = '<%=CM_HYKGL_MMCZ %>';        
    </script>
    <script src="HYKGL_HYMMXG.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../CrmLib/CrmLib_FillBGDD.js"></script>
    <style type="text/css">
        /*input[type='password'] {
            border: 1px #90A9B7 solid;
            width: 300px;
            height: 24px;
            border-radius: 4px;
            -webkit-border-radius: 4px;
            border-radius: 4px;
            -moz-border-radius: 4px;
            background: #f1f4f8;
        }*/
    </style>
</head>
<body>

    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" onblur="GetHYXX();" />
                <input id="HF_HYID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">姓名</div>
            <div class="bffld_right">
                <input id="TB_HYNAME" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">操作地点 </div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" runat="server" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">原密码</div>
            <div class="bffld_right">
                <input id="TB_YMM" type="password" />
                <input id="HF_YMMYZ" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow" id="DV_SFZBH">
        <div class="bffld">
            <div class="bffld_left">身份证号</div>
            <div class="bffld_right">
                <input id="TB_SFZBH" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow" id="DV_MMXG">
        <div class="bffld">
            <div class="bffld_left">新密码</div>
            <div class="bffld_right">
                <input id="TB_XMM" type="password" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">确认密码</div>
            <div class="bffld_right">
                <input id="TB_QRMM" type="password" />
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
    <div id="menuContent" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
</body>
</html>
