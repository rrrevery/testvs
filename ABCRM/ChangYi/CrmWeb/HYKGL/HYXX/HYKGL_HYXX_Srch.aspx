<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYXX_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYXX.HYKGL_HYXX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_CXHYXX%>';
        bCanShowPublic = CheckMenuPermit(iDJR, <%=CM_HYKGL_CXHYXX_XS%>);
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="HYKGL_HYXX_Srch.js"></script>
    <style type="text/css">
        .spanstatus {
            display: -moz-inline-box;
            display: inline-block;
            width: 120px;
        }
    </style>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow" style="display: none;">
        <div class="bffld">
            <div class="bffld_left">主从卡</div>
            <div class="bffld_right">
                <input type="checkbox" name="CB_ZCK" />主卡
                <input type="checkbox" name="CB_ZCK" />从卡
                <input type="checkbox" name="CB_ZCK" />所有卡
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_KLX" type="text" />
                <input id="HF_KLX" type="hidden" />
                <input id="zHF_KLX" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">所属门店</div>
            <div class="bffld_right">
                <input id="TB_MD" type="text" />
                <input id="HF_MD" type="hidden" />
                <input id="zHF_MD" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow" style="display: none">
        <div class="bffld">
            <div class="bffld_left">商圈名称</div>
            <div class="bffld_right">
                <input id="TB_SQMC" type="text" />
                <input id="HF_SQID" type="hidden" />
                <input id="zHF_SQID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">匹配小区</div>
            <div class="bffld_right">
                <input id="TB_XQMC" type="text" />
                <input id="HF_XQID" type="hidden" />
                <input id="zHF_XQID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld" style="display: none">
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
                <input id="TB_HYKNO" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">会员姓名</div>
            <div class="bffld_right">
                <input id="TB_HYNAME" type="text" tabindex="2" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员性别</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" name="CB_SEX" id="C_A" value="" />
                <label for="C_A">全部</label>
                <input class="magic-checkbox" type="checkbox" name="CB_SEX" id="C_B" value="0" />
                <label for="C_B">男</label>
                <input class="magic-checkbox" type="checkbox" name="CB_SEX" id="C_G" value="1" />
                <label for="C_G">女</label>
                <input type="hidden" id="HF_SEX" />
            </div>
        </div>

        <div class="bffld">
            <div class="bffld_left">手机号码</div>
            <div class="bffld_right">
                <input id="TB_SJHM" type="text" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">证件类型</div>
            <div class="bffld_right">
                <select id="DDL_ZJLX" name="DDL_ZJLX" tabindex="4" runat="server">
                    <option></option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">证件号码</div>
            <div class="bffld_right">
                <input id="TB_ZJHM" type="text" tabindex="5" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow" style="display: none;">
        <div class="bffld">
            <div class="bffld_left">动态标记</div>
            <div class="bffld_right">
                <input id="TB_DTBJ" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">发行单位</div>
            <div class="bffld_right">
                <input id="TB_FXDWMC" type="text" />
                <input type="hidden" id="HF_FXDWID" />
                <input type="hidden" id="zHF_FXDWID" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">出生日期</div>
            <div class="bffld_right twodate">
                <input id="TB_CSRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,maxDate:'#F{$dp.$D(\'TB_CSRQ2\')}'})" />
                <span class="Wdate_span">至</span>
                <input id="TB_CSRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,minDate:'#F{$dp.$D(\'TB_CSRQ1\')}'})" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">生日</div>
            <div class="bffld_right twodate">
                <input id="TB_SR1" type="text" class="Wdate" onfocus="WdatePicker({dateFmt:'MM-dd',maxDate:'#F{$dp.$D(\'TB_SR2\')}'} )" />
                <span class="Wdate_span">至</span>
                <input id="TB_SR2" type="text" class="Wdate" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'TB_SR1\')}',dateFmt:'MM-dd'})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">年龄</div>
            <div class="bffld_right twovalue">
                <input id="TB_Age1" type="text" />
                <span class="Wdate_span">至</span>
                <input id="TB_Age2" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">交通工具</div>
            <div class="bffld_right">
                <select id="DDL_JTGJ">
                    <option></option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">教育程度</div>
            <div class="bffld_right">
                <select id="DDL_XL">
                    <option></option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">家庭收入(月)</div>
            <div class="bffld_right">
                <select id="DDL_JTSR">
                    <option></option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">家庭成员</div>
            <div class="bffld_right">
                <select id="DDL_JTCY">
                    <option></option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow" style="display: none">

        <div class="bffld" style="display: none">
            <div class="bffld_left">预警会员状态</div>
            <div class="bffld_right" style="white-space: nowrap;">
                <input type="checkbox" name="CB_KYHY" value="0" checked="checked" />全部
                <input type="checkbox" name="CB_KYHY" value="1" />非可疑 
                <input type="checkbox" name="CB_KYHY" value="2" />可疑 
                <input type="hidden" id="HF_KYHY" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">建卡日期</div>
            <div class="bffld_right twodate">
                <input id="TB_JKRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,maxDate:'#F{$dp.$D(\'TB_JKRQ2\')}'})" />
                <span class="Wdate_span">至</span>
                <input id="TB_JKRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">有效期</div>
            <div class="bffld_right twodate">
                <input id="TB_YXQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,maxDate:'#F{$dp.$D(\'TB_YXQ2\')}'})" />
                <span class="Wdate_span">至</span>
                <input id="TB_YXQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
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
            <div class="bffld_right twodate">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <fieldset style="height: 120px;">
            <legend>状态</legend>

            <div class="bffld">
                <%--<div class="bffld_left">状态</div>--%>
                <div class="bffld_right" style="white-space: nowrap; width: auto">
                    <input class="magic-checkbox" type="checkbox" name="CB_STATUS" id="C_YXK" value="0" />
                    <label for="C_YXK">有效卡(发售卡)</label>
                    <input class="magic-checkbox" type="checkbox" name="CB_STATUS" id="C_FSK" value="1" />
                    <label for="C_FSK">有效卡(已消费卡)</label>
                    <input class="magic-checkbox" type="checkbox" name="CB_STATUS" id="C_TYK" value="-4" />
                    <label for="C_TYK">无效卡(停用卡)</label>
                    <input class="magic-checkbox" type="checkbox" name="CB_STATUS" id="C_GSK" value="-3" />
                    <label for="C_GSK">无效卡(挂失卡)</label>
                    <input class="magic-checkbox" type="checkbox" name="CB_STATUS" id="C_ZFK" value="-1" />
                    <label for="C_ZFK">无效卡(作废卡)</label>
                    <%--<input type="checkbox" name="CB_STATUS" value="-8" />无效卡(应回收卡)
                                        <input type="checkbox" name="CB_STATUS" value="-7" />无效卡(应降级卡)
                                        <input type="checkbox" name="CB_STATUS" value="-6" />无效卡(终止卡)
                                        <input type="checkbox" name="CB_STATUS" value="-5" />无效卡(纸券已经消费)--%>
                    <%--                    <input type="checkbox" name="CB_STATUS" value="0" />有效卡(发售卡)
                    <input type="checkbox" name="CB_STATUS" value="1" />有效卡(已消费卡)
                    <input type="checkbox" name="CB_STATUS" value="-4" />无效卡(停用卡)--%>
                    <%--<br />--%>
                    <%--<input type="checkbox" name="CB_STATUS" value="-3" />无效卡(挂失卡)--%>
                    <%--<input type="checkbox" name="CB_STATUS" value="-2" />无效卡(退卡)--%>
                    <%-- <input type="checkbox" name="CB_STATUS" value="-1" />无效卡(作废卡)--%>
                    <%--<br />
                                            <input type="checkbox" name="CB_STATUS" value="2" />有效卡(升级卡)
                                        <input type="checkbox" name="CB_STATUS" value="3" />有效卡(睡眠卡)
                                        <input type="checkbox" name="CB_STATUS" value="4" />有效卡(异常卡)--%>
                    <input type="hidden" id="HF_STATUS" />
                </div>


            </div>
        </fieldset>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
