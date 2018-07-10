<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_YHQDQYE_Srch.aspx.cs" Inherits="BF.CrmWeb.YHQGL.YHQDQYE.YHQGL_YHQDQYE_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>vPageMsgID = '<%=CM_HYKGL_YHQ_CurrYE%>';</script>
    <script src="YHQGL_YHQDQYE_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO1" type="text" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_HYKNO2" type="text" />
            </div>
        </div>
    </div>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">余额</div>
            <div class="bffld_right">
                <input id="TB_YE1" type="text" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_YE2" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSRQ1" type="text" class="Wdate" onfocus="WdatePicker()" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_JSRQ2" type="text" class="Wdate" onfocus="WdatePicker()" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input id="TB_YHQMC" type="text" />
                <input id="HF_YHQID" type="hidden" />
                <input id="zHF_YHQID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">促销活动</div>
            <div class="bffld_right">
                <input id="TB_CXZT" type="text" />
                <input id="HF_CXID" type="hidden" />
                <input id="zHF_CXID" type="hidden" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡状态</div>
            <div class="bffld_right" style="width: auto">
                <input id="Radio1" type="radio" name="cld" value="0" />全部
                <input id="Radio2" type="radio" name="cld" value="1" checked="checked" />有效卡
                <input id="Radio3" type="radio" name="cld" value="-4" />停用卡
                <input id="Radio4" type="radio" name="cld" value="3" />无效卡
                <input id="HF_HYKStatus" type="hidden" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
