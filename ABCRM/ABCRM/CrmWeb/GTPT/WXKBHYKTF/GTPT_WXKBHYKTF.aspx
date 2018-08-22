<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXKBHYKTF.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXKBHYKTF.GTPT_WXKBHYKTF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = <%=CM_GTPT_WXKBHYKTF%>;
    </script>
    <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXKBHYKTF.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh" style="display: none">
        </div>
        <div class="bffld">
            <div class="bffld_left">卡包会员卡ID</div>
            <div class="bffld_right">
                <input id="TB_CARD" type="text" />
                <input id="HF_CARD" type="hidden" />
            </div>
        </div>

    </div>
    <fieldset id="thumb" class="rule_ask">
        <legend>二维码投放</legend>
        <div class="bffld_l">
            <font color='red'>二维码投放，点击生成二维码地址，有效时间365天</font>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">生成地址</div>
                <div class="bffld_right">
                    <input type="button" class="bfbut bfblue" value="生成" onclick="ShowQRCODE()" />
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld_l">
                <div class="bffld_left">二维码投放</div>
                <div class="bffld_right">
                    <input id="TB_QRCODEURL" type="text" readonly="readonly" />
                </div>
            </div>
        </div>

    </fieldset>
    <fieldset id="thumb1" class="rule_ask" style="height: 200px;display:none">
        <legend>图文消息投放</legend>
        <div class="bffld_l">
            <font color='red'>图文消息投放，生成的内容填入上传图文素材接口中content字段，即可获取嵌入卡券的图文消息素材</font>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">生成图文消息</div>
                <div class="bffld_right">
                    <input type="button" class="bfbut bfblue" value="生成" onclick="ShowCONTENT()" />
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld_l">
                <div class="bffld_left">图文消息投放</div>
                <div class="bffld_right">
                    <textarea id="TA_CONTENT" cols="35" rows="5" readonly="readonly" style="width: 100%; box-sizing: border-box; resize: none;"></textarea>
                </div>
            </div>
        </div>
    </fieldset>
    <%=V_InputBodyEnd %>
</body>
</html>
