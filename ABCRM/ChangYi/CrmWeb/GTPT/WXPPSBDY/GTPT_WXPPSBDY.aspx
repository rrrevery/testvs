<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXPPSBDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXPPSBDY.GTPT_WXPPSBDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = <%=CM_GTPT_WXPPSB%>;
    </script>
    <script src="GTPT_WXPPSBDY.js?ts=1"></script>
    <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src='../../../Js/KindEditor/kindeditor.js'></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow" style="display: none">
        <div class="bffld" id="jlbh">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">分类名称</div>
            <div class="bffld_right">
                <input id="TB_FLMC" type="text" tabindex="1" />
                <input id="HF_FLID" type="hidden" />
                <input id="zHF_FLID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">商标名称</div>
            <div class="bffld_right">
                <input id="TB_SBMC" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">电话</div>
            <div class="bffld_right">
                <input id="TB_PHONE" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">网址</div>
            <div class="bffld_right">
                <input id="TB_IP" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">地址</div>
            <div class="bffld_right">
                <input id="TB_DZ" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">优先级</div>
            <div class="bffld_right">
                <input id="TB_INX" type="text" />
            </div>
            <div style="left: 155px; position: relative; line-height: 25px; color: red;">注：正数序号越小，排序越靠前</div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">上传LOGO图片</div>
            <div class="bffld_right">
                <input id="TB_LOGO" type="text" tabindex="2" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">上传主图片</div>
            <div class="bffld_right">
                <input id="TB_IMG" type="text" tabindex="2" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">英文名称</div>
            <div class="bffld_right">
                <input id="TB_YWM" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">品牌说明</div>
            <div class="bffld_right">
                <textarea id="TA_CONTENT"></textarea>
            </div>
        </div>
    </div>

    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>
    <%=V_InputBodyEnd %>
    <script>
        KindEditor.ready(function (K) {
            window.editor = K.create('#TA_CONTENT', { uploadJson: "../../CrmLib/CrmLib.ashx?func=upload&folder=sample1"});
        });
    </script>
</body>
</html>
