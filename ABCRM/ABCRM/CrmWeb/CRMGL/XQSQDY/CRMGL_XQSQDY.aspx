<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_XQSQDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.XQSQDY.CRMGL_XQSQDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = '<%=CM_CRMGL_XQSQDY%>';
    </script>
    <script src="CRMGL_XQSQDY.js"></script>
</head>
<body>
    <div>
        <div id='TopPanel' class='topbox'>
            <div id='location'>
                <div id='switchspace'></div>
            </div>
            <div id='btn-toolbar'>
                <div id='morebuttons'><i class='fa fa-list-ul fa-lg' aria-hidden='true' style='color: rgb(140,151,157)'></i></div>
            </div>
        </div>
        <div style="height: 30px; width: 100%; position: fixed;">
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">门店</div>
                    <div class="bffld_right">
                        <input type="text" id="TB_MDMC" />
                        <input type="hidden" id="HF_MDID" />
                        <input type="hidden" id="zHF_MDID" />
                    </div>
                </div>
            </div>
            <div class="bfrow" style="display: none">
                <div class="bffld" id="jlbh">
                </div>
            </div>
        </div>
        <div class='bfbox' style="margin-top: 50px">

            <div class='common_menu_tit'><span id='bftitle'></span></div>
            <div class='maininput2'>
                <div id='TreePanel' class='bfblock_left'>
                    <ul id='TreeSQ' class='ztree' style='margin-top: 0;'></ul>
                </div>
                <div id='MainPanel' class='bfblock_right'>

                    <div class="bfrow bfrow_table">
                        <table id="list" style="border: thin"></table>
                    </div>
                    <div id="tb" class="item_toolbar">
                        <span style="float: left"></span>
                        <button id="AddItem" type='button' class="item_addtoolbar">添加小区</button>
                        <button id="DelItem" type='button' class="item_deltoolbar">删除小区</button>
                    </div>
                </div>
            </div>
        </div>

        <div id="status-bar" style="width: 800px; display: none"></div>
    </div>


</body>
</html>
