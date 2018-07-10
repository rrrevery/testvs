<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_NEWSDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.NEWSDY.GTPT_NEWSDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_NEWSDY %>';
    </script>
    <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_NEWSDY.js"></script>
    <script src='../../../Js/KindEditor/kindeditor.js'></script>
    <script src="../../../Js/KindEditor/lang/zh-CN.js"></script>

</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">名称</div>
            <div class="bffld_right">
                <input id="TB_NAME" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow" style="display:none">
        <div class="bffld">
            <div class="bffld_left">图文标识</div>
            <div class="bffld_right">
                <input id="TB_MEDIA_ID" type="text" />
            </div>
        </div>
    </div>
    <div style="clear: both;"></div>



    <fieldset id="fieldtw" class="rule_ask" style="height: auto">
        <legend>图文消息</legend>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">标题</div>
                <div class="bffld_right">
                    <input id="TB_TITLE" type="text" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">缩略图</div>
                <div class="bffld_right">
                    <input id="TB_THUMB_TITLE" type="text" />
                    <input id="TB_THUMB_MEDIA_ID" type="hidden" />
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">作者</div>
                <div class="bffld_right">
                    <input id="TB_AUTHOR" type="text" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">图文消息摘要</div>
                <div class="bffld_right">
                    <input id="TB_DIGEST" type="text" />
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">是否显示封面</div>
                <div class="bffld_right">
                    <input id="CB_BJ_COVER" type="checkbox" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">原文地址</div>
                <div class="bffld_right">
                    <input id="TB_YWURL" type="text" />
                </div>
            </div>
        </div>

        <div class="bfrow" style="height:200px">
            <div class="bffld_l">
                <div class="bffld_left">内容</div>
                <div class="bffld_right">
                    <textarea id="TA_CONTENT"></textarea>
                </div>
            </div>
        </div>
    </fieldset>

            <div class="bfrow bfrow_table">
            <table id="list" style="border: thin"></table>
        </div>

        <div id="tb" class="item_toolbar">
            <span style="float: left"></span>
            <button id="AddItem" type='button' class="item_addtoolbar">添加图文</button>
            <button id="DelItem" type='button' class="item_deltoolbar">删除图文</button>
        </div>
    <%=V_InputBodyEnd %>
    <script>
        KindEditor.ready(function (K) {
            window.editor = K.create('#TA_CONTENT', { uploadJson: "../UpLoadMediaToWXServer.ashx?type=newsimg&PUBLICIF=" + sWXPIF });

        });
    </script>
</body>
</html>
