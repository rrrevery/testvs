<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_HTSFQJLHZ_Srch.aspx.cs" Inherits="BF.CrmWeb.YHQGL.HTSFQJLHZ.YHQGL_HTSFQJLHZ_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>vPageMsgID = '<%=CM_YHQGL_HZCXJLByHT%>';</script>
    <script src="YHQGL_HTSFQJLHZ_Srch.js"></script>
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
            <div class="bffld_left">销售部门</div>
            <div class="bffld_right">
                <input id="TB_XSBMMC" type="text" />
                <input id="HF_XSBMDM" type="hidden" />
                <input id="zHF_XSBMDM" type="hidden" />

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
            <div class="bffld_left">合同号</div>
            <div class="bffld_right">
                <input id="TB_HTH" type="text" />

            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">统计日期</div>
            <div class="bffld_right twodate">
                <input id="TB_TJRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_TJRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
