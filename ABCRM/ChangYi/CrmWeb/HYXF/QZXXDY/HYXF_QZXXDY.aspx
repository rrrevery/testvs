<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_QZXXDY.aspx.cs" Inherits="BF.CrmWeb.HYXF.QZXXDY.HYXF_QZXXDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = '<%=CM_HYXF_QZXXDY%>';
    </script>
    
    <script src="HYXF_QZXXDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">圈子名称</div>
            <div class="bffld_right">
                <input id="TB_QZMC" type="text" tabindex="1" />
                <input id="HF_STATUS" type="hidden" />
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
            <div class="bffld_left">成员人数</div>
            <div class="bffld_right">
                <label id="LB_QZCYRS" style="text-align: left"></label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">圈子类型</div>
            <div class="bffld_right">
                <select id="S_QZLX">
                    <option></option>
                </select>
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>
    <div class="clear"></div>
    <div id="zMP1" class="common_menu_tit slide_down_title">
        <span>成员信息</span>
    </div>
    <div id="zMP1_Hidden" class="maininput">
        <div style="float: right">
            <button id="AddItem" type='button' class="item_addtoolbar">添加卡</button>
            <button id="DelItem" type='button' class="item_deltoolbar">删除卡</button>
        </div>
        <div class="bfrow bfrow_table">
            <table id="list" style="border: thin"></table>
        </div>
        <div style="clear: both;"></div>
    </div>
    <div id="zMP2" class="common_menu_tit slide_down_title" style="display:none" >
        <span>成员喜爱部门</span>
    </div>
    <div id="zMP2_Hidden" class="maininput" style="display:none" >
        <div style="float: right">
            <button id="Add_BM" type='button' class="item_addtoolbar">添加部门</button>
            <button id="Del_BM" type='button' class="item_deltoolbar">删除部门</button>
        </div>
        <div class="bfrow bfrow_table">
            <table id="list_BM" style="border: thin"></table>
        </div>

        <div style="clear: both;"></div>
    </div>
    <div id="zMP3" class="common_menu_tit slide_down_title"  style="display:none">
        <span>成员喜爱品类</span>
    </div>
    <div id="zMP3_Hidden" class="maininput"  style="display:none">
        <div style="float: right">
            <button id="Add_PL" type='button' class="item_addtoolbar">添加品类</button>
            <button id="Del_PL" type='button' class="item_deltoolbar">删除品类</button>
        </div>
        <div class="bfrow bfrow_table">
            <table id="list_PL" style="border: thin"></table>
        </div>

        <div style="clear: both;"></div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
