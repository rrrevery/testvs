<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_TDJFZKD.aspx.cs" Inherits="BF.CrmWeb.HYXF.TDJFZKD.HYXF_TDJFZKD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vDJLX = GetUrlParam("djlx");
        if (vDJLX == "0") {
            vPageMsgID = '<%=CM_HYXF_HYTDJFDYD%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYTDJFDYD_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYTDJFDYD_SH%>');
            bCanStart = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYTDJFDYD_QD%>');
            bCanStop = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYTDJFDYD_ZZ%>');
        }
        else {
            vPageMsgID = '<%=CM_HYXF_HYTDZKDYD%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYTDZKDYD_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYTDZKDYD_SH%>');
            bCanStart = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYTDZKDYD_QD%>');
            bCanStop = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYTDZKDYD_ZZ%>');
        }
    </script>
    <script src="HYXF_TDJFZKD.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <input id="zHF_HYZ" type="hidden" />
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
        <div class="bffld" id="SJJFBS">
            <div class="bffld_left">多倍升级积分</div>
            <div class="bffld_right">         
                 <input class="magic-checkbox" type="checkbox" name="" id="BJ_BQJFBS" value="0" />
                <label for="BJ_BQJFBS"></label>
            </div>
        </div>
    </div>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">日期范围</div>
            <div class="bffld_right">
                <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>

        <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员组</div>
            <div class="bffld_right">
                <input id="TB_HYZMC" type="text" />
                <input id="HF_HYZID" type="hidden" />
                <input id="zHF_HYZID" type="hidden" />
            </div>
        </div>
    </div>
    <div id="tb" class="item_toolbar">
        <span style="float: left">列表</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
    </div>
    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>



    <%=V_InputBodyEnd %>

</body>
</html>
