<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKPLCK.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKPLCK.MZKGL_MZKPLCK" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = '<%=CM_MZKGL_FSCK%>';   
    </script>
    <script src="MZKGL_MZKPLCK.js"></script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>

</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
             <div class="bffld_left">操作地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow" style="display: none">
        <div class="bffld">
            <div class="bffld_left">大客户</div>
            <div class="bffld_right">
                <input id="TB_KHMC" type="text" tabindex="4" />
                <input id="HF_KHID" type="hidden" />
                <input id="HF_KHDM" type="hidden" />
                <input id="HF_YWY" type="hidden" />
            </div>
        </div>

        <div class="bffld">
            <div class="bffld_left">+</div>
            <div class="bffld_right">
                <input id="TB_KHMC1" type="text" tabindex="4" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TA_ZY" type="text" />
            </div>
        </div>
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
            <div class="bffld_left">总存款金额</div>
            <div class="bffld_right">
                <label id="LB_ZCKJE" style="text-align: left;" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">总存款数量</div>
            <div class="bffld_right">
           <label id="LB_ZCKSL" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>
    <div style="float: left; width: 50%" id="kdxx">
        <div id="zMP5" class="common_menu_tit slide_down_title bfborder_top">
            <span>卡段信息</span>
        </div>
        <div id="zMP5_Hidden" class="maininput">
            <div class="bfrow" id="hyktype">
                <div class="bffld">
                    <div class="bffld_left">存款金额</div>
                    <div class="bffld_right">
                        <input id="TB_SK_JE" type="text" />
                    </div>
                </div>
                <div class="bffld_l">
                    <div class="bffld_left">&nbsp;</div>
                        <button id="AddItem" type='button' class="bfbut bfblue">添加卡段</button>
                        <button id="DelItem" type='button' class="bfbut bfblue">删除卡段</button><%--B_SK_Import--%>
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
            <div class="bfrow bfrow_table">
                <table id="list" style="border: thin"></table>
            </div>
        </div>
    </div>

    <div style="float: left; width: 50%; text-align: center" id="fkxx">
        <div id="zMP1" class="common_menu_tit slide_down_title bfborder_top">
            <span>付款信息</span>
        </div>
        <div id="zMP4_Hidden" class="maininput">
            <div class="bfrow" style="width: 33%">
                <div class="bffld">
                    <div class="bffld_left">&nbsp;</div>
                    <div class="bffld_right">
                        &nbsp;
                    </div>
                </div>
            </div>
            <div class="bfrow" style="width: 33%">
                <div class="bffld">
                    <div class="bffld_left">&nbsp;</div>
                    <div class="bffld_right">
                        &nbsp;
                    </div>
                </div>
            </div>
            <div class="bfrow bfrow_table">
                <table id="GV_SKFS" style="border: thin"></table>
            </div>
        </div>
    </div>

    <%--    <fieldset style="width: 400px; float: left; height: 250px">
        <legend>赠券明细</legend>
        <div class="bfrow1" style="width: 399px;">
            <div class="div31" style="width: 397px;">
                会员卡号:<input id="TB_HYKNO_ZQ" type="text" style="width: 90px;" />
                <button id="B_ZK_Search" type='button' class='button'>查询</button>
            </div>
        </div>

        <div class="bfrow1">
            <table id="GV_ZK"></table>
            <div id="P_ZK"></div>
            <button id="B_ZK_Delete" type='button' class='button' style="float: left;">删除卡</button>
        </div>
    </fieldset>

    <fieldset style="width: 400px; float: left; height: 250px;">
        <legend>赠积分明细</legend>
        <div class="bfrow1">
            <div style="height: 25px;">
                <div class="div31">
                    会员卡号:<input id="TB_HYKNO_JF" type="text" style="width: 90px;" />
                    <button id="B_ZJF_Search" type='button' class='button'>查询</button>
                </div>
                <div class="div31">
                    <table id="GV_ZJF"></table>
                    <div id="P_ZJF"></div>
                    <button id="B_ZJF_Delete" type='button' class='button' style="float: left;">删除卡</button>
                </div>
            </div>
        </div>
    </fieldset>

    <fieldset style="width: 810px; float: left; height: 120px;">
        <legend>核对</legend>
        <div class="bfrow1" style="width: 800px">
            <div class="div31">
                <div style="height: 25px;"></div>
                <div style="height: 25px; padding-top: 10px; width: 800px">
                    <div style="float: left;">
                        <label style="width: 80px;">存款总金额：</label>
                    </div>
                    <div style="float: left;">
                        <label id="LB_SK_YSJE" style="text-align: left;">0</label>
                    </div>
                    <div style="float: left; padding-left: 80px;">
                        <label>存款卡总数：</label>
                    </div>
                    <div style="float: left;">
                        <label id="LB_SK_YSZS" style="text-align: left;">0</label>
                    </div>

                    <div style="float: left; padding-left: 80px;">
                        <label style="width: 80px;">存款应赠券：</label>
                    </div>
                    <div style="float: left;">
                        <label id="LB_SK_ZK" style="text-align: left;">0</label>
                        <input id="HF_ZKYHQMC" type="hidden" />
                        <input id="HF_ZKYHQLX" type="hidden" />
                        <input id="HF_ZKYHQTS" type="hidden" />
                    </div>

                </div>
                <div style="height: 25px; padding-top: 10px; width: 800px">
                    <div style="float: left;">
                        <label style="width: 80px;">存款应赠积分：</label>
                    </div>
                    <div style="float: left;">
                        <label id="LB_SK_JF" style="text-align: left">0</label>
                    </div>
                    <div style="float: left; padding-left: 80px;">
                        <label>赠券总金额：</label>
                    </div>
                    <div style="float: left;">
                        <label id="LB_CK_ZSJE" style="text-align: left;">0</label>
                    </div>
                    <div style="float: left;">
                        <label style="width: 80px; padding-left: 40px;">总赠送积分：</label>
                    </div>
                    <div style="float: left;">
                        <label id="LB_CK_ZSJF" style="text-align: left;">0</label>
                    </div>
                </div>
                <div style="height: 25px; padding-top: 10px;">
                </div>
                <div style="height: 25px; padding-top: 10px;">
                    <div style="float: left; display: none;">
                        <label id="LB_ZKL">0</label>
                    </div>
                    <div style="float: left; display: none;">
                        <label id="LB_ZKLPBL">0</label>
                    </div>
                </div>
            </div>
        </div>
    </fieldset>--%>
    <%=V_InputBodyEnd %>
</body>
</html>

