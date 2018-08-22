<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKKSJJL_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKKSJJL.HYKGL_HYKKSJJL_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_SrchHYKKSJJL%>';
    </script>
    <script src="HYKGL_HYKKSJJL_Srch.js"></script>

</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">原卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>

        </div>
        <div class="bffld">
            <div class="bffld_left">新卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME_NEW" type="text" />
                <input id="HF_HYKTYPE_NEW" type="hidden" />
                <input id="zHF_HYKTYPE_NEW" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
    </div>
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
            <div class="bffld_left">有效期积分</div>
            <div class="bffld_right">
                <input id="TB_BQJF1" type="text" />
            </div>

        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_BQJF2" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">可升级日期</div>
            <div class="bffld_right">
                <input id="TB_SJRQ1" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" type="text" style="width: 45%; float: left;" />
                 <span class="Wdate_span">至</span>
                <input id="TB_SJRQ2" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" type="text" style="width: 45%; float: left;" />
            </div>

        </div>
        <div class="bffld">
            <div class="bffld_left">有效期</div>
            <div class="bffld_right">
                <input id="TB_YXQ1" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" type="text" style="width: 45%; float: left;" />
                 <span class="Wdate_span">至</span>
                <input id="TB_YXQ2" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" type="text" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
    <div class="bfrow"  style="display:none">
        <div class="bffld">
            <div class="bffld_left">排除可疑会员</div>
            <div class="bffld_right">
                <input type="checkbox" name="CB_KY" value="1" />
                <input type="hidden" id="HF_KY" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
