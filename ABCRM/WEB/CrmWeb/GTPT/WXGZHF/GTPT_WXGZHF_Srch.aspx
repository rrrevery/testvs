<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXGZHF_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXGZHF.GTPT_WXGZHF_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=V_Head_Search %>

    <script>
        vTYPE = GetUrlParam("type");
        if (vTYPE == "2") {
            vPageMsgID = '<%=CM_GTPT_WXHF_GZ%>';
            vCaption = "微信关注回复";
        }
        if (vTYPE == "3") {
            vPageMsgID = '<%=CM_GTPT_WXHF_MR%>';
            vCaption = "微信默认回复";
        }
        if (vTYPE == "0") {
            vPageMsgID = '<%=CM_GTPT_WXHF_GJC%>';
            vCaption = "微信关键字回复";
        }
        if (vTYPE == "1") {
            vPageMsgID = '<%=CM_GTPT_WXHF_TS%>';
            vCaption = "微信菜单推送回复";
        }
    </script>

     <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXGZHF_Srch.js?ts=1"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">回复答案ID</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld_l">
            <div class="bffld_left">消息类型</div>
            <div class="bffld_right">
                <input type="checkbox" name="CB_XXLX" value="" checked="checked" class="magic-checkbox" id="C_ALL"/>
                <label for="C_ALL">全部</label>
                <input type="checkbox" name="CB_XXLX" value="1" class="magic-checkbox" id="C_TEXT"/>
                <label for="C_TEXT">文本</label>
                <input type="checkbox" name="CB_XXLX" value="2" class="magic-checkbox"id="C_IMG"/>
                <label for="C_IMG">图片</label>
                <input type="checkbox" name="CB_XXLX" value="3" class="magic-checkbox"id="C_VOICE"/>
                <label for="C_VOICE">语音</label>
                <input type="checkbox" name="CB_XXLX" value="4" class="magic-checkbox"id="C_VIDEO"/>
                <label for="C_VIDEO">视频</label>
                <input type="checkbox" name="CB_XXLX" value="5" class="magic-checkbox"id="C_MUSIC"/>
                <label for="C_MUSIC">音乐</label>
                <input type="checkbox" name="CB_XXLX" value="6" class="magic-checkbox"id="C_IMG-TEXT"/>
                <label for="C_IMG-TEXT">图文</label>
                <input type="hidden" id="HF_XXLX" value="" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
