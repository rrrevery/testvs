<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYTJGZDY.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYTJGZDY.HYKGL_HYTJGZDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_HYTJGZ%>';
    </script>
    <script src="HYKGL_HYTJGZDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld" style="display: none">
            <div class="bffld_left" style="white-space: nowrap;">规则名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_GZMC" />
            </div>
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
            <div class="bffld_left">推荐范围</div>
            <div class="bffld_right">
                <select id="DDL_TJFW">
                    <option value="0" selected="selected">全部</option>
                    <option value="1">门店</option>
                    <option value="2">微信</option>
                    <option value="3">APP</option>
                </select>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">推荐数量</div>
            <div class="bffld_right">
                <input id="TB_TJSL" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">获奖方式</div>
            <div class="bffld_right">
                <select id="DDL_HJFS">
                    <option value="0" selected="selected">申请人</option>
                    <option value="1">推荐人</option>
                    <option value="2">全部</option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">规则类型</div>
            <div class="bffld_right">
                <select id="DDL_GZLX">
                    <option value="0" selected="selected">全部</option>
                    <option value="1">线上</option>
                    <option value="2">线下</option>

                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">奖励方式</div>
            <div class="bffld_right">
                <select id="DDL_JLFS">
                    <option value="0" selected="selected">礼品</option>
                    <option value="1">积分</option>
                    <option value="2">优惠券</option>
                </select>
            </div>
        </div>

    </div>

    <div class="bfrow" id="DV_YHQ" style="display: none">
        <div class="bffld">
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input type="text" id="TB_YHQMC" />
                <input type="hidden" id="HF_YHQID" />
                <input type="hidden" id="zHF_YHQID" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">奖励规则</div>
            <div class="bffld_right">
                <select id="DDL_JLGZ">
                    <option value="0" selected="selected">首单交易额百分比</option>
                    <option value="1">固定优惠券金额</option>
                </select>
            </div>
        </div>
    </div>

    <div class="bfrow" id="DV_JF" style="display: none">
        <div class="bffld">
            <div class="bffld_left">积分</div>
            <div class="bffld_right">
                <input type="text" id="TB_JF" />
            </div>
        </div>
    </div>

    <div id="DV_LP">
        <div class="clear"></div>
        <div id="zMPSK" class="common_menu_tit slide_down_title" style="display: none">
            <span>礼品信息</span>
        </div>

        <div id="zMPSK_Hidden" class="maininput bfborder_bottom">
            <table id="list" style="border: thin"></table>
            <div id="tb" class="item_toolbar">
                <span style="float: left">礼品信息</span>
                <button id="AddItem" type='button' class="item_addtoolbar">添加礼品</button>
                <button id="DelItem" type='button' class="item_deltoolbar">删除礼品</button>
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
