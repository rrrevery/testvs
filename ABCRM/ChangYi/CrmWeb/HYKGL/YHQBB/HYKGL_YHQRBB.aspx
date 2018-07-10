<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_YHQRBB.aspx.cs" Inherits="BF.CrmWeb.HYKGL.YHQBB.HYKGL_YHQRBB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        var vDJLX = IsNullValue(GetUrlParam("djlx"), "0");
        switch (vDJLX) {
            case "0": 
                vPageMsgID = '<%=CM_HYKGL_YHQZHRBB%>'; 
                vCaption = "优惠券日报表";
                break;
            case "1": 
                vPageMsgID = '<%=CM_HYKGL_YHQZHYBB%>';
                vCaption = "优惠券月报表"; 
                break;
            case "2": 
                vPageMsgID = '<%=CM_HYKGL_YHQZHNBB%>';
                vCaption = "优惠券年报表"; 
                break;
            default: break;
        }
    </script>
    <script src="HYKGL_YHQRBB.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="4" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
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
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input id="TB_YHQMC" type="text" />
                <input id="HF_YHQID" type="hidden" />
                <input id="zHF_YHQID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">查询日期</div>
            <div class="bffld_right">
                <input id="TB_CXRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_CXRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
