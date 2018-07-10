<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LPGL_LPDY_Srch.aspx.cs" Inherits="BF.CrmWeb.LPGL.LPDY.LPGL_LPDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = <%=CM_LPGL_JFFHLPDY%>;
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="LPGL_LPDY_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">礼品代码</div>
            <div class="bffld_right">
                <input id="TB_LPDM" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">礼品名称</div>
            <div class="bffld_right">
                <input id="TB_LPMC" type="text" tabindex="3" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">礼品规格</div>
            <div class="bffld_right">
                <input id="TB_LPGG" type="text" tabindex="3" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">礼品单价</div>
            <div class="bffld_right">
                <input id="TB_LPDJ" type="text" tabindex="3" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">礼品进价</div>
            <div class="bffld_right">
                <input id="TB_LPJJ" type="text" tabindex="3" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记人</div>
            <div class="bffld_right">
                <input id="TB_DJRMC" type="text" />
                <input id="HF_DJR" type="hidden" />
                <input id="zHF_DJR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">登记时间</div>
            <div class="bffld_right">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">修改人</div>
            <div class="bffld_right">
                <input id="TB_XGRMC" type="text" />
                <input id="HF_XGR" type="hidden" />
                <input id="zHF_XGR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">修改时间</div>
            <div class="bffld_right">
                <input id="TB_XGSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_XGSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
