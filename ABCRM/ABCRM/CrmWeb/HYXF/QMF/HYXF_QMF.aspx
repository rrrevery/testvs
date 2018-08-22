<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_QMF.aspx.cs" Inherits="BF.CrmWeb.HYXF.QMF.HYXF_QMF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_HYXF_CRMYQMJF%>';
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="HYXF_QMF.js"></script>
    <script src="../../CrmLib/CrmLib_FillBGDD.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYK_NO" type="text" />
                <input id="HF_HYID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">销售门店</div>
            <div class="bffld_right">
                <%--                                       <select id="S_MDMC">
                                            <option></option>
                                        </select>--%>
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>

        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <label id="LB_HYKNAME"></label>
                <input type="hidden" id="HF_HYKTYPE" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">操作地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" runat="server" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>

        </div>
        <div class="bffld">
        </div>
    </div>
    <div class="clear"></div>
    <div id="zMP1" class="common_menu_tit slide_down_title">
        <span>账户信息</span>

    </div>
    <div id="zMP1_Hidden" class="maininput bfborder_bottom">

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left" style="white-space: nowrap;">未处理积分</div>
                <div class="bffld_right">
                    <label id="LB_YWCLJF"></label>
                </div>

            </div>
            <div class="bffld">
                <div class="bffld_left" style="white-space: nowrap;">年积分</div>
                <div class="bffld_right">
                    <label id="LB_YNLJF"></label>
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left" style="white-space: nowrap;">优惠券金额</div>
                <div class="bffld_right">
                    <label id="LB_YYHQJE"></label>
                </div>

            </div>
            <div class="bffld">
                <div class="bffld_left" style="white-space: nowrap;">储值卡金额</div>
                <div class="bffld_right">
                    <label id="LB_YCZKYE"></label>
                </div>
            </div>
        </div>
    </div>
    <div class="clear"></div>
    <div id="zMP2" class="common_menu_tit slide_down_title">
        <span>积分购买</span>
    </div>
    <div id="zMP2_Hidden" class="maininput bfborder_bottom">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left" style="white-space: nowrap;">退货积分</div>
                <div class="bffld_right">
                    <input type="text" id="TB_THJF" />
                </div>

            </div>
            <div class="bffld">
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left" style="white-space: nowrap;">应买积分</div>
                <div class="bffld_right">
                    <label id="LB_YMJJF"></label>
                </div>

            </div>
            <div class="bffld">
                <div class="bffld_left" style="white-space: nowrap;">应付金额</div>
                <div class="bffld_right">
                    <label id="LB_YFJE_J"></label>
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left" style="white-space: nowrap;">实买积分</div>
                <div class="bffld_right">
                    <label id="LB_SMJJF"></label>
                </div>

            </div>
            <div class="bffld">
                <div class="bffld_left" style="white-space: nowrap;">实付金额</div>
                <div class="bffld_right">
                    <input id="TB_SFJE_J" type="text" onkeyup="clearNoNum(this)" />
                </div>
            </div>
        </div>
        <div class="bfrow" style="display:none">
            <div class="bffld">
                <div class="bffld_left">现金</div>
                <div class="bffld_right">
                    <input id="TB_XJ" type="text" />
                </div>

            </div>
            <div class="bffld">
                <%--<div class="bffld_left">备注</div>
                                    <div class="bffld_right">
                                        <input id="TB_ZY" type="text"   />                                        
                                    </div>
                --%>
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
    <div id="menuContent" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
</body>
</html>
