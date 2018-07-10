<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_SPSFQJLHZ_Srch.aspx.cs" Inherits="BF.CrmWeb.YHQGL.SPSFQJLHZ.YHQGL_SPSFQJLHZ_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>vPageMsgID = '<%=CM_YHQGL_HZCXJLBySP%>';</script>
    <script src="YHQGL_SPSFQJLHZ_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">商户名称</div>
            <div class="bffld_right">
                <input id="TB_SHMC" type="text" />
                <input id="HF_SHDM" type="hidden" />
                <input id="zHF_SHDM" type="hidden" />
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
            <div class="bffld_left">限定部门</div>
            <div class="bffld_right">
                <input id="TB_SHBMMC" type="text" />
                <input id="HF_SHBMDM" type="hidden" />
                <input id="zHF_SHBMDM" type="hidden" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">商品分类</div>
            <div class="bffld_right">
                <input id="TB_SPFL" type="text" />
                <input id="HF_SPFLID" type="hidden" />
                <input id="zHF_SPFLID" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">商品代码</div>
            <div class="bffld_right">
                <input id="TB_SPDM" type="text" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input id="TB_YHQMC" type="text" />
                <input id="HF_YHQID" type="hidden" />
                <input id="zHF_YHQID" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">促销活动</div>
            <div class="bffld_right">
                <input id="TB_CXZT" type="text" />
                <input id="HF_CXID" type="hidden" />
                <input id="zHF_CXID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">商户品牌</div>
            <div class="bffld_right">
                <input id="TB_SBMC" type="text" />
                <input id="HF_SBID" type="hidden" />
                <input id="zHF_SBID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bffld">
        <div class="bffld_left">统计日期</div>
        <div class="bffld_right twodate">
            <input id="TB_TJRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            <span class="Wdate_span">至</span>
            <input id="TB_TJRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
        </div>
    </div>


    <%=V_SearchBodyEnd %>
</body>
</html>
