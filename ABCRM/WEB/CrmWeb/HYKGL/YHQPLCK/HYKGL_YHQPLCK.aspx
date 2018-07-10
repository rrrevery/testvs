<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_YHQPLCK.aspx.cs" Inherits="BF.CrmWeb.HYKGL.YHQPLCK.HYKGL_YHQPLCK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <link href="../../../Css/CRM/style.css" rel="stylesheet" />
    <script src="../../../Js/jquery.tabify.js"></script>
    <script src="../../../Js/plupload.full.min.js"></script>
    <script src="../../../Js/zh_CN.js"></script>
    <script>
        vPageMsgID = '<%=CM_HYKGL_YHQZHPLCKCL%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYKGL_YHQZHPLCKCL_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYKGL_YHQZHPLCKCL_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_HYKGL_YHQZHPLCKCL_CX%>');
    </script>
    <script src="../../CrmLib/CrmLib_BaseImport.js"></script>
    <script src="HYKGL_YHQPLCK.js"></script>
    <script src="../../CrmLib/CrmLib_FillBGDD.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../CrmLib/CrmLib_FillHYKTYPE.js"></script>
    <style>
        .mini-buttonedit-border {
            border: solid 1px #a5acb5;
            display: block;
            position: relative;
            overflow: hidden;
            padding-right: 30px;
        }

        .mini-buttonedit-input {
            border: 0;
            float: left;
        }

        .mini-buttonedit-buttons {
            position: absolute;
            width: 30px;
            height: 100%;
        }
    </style>

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
                <input id="TB_YHQ" type="text" tabindex="3" />
                <input id="HF_YHQ" type="hidden" />
                <input id="zHF_YHQ" type="hidden" />
                <input id="HF_FS_YQMDFW" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">促销活动</div>
            <div class="bffld_right">
                <input type="text" id="TB_CXHD" />
                <input type="hidden" id="HF_CXID" />
                <input type="hidden" id="zHF_CXID" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">存款金额</div>
            <div class="bffld_right">
                <input id="TB_YHQCZJE" type="text" onkeyup="check()" />
                <input type="button" class="bfbtn btn_trim" id="BTN_TZJE" />
            </div>

        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSSJ" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">用券范围</div>
            <div class="bffld_right">
                <input id="TB_YQFWMC" type="text" tabindex="3" />
                <input id="HF_YQFWDM" type="hidden" />
                <input id="zHF_YQFWDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">操作地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" name="" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>
    </div>
    <%--    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">
            </div>
            <button id="AddItem" tabindex="100" style="height: 28px; width: 70px;" type='button' class="btn-dynamic">添加卡</button>
            <button id="DelItem" tabindex="100" style="height: 28px; width: 70px;" type='button' class="btn-dynamic">删除卡</button>
            <input id="B_Import" type="button" class="btn-dynamic" value="导入卡" />
        </div>
    </div>

                    <div id="tab_1" class="tabcontent">
                        <table id="list"></table>
                        <div id="pager"></div>
                    </div>--%>
    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">卡段信息</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加卡段</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除卡段</button>
        <%--  <button id="B_Import" type='button' class="item_deltoolbar">导入卡</button>--%>
    </div>
    <%=V_InputBodyEnd %>
    <div id="menuContent" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
    <div id="menuContentHYKTYPE" class="menuContent">
        <ul id="TreeHYKTYPE" class="ztree"></ul>
    </div>
</body>
</html>
