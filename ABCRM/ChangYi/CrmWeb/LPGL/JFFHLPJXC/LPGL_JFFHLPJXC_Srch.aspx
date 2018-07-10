<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LPGL_JFFHLPJXC_Srch.aspx.cs" Inherits="BF.CrmWeb.LPGL.JFFHLPJXC.LPGL_JFFHLPJXC_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_LPGL_JFFHLPMX_CX%>';
    </script>
    <script src="LPGL_JFFHLPJXC_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">礼品代码</div>
            <div class="bffld_right">
                <input id="TB_LPDM" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">礼品名称</div>
            <div class="bffld_right">
                <input id="TB_LPMC" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">进货数量</div>
            <div class="bffld_right">
                <input id="TB_JHSL" type="text" name="" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">期初数量</div>
            <div class="bffld_right">
                <input id="TB_QCSL" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">拨入数量</div>
            <div class="bffld_right">
                <input id="TB_BRSL" type="text" name="" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">拨出数量</div>
            <div class="bffld_right">
                <input id="TB_BCSL" type="text" />
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
            <div class="bffld_left">汇总日期</div>
            <div class="bffld_right">
                <input id="TB_RQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_RQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
