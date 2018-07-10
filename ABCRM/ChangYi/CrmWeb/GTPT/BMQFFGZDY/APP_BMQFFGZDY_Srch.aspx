<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="APP_BMQFFGZDY_Srch.aspx.cs" Inherits="BF.CrmWeb.APP.BMQFFGZDY.APP_BMQFFGZDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script src="APP_BMQFFGZDY_Srch.js"></script>

    <script>
        vPageMsgID = '<%=CM_LMSHGL_BMQFFGZDY%>';
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_LMSHGL_BMQFFGZDY_CX%>');
    </script>
</head>
<body>
    <%=V_SearchBodyBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">规则ID</div>
            <div class="bffld_right">
                <input id="TB_BMQFFGZID" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">规则名称</div>
            <div class="bffld_right">
                <input id="TB_GZMC" type="text" tabindex="2" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">总限制次数</div>
            <div class="bffld_right">
                <input id="TB_XZCS" type="text" tabindex="3" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">总限制提示</div>
            <div class="bffld_right">
                <input id="TB_XZTS" type="text" tabindex="4" />
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
            <div class="bffld_left">执行人</div>
            <div class="bffld_right">
                <input id="TB_XGRMC" type="text" />
                <input id="HF_XGR" type="hidden" />
                <input id="zHF_XGR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">执行日期</div>
            <div class="bffld_right">
                <input id="TB_XGSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_XGSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>


    <%=V_SearchBodyEnd %>
</body>
</html>
