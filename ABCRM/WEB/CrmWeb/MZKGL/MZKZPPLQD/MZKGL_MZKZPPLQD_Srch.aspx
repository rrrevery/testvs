<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKZPPLQD_Srch.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKZPPLQD.MZKGL_MZKZPPLQD_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%= CM_MZKGL_MZKZPPLQD%>';
    </script>
    <script src="MZKGL_MZKZPPLQD_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">售卡编号</div>
            <div class="bffld_right">
                <input id="TB_SKJLBH" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">售卡地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="zHF_BGDDDM" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">面值金额</div>
            <div class="bffld_right">
                <input id="TB_MZJE" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">实收金额</div>
            <div class="bffld_right">
                <input id="TB_SSJE" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO_Begin" type="text"/>
            </div>

        </div>
        <div class="bffld">
            <div class="bffld_left">结束卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO_End" type="text" />
            </div>

        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记时间</div>
            <div class="bffld_right">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
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
    <%=V_SearchBodyEnd %>
</body>
</html>
