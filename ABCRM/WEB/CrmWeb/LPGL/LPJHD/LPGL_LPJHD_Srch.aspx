<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LPGL_LPJHD_Srch.aspx.cs" Inherits="BF.CrmWeb.LPGL.LPJHD.LPGL_LPJHD_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        var DJLX = GetUrlParam("iDJLX");
        if (DJLX==0){
            vPageMsgID = <%=CM_LPGL_JFFHLPJHD%>;
            bCanEdit = CheckMenuPermit(iDJR,  <%=CM_LPGL_JFFHLPJHD_LR%>);
            bCanExec = CheckMenuPermit(iDJR,  <%=CM_LPGL_JFFHLPJHD_SH%>);
            bCanSrch = CheckMenuPermit(iDJR,  <%=CM_LPGL_JFFHLPJHD_CX%>);
        }
        else if (DJLX==1){
            vPageMsgID = <%=CM_LPGL_JFFHLPTHD%>;
            bCanEdit = CheckMenuPermit(iDJR,  <%=CM_LPGL_JFFHLPTHD_LR%>);
            bCanExec = CheckMenuPermit(iDJR,  <%=CM_LPGL_JFFHLPTHD_SH%>);
            bCanSrch = CheckMenuPermit(iDJR,  <%=CM_LPGL_JFFHLPTHD_CX%>);
        }
    </script>
    <script src="LPGL_LPJHD_Srch.js"></script>
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
            <div class="bffld_left">供货商</div>
            <div class="bffld_right">
                <input id="TB_GHSMC" type="text" tabindex="2" />
                <input id="HF_GHSID" type="hidden" />
                <input id="zHF_GHSID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">礼品地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" tabindex="3" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="zHF_BGDDDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">进价金额</div>
            <div class="bffld_right">
                <input id="TB_JJJE" type="text" tabindex="4" />
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
