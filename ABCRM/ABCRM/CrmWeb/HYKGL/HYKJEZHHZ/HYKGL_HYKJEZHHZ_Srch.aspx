<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKJEZHHZ_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKJEZHHZ.HYKGL_HYKJEZHHZ_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_JEZHHZ%>';
    </script>
    <script src="HYKGL_HYKJEZHHZ_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow" >
        <div class="bffld" id="RQ">
            <div class="bffld_left">日期</div>
            <div class="bffld_right twodate">
                <input id="TB_RQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_RQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
         <div class="bffld" id="MD">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right" >
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
        <div class="bffld" id="SKT">
            <div class="bffld_left">收款台</div>
            <div class="bffld_right">
                <input id="TB_SKTNO" type="text" />
            </div>
        </div>
    </div>
     <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">查询方式</div>
            <div class="bffld_right">
                <input type="radio" value="0" name="DJLX" checked="checked" class="magic-radio" id="MDHZ" />
                <label for="MDHZ">按门店汇总</label>
                <input type="radio" value="1" name="DJLX" class="magic-radio" id="SKTHZ" />
                <label for="SKTHZ">按收款台汇总</label>
                <input type="radio" value="2" name="DJLX" class="magic-radio" id="MDMX" />
                <label for="MDMX">门店明细</label>
                <input type="radio" value="3" name="DJLX" class="magic-radio" id="SKTMX" />
                <label for="SKTMX">收款台明细</label>
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
