<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_BQXMDY.aspx.cs" Inherits="BF.CrmWeb.HYKGL.BQXMDY.HYKGL_BQXMDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Tree %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = '<%=CM_HYKGL_BQXMDY%>'
    </script>
    <script src="HYKGL_BQXMDY.js"></script>
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
                    <div class="bffld_left">标签类别</div>
                    <div class="bffld_right">
                        <select id="DDL_BQLB" onchange="BQLBChange()">
                            <option></option>
                        </select>
                    </div>
                </div>
            </div>

        </div>
        <div class='bfbox' style="margin-top: 50px">

            <div class='common_menu_tit'><span id='bftitle'></span></div>
            <div class='maininput2'>
                <div id='TreePanel' class='bfblock_left'>
                </div>
                <div id='MainPanel' class='bfblock_right'>
                    <div class="bfrow" id="KLX">
                        <div class="bffld">
                            <div class="bffld_left">会员卡类型</div>
                            <div class="bffld_right">
                                <input id="TB_HYKNAME" type="text" />
                                <input type="hidden" id="HF_HYKTYPE" />
                                <input type="hidden" id="zHF_HYKTYPE" />
                            </div>
                        </div>
                    </div>
                    <div class="bfrow">
                        <div class="bffld">
                            <div class="bffld_left">标签描述</div>
                            <div class="bffld_right">
                                <input type="text" id="TB_BZ" />
                            </div>
                        </div>
                        <div class="bffld">
                            <div class="bffld_left">唯一标记</div>
                            <div class="bffld_right">
                                <input class="magic-checkbox" type="checkbox" name="CB_WYBJ" id="CB_WY" value="" />
                                <label for="CB_WY"></label>
                            </div>
                        </div>
                    </div>

                    <div class="bfrow">
                        <div class="bffld" id="mjbj">
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</body>
</html>
