<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXQFXXDY_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXQFXXDY.GTPT_WXQFXXDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search%>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXQFXXDY%>';
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXQFXXDY_Srch.js"></script>
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
            <div class="bffld_left">消息类型</div>
            <div class="bffld_right">
                <select id="DDL_TYPE" class="easyui-combobox">
                    <option></option>
                </select>
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
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">单据状态</div>
            <div class="bffld_right">
                <input name="STATUS" value="10" class="magic-radio" type="radio"  id="R_ALL"  />
                <label for="R_ALL">全部</label>
                <input name="STATUS" value="1" class="magic-radio" type="radio" id="R_BC" />
                <label for="R_BC">已保存</label>
                <input name="STATUS" value="2" class="magic-radio" type="radio" id="R_SH" />
                <label for="R_SH">已审核</label>
                <input  name="STATUS" value="3" class="magic-radio" type="radio" id="R_QFSUC" />
                <label for="R_QFSUC">群发成功</label>
                <input name="STATUS" value="4" class="magic-radio" type="radio" id="R_QFFAIL" />
                <label for="R_QFFAIL">群发失败</label>
                <input name="STATUS" value="5" class="magic-radio" type="radio" id="R_DELQF" />
                <label for="R_DELQF">删除群发</label>
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
