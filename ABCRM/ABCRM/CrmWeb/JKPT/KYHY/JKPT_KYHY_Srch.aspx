<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JKPT_KYHY_Srch.aspx.cs" Inherits="BF.CrmWeb.JKPT.KYHY.JKPT_KYHY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script src="JKPT_KYHY_Srch.js"></script>
    <script>
        vPageMsgID = '<%=CM_JKPT_KYHY%>';
    </script>
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
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">可疑消费日期</div>
            <div class="bffld_right">
                <input id="TB_XFRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">可疑消费年月</div>
            <div class="bffld_right">
                <input id="TB_XFNY" type="text" class="Wdate" onfocus="WdatePicker({dateFmt:'yyyyMM'})" />
            </div>
        </div>
    </div>
    <div class="bffld">
        <div class="bffld_left">单据状态</div>
        <div class="bffld_right">
            <input class="magic-radio" type="radio" name="djzt" id="R_QB" value="0" />
            <label for="R_QB">全部</label>
            <input class="magic-radio" type="radio" name="djzt" id="R_ZX" value="2" />
            <label for="R_ZX">已审核</label>
            <input class="magic-radio" type="radio" name="djzt" id="R_BC" value="1" />
            <label for="R_BC">未审核</label>
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
            <div class="bffld_right twodate">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
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
            <div class="bffld_right twodate">
                <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">备注</div>
            <div class="bffld_right">
                <input id="TB_BZ" type="text" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
