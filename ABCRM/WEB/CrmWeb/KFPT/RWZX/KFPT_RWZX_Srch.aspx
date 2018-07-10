<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_RWZX_Srch.aspx.cs" Inherits="BF.CrmWeb.KFPT.RWZX.KFPT_RWZX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_KFPT_RWZX%>';
    </script>
    <script src="KFPT_RWZX_Srch.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">是否执行</div>
            <div class="bffld_right" style="width: auto">
                <input class="magic-radio" type="radio" name="sfzx" id="R_ZXALL" value="all" checked="checked" />
                <label for="R_ZXALL">全部</label>
                <input class="magic-radio" type="radio" name="sfzx" id="R_WZX" value="0" />
                <label for="R_WZX">未执行</label>
                <input class="magic-radio" type="radio" name="sfzx" id="R_YZX" value="1" />
                <label for="R_YZX">已执行</label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">是否完成</div>
            <div class="bffld_right" style="width: auto">
                <input class="magic-radio" type="radio" name="rwwczt" id="R_ALL" value="all" checked="checked" />
                <label for="R_ALL">全部</label>
                <input class="magic-radio" type="radio" name="rwwczt" id="R_WWC" value="0" />
                <label for="R_WWC">未完成</label>
                <input class="magic-radio" type="radio" name="rwwczt" id="R_YWC" value="1" />
                <label for="R_YWC">已完成</label>
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
            <div class="bffld_left">执行人</div>
            <div class="bffld_right">
                <input id="TB_ZXRMC" type="text" />
                <input id="HF_ZXR" type="hidden" />
                <input id="zHF_ZXR" type="hidden" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
