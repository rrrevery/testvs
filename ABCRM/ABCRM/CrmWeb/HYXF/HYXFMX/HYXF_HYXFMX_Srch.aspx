<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_HYXFMX_Srch.aspx.cs" Inherits="BF.CrmWeb.HYXF.HYXFMX.HYXF_HYXFMX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    
    <%=V_Head_Search %>
    <script>

        var DJLX = GetUrlParam("DJLX");
        if (DJLX == 1) {
            vPageMsgID = '<%=CM_HYKGL_THJLCX %>';

        }
        else {
            vPageMsgID = '<%=CM_HYXF_HYXFMXCX %>';
        }
        tp_condition = '<%=tp_condition%>';
        tp_conditionOne = '<%=tp_conditionOne%>';
    </script>
    <script src="HYXF_HYXFMX_Srch.js"></script>
    
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" />
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
            <div class="bffld_left">收款台</div>
            <div class="bffld_right">
                <input id="TB_SKTNO" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">小票号码</div>
            <div class="bffld_right">
                <input id="TB_XPHM" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">折扣金额</div>
            <div class="bffld_right">
                <input id="TB_ZKJE" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">消费金额</div>
            <div class="bffld_right">
                <input id="TB_XFJE1" type="text" style="width: 45%; float: left;"/>
                <span class="Wdate_span">至</span>
                <input id="TB_XFJE2" type="text" style="width: 45%; float: left;"/>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">消费积分</div>
            <div class="bffld_right">
                <input id="TB_XFJF1" type="text" style="width: 45%; float: left;"/>
                <span class="Wdate_span">至</span>
                <input id="TB_XFJF2" type="text" style="width: 45%; float: left;"/>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">日期</div>
            <div class="bffld_right">
                <input id="TB_RQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;"  />
                <span class="Wdate_span">至</span>
                <input id="TB_RQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;"  />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
