<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_FQDYD.aspx.cs" Inherits="BF.CrmWeb.HYXF.FQDYD.HYXF_FQDYD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_CXD %>
    <script type="text/javascript">
        lx = IsNullValue(GetUrlParam("lx"), "0");//券类型（BJ_TS:0一般券;1礼品券;2抽奖券;3积分券;4促销积分券;5积分抵现券;）
        djlx = IsNullValue(GetUrlParam("djlx"), "0");//优惠券定义里的发券类型（FQLX:0:按商品;2:按支付送;3:开卡礼";）
        vPageMsgID = '<%=CM_YHQGL_YHQFFDYD%>'
        switch (djlx+lx) {
            case "00":
                vCaption = "优惠券发放定义单";
                vPageMsgID = '<%=CM_YHQGL_YHQFFDYD%>';
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_CX%>);
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_SH%>);
                bCanStart = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_QD%>);
                bCanStop = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_ZZ%>);
                break;
            case "01": 
                vCaption = "促销礼品发放定义单"; 
                vPageMsgID = '<%=CM_YHQGL_YHQFFDYD_LP%>';
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_LP_CX%>);
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_LP_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_LP_SH%>);
                bCanStart = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_LP_QD%>);
                bCanStop = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_LP_ZZ%>);
                break;
            case "02": 
                vCaption = "促销抽奖发放定义单"; 
                vPageMsgID = '<%=CM_YHQGL_YHQFFDYD_CJ%>';
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_CJ_CX%>);
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_CJ_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_CJ_SH%>);
                bCanStart = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_CJ_QD%>);
                bCanStop = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_CJ_ZZ%>);
                break;
            case "03": 
                vCaption = "赠送积分定义单"; 
                vPageMsgID = '<%=CM_YHQGL_YHQFFDYD_JF%>';
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_JF_CX%>);
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_JF_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_JF_SH%>);
                bCanStart = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_JF_QD%>);
                bCanStop = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_JF_ZZ%>);                
                break;
            case "04": vCaption = "促销积分定义单"; break;

            case "20": vCaption = "优惠券发放定义单支付送"; break;
            case "21": vCaption = "促销礼品发放定义单支付送"; break;
            case "22": vCaption = "促销抽奖发放定义单支付送"; break;
            case "23": vCaption = "赠送积分定义单支付送"; break;
            case "24": vCaption = "促销积分定义单支付送"; break;

            case "33": vCaption = "赠送积分定义单首刷礼"; break;
        }
    </script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="HYXF_FQDYD.js"></script>
</head>
<body>
    <%=V_NoneBodyBegin %>
    <div class="bfbox">
        <div class="common_menu_tit">
            <span id='bftitle'></span>
        </div>
        <div class="maininput2">
            <div id='TreePanel' class='bfblock_left2'>
            </div>

            <div id='MainPanel' class='bfblock_right2'>
                <div class="bfrow">
                    <div class="bffld" id="jlbh">
                    </div>

                </div>

                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">促销活动</div>
                        <div class="bffld_right">
                            <input id="TB_CXZT" type="text" tabindex="1" />
                            <input id="HF_CXID" type="hidden" />
                            <input id="zHF_CXID" type="hidden" />
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left">券结束日期</div>
                        <div class="bffld_right">
                            <label id="TB_YHQSYJSRQ" />
                        </div>
                    </div>
                </div>
                <div class="bfrow">
                    <div class="bffld_l">
                        <div class="bffld_left">累计方式</div>
                        <div class="bffld_right">
                            <input class="magic-radio" type="radio" name="LJFS" id="CK_DB" value="0" />
                            <label for="CK_DB">单笔</label>
                            <input class="magic-radio" type="radio" name="LJFS" id="CK_DR" value="1" />
                            <label for="CK_DR">当日累计</label>
                            <input class="magic-radio" type="radio" name="LJFS" id="CK_HD" value="2" />
                            <label for="CK_HD">活动期累计</label>
                            <input class="magic-radio" type="radio" name="LJFS" id="CK_DJ" value="3" />
                            <label for="CK_DJ">单件</label>
                            <input class="magic-radio" type="radio" name="LJFS" id="CK_DBDG" value="6" />
                            <label for="CK_DBDG">单笔单柜</label>

                        </div>
                    </div>
                </div>

                <div id="tt" class="easyui-tabs" data-options="tools:'#tab-tools'" style="width: 100%; height: 39px">
                </div>
                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left" id="DV_CXSD" no_control="true">促销时段</div>
                        <div class="bffld_right">
                            <label runat="server" style="text-align: left; border-bottom: none" id="LB_CXSDSTR">请选择促销时段</label>
                            <input id="HF_CXSD" type="hidden" />
                        </div>
                    </div>
                </div>
                <div id="status-bar" style="top: 5px"></div>
            </div>
        </div>
    </div>
    <%=V_NoneBodyEnd %>
</body>
</html>
