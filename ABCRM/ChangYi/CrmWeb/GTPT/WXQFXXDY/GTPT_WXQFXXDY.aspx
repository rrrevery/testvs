<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXQFXXDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXQFXXDY.GTPT_WXQFXXDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script type="text/javascript">
        vPageMsgID = '<%=CM_GTPT_WXQFXXDY%>';
    </script>
    <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXQFXXDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <input id="HF_MSG_ID" type="hidden"/>

    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">群发对象</div>
            <div class="bffld_right">
                <select id="DDL_QFDX" class="easyui-combobox">   
                    <option></option>
                    <option value="0">全部</option>
                    <option value="1">按标签</option>
                    <option value="2">按用户</option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">消息类型</div>
            <div class="bffld_right">
                <select id="DDL_TYPE" class="easyui-combobox">
                </select>
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld" id="div_tag">
            <div class="bffld_left">标签</div>
            <div class="bffld_right">
                <input type="text" id="TB_TAGMC" />
                <input type="hidden" id="HF_TAGID" />
            </div>
        </div>
        <div class="bffld" id="div_media">
            <div class="bffld_left">媒体素材</div>
            <div class="bffld_right">
                <input id="TB_MEDIA_TITLE" type="text" />
                <input id="TB_MEDIA_ID" type="hidden" />
            </div>
        </div>
        <div class="bffld" id="div_kbid">
            <div class="bffld_left">卡包ID</div>
            <div class="bffld_right">
               <label id="LB_CARDID"></label>
            </div>
        </div>
    </div>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">原创是否可转载</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="CB_BJ_ZZ" value="" />
                <label for="CB_BJ_ZZ"></label>
            </div>
        </div>
    </div>
    <div class="bfrow" id="div_content">
        <div class="bffld_l">
            <div class="bffld_left">文本内容</div>
            <div class="bffld_right">
                <input type="text" id="TB_CONTENT" />
            </div>
        </div>
    </div>

    <div id="div_yhlist">
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
