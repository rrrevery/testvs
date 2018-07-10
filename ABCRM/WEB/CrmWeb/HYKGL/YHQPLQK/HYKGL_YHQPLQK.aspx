<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_YHQPLQK.aspx.cs" Inherits="BF.CrmWeb.HYKGL.YHQPLQK.HYKGL_YHQPLQK" %>

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
    <script src="HYKGL_YHQPLQK.js"></script>
    <script src="../../CrmLib/CrmLib_BaseImport.js"></script>
    <script>vPageMsgID =<%=CM_HYKGL_YHQZHPLQKCL%></script>
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
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input id="TB_YHQ" type="text" tabindex="3" />
                <input id="HF_YHQ" type="hidden" />
                <input id="zHF_YHQ" type="hidden" />
            </div>
            <input type="hidden" id="HF_CZYMDID" />
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">促销活动</div>
            <div class="bffld_right">
                <input type="text" id="TB_CXHD" />
                <input type="hidden" id="HF_CXID" />
                <input type="hidden" id="zHF_CXID" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">统一取款金额</div>
            <div class="bffld_right">
                <input id="TB_YHQQKJE" type="text" onkeyup="check()" />
                <input type="button" id="BTN_TZJE" class="bfbtn btn_trim" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">结束时间</div>
            <div class="bffld_right">
                <input id="TB_JSSJ" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">操作门店</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
    </div>

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
    <div id="menuContentHYKTYPE" class="menuContent">
        <ul id="TreeHYKTYPE" class="ztree"></ul>
    </div>
</body>
</html>
