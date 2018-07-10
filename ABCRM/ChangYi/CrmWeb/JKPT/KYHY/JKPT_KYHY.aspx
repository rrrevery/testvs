<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JKPT_KYHY.aspx.cs" Inherits="BF.CrmWeb.JKPT.KYHY.JKPT_KYHY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script type="text/javascript">
        vPageMsgID = '<%=CM_JKPT_KYHY%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_JKPT_KYHY%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_JKPT_KYHY%>');
    </script>
    <script src="JKPT_KYHY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" />
                <input id="HF_HYID" type="hidden" />
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
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_MDMC" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">新状态</div>
            <input class="magic-radio" type="radio" name="status" id="rd_FKY" value="0" />
            <label for="rd_FKY">非可疑会员</label>
            <input class="magic-radio" type="radio" name="status" id="rd_KY" value="1" />
            <label for="rd_KY">可疑会员</label>
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

    <%=V_InputBodyEnd %>
</body>
</html>
