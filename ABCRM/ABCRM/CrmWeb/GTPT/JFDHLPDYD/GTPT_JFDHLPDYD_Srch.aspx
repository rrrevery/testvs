<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_JFDHLPDYD_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.JFDHLPDYD.GTPT_JFDHLPDYD_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <%=V_Head_Search %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../CrmLib/CrmLib_BillWX.js"></script>

    <script>
        vPageMsgID = '<%=CM_GTPT_JFDHLPDYD%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_JFDHLPDYD_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_JFDHLPDYD_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_JFDHLPDYD_QD%>');
        bCanStart = CheckMenuPermit(iDJR, '<%=CM_GTPT_JFDHLPDYD_ZZ%>');
        bCanStop = CheckMenuPermit(iDJR, '<%=CM_GTPT_JFDHLPDYD_CX%>');
    </script>
    <script src="GTPT_JFDHLPDYD_Srch.js"></script>

</head>
<body>
    <%=V_SearchBodyBegin %>


    <%--                                <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">选择公众号</div>
                                    <div class="bffld_right">
                                        <input id="TB_PUBLICNAME" type="text" tabindex="1" />
                                    </div>
                                </div>
                                <div class="bffld">
                                </div>
                            </div>--%>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>



    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right">
                <input id="TB_KSRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_KSRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_JSRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
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
        <div class="bffld">
            <div class="bffld_left">启动人</div>
            <div class="bffld_right">
                <input id="TB_QDRMC" type="text" />
                <input id="HF_QDR" type="hidden" />
                <input id="zHF_QDR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">启动时间</div>
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

    <div class="bfrow">
        <div class="bffld" style="width: 800px">
            <div class="bffld_left" style="width: 11%">单据状态</div>
            <div class="bffld_right" style="width: 89%">
                <input class="magic-radio" type="radio" name="STATUS" id="all" value="all" checked="checked"   />
                <label for='all'>全部</label>
                <input class="magic-radio" type="radio" name="STATUS" id="R_BC" value="0" />
                <label for="R_BC">未审核</label>
                <input class="magic-radio" type="radio" name="STATUS" id="R_SH" value="1" />
                <label for="R_SH">已审核</label>
                <input class="magic-radio" type="radio" name="STATUS" id="R_QD" value="2" />
                <label for="R_QD">已启动</label>
                <input class="magic-radio" type="radio" name="STATUS" id="R_ZZ" value="3" />
                <label for="R_ZZ">已终止</label>
            </div>
        </div>
    </div>


    <%=V_SearchBodyEnd %>
</body>
</html>
