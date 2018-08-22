<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_JCBQGZDY.aspx.cs" Inherits="BF.CrmWeb.HYKGL.JCBQGZDY.HYKGL_JCBQGZDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_HYJCBQGZDY%>';
    </script>
    <script src="../../CrmLib/CrmLib_FillCheckTree.js"></script>
    <script src="HYKGL_JCBQGZDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow" style="display: none">
        <div class="bffld" id="jlbh">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员标签</div>
            <div class="bffld_right">
                <input id="TB_HYBQ" type="text" />
                <input id="HF_HYBQ" type="hidden" />
                <input id="zHF_HYBQ" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">有效月份</div>
            <div class="bffld_right">
                <input id="TB_YXYF" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" name="CB_TYZT" id="CB_TY" value="" />
                <label for="CB_TY"></label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">继承标签</div>
            <div class="bffld_right">
                <input id="TB_XHYBQ" type="text" />
                <input id="HF_XHYBQ" type="hidden" />
                <input id="zHF_XHYBQ" type="hidden" />
            </div>
        </div>
    </div>


    <div style="clear: both;"></div>
    <div id="zMP1" class="common_menu_tit slide_down_title">
        <span>标签商品信息</span>
    </div>
    <div id="zMP1_Hidden" class="maininput">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">商户名称</div>
                <div class="bffld_right">
                    <input id="TB_SHMC" type="text" />
                    <input id="HF_SHDM" type="hidden" />
                    <input id="zHF_SHDM" type="hidden" />
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">商品分类</div>
                <div class="bffld_right">
                    <input id="TB_SPFLMC" type="text" />
                    <input id="HF_SPFLDM" type="hidden" />
                    <input id="zHF_SPFLDM" type="hidden" />
                </div>
            </div>
        </div>

        <div class="bfrow bfrow_table">
            <table id="list" style="border: thin"></table>
        </div>
        <div id="tb" class="item_toolbar">
            <span style="float: left"></span>
            <button id="Add_SPXX" type='button' class="item_addtoolbar">添加商品</button>
            <button id="Del_SPXX" type='button' class="item_deltoolbar">删除商品</button>
        </div>
        <div style="clear: both;"></div>
    </div>

    <div style="clear: both;"></div>
    <div id="zMP2" class="common_menu_tit slide_down_title">
        <span>标签商品分类信息</span>
    </div>
    <div id="zMP2_Hidden" class="maininput">
        <div class="bfrow bfrow_table">
            <table id="SPFLList" style="border: thin"></table>
        </div>
        <div id="SPFLList_tb" class="item_toolbar">
            <span style="float: left">分类列表</span>
            <button id="Add_SPFL" type='button' class="item_addtoolbar">添加分类</button>
            <button id="Del_SPFL" type='button' class="item_deltoolbar">删除分类</button>
        </div>
    </div>


    <div style="clear: both;"></div>
    <div id="zMP3" class="common_menu_tit slide_down_title">
        <span>标签品牌信息</span>
    </div>
    <div id="zMP3_Hidden" class="maininput">
        <div id="PLList_tb" class="item_toolbar">
            <span style="float: left">品牌列表</span>
            <button id="Add_PL" type='button' class="item_addtoolbar">添加品牌</button>
            <button id="Del_PL" type='button' class="item_deltoolbar">删除品牌</button>
        </div>
        <div class="bfrow bfrow_table">
            <table id="PLList" style="border: thin"></table>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
