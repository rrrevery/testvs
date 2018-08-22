<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_JFTZD.aspx.cs" Inherits="BF.CrmWeb.HYXF.JFTZD.HYXF_JFTZD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = '<%=CM_HYXF_JFTZ%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYXF_JFTZ_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYXF_JFTZ_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, <%=CM_HYXF_JFTZ_CX%>);
    </script>
    <script src="HYXF_JFTZD.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" onblur="GetHYXX();" />
                <input id="HF_HYID" type="hidden" />
            </div>
        </div>
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
        <div class="bffld">
            <div class="bffld_left">收款台号</div>
            <div class="bffld_right">
                <input id="TB_SKTNO" type="text" />
            </div>
        </div>
    </div>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">交易号</div>
            <div class="bffld_right">
                <input id="TB_XSJYBH" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>
        <div id="tb" class="item_toolbar">
        <span style="float: left">交易明细</span>
        <button id="AddItem" type='button' class="bftoolbtn fa fa-search">查询</button>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
