<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXQDSJFGZDY_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXQDSJFGZDY.GTPT_WXQDSJFGZDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title></title>

        <%=V_Head_Search %>

    <script>
        vPageMsgID = <%=CM_GTPT_WXQDGZ%>
        bCanEdit = CheckMenuPermit(iDJR, <%=CM_GTPT_WXQDGZ_LR %>);
        bCanExec = CheckMenuPermit(iDJR, <%=CM_GTPT_WXQDGZ_SH%>);
        bCanSrch = CheckMenuPermit(iDJR, <%=CM_GTPT_WXQDGZ_CX%>);
        bCanStart = CheckMenuPermit(iDJR, <%=CM_GTPT_WXQDGZ_QD%>);
        bCanStop =  CheckMenuPermit(iDJR, <%=CM_GTPT_WXQDGZ_ZZ%>);
    </script>
    <script src="GTPT_WXQDSJFGZDY_Srch.js?ts=1"></script>
  

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

    </div>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right">
                <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_DJSJ2\')}'} )" tabindex="8" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" tabindex="9" />
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
                <input id="TB_ZXRMC" type="text" />
                <input id="HF_ZXR" type="hidden" />
                <input id="zHF_ZXR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">执行日期</div>
            <div class="bffld_right">
                <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">启动人</div>
            <div class="bffld_right">
                <input id="TB_DQRMC" type="text" />
                <input id="HF_QDR" type="hidden" />
                <input id="zHF_QDR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">启动日期</div>
            <div class="bffld_right">
                <input id="TB_QDSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_QDSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">终止人</div>
            <div class="bffld_right">
                <input id="TB_ZZRMC" type="text" />
                <input id="HF_ZZR" type="hidden" />
                <input id="zHF_ZZR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">终止日期</div>
            <div class="bffld_right">
                <input id="TB_ZZSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZZSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>


    <div class="bffld_l">
        <div class="bffld_left">单据状态</div>
        <div class="bffld_right">
            <input type="checkbox" name="CB_STATUS" value="0" class="magic-checkbox" id="C_BC" />
            <label for="C_BC">已保存</label>
            <input type="checkbox" name="CB_STATUS" value="1" class="magic-checkbox" id="C_SH" />
            <label for="C_SH">已审核</label>
            <input type="checkbox" name="CB_STATUS" value="2" class="magic-checkbox" id="C_QD" />
            <label for="C_QD">已启动</label>
            <input type="checkbox" name="CB_STATUS" value="-1" class="magic-checkbox" id="C_ZZ" />
            <label for="C_ZZ">已终止</label>
            <input type="hidden" id="HF_STATUS" />
        </div>
    </div>




    <%=V_SearchBodyEnd %>
</body>
</html>
