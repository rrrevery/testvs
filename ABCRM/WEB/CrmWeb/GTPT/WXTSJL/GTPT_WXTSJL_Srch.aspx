<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXTSJL_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXTSJL.GTPT_WXTSJL_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>

    <script>
        vPageMsgID = <%=CM_GTPT_TSCL%>
        bCanEdit = CheckMenuPermit(iDJR, <%=CM_GTPT_TSCL_CL %>);
        bCanExec = CheckMenuPermit(iDJR, <%=CM_GTPT_TSCL_HF%>);
        bCanSrch = CheckMenuPermit(iDJR, <%=CM_GTPT_TSCL_CX%>);      
    </script>
    <script src="GTPT_WXTSJL_Srch.js?ts=1"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" id="jlbh">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记时间</div>
            <div class="bffld_right">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">处理日期</div>
            <div class="bffld_right">
                <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">回访日期</div>
            <div class="bffld_right">
                <input id="TB_HFRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_HFRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>

    </div>


    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">单据状态</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" name="CB_STATUS" id="C_WSH" value="0" />
                <label for="C_WSH">未处理</label>
                <input class="magic-checkbox" type="checkbox" name="CB_STATUS" id="C_YSH" value="1" />
                <label for="C_YSH">已处理</label>
                <input class="magic-checkbox" type="checkbox" name="CB_STATUS" id="C_YQD" value="2" />
                <label for="C_YQD">未回访</label>
                <input class="magic-checkbox" type="checkbox" name="CB_STATUS" id="C_YZZ" value="-1" />
                <label for="C_YZZ">已回访</label>
            </div>
            <input type="hidden" id="HF_STATUS" />
        </div>
    </div>



    <%=V_SearchBodyEnd %>
</body>
</html>
