<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_JFDXBL_Srch.aspx.cs" Inherits="BF.CrmWeb.HYXF.JFDXBL.HYXF_JFDXBL_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_YHQGL_DEFJFDXBL%>';
    </script>
    <script src="YHQGL_JFDXBL_Srch.js"></script>

</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="4" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">状态</div>
            <div class="bffld_right">
                <input type="radio" value="0" name="djzt" />全部
                <input type="radio" value="1" name="djzt" />停用
                <input type="radio" value="2" name="djzt" />有效
            </div>
        </div>
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
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right">
                <input id="TB_KSRQ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_KSRQ2\')}'} )" tabindex="8" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_KSRQ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" tabindex="9" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSRQ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_JSRQ2\')}'} )" tabindex="11" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_JSRQ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" tabindex="9" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记人</div>
            <div class="bffld_right">
                <input id="TB_DJRMC" type="text" tabindex="5" />
                <input id="HF_DJR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">审核人</div>
            <div class="bffld_right">
                <input id="TB_ZXRMC" type="text" tabindex="5" />
                <input id="HF_ZXR" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记时间</div>
            <div class="bffld_right">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'} )" tabindex="8" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" tabindex="9" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">审核时间</div>
            <div class="bffld_right">
                <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'} )" tabindex="8" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" tabindex="9" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
