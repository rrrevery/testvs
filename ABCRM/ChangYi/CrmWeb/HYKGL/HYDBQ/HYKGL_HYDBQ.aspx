<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYDBQ.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYDBQ.HYKGL_HYDBQ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = '<%=CM_HYKGL_HYDBQ%>'
    </script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="HYKGL_HYDBQ.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow" style="display: none">
        <div class="bffld" id="jlbh">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" />
                <input id="HF_HYID" type="hidden" />
            </div>
        </div>
    </div>

    <div style="clear: both;"></div>
    <div id="zMP3" class="common_menu_tit slide_down_title bfborder_top">
        <span>标签信息</span>
    </div>
    <div id="zMP3_Hidden" class="maininput">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">标签类别</div>
                <div class="bffld_right">
                    <select id="DDL_BQLB" onchange="BQLBChange()">
                        <option></option>
                    </select>
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">标签项目</div>
                <div class="bffld_right">
                    <input id="TB_BQXMMC" type="text" />
                    <input id="HF_BQXMID" type="hidden" />
                </div>
            </div>
        </div>

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">标签值</div>
                <div class="bffld_right">
                    <select id="DDL_BQZ">
                        <option></option>
                    </select>
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">有效期</div>
                <div class="bffld_right">
                    <input id="TB_YXQ" type="text" class="Wdate" onfocus="WdatePicker()" />
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">唯一标记</div>
                <div class="bffld_right">
                    <input class="magic-checkbox" type="checkbox" name="CB_WYBJ" id="CB_WY" value="" />
                    <label for="CB_WY"></label>
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

    <%=V_InputBodyEnd %>
</body>
</html>
