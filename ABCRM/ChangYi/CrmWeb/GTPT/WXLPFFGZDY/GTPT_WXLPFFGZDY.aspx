<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXLPFFGZDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXLPFFGZDY.GTPT_WXLPFFGZDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = <%=CM_GTPT_WXLBFFGZ%>;
    </script>
    <script src="GTPT_WXLPFFGZDY.js?ts=1"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">规则名称</div>
            <div class="bffld_right">
                <input id="TB_GZMC" type="text" />

            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">规则类型</div>
            <div class="bffld_right">
                <select id="DDL_GZLX">
                    <option></option>
                    <option value="2">抽奖</option>
                    <option value="1">抢红包</option>
                    <option value="3">刮刮卡</option>

                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">渠道</div>
            <div class="bffld_right">
                <select id="DDL_QD">
                    <option></option>
                    <option value="0">全部</option>
                    <option value="1">微信</option>
                    <option value="2">APP</option>

                </select>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld" style="width: 900px;">
            <div class="bffld_left" style="width: 145px">活动期内限制次数</div>
            <div class="bffld_right">

                <input id="TB_XZCS" type="text" />
            </div>

        </div>
    </div>
    <div class="bfrow">
        <div class="bffld" style="width: 900px;">
            <div class="bffld_left" style="width: 145px">活动期内限制提示</div>
            <div class="bffld_right">

                <input id="TB_XZTS" type="text" />
            </div>

        </div>
    </div>
    <div class="bfrow">
        <div class="bffld" style="width: 900px;">
            <div class="bffld_left" style="width: 145px">活动期内单会员限制次数</div>
            <div class="bffld_right">

                <input id="TB_XZCS_HY" type="text" />
            </div>
        </div>

    </div>

    <div class="bfrow">
        <div class="bffld" style="width: 900px;">
            <div class="bffld_left" style="width: 145px">活动期内单会员限制提示</div>
            <div class="bffld_right">

                <input id="TB_XZTS_HY" type="text" />
            </div>

        </div>
    </div>
    <div class="bfrow">
        <div class="bffld" style="width: 900px;">
            <div class="bffld_left" style="width: 145px">单会员每日限制次数</div>
            <div class="bffld_right">

                <input id="TB_XZCS_DAY_HY" type="text" />
            </div>

        </div>
    </div>
    <div class="bfrow">
        <div class="bffld" style="width: 900px;">
            <div class="bffld_left" style="width: 145px">单会员每日限制提示</div>
            <div class="bffld_right">

                <input id="TB_XZTS_DAY_HY" type="text" />
            </div>
        </div>

    </div>
    <div style="clear: both;"></div>
    <div id="zMP1" class="common_menu_tit slide_down_title">
        <span>奖品级次</span>
    </div>
    <div id="zMP1_Hidden" class="maininput bfborder_bottom">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">奖品级次</div>
                <div class="bffld_right">
                    <select id="DDL_JPJC">
                        <option></option>
                    </select>
                </div>
            </div>
        </div>
        <div class="bfrow bfrow_table">
            <table id="list" style="border: thin"></table>
        </div>

        <div id="tb" class="item_toolbar">
            <span style="float: left"></span>
            <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
            <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
        </div>
    </div>

    <div style="clear: both;"></div>
    <div id="zMP2" class="common_menu_tit slide_down_title">
        <span>转盘角度</span>
    </div>
    <div id="zMP2_Hidden" class="maininput bfborder_bottom">
        <div class="bfrow" id="zpRule">
            <div class="bffld">
                <div class="bffld_left">转盘角度</div>
                <div class="bffld_right">
                    <input id="TB_VAL" type="text" />
                </div>
            </div>

            <div class="bfrow bfrow_table">
                <table id="listJD" style="border: thin"></table>
            </div>

            <div id="listJD_tb" class="item_toolbar">
                <span style="float: left"></span>
                <button id="Add" type='button' class="item_addtoolbar">添角度</button>
                <button id="Del" type='button' class="item_deltoolbar">删角度</button>
            </div>
        </div>
    </div>
    <%--    <fieldset style="width: 795px; float: left;">
        <legend>条件</legend>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">奖品级次</div>
                <div class="bffld_right">
                    <select id="DDL_JPJC">
                        <option></option>
                    </select>
                </div>
            </div>
        </div>
    </fieldset>
    <div class="bfrow" id="zpRule">
        <div class="bffld">
        </div>
        <div class="bffld">
            <div class="bffld_left">转盘角度</div>
            <div class="bffld_right">
                <input id="TB_VAL" type="text" tabindex="2" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left"></div>
            <div class="bffld_right">
                <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
                <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left"></div>
            <div class="bffld_right">
                <button id="Add" type='button' class="item_addtoolbar">添角度</button>
                <button id="Del" type='button' class="item_deltoolbar">删角度</button>

            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left"></div>
            <div class="bffld_right">
                <table id="list"></table>
            </div>
        </div>
        <div class="bffld_l" id="tab_2">
            <div class="bffld_left"></div>
            <div class="bffld_right">
                <table id="listJD"></table>
            </div>
        </div>
    </div>--%>



    <%=V_InputBodyEnd %>
</body>
</html>
