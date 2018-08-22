<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_ZHBQGZDY.aspx.cs" Inherits="BF.CrmWeb.HYKGL.ZHBQGZDY.HYKGL_ZHBQGZDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_ZHBQGZDY%>';
    </script>
    <script src="../../CrmLib/CrmLib_FillCheckTree.js"></script>
    <script src="HYKGL_ZHBQGZDY.js"></script>
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
            <div class="bffld_left">条件数量</div>
            <div class="bffld_right">
                <input id="TB_TJSL" type="text" onkeyup="this.value=this.value.replace(/\D/g,'')" onblur="FillLabelGroup()" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">统计月份</div>
            <div class="bffld_right">
                <input id="TB_TJYF" type="text" />
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
                <input class="magic-checkbox" type="checkbox" name="" id="CB_TY" value="" />
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

    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">标签列表</span>
        <span style="color:black">分组</span><select class="Text" id="DDL_LabelGroup" style="width: 100px;"></select>
        <button id="Add_BQ" type='button' class="item_addtoolbar">添加标签</button>
        <button id="Del_BQ" type='button' class="item_deltoolbar">删除标签</button>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>
