<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LPGL_LPKCBDJL_Srch.aspx.cs" Inherits="BF.CrmWeb.LPGL.LPKCBDJL.LPGL_LPKCBDJL_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>vPageMsgID = '<%=CM_LPGL_LPKCBDJL%>';</script>
    <script src="LPGL_LPKCBDJL_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">礼品名称</div>
            <div class="bffld_right">
                <input id="TB_LPMC" type="text" />
                <input id="HF_LPID" type="hidden" />
                <input id="zHF_LPID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="zHF_BGDDDM" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">处理类型</div>
            <div class="bffld_right">
                <select id="DDL_CLLX">
                    <option></option>
                    <option value="0">进货</option>
                    <option value="1">拨出</option>
                    <option value="2">拨入</option>
                    <option value="3">退货</option>
                    <option value="4">发放</option>
                    <option value="5">作废</option>
                    <option value="6">损益</option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">处理时间</div>
            <div class="bffld_right">
                <input id="TB_RQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_RQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
