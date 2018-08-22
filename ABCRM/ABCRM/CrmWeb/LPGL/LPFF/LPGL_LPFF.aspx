<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LPGL_LPFF.aspx.cs" Inherits="BF.CrmWeb.LPGL.LPFF.LPGL_LPFF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>

    <script>
        //vJFLX = GetUrlParam("JFLX") || 0;
        var vGZLX = GetUrlParam("vGZLX") || 0;
        if (vGZLX == "5") {
            vPageMsgID = '<%= CM_LPGL_LPFF_SR%>';
        }
        if (vGZLX == "1") {
            vPageMsgID = '<%= CM_LPGL_LPFF_SS%>';
        }
        if (vGZLX == "2") {
            vPageMsgID = '<%= CM_LPGL_LPFF_BK%>';
        }
        if (vGZLX == "3") {
            vPageMsgID = '<%= CM_LPGL_LPFF_JFFL%>';
        }
        if (vGZLX == "4") {
            vPageMsgID = '<%= CM_LPGL_LPFF_LD%>';
        }
    </script>
    <script src="LPGL_LPFF.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../CrmLib/CrmLib_FillBGDD.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" />
                <input id="HF_HYID" type="hidden" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="HF_BJ_CZ" type="hidden" />
                <input type="button" id="btn_HYKHM" class="bfbtn btn_search" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员名称</div>
            <div class="bffld_right">
                <label id="LB_HYNAME" runat="server" style="text-align: left"></label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <label runat="server" id="LB_HYKNAME" style="text-align: left"></label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">证件号码</div>
            <div class="bffld_right">
                <label runat="server" id="LB_ZJHH" style="text-align: left"></label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">原积分</div>
            <div class="bffld_right">
                <label id="LB_WCLJF_OLD" runat="server" style="text-align: left" />
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" style="background-color: #F4F6F7" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>

        <div class="bffld">
            <div class="bffld_left">处理积分</div>
            <div class="bffld_right">
                <label id="LB_CLJF" runat="server" style="text-align: left" />
                <input id="HF_LQSL" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">发放规则</div>
            <div class="bffld_right">
                <select id="DDL_FFGZ" class="easyui-combobox">
                    <option></option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" type="text"  />
            </div>
        </div>
    </div>
    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>
    <div id="tb" class="item_toolbar">
        <span style="float: left">礼品列表</span>
        <button id="DelItem" type='button' class="item_deltoolbar">删除礼品</button>
    </div>
    <%=V_InputBodyEnd %>
    <div id="menuContent" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
</body>
</html>
