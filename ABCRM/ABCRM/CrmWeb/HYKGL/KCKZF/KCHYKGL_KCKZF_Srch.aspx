<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KCHYKGL_KCKZF_Srch.aspx.cs" Inherits="BF.CrmWeb.KCHYKGL.KCKGL.KCHYKGL_KCKZF_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        var vHF = GetUrlParam("hf");
        if (vHF == "0") {
            vPageMsgID = '<%=CM_HYKGL_KCKZF%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYKGL_KCKZF_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYKGL_KCKZF_SH%>');
        }
        if (vHF == "1") {
            vPageMsgID = '<%=CM_HYKGL_KCKZFHF%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYKGL_KCKZFHF_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYKGL_KCKZFHF_SH%>');
        }
    </script>
    <script src="KCHYKGL_KCKZF_Srch.js"></script>
    <script src="../../CrmLib/CrmLib_FillHYKTYPE.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" />
            </div>
        </div>
        <div class="bffld" style="display:none">
            <div class="bffld_left">操作门店</div>
            <div class="bffld_right">
                <input id="TB_CZMD" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">

        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="1" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>

        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDDM" type="text" tabindex="3" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="zHF_BGDDDM" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">

        <div class="bffld">
            <div class="bffld_left">作废数量</div>
            <div class="bffld_right">
                <input id="TB_ZFKSL" type="text" tabindex="2" />
            </div>
        </div>

        <div class="bffld">
            <div class="bffld_left">作废金额</div>
            <div class="bffld_right">
                <input id="TB_ZFKJE" type="text" tabindex="2" />
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

    <div class="bfrow" style="display:none">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY"  type="text" />
            </div>

        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
