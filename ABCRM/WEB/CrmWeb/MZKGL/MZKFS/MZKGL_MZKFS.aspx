<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKFS.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKFS.MZKGL_MZKFS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_MZKGL_FSNEW%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_MZKGL_FSNEW_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_MZKGL_FSNEW_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_MZKGL_FSNEW_CX%>');
    </script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="MZKGL_MZKFS.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" readonly="true" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">客户</div>
            <div class="bffld_right">
                <input id="TB_KHMC" type="text" tabindex="4" />
                <input id="HF_KHID" type="hidden" />
                <input id="zHF_KHID" type="hidden" />
                <input id="HF_KHDM" type="hidden" />
                <input id="HF_YWY" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">联系人</div>
            <div class="bffld_right">
                <%--<input id="TB_LXRXM" type="text" tabindex="4" />--%>
                <label id="LB_LXRXM" style="text-align: left;"></label>
                <input id="HF_STATUS" type="hidden" />
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">联系人电话</div>
            <div class="bffld_right">
                <%--<input id="TB_LXRSJ" type="text" tabindex="4" />--%>
                <label id="LB_LXRSJ" style="text-align: left;"></label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TA_ZY" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">售卡总金额</div>
            <div class="bffld_right">
                <label id="LB_SK_YSJE" style="text-align: left;">0</label>
            </div>
        </div>
         <div class="bffld">
            <div class="bffld_left">售卡总数</div>
            <div class="bffld_right">
                <label id="LB_SK_YSZS" style="text-align: left;">0</label>
            </div>
        </div>    
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">售卡应赠券</div>
            <div class="bffld_right">
                <label id="LB_SK_ZK" style="text-align: left;">0</label>
                <input id="HF_ZKYHQMC" type="hidden" />
                <input id="HF_ZKYHQLX" type="hidden" />
                <input id="HF_ZKYHQTS" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">赠券总金额</div>
            <div class="bffld_right">
                <label id="LB_ZK_YSJE" style="text-align: left;">0</label>
            </div>
        </div>

    </div>
        <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">售卡应赠积分</div>
            <div class="bffld_right">
                <label id="LB_SK_JF" style="text-align: left;">0</label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">总赠送积分</div>
            <div class="bffld_right">
                <label id="LB_ZSJF" style="text-align: left;">0</label>
            </div>
        </div>

    </div>
    <div style="clear: both;"></div>

    <div style="float: left; width: 50%;">
        <div id="zMP1" class="common_menu_tit slide_down_title bfborder_top">
            <span>售卡明细</span>
        </div>
        <div id="zMP1_Hidden" class="maininput">
            <div class="bfrow" id="hyktype">
                <div class="bffld">
                    <div class="bffld_left">卡类型</div>
                    <div class="bffld_right">
                        <input id="TB_HYKNAME" type="text" />
                        <input id="HF_HYKTYPE" type="hidden" />
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">&nbsp;</div>
                    <div class="bffld_right">
                        <button id="AddItem" type='button' class="bfbut bfblue">添加卡段</button>
                        <button id="DelItem" type='button' class="bfbut bfblue">删除卡段</button>
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
            <div class="bfrow bfrow_table">
                <table id="list" style="border: thin"></table>
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
    <div style="clear: both;"></div>
    <div style="float: left; width: 50%;">
        <div id="zMP3" class="common_menu_tit slide_down_title bfborder_top">
            <span>赠券明细</span>
        </div>


        <div id="zMP3_Hidden" class="maininput">
            <div class="bffld">
                <div class="bffld_left">会员卡号</div>
                <div class="bffld_right">
                    <input id="TB_HYKNO_ZQ" type="text" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">&nbsp;</div>
                <div class="bffld_right">
                    <button id="B_ZK_Search" type='button' class="bfbut bfblue">查询</button>
                    <button id="B_ZK_Delete" type='button' class="bfbut bfblue">删除卡</button>
                </div>
            </div>
            <div class="bfrow bfrow_table">
                <table id="GV_ZK" style="border: thin"></table>
            </div>
        </div>
    </div>
    <div style="float: left; width: 50%;">
        <div id="zMP4" class="common_menu_tit slide_down_title bfborder_top">
            <span>赠积分明细</span>
        </div>


        <div id="zMP4_Hidden" class="maininput">
            <div class="bffld">
                <div class="bffld_left">会员卡号</div>
                <div class="bffld_right">
                    <input id="TB_HYKNO_ZF" type="text" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">&nbsp;</div>
                <div class="bffld_right">
                    <button id="B_ZF_Search" type='button' class="bfbut bfblue">查询</button>
                    <button id="B_ZF_Delete" type='button' class="bfbut bfblue">删除卡</button>
                </div>
            </div>
            <div class="bfrow bfrow_table">
                <table id="GV_ZJF" style="border: thin"></table>
            </div>
        </div>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>
