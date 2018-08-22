<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_JCBQGZDY_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.JCBQGZDY.HYKGL_JCBQGZDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_HYJCBQGZDY%>';
    </script>
    <script src="HYKGL_JCBQGZDY_Srch.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员标签</div>
            <div class="bffld_right">
                <input id="TB_HYBQ" type="text" />
                <input id="HF_HYBQ" type="hidden" />
                <input id="zHF_HYBQ" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">状态</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="RD_ZT" id="R_ALL" value="1" checked="checked" />
                <label for="R_ALL">全部</label>
                <input class="magic-radio" type="radio" name="RD_ZT" id="R_YX" value="0" />
                <label for="R_YX">有效</label>
                <input class="magic-radio" type="radio" name="RD_ZT" id="R_TY" value="-1" />
                <label for="R_TY">停用</label>
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
    <%=V_SearchBodyEnd %>
</body>
</html>
