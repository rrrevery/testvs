<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_YHQYEQL.aspx.cs" Inherits="BF.CrmWeb.HYKGL.YHQYEQL.HYKGL_YHQYEQL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_YHQYEQLTZD%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYKGL_YHQYEQLTZD_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYKGL_YHQYEQLTZD_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_HYKGL_YHQYEQLTZD_CX%>');
    </script>
    <script src="HYKGL_YHQYEQL.js"></script>
    <script src="../../CrmLib/CrmLib_FillBGDD.js"></script>
    <script src="../../CrmLib/CrmLib_FillHYKTYPE.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>

</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input id="TB_YHQMC" type="text" />
                <input id="HF_YHQID" type="hidden" />
                <input id="zHF_YHQID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">促销活动</div>
            <div class="bffld_right">
                <input id="TB_CXZT" type="text" />
                <input id="HF_CXID" type="hidden" />
                <input id="zHF_CXID" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" />
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">操作地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
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

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">调整总金额</div>
            <div class="bffld_right">
                <label id="LB_TZJE" runat="server" style="text-align: left"></label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">调整数量</div>
            <div class="bffld_right">
                <label id="LB_TZSL" runat="server" style="text-align: left"></label>
            </div>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div id="zMP3" class="common_menu_tit slide_down_title bfborder_top">
        <span>优惠券账户</span>
        <div id="Div1" class="btn_dropdown">
            <i class="fa fa-angle-down" aria-hidden="true" style="color: white"></i>
        </div>
    </div>
    <div id="zMP3_Hidden" class="maininput">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">有效期范围</div>
                <div class="bffld_right twodate" >
                    <input id="TB_JSSJ1" type="text" onfocus="WdatePicker({isShowWeek:true})" class="Wdate" />
                    <span class="Wdate_span">至</span>
                    <input id="TB_JSSJ2" type="text" onfocus="WdatePicker({isShowWeek:true})" class="Wdate" />
                </div>
            </div>
        </div>
        <div class="bfrow bfrow_table">
            <table id="list" style="border: thin"></table>
        </div>

        <div id="tb" class="item_toolbar">
            <span style="float: left">优惠券账户</span>
            <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
            <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
        </div>
    </div>
    <%=V_InputBodyEnd %>
    <div id="menuContentHYKTYPE" class="menuContent">
        <ul id="TreeHYKTYPE" class="ztree"></ul>
    </div>
    <div id="menuContent" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
</body>
</html>
