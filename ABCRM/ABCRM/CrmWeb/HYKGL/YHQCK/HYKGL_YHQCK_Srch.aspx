<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_YHQCK_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.YHQCK.HYKGL_YHQCK_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_YHQZHCKCL%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYKGL_YHQZHCKCL_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYKGL_YHQZHCKCL_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_HYKGL_YHQZHCKCL_CX%>');
    </script>
    <script src="HYKGL_YHQCK_Srch.js"></script>
    <%--    <script src="../../CrmLib/CrmLib_GetData.js"></script>--%>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" name="" />
            </div>
        </div>
        <div class="bffld" style="display:none">
            <div class="bffld_left">操作门店</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" tabindex="3" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" name="" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input id="TB_YHQ" type="text" tabindex="3" />
                <input id="HF_YHQ" type="hidden" />
                <input id="zHF_YHQ" type="hidden" />

            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">促销活动</div>
            <div class="bffld_right">
                <input id="TB_CXHD" type="text" tabindex="3" />
                <input id="HF_CXHD" type="hidden" />
                <input id="zHF_CXHD" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">存款金额</div>
            <div class="bffld_right">
                <input id="TB_CKJE" type="text" name="" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">原金额</div>
            <div class="bffld_right">
                <input id="TB_YJE" type="text" name="" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker()" tabindex="9" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" type="text" />
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
    <%=V_SearchBodyEnd %>
    <div id="menuContent" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
</body>
</html>
