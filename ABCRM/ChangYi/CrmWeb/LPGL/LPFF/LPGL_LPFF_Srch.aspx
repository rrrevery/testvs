<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LPGL_LPFF_Srch.aspx.cs" Inherits="BF.CrmWeb.LPGL.LPFF.LPGL_LPFF_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script src="LPGL_LPFF_Srch.js"></script>
    <script>
        var vGZLX = GetUrlParam("GZLX") || 0;
        //vJFLX = GetUrlParam("JFLX") || 0;
        var vCaption;
        if (vGZLX == "5") {
            vPageMsgID = '<%=CM_LPGL_LPFF_SR %> ';
            vCaption = "生日礼品发放";
        }
        if (vGZLX == "1") {
            vPageMsgID = '<%= CM_LPGL_LPFF_SS%> ';
            vCaption = "首刷礼品发放";
        }
        if (vGZLX == "2") {
            vPageMsgID = '<%=CM_LPGL_LPFF_BK%> ';
            vCaption = "办卡礼品发放";
        }
        if (vGZLX == "3") {
            vPageMsgID = '<%= CM_LPGL_LPFF_JFFL%> ';
            vCaption = "积分礼品发放";
        }
        if (vGZLX == "4") {
            vPageMsgID = '<%= CM_LPGL_LPFF_LD%> ';
            vCaption = "来店礼品发放";
        }
    </script>

</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" tabindex="1" />
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">发放地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" tabindex="2" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="zHF_BGDDDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" />
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
            <div class="bffld_left">审核人</div>
            <div class="bffld_right">
                <input id="TB_ZXRMC" type="text" />
                <input id="HF_ZXR" type="hidden" />
                <input id="zHF_ZXR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">审核日期</div>
            <div class="bffld_right">
                <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">冲正人</div>
            <div class="bffld_right">
                <input id="TB_CZRMC" type="text" />
                <input id="HF_CZR" type="hidden" />
                <input id="zHF_CZR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">冲正日期</div>
            <div class="bffld_right">
                <input id="TB_CZRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_CZRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>

    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
