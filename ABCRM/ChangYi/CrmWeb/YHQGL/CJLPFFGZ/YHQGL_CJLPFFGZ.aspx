<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_CJLPFFGZ.aspx.cs" Inherits="BF.CrmWeb.YHQGL.CJLPFFGZ.YHQGL_CJLPFFGZ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_YHQGL_YHQDEFFFGZ_LP%>';
    </script>
    <script src="YHQGL_CJLPFFGZ.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" >
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">发放规则名称</div>
            <div class="bffld_right">
                <input id="TB_FFGZMC" type="text" tabindex="2" />
            </div>
        </div>
    </div>
    <div class="bfrow">

        <div class="bffld">
            <div class="bffld_left">礼品数量上限</div>
            <div class="bffld_right">
                <input id="TB_LPSLSX" type="text" tabindex="2" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">起点金额</div>
            <div class="bffld_right">
                <input id="TB_QDJE" type="text" tabindex="1" />
            </div>
        </div>
    </div>

    <div class="bfrow">

        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" name="CB_BJTY" id="CB_BJ_TY" value="" />
                <label for="CB_BJ_TY"></label>
            </div>
        </div>
    </div>
     <div class="clear"></div>
    <div id="zMP7" class="common_menu_tit slide_down_title">
        <span>规则列表</span>
    </div>
    <div id="zMP7_Hidden">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">销售金额</div>
                <div class="bffld_right">
                    <input id="TB_XSJE" type="text" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">抽奖次数</div>
                <div class="bffld_right">
                    <input id="TB_CJCS" type="text" />
                </div>
            </div>
        </div>

        <div class="bfrow bfrow_table">
            <table id="list" style="border: thin"></table>
        </div>
        <div id="tb" class="item_toolbar">
            <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
            <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
