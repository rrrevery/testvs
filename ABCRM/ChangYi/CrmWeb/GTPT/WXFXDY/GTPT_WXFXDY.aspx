<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXFXDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXFXDY.GTPT_WXFXDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../../Js/plupload.full.min.js"></script>
    <script src="../../../Js/zh_CN.js"></script>
    <script src="../../../Js/jquery.form.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXFXDY.js"></script>
    <script>vPageMsgID = '<%=CM_GTPT_WXFXDY%>';</script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
            <input id="HF_JLBH" type="hidden" />

        </div>
        <div class="bffld">
            <div class="bffld_left">名称</div>
            <div class="bffld_right">
                <input id="TB_NAME" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">分享标题</div>
            <div class="bffld_right">
                <input id="TB_TITLE" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">分享描述</div>
            <div class="bffld_right">
                <input id="TB_DESCRIBE" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">分享链接</div>
            <div class="bffld_right">
                <input id="TB_URL" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">&nbsp;&nbsp;&nbsp;</div>
            <div class="bffld_right">
                <font color='red'>注：URL的格式为http://www.baidu.com</font>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">禁用分享标记</div>
            <div class="bffld_right">
                <input type="checkbox" id="CB_BJ_SHARE" class="magic-checkbox" />
                <label for="CB_BJ_SHARE"></label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">上传图片 </div>
            <div class="bffld_right">
                <input id="TB_IMG" type="text" tabindex="2" />
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
