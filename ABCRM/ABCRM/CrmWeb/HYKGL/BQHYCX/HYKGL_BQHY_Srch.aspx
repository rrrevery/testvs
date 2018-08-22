<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_BQHY_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.BQHYCX.HYKGL_BQHY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_BQHYCX%>';
        bCanShowPublic = CheckMenuPermit(iDJR, <%=CM_HYKGL_CXHYXX_XS%>);
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="HYKGL_BQHY_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
     <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                 <input id="TB_KLX" type="text"  />
                <input id="HF_KLX" type="hidden" />
                <input id="zHF_KLX" type="hidden" />
            </div>
        </div>
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
            <div class="bffld_left">标签</div>
            <div class="bffld_right">
                <input id="TB_HYBQ" type="text" />
                <input id="HF_HYBQ" type="hidden" />
                <input id="zHF_HYBQ" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">办卡业态</div>
            <div class="bffld_right">
                <input id="TB_SHMC" type="text" />
                <input id="HF_SHDM" type="hidden" />
                <input id="zHF_SHDM" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO1" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_HYKNO2" type="text" tabindex="2" runat="server" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">标签权重</div>
            <div class="bffld_right">
                <input id="TB_BQQZ" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">标签来源</div>
            <div class="bffld_right">
                <select id="DDL_BQLY">
                    <option></option>
                    <option value="0">消费标签</option>
                    <option value="1">手工标签</option>
                    <option value="2">导入标签</option>
                </select>
            </div>
        </div>
    </div>

        <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">出生日期</div>
            <div class="bffld_right">
                <input id="TB_CSRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_CSRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">生日</div>
            <div class="bffld_right">
                <input id="TB_SR1" type="text" class="Wdate" onfocus="WdatePicker({dateFmt:'MM-dd'} )" " style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_SR2" type="text" class="Wdate" onfocus="WdatePicker({dateFmt:'MM-dd'})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">有效期</div>
            <div class="bffld_right">
                <input id="TB_YXQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_YXQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">年龄</div>
            <div class="bffld_right">
                <input id="TB_Age1" type="text" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_Age2" type="text" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld" style="width:800px">
            <div class="bffld_left" style="width:12%">有效期</div>
            <div class="bffld_right" style="width:88%">
                <input class="magic-checkbox" type="checkbox" name="CB_STATUS" id="C_FS" value="0" />
                <label for="C_FS">有效卡(发售卡)</label>
                <input class="magic-checkbox" type="checkbox" name="CB_STATUS" id="C_XF" value="1" />
                <label for="C_XF">有效卡(已消费卡)</label>
                <input class="magic-checkbox" type="checkbox" name="CB_STATUS" id="C_TY" value="-4" />
                <label for="C_TY">无效卡(停用卡)</label>
                <input class="magic-checkbox" type="checkbox" name="CB_STATUS" id="C_GS" value="-3" />
                <label for="C_GS">无效卡(挂失卡) </label>
                <input class="magic-checkbox" type="checkbox" name="CB_STATUS" id="C_ZF" value="-1" />
                <label for="C_ZF">无效卡(作废卡)</label>
                <input type="hidden" id="HF_STATUS" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
