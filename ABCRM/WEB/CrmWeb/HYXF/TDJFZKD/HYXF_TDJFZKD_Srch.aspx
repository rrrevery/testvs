<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_TDJFZKD_Srch.aspx.cs" Inherits="BF.CrmWeb.HYXF.TDJFZKD.HYXF_TDJFZKD_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vLX = GetUrlParam("lx");
        if (vLX == "0") {
            vPageMsgID = '<%=CM_HYXF_HYTDJFDYD%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYTDJFDYD_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYTDJFDYD_SH%>');
            vCaption = "会员特定积分定义单";
        } else {
            vPageMsgID = '<%=CM_HYXF_HYTDZKDYD%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYTDZKDYD_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYTDZKDYD_SH%>');
            vCaption = "会员特定折扣定义单";
        }
    </script>
    <script src="HYXF_TDJFZKD_Srch.js"></script>
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
            <div class="bffld_left">门店</div>
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
            <div class="bffld_right twodate">
                <input id="TB_KSRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_KSRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right twodate">
                <input id="TB_JSRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_JSRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
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
    <div class="bfrow">
        <div class="bffld" style="width: 800px">
            <div class="bffld_left" style="width: 11%">单据状态</div>
            <div class="bffld_right" style="width: 89%">
                <input class="magic-radio" type="radio" name="djzt" id="R_ALL" value="10" />
                <label for="R_ALL">全部</label>
                <input class="magic-radio" type="radio" name="djzt" id="R_BC" value="0" />
                <label for="R_BC">未审核</label>
                <input class="magic-radio" type="radio" name="djzt" id="R_QD" value="2" />
                <label for="R_QD">已启动</label>
                <input class="magic-radio" type="radio" name="djzt" id="R_ZZ" value="3" />
                <label for="R_ZZ">已终止</label>
            </div>
        </div>
    </div>


    <%=V_SearchBodyEnd %>
</body>
</html>
