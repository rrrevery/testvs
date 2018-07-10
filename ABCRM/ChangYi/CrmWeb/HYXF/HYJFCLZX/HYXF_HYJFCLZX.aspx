<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_HYJFCLZX.aspx.cs" Inherits="BF.CrmWeb.HYXF.HYJFCLZX.HYXF_HYJFCLZX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>vPageMsgID = <%=CM_HYXF_HYKJFHBZX%>;</script>
    <script src="HYXF_HYJFCLZX.js"></script>
    <script src="../../CrmLib/CrmLib_FillBGDD.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <%--<script src="../../../Js/LodopFuncs.js"></script>--%>
    <%--    <object id="LODOP_OB" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width="0" height="0">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0" pluginspage="install_lodop32.exe"></embed>
    </object>--%>
</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">返利地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" runat="server" />
                <input id="HF_BGDDDM" type="hidden" />
                <input type="hidden" id="HF_MDID" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKH" type="text" onblur="GetHYXX()" />
                <input id="HF_HYID" type="hidden" />
                <input type="button" id="btn_HYKHM" class="bfbtn btn_search" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">会员积分</div>
            <div class="bffld_right">
                <label runat="server" id="LB_HYJF" style="text-align: left"></label>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员姓名</div>
            <div class="bffld_right">
                <label id="LB_HYNAME" runat="server" style="text-align: left"></label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <label id="TB_HYKNAME" runat="server" style="text-align: left"></label>
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">证件号码</div>
            <div class="bffld_right">
                <label runat="server" id="LB_ZJHM" style="text-align: left"></label>
            </div>
        </div>

        <div class="bffld">
            <div class="bffld_left">规则名称</div>
            <div class="bffld_right">
                <select id="DDL_FQGZ"></select>
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
            <div class="bffld_left">处理积分</div>
            <div class="bffld_right">
                <input type="text" id="TB_CLJF" />
                <input type="hidden" id="HF_FQJE" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">总兑换金额</div>
            <div class="bffld_right">
                <label runat="server" id="LB_ZJE" style="text-align: left"></label>
            </div>
        </div>

    </div>

    <div class="clear"></div>
    <div id="zMP1" class="common_menu_tit slide_down_title">
        <span>规则信息</span>
    </div>
    <div id="zMP1_Hidden">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">起止时间</div>
                <div class="bffld_right">
                    <input id="TB_KSRQ" type="text" class="Wdate" readonly="true" style="background-color: #FFE4C4" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">----------</div>
                <div class="bffld_right">
                    <input id="TB_JSRQ" type="text" class="Wdate" readonly="true" style="background-color: #FFE4C4" />
                </div>
            </div>
        </div>

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">有效时间</div>
                <div class="bffld_right">
                    <input id="TB_YXQSL" type="text" readonly="true" style="background-color: #FFE4C4" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">结束日期</div>
                <div class="bffld_right">
                    <input id="TB_YHQJSRQ" type="text" readonly="true" style="background-color: #FFE4C4" />
                </div>
            </div>
        </div>

        <div class="bfrow" style="display: none">
            <div class="bffld">
                <div class="bffld_left">门店选择</div>
                <div class="bffld_right">
                    <input id="Radio1" type="radio" name="BJ_MD" value="0" checked="checked" disabled="disabled" />总部
                <input id="Radio2" type="radio" name="BJ_MD" value="1" disabled="disabled" />门店
                <input type="hidden" id="HF_MDDM" />
                </div>
            </div>
        </div>
    </div>

    <div class="clear"></div>
    <div id="zMP2" class="common_menu_tit slide_down_title">
        <span>规则明细</span>
    </div>
    <div id="zMP2_Hidden">

        <div class="bfrow">
            <%--style="margin-left: 20px"--%>
            <table id="list"></table>
            <div id="pager"></div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
    <div id="menuContent" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
</body>
</html>
