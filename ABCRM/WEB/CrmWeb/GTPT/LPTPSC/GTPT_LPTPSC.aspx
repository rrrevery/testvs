<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_LPTPSC.aspx.cs" Inherits="BF.CrmWeb.GTPT.LPTPSC.GTPT_LPTPSC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>vPageMsgID = '<%=CM_GTPT_LPTPSC%>';</script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src='../../../Js/KindEditor/kindeditor.js'></script>
    <script src="GTPT_LPTPSC.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">礼品名称</div>
            <div class="bffld_right">
                <input id="TB_LPMC" type="text" tabindex="1" />
                <input id="HF_LPID" type="hidden" />
                <input id="zHF_LPID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">礼品全称</div>
            <div class="bffld_right">
                <input id="TB_LPQC" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">微信兑换积分</div>
            <div class="bffld_right">
                <input id="TB_WXDHJF" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">类型</div>
            <div class="bffld_right">
                <select id="DDL_LX">
                    <option></option>
                    <option value="0">普通礼品 </option>
                    <option value="1">微信常驻积分兑换礼品</option>
                </select>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">上传图片 </div>
            <div class="bffld_right">
                <input id="TB_IMG" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">上传logo图片 </div>
            <div class="bffld_right">
                <input id="TB_PIC_URL" type="text" />
            </div>
        </div>
    </div>


    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">礼品介绍</div>
            <div class="bffld_right">
                <textarea id="TA_LPJS"></textarea>
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
    <script>
        KindEditor.ready(function (K) {
            window.editor = K.create('#TA_LPJS');
        });
    </script>
</body>
</html>
