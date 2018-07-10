<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXSQJLCX_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXSQJLCX.GTPT_WXXSQJLCX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXSQJLCX%>';
    </script>
    <script src="GTPT_WXSQJLCX_Srch.js"></script>
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
        <div class="bffld">
            <div class="bffld_left">微信号</div>
            <div class="bffld_right">
                <input id="TB_WX_NO" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYK_NO" type="text" />
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
            <div class="bffld_left">优惠券规则</div>
            <div class="bffld_right">
                <input id="TB_GZMC" type="text" />
                <input type="hidden" id="HF_GZID" />
                <input type="hidden" id="zHF_GZID" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
            </div>
        </div>
    </div>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" />
                <input type="hidden" id="HF_HYKTYPE" />
                <input type="hidden" id="zHF_HYKTYPE" />
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

    <%=V_SearchBodyEnd %>
</body>
</html>
