<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXCJDDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXCJDYD.GTPT_WXCJDDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vDJLX = GetUrlParam("djlx");
        if (vDJLX == "2") {
            vPageMsgID = '<%=CM_GTPT_WXCJDYD%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXCJDYD_LR %>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXCJDYD_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXCJDYD_CX%>');
            bCanStart = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXCJDYD_QD%>');
            bCanStop = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXCJDYD_ZZ%>');
            vCaption = "微信大转盘定义单";
        }
        if (vDJLX == "1") {
            vPageMsgID = '<%=CM_GTPT_WXQHBDYD%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXQHBDYD_LR %>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXQHBDYD_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXQHBDYD_CX%>');
            bCanStart = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXQHBDYD_QD%>');
            bCanStop = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXQHBDYD_ZZ%>');
            vCaption = "微信抢红包定义单";
        }
        if (vDJLX == "3") {
            vPageMsgID = '<%=CM_GTPT_WXGGKDYD%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXQHBDYD_LR %>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXQHBDYD_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXQHBDYD_CX%>');
            bCanStart = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXQHBDYD_QD%>');
            bCanStop = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXQHBDYD_ZZ%>');
            vCaption = "微信刮刮卡定义单";
        }
    </script>
        <script src="../../CrmLib/CrmLib_BillWX.js"></script>
     <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXCJDDY.js"></script>

</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">发放规则</div>
            <div class="bffld_right">
                <input id="TB_GZMC" type="text" />
                <input type="hidden" id="HF_GZID" />
                <input type="hidden" id="zHF_GZID" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right">
                <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">领奖有效期</div>
            <div class="bffld_right">
                <input id="TB_YXQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <font color='red'>注：有效期用于奖品后台领取及查询</font>
        </div>
    </div>

    <div style="clear: both;"></div>
    <div id="zMP1" class="common_menu_tit slide_down_title">
        <span>提示</span>
    </div>
    <div id="zMP1_Hidden" class="maininput bfborder_bottom">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">日期</div>
                <div class="bffld_right">
                    <input id="TB_RQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">-------</div>
                <div class="bffld_right">
                    <input id="TB_RQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                </div>
            </div>

        </div>
        <div class="bfrow">
             <div class="bffld">
                <div class="bffld_left">限制数量</div>
                <div class="bffld_right">
                    <input id="TB_XZSL" type="text" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">微信提示</div>
                <div class="bffld_right">
                    <input id="TB_WXTS" type="text" />

                </div>
            </div>
        </div>
        <div class="bfrow bfrow_table">
            <table id="list" style="border: thin"></table>
        </div>

        <div id="tb" class="item_toolbar">
            <span style="float: left"></span>
            <button id="AddItem" type='button' class="item_addtoolbar">添加提示</button>
            <button id="DelItem" type='button' class="item_deltoolbar">删除提示</button>
        </div>
    </div>

        <div style="clear: both;"></div>
    <div id="zMP2" class="common_menu_tit slide_down_title">
        <span>门店</span>
    </div>
    <div id="zMP2_Hidden" class="maininput bfborder_bottom">
        <div class="bfrow bfrow_table">
            <table id="listMD" style="border: thin"></table>
        </div>

        <div id="listMD_tb" class="item_toolbar">
            <span style="float: left"></span>
            <button id="AddMD" type='button' class="item_addtoolbar">添加门店</button>
            <button id="DelMD" type='button' class="item_deltoolbar">删除门店</button>
        </div>
        </div>
    <%=V_InputBodyEnd %>

</body>
</html>
