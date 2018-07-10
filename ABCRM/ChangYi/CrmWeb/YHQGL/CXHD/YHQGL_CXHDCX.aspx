<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_CXHDCX.aspx.cs" Inherits="BF.CrmWeb.YHQGL.CXHD.YHQGL_CXHDCX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = '<%=CM_YHQGL_CXHDZT%>'
    </script>
    <script src="YHQGL_CXHDCX.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">商户</div>
            <div class="bffld_right">
                <input id="TB_SHMC" type="text" />
                <input id="HF_SHDM" type="hidden" />
                <input id="zHF_SHDM" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">名称</div>
            <div class="bffld_right">
                <input id="TB_CXHDMC" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">年份</div>
            <div class="bffld_right">
                <input id="TB_NIAN" type="text" />
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right">
                <input id="TB_KSSJ" type="text" onfocus="WdatePicker()" class="Wdate" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSSJ" type="text" onfocus="WdatePicker()" class="Wdate" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">活动期数</div>
            <div class="bffld_right">
                <input id="TB_CXQS" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">促销内容</div>
            <div class="bffld_right">
                <input id="TB_CXNR" type="text" class="long" />
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
