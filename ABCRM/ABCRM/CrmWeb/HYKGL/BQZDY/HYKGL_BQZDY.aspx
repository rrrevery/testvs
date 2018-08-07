<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_BQZDY.aspx.cs" Inherits="BF.CrmWeb.HYKGL.BQZDY.HYKGL_BQZDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = '<%=CM_HYKGL_BQZDY%>'; 
    </script>
    <script src="HYKGL_BQZDY.js"></script>
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
        <div style=" height: 30px; width: 100%; position: fixed;">
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">标签类别</div>
                    <div class="bffld_right">
                        <select id="DDL_BQLB" onchange="BQLBChange()">
                            <option></option>
                        </select>
                        <input type="hidden" id="HF_LABELXMID"/>
                    </div>
                </div>
            </div>
            <div class="bfrow" style="display:none">
                <div class="bffld" id="jlbh">
                </div>
            </div>
        </div>
        <div class='bfbox' style="margin-top: 50px">

            <div class='common_menu_tit'><span id='bftitle'></span></div>
            <div class='maininput2'>
                <div id='TreePanel' class='bfblock_left'>
                    <ul id='TreeBQ' class='ztree' style='margin-top: 0;'></ul>
                </div>
                <div id='MainPanel' class='bfblock_right'>

                    <div class="bfrow bfrow_table">
                        <table id="list" style="border: thin"></table>
                    </div>

                </div>
            </div>
        </div>

          <div id="status-bar" style="width: 800px;display:none"></div>
    </div>

<%--    <div id="panelwrap">
        <div class="center_content">
            <div id="right_wrap">
                <div id="right_content">
                    <h2>标签值定义</h2>
                    <div style="width: 800px;" id="btn-toolbar">
                        <button id="BTN_ADD">添加</button>
                        <button id="BTN_DEL">删除</button>
                    </div>
                    <div class="form_row">
                        <div class="div1">
                            <div class="dv_sub_left">标签类别</div>
                            <div class="dv_sub_right">
                                <select id="DDL_BQLB" onchange="BQLBChange()">
                                    <option></option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div id="tab1" class="tabcontent">

                        <div id="MainPanel" class="form">

                            <div class="form_row_left" style="border-right: 1px #ddd solid;">
                                <div id="TreeBQ" class="ztree" style="margin-top: 0px;"></div>
                            </div>
                            <div class="form_row_right" style="border-left: 0px;">
                                <div class="form_row">
                                    <table id="list"></table>
                                    <div id="pager"></div>
                                </div>
                            </div>
                            <div class="form_row" style="display: none">
                                <div class="div2" id="jlbh">
                                </div>
                                <input id="zHF_QKDM" type="text" hidden="hidden" />
                            </div>

                            <div id="status-bar" style="width: 800px;"></div>
                        </div>
                        <div class="clear"></div>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>--%>
</body>
</html>
