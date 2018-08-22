<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_GHKLX_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.GHKLX.HYKGL_GHKLX_Srch" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_HYKGHKLX%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYKGL_HYKGHKLX_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYKGL_HYKGHKLX_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_HYKGL_HYKGHKLX_CX%>');
    </script>
    <script src="HYKGL_GHKLX_Srch.js"></script>
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
        <div class="bffld" style="display:none">
            <div class="bffld_left">操作地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="zHF_BGDDDM" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">原卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKHM_OLD" type="text" tabindex="2" />
            </div>

        </div>
        <div class="bffld">
            <div class="bffld_left">新卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKHM_NEW" type="text" tabindex="3" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">原卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME_OLD" type="text" tabindex="4" />
                <input id="HF_HYKTYPE_OLD" type="hidden" />
                <input id="zHF_HYKTYPE_OLD" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">新卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME_NEW" type="text" tabindex="4" />
                <input id="HF_HYKTYPE_NEW" type="hidden" />
                <input id="zHF_HYKTYPE_NEW" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">积分</div>
            <div class="bffld_right">
                <input id="TB_JF" type="text" tabindex="4" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">金额</div>
            <div class="bffld_right">
                <input id="TB_JE" type="text" tabindex="4" />

            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" type="text" tabindex="6" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">证件号码</div>
            <div class="bffld_right">
                <input id="TB_SFZBH" type="text" tabindex="6" />
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
</body>
</html>

