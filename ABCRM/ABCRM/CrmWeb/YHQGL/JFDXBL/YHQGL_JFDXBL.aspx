<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_JFDXBL.aspx.cs" Inherits="BF.CrmWeb.HYXF.JFDXBL.HYXF_JFDXBL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_YHQGL_DEFJFDXBL%>';
    </script>

    <script src="YHQGL_JFDXBL.js"></script>
    <script src="../../CrmLib/CrmLib_FillHYKTYPE.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="4" />
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">兑换积分</div>
            <div class="bffld_right">
                <input id="TB_DHJF" type="text" tabindex="5" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">兑换金额</div>
            <div class="bffld_right">
                <input id="TB_DHJE" type="text" tabindex="5" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">铺底积分</div>
            <div class="bffld_right">
                <input id="TB_QDJF" type="text" tabindex="5" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right">
                <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_JSRQ\')}'} )" tabindex="8" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" tabindex="9" />
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
    <div id="menuContentHYKTYPE" class="menuContent">
        <ul id="TreeHYKTYPE" class="ztree"></ul>
    </div>
</body>
</html>
