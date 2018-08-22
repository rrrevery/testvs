<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_ASKFSTJMZKCQK.aspx.cs" Inherits="BF.CrmWeb.MZKGL.ASKFSTJMZKCQK.MZKGL_ASKFSTJMZKCQK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>vPageMsgID = '<%=CM_MZKGL_ASKFSTJMZKCQK%>';</script>
    <script src="MZKGL_ASKFSTJMZKCQK.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">审核人</div>
            <div class="bffld_right">
                <input id="TB_ZXRMC" type="text" />
                <input id="HF_ZXR" type="hidden" />
                <input id="zHF_ZXR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">审核日期</div>
            <div class="bffld_right twodate">
                <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">查询范围</div>
                <input class="magic-radio" type="radio" name="RD_CXFW" id="R_ALL" value="0" checked="checked" />
                <label for="RD_ALL">全部</label>
                <input class="magic-radio" type="radio" name="RD_CXFW" id="RD_CK" value="1" />
                <label for="RD_CK">存款信息</label>
                <input class="magic-radio" type="radio" name="RD_CXFW" id="R_QK" value="2" />
                <label for="R_QK">取款信息</label>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
