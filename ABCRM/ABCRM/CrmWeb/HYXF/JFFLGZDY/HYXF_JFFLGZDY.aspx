<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_JFFLGZDY.aspx.cs" Inherits="BF.CrmWeb.HYXF.JFFLGZDY.HYXF_JFFLGZDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>vPageMsgID = <%=CM_HYXF_JFCLGZDY%>;</script>
    <script src="HYXF_JFFLGZDY.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
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
                <input type="text" id="TB_GZMC" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">商户</div>
            <div class="bffld_right">
                <input id="TB_SHMC" type="text" />
                <input id="HF_SHDM" type="hidden" />
                <input id="zHF_SHDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_MDMC" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型 </div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" />
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input id="TB_YHQMC" type="text" tabindex="3" />
                <input id="HF_YHQID" type="hidden" />
                <input id="zHF_YHQID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">起止时间</div>
            <div class="bffld_right">
                <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker()" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker()" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">有效时间</div>
            <div class="bffld_right">
                <input id="TB_YXQSL" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">有效期单位</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="BJ_YXQDW" id="R_DAY" value="1" checked="checked" />
                <label for="R_DAY">天</label>
                <input class="magic-radio" type="radio" name="BJ_YXQDW" id="R_MONTH" value="2" />
                <label for="R_MONTH">月</label>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_YHQJSRQ" type="text" class="Wdate" onfocus="WdatePicker()" />
            </div>
        </div>
        <div class="bffld" style="display: none">
            <div class="bffld_left" style="width: auto">金额截取位数</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="BJ_JEJQFS" id="R_Y" value="0" />
                <label for="R_Y">元</label>
                <input class="magic-radio" type="radio" name="BJ_JEJQFS" id="R_J" value="1" />
                <label for="R_J">角</label>
                <input class="magic-radio" type="radio" name="BJ_JEJQFS" id="R_F" value="2" />
                <label for="R_F">分</label>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld" style="width: 100%">
            <div class="bffld_left" style="width: 8%">参加限制</div>
            <div class="bffld_right" style="width: 92%">
                <input class="magic-radio" type="radio" name="BJ_FQCSXZ" id="R_BKZ" value="0" />
                <label for="R_BKZ">不控制</label>
                <input class="magic-radio" type="radio" name="BJ_FQCSXZ" id="R_YC" value="1" />
                <label for="R_YC">活动期内一次</label>
                <input class="magic-radio" type="radio" name="BJ_FQCSXZ" id="R_MTYC" value="2" />
                <label for="R_MTYC">活动期内每天一次</label>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld" style="width: 100%">
            <div class="bffld_left" style="width: 8%">积分处理方式</div>
            <div class="bffld_right" style="width: 92%">
                <input class="magic-radio" type="radio" name="BJ_CLFS" id="R_CLFS1" value="1" />
                <label for="R_CLFS1">积分达到一档生成一档的金额，从低档开始</label>
                <input class="magic-radio" type="radio" name="BJ_CLFS" id="R_CLFS2" value="2" />
                <label for="R_CLFS2">生成相应档次对应的金额</label>
                <input class="magic-radio" type="radio" name="BJ_CLFS" id="R_CLFS3" value="3" />
                <label for="R_CLFS3">从高档开始，生成所有满足档次下限陪数的金额</label>
            </div>
        </div>
    </div>

    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">规则列表</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加规则</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除规则</button>
    </div>
    <%=V_InputBodyEnd %>
    <div id="menuContentHYKTYPE" class="menuContent">
        <ul id="TreeHYKTYPE" class="ztree"></ul>
    </div>
</body>
</html>
