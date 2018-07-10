<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_PLHYKFF.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKFF.HYKGL_PLHYKFF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_JFKPLFF%>';
    </script>
    <script src="HYKGL_PLHYKFF.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <style>
        .mini-buttonedit-border {
            border: solid 1px #a5acb5;
            display: block;
            position: relative;
            overflow: hidden;
            padding-right: 30px;
        }

        .mini-buttonedit-input {
            border: 0;
            float: left;
        }

        .mini-buttonedit-buttons {
            position: absolute;
            width: 30px;
            height: 100%;
        }
    </style>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>

        <div class="bffld">
            <div class="bffld_left">单张卡费</div>
            <div class="bffld_right">
                <input id="TB_DZKFJE" type="text" value="0" readonly="true" />
            </div>
        </div>
    </div>
    <%--                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">客户</div>
                                    <div class="bffld_right">
                                        <input id="TB_KHMC" type="text" />
                                        <input id="HF_KHID" type="hidden" />
                                    </div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">联系人</div>
                                    <div class="bffld_right" style="width: 300px;">
                                        <span class="mini-buttonedit-border">
                                            <input class="mini-buttonedit-input" id="TB_LXRMC" type="text" placeholder="请输入..." autocomplete="off" style="width: 266px;" readonly="true" />
                                            <input id="B_LXR" type="button" value="···" class="mini-buttonedit-buttons" />
                                        </span>
                                        <input id="HF_LXR" type="hidden" />
                                    </div>
                                </div>
                            </div>--%>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" readonly="true" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>

        <div class="bffld">
            <div class="bffld_left">发放总数</div>
            <div class="bffld_right">
                <label id="LB_SKSL">0</label>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TA_ZY" class="long" type="text" />
            </div>
        </div>
    </div>

    <div style="clear: both;"></div>
    <div id="zMP3" class="common_menu_tit slide_down_title bfborder_top">
        <span>卡段信息</span>
    </div>

    <div id="zMP3_Hidden" class="maininput">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">卡类型</div>
                <div class="bffld_right">
                    <input id="TB_HYKNAME" type="text" />
                    <input id="HF_HYKTYPE" type="hidden" />
                </div>
            </div>
        </div>

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">卡号段</div>
                <div class="bffld_right">
                    <input id="TB_CZKHM_BEGIN" type="text" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">----------</div>
                <div class="bffld_right">
                    <input id="TB_CZKHM_END" type="text" />
                </div>
            </div>
        </div>

    </div>



    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">卡段列表</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加卡段</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除卡段</button>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
