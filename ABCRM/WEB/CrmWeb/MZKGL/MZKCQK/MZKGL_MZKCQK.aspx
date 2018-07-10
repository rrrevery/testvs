<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKCQK.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKCQK.MZKGL_MZKCQK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_MZKGL_MZKCQK%>';    
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="MZKGL_MZKCQK.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">请刷卡</div>
            <div class="bffld_right">
                <input id="TB_CDNR" type="text" />
                <input id="HF_HYID" type="hidden" />               
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <label id="LB_HYK_NO" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <label id="LB_HYKNAME" runat="server" />
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">有效期</div>
            <div class="bffld_right">
                <label id="LB_YXQ" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">铺底金额</div>
            <div class="bffld_right">
                <label id="LB_PDJE" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">操做门店</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">发行单位</div>
            <div class="bffld_right">
                <label id="LB_FXDWMC" runat="server" />
            </div>

        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">本卡余额</div>
            <div class="bffld_right">
                <label id="LB_YJE" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" type="text" />
            </div>
        </div>
    </div>

    <div style="clear: both;"></div>

    <div style="float: left; width: 50%;">
        <div id="zMP1" class="common_menu_tit slide_down_title bfborder_top">
            <span>存取款</span>
        </div>
        <div id="zMP1_Hidden" class="maininput">
            <div class="easyui-tabs" id="tabs" style="width: 100%; height: 650px">
                <div title="存款" id="ck" style="padding: 10px">
                    <div class="bfrow">
                        <div class="bffld">
                            <div class="bffld_left">存款金额</div>
                            <div class="bffld_right">
                                <input id="TB_CKJE" type="text"  />
                            </div>
                        </div>
                    </div>
                    <div class="bfrow">
                        <div class="bffld">
                            <div class="bffld_left">付款金额</div>
                            <div class="bffld_right">
                                <label id="LB_FKJE" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="bfrow">
                        <div class="bffld">
                            <div class="bffld_left">找零</div>
                            <div class="bffld_right">
                                <label id="LB_ZL" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div title="取款" id="qk" style="padding: 10px">
                    <div class="bfrow">
                        <div class="bffld">
                            <div class="bffld_left">取款金额</div>
                            <div class="bffld_right">
                                <input id="TB_QKJE" type="text" />
                            </div>
                        </div>
                    </div>
                </div>
                <div title="退卡" id="tk" style="padding: 10px;display:none">
                    <div class="bfrow">
                        <div class="bffld">
                            <div class="bffld_left">退卡金额</div>
                            <div class="bffld_right">
                                <input id="TB_TKJE" type="text" disabled="disabled" />
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div style="float: left; width: 50%;">
        <div id="zMP2" class="common_menu_tit slide_down_title bfborder_top">
            <span>付款信息</span>
        </div>
        <div id="zMP2_Hidden" class="maininput">
            <button id="R_ZFFS" type='button' class="bfbut bfblue">修改支付方式</button>
            <div class="bfrow bfrow_table">
                <table id="GV_SKFS" style="border: thin"></table>
            </div>

        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
