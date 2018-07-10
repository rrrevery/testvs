<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKSKQKCX_Srch.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKSKQKCX.MZKGL_MZKSKQKCX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_MZKGL_MZKSKQKCX%>';
    </script>
    <script src="MZKGL_MZKSKQKCX_Srch.js"></script>
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
            <div class="bffld_left">面值金额</div>
            <div class="bffld_right">
                <input type="text" id="TB_MZJE" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">实收金额</div>
            <div class="bffld_right">
                <input type="text" id="TB_SSJE" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">客户</div>
            <div class="bffld_right">
                <input id="TB_KHMC" type="text" tabindex="2" />
                <input id="HF_KHID" type="hidden" />
                <input id="zHF_KHDM" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDDM" type="text" tabindex="3" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="zHF_BGDDDM" type="hidden" />
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
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">启动日期</div>
            <div class="bffld_right twodate">
                <input id="TB_QDSJ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_DJSJ2\')}'} )" tabindex="11" />
                <span class="Wdate_span">至</span>
                <input id="TB_QDSJ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" tabindex="9" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">单据状态</div>
            <div class="bffld_right" style="white-space: nowrap;">
                <input type="checkbox" name="CB_STATUS" value="0" class="magic-checkbox" id="c0" />
                <label for="c0">未审核</label>
                <input type="checkbox" name="CB_STATUS" value="1" class="magic-checkbox" id="c1" />
                <label for="c1">已审核</label>
                <input type="checkbox" name="CB_STATUS" value="2" class="magic-checkbox" id="c2" />
                <label for="c2">已启动</label>
                <input type="hidden" id="HF_STATUS" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">付款情况</div>
            <div class="bffld_right" style="white-space: nowrap;">
                <input type="radio" name="RD_FKQK" value="2" checked="checked" class="magic-radio" id="r2" />
                <label for="r2">全部</label>
                <input type="radio" name="RD_FKQK" value="1" class="magic-radio" id="r1" />
                <label for="r1">已付清</label>
                <input type="radio" name="RD_FKQK" value="0" class="magic-radio" id="r0" />
                <label for="r0">未付清</label>
                <input type="hidden" id="HF_FKQK" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
