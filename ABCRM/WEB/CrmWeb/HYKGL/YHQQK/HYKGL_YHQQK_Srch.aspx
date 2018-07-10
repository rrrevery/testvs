<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_YHQQK_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.YHQQK.HYKGL_YHQQK_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_YHQZHQKCL%>';
    </script>
    <script src="HYKGL_YHQQK_Srch.js"></script>



    <%--必须放在后面--%>
    <style>
        input[type='button'] {
            width: 80px;
        }
    </style>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" name="" />
            </div>
        </div>
        <div class="bffld">

            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" name="" />
            </div>
            <%--                                        <div class="bffld_left">操作地点</div>
                                        <div class="bffld_right">
                                            <input id="TB_BGDDDM" type="text" tabindex="3" />
                                            <input id="HF_BGDDDM" type="hidden" />
                                            <input id="zHF_BGDDDM" type="hidden" />
                                        </div>--%>
        </div>
    </div>
    <div class="bfrow">

        <div class="bffld">
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input id="TB_YHQ" type="text" tabindex="3" />
                <input id="HF_YHQ" type="hidden" />
                <input id="zHF_YHQ" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">促销活动</div>
            <div class="bffld_right">
                <input id="TB_CXHD" type="text" tabindex="3" />
                <input id="HF_CXHD" type="hidden" />
                <input id="zHF_CXHD" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow" style="display: none">

        <div class="bffld">
            <div class="bffld_left">操作门店</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">原余额</div>
            <div class="bffld_right">
                <input id="TB_YYE" type="text" name="" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">取款金额</div>
            <div class="bffld_right">
                <input id="TB_QKJE" type="text" name="" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记人</div>
            <div class="bffld_right">
                <input id="TB_DJRMC" type="text" />
                <input id="HF_DJR" type="hidden" />
                <input id="zHF_DJR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">登记时间</div>
            <div class="bffld_right">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">审核人</div>
            <div class="bffld_right">
                <input id="TB_ZXRMC" type="text" />
                <input id="HF_ZXR" type="hidden" />
                <input id="zHF_ZXR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">审核日期</div>
            <div class="bffld_right">
                <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>

    </div>
    <%=V_SearchBodyEnd %>
    <div id="menuContent" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
</body>
</html>
