<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LPGL_LPFFGZ.aspx.cs" Inherits="BF.CrmWeb.LPGL.LPFFGZ.LPGL_LPFFGZ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="LPGL_LPFFGZ.js"></script>
    <script>vPageMsgID = '<%=CM_LPGL_LPFFGZDEF%>'</script>
    <script src="../../CrmLib/CrmLib_FillHYKTYPE.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">规则名称</div>
            <div class="bffld_right">
                <input id="TB_GZMC" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">日期范围</div>
            <div class="bffld_right twodate">
                <input id="TB_KSRQ" runat="server" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_JSRQ" runat="server" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" />
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">规则类型</div>
            <div class="bffld_right">
                <input id="rd_SRL" type="radio" tabindex="1" name="GZLX" value="5" class="magic-radio" />
                <label for="rd_SRL">生日礼</label>
                <input id="rd_BKL" type="radio" tabindex="1" name="GZLX" value="2" class="magic-radio" />
                <label for="rd_BKL">办卡礼</label>
                <input id="rd_LDL" type="radio" tabindex="1" name="GZLX" value="4" class="magic-radio" />
                <label for="rd_LDL">来店礼</label>
                <input id="rd_SSL" type="radio" tabindex="1" name="GZLX" value="1" class="magic-radio" />
                <label for="rd_SSL">首刷礼</label>
                <input id="rd_JJFL" type="radio" tabindex="1" name="GZLX" value="3" class="magic-radio" />
                <label for="rd_JJFL">积分返礼</label>
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">生日判断方式</div>
            <div class="bffld_right">
                <input id="rd_BXZ" type="radio" tabindex="1" name="BJ_SR" value="0" class="magic-radio" />
                <label for="rd_BXZ">不限制</label>
                <input id="rd_DYSR" type="radio" tabindex="1" name="BJ_SR" value="1" class="magic-radio" />
                <label for="rd_DYSR">当月生日</label>
                <input id="rd_DRSR" type="radio" tabindex="1" name="BJ_SR" value="2" class="magic-radio" />
                <label for="rd_DRSR">当日生日</label>
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">限制参加次数</div>
            <div class="bffld_right">
                <input id="rd_BXZCS" type="radio" tabindex="1" name="BJ_DC" value="0" class="magic-radio" />
                <label for="rd_BXZCS">不限制</label>
                <input id="rd_ZCJYC" type="radio" tabindex="1" name="BJ_DC" value="1" class="magic-radio" />
                <label for="rd_ZCJYC">只能参加一次</label>
                <input id="rd_DRYC" type="radio" tabindex="1" name="BJ_DC" value="2" class="magic-radio" />
                <label for="rd_DRYC">当日只能参加一次</label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">消费金额计算方式</div>
            <div class="bffld_right">
                <input id="rd_BXZ_XF" type="radio" tabindex="1" name="BJ_LJ" value="0" class="magic-radio" />
                <label for="rd_BXZ_XF">不限制</label>
                <input id="rd_DRLJ_XF" type="radio" tabindex="1" name="BJ_LJ" value="4" class="magic-radio" />
                <label for="rd_DRLJ_XF">当日累计</label>
                <input id="rd_HDQLJ_XF" type="radio" tabindex="1" name="BJ_LJ" value="2" class="magic-radio" />
                <label for="rd_HDQLJ_XF">活动期累计</label>
                <input id="rd_SCSC_XF" type="radio" tabindex="1" name="BJ_LJ" value="5" class="magic-radio" />
                <label for="rd_SCSC_XF">首次刷卡</label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">办卡时间限制</div>
            <div class="bffld_right">
                <input id="rd_BXZ_BK" type="radio" tabindex="1" name="BJ_BK" value="0" class="magic-radio" />
                <label for="rd_BXZ_BK">不限制</label>
                <input id="rd_HDQBK_BK" type="radio" tabindex="1" name="BJ_BK" value="1" class="magic-radio" />
                <label for="rd_HDQBK_BK">活动期办卡</label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">礼品数量限制</div>
            <div class="bffld_right">
                <input id="rd_BXZ_SL" type="radio" tabindex="1" name="BJ_SL" value="0" class="magic-radio" />
                <label for="rd_BXZ_SL">不限制</label>
                <input id="rd_ZNLYG_SL" type="radio" tabindex="1" name="BJ_SL" value="1" class="magic-radio" />
                <label for="rd_ZNLYG_SL">只能领一个</label>
            </div>
        </div>
    </div>
    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">礼品列表</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加礼品</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除礼品</button>
    </div>
    <%=V_InputBodyEnd %>
    <div id="menuContentHYKTYPE" class="menuContent">
        <ul id="TreeHYKTYPE" class="ztree"></ul>
    </div>
</body>
</html>
