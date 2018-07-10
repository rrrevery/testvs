<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXKKZSJFGZDY_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXKKZSJFGZDY.GTPT_WXKKZSJFGZDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vGZLX = GetUrlParam("gzlx");
        if (vGZLX == "1") {
            vPageMsgID = '<%=CM_GTPT_WXKKSFGZ%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXKKSFGZ_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXKKSFGZ_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXKKSFGZ_CX%>');
            bCanStart = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXKKSFGZ_QD%>');
            bCanStop = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXKKSFGZ_ZZ%>');
            vCaption = "微信开卡赠送积分规则定义";
        }
        else {
            vPageMsgID = '<%=CM_GTPT_WXBKSFGZ%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXBKSFGZ_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXBKSFGZ_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXBKSFGZ_CX%>');
            bCanStart = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXBKSFGZ_QD%>');
            bCanStop = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXBKSFGZ_ZZ%>');
            vCaption = "微信绑卡赠送积分规则定义";
        }
    </script>
     <script src="../../CrmLib/CrmLib_BillWX.js"></script>
     <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXKKZSJFGZDY_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin%>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">限制数量</div>
            <div class="bffld_right">
                <input id="TB_XZSL" type="text" tabindex="1" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">领取有效期</div>
            <div class="bffld_right">
                <input id="TB_LJYXQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                 <span class="Wdate_span">至</span>
                <input id="TB_LJYXQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">微信摘要</div>
            <div class="bffld_right">
                <input id="TB_WXZY" type="text" tabindex="1" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right">
                <input id="TB_KSRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_KSRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>

        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_JSRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>

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
            <div class="bffld_left">执行人</div>
            <div class="bffld_right">
                <input id="TB_ZXRMC" type="text" />
                <input id="HF_ZXR" type="hidden" />
                <input id="zHF_ZXR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">执行日期</div>
            <div class="bffld_right">
                <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">启动人</div>
            <div class="bffld_right">
                <input id="TB_DQRMC" type="text" />
                <input id="HF_QDR" type="hidden" />
                <input id="zHF_QDR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">启动日期</div>
            <div class="bffld_right">
                <input id="TB_QDSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_QDSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">终止人</div>
            <div class="bffld_right">
                <input id="TB_ZZRMC" type="text" />
                <input id="HF_ZZR" type="hidden" />
                <input id="zHF_ZZR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">终止日期</div>
            <div class="bffld_right">
                <input id="TB_ZZSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZZSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
