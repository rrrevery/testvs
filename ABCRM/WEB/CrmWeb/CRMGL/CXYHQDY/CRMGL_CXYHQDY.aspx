<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_CXYHQDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.CXYHQDY.CRMGL_CXYHQDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>

    <script>
        vPageMsgID=<%=CM_CRMGL_DEFYHQCXHD%>;
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="CRMGL_CXYHQDY.js"></script>

</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow" style="display: none;">
        <div class="bffld">
            <div class="bffld" id="jlbh">
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">商户</div>
            <div class="bffld_right">
                <input id="TB_SHMC" type="text" />
                <input id="HF_SHDM" type="hidden" />
                <input id="zHF_SHDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">促销主题</div>
            <div class="bffld_right">
                <input id="TB_CXZT" type="text" name="" />
                <input id="HF_CXID" hidden="hidden" />
                <input id="zHF_CXID" hidden="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input type="text" id="TB_YHQMC" />
                <input type="hidden" id="HF_YHQID" />
                <input type="hidden" id="zHF_YHQID" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">有效期天数</div>
            <div class="bffld_right">
                <input id="TB_YXQTS" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" class="Wdate" onfocus="WdatePicker()" type="text" name="" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">备注1</div>
            <div class="bffld_right">
                <input id="TB_ZQBZ1" type="text"/>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">备注2</div>
            <div class="bffld_right">
                <input id="TB_ZQBZ2" type="text"/>
            </div>
        </div>
    </div>

    <div style="clear: both;"></div>
    <div id="zMP3" class="common_menu_tit slide_down_title">
        <span>促销活动信息</span>
    </div>
    <div id="zMP3_Hidden" class="maininput">

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">促销ID</div>
                <div class="bffld_right">
                    <label id="LB_CXID" style="text-align: left;"></label>
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">活动编号</div>
                <div class="bffld_right">
                    <label id="LB_CXHDBH" style="text-align: left;"></label>
                </div>
            </div>
        </div>

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">开始时间</div>
                <div class="bffld_right">
                    <label id="LB_KSSJ" style="text-align: left;"></label>
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">结束时间</div>
                <div class="bffld_right">
                    <label id="LB_JSSJ" style="text-align: left;"></label>
                </div>
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
