<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_SYDHDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXSYDHDY.GTPT_SYDHDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXSYDHDY%>'
    </script>
    <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_SYDHDY.js?ts=1"></script>
</head>
<body>

    <%=V_InputBodyBegin %>


    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
       <%-- <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input id="TB_WXMDMC" type="text" />
                <input id="HF_WXMDID" type="hidden" />
                <input id="zHF_WXMDID" type="hidden" />
            </div>
        </div>--%>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">导航名称</div>
            <div class="bffld_right">
                <label id="TB_DHMC" runat="server" style="text-align: left;" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">导航类型</div>
            <div class="bffld_right">
                <select id="DDL_DHLX">
                    <option value="0">首页</option>
                    <option value="1">会员专区</option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="status" id="R_ZX" value="0" />
                <label for="R_ZX">否</label>
                <input class="magic-radio" type="radio" name="status" id="R_BC" value="1" />
                <label for="R_BC">是</label>
            </div>
        </div>
    </div>
    <div id="ruleList1">

        <fieldset style="width: auto;" id="news">
            <legend>导航轮播图</legend>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">序号</div>
                    <div class="bffld_right">
                        <input id="TB_TURNINX" type="text" tabindex="" />
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">链接地址</div>
                    <div class="bffld_right">
                        <input id="TB_TURNURL" type="text" tabindex="" />
                    </div>
                </div>
            </div>

            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">图片链接</div>
                    <div class="bffld_right">
                        <input id="TB_TURNIMG" type="text" readonly="true" />
                    </div>
                </div>
            </div>



            <div class="bfrow bfrow_table">
                <table id="list1" style="border: thin"></table>
            </div>
            <div id="list1_tb" class="item_toolbar">
                <span style="float: left"></span>
                <button id="turn_Add" type='button' class="item_addtoolbar">添加</button>
                <%--             <button id="turn_Update" type='button' class="item_addtoolbar">修改</button>--%>
                <button id="turn_Del" type='button' class="item_deltoolbar">删除</button>
            </div>

        </fieldset>
    </div>

    <div id="ruleList2">
        <fieldset style="width: auto;" id="Fieldset1">
            <legend>子导航</legend>
            <div class="bfrow">
                <div class="bffld" style="height: inherit">
                    <font color='red'>注：子导航顺序号由小到大排序最多为8条</font>
                </div>
                <div class="bffld">
                </div>
            </div>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">序号</div>
                    <div class="bffld_right">
                        <input id="TB_NAVIINX" type="text" tabindex="" />
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">子导航名称</div>
                    <div class="bffld_right">
                        <input id="TB_NAVINAME" type="text" tabindex="" />
                    </div>
                </div>
            </div>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">显示名称</div>
                    <div class="bffld_right">
                        <input id="TB_VIEWNAME" type="text" tabindex="" />
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">链接地址</div>
                    <div class="bffld_right">
                        <input id="TB_NAVIURL" type="text" tabindex="" />
                    </div>
                </div>
            </div>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">
                        图片链接
                    </div>
                    <div class="bffld_right">
                        <input id="TB_NAVIIMG" type="text" readonly="true" />
                    </div>
                </div>
            </div>

            <div class="bfrow bfrow_table">
                <table id="list2" style="border: thin"></table>
            </div>
            <div id="list2_tb" class="item_toolbar">
                <span style="float: left"></span>
                <button id="sub_Add" type='button' class="item_addtoolbar">添加</button>
                <%--             <button id="sub_Update" type='button' class="item_addtoolbar">修改</button>--%>
                <button id="sub_Del" type='button' class="item_deltoolbar">删除</button>
            </div>

        </fieldset>
    </div>
    <div id="ruleList3">
        <fieldset style="width: auto;" id="Fieldset2" class="rule_ask">
            <legend>额外展示</legend>
            <div class="bfrow">
                <div class="bffld" style="height: inherit">
                    <font color='red'>注：对于首页导航，额外展示顺序号由小到大排序最多为2条；对于会员专区，额外展示顺序号由小到大排序为4条</font>
                </div>
                <div class="bffld">
                </div>
            </div>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">序号</div>
                    <div class="bffld_right">
                        <input id="TB_SHOWINX" type="text" tabindex="" />
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">展示名称</div>
                    <div class="bffld_right">
                        <input id="TB_SHOWNAME" type="text" tabindex="" />
                    </div>
                </div>
            </div>
            <div class="bfrow">

                <div class="bffld">
                    <div class="bffld_left">展示简介</div>
                    <div class="bffld_right">
                        <input id="TB_SHOWTITLE" type="text" tabindex="" />
                    </div>
                </div>
            </div>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">图片链接</div>
                    <div class="bffld_right">
                        <input id="TB_SHOWIMG" type="text" readonly="true" />
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">链接地址</div>
                    <div class="bffld_right">
                        <input id="TB_SHOWURL" type="text" tabindex="" />
                    </div>
                </div>
            </div>

            <div class="bfrow bfrow_table">
                <table id="list3" style="border: thin"></table>
            </div>
            <div id="list3_tb" class="item_toolbar">
                <span style="float: left"></span>
                <button id="show_Add" type='button' class="item_addtoolbar">添加</button>
                <%--             <button id="show_Update" type='button' class="item_addtoolbar">修改</button>--%>
                <button id="show_Del" type='button' class="item_deltoolbar">删除</button>
            </div>
        </fieldset>
    </div>


    <%=V_InputBodyEnd %>
</body>
</html>
