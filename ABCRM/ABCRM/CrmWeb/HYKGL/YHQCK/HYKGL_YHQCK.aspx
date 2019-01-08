<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_YHQCK.aspx.cs" Inherits="BF.CrmWeb.HYKGL.YHQCK.HYKGL_YHQCK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_YHQZHCKCL%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYKGL_YHQZHCKCL_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYKGL_YHQZHCKCL_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_HYKGL_YHQZHCKCL_CX%>');
    </script>
    <script src="HYKGL_YHQCK.js"></script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>

</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" onblur="GetHYXX();" />
                <input id="HF_HYID" type="hidden" />
                <input type="button" id="btn_HYKHM" class="bfbtn btn_search" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input id="TB_YHQ" type="text" tabindex="3" />
                <input id="HF_YHQ" type="hidden" />
                <input id="zHF_YHQ" type="hidden" />
                <input id="HF_FS_YQMDFW" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">促销活动</div>
            <div class="bffld_right">
                <input id="TB_CXHD" type="text" tabindex="3" />
                <input id="HF_CXHD" type="hidden" />
                <input id="zHF_CXHD" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">存款金额</div>
            <div class="bffld_right">
                <input id="TB_CKJE" runat="server" />
                <input id="HF_YJE" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker({minDate:'%y-%M-#{%d+1}'})" />
                <input id="HF_MDFWDM" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">用券范围</div>
            <div class="bffld_right">
                <input id="TB_YQFWMC" type="text" tabindex="3" />
                <input id="HF_YQFWDM" type="hidden" />
                <input id="zHF_YQFWDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">操作地点 </div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
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
    <input type="hidden" id="HF_CZYMDID" />
    <%=V_InputBodyEnd %>
</body>
</html>
