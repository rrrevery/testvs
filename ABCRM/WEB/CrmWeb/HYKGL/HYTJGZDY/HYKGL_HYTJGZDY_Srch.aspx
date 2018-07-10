<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYTJGZDY_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYTJGZDY.HYKGL_HYTJGZDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>vPageMsgID = '<%=CM_HYKGL_HYTJGZ%>'</script>
    <script src="HYKGL_HYTJGZDY_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">促销活动</div>
            <div class="bffld_right">
                <input type="text" id="TB_CXZT" />
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
            <div class="bffld_right twodate">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
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
            <div class="bffld_right twodate">
                <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">启动人</div>
            <div class="bffld_right">
                <input id="TB_QDRMC" type="text" />
                <input id="HF_QDR" type="hidden" />
                <input id="zHF_QDR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">启动时间</div>
            <div class="bffld_right twodate">
                <input id="TB_QDSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_QDSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
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
            <div class="bffld_right twodate">
                <input id="TB_ZZRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZZRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
