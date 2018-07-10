<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_RWCL_Srch.aspx.cs" Inherits="BF.CrmWeb.KFPT.RWCL.KFPT_RWCL_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_KFPT_RWCL%>';
    </script>
    <script src="KFPT_RWCL_Srch.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">是否处理</div>
            <div class="bffld_right" style="width: auto">
                <input class="magic-radio" type="radio" name="sfzx" id="R_ZXALL" value="all" checked="checked" />
                <label for="R_ZXALL">全部</label>
                <input class="magic-radio" type="radio" name="sfzx" id="R_WZX" value="1" />
                <label for="R_WZX">已执行未处理</label>
                <input class="magic-radio" type="radio" name="sfzx" id="R_YZX" value="2" />
                <label for="R_YZX">已处理</label>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right twodate">
                <input id="TB_KSRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_KSRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right twodate">
                <input id="TB_JSRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_JSRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">任务主题</div>
            <div class="bffld_right">
                <select id="S_RWZT">
                    <option></option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">评语人</div>
            <div class="bffld_right">
                <input id="TB_PYRMC" type="text" />
                <input id="HF_PYR" type="hidden" />
                <input id="zHF_PYR" type="hidden" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
