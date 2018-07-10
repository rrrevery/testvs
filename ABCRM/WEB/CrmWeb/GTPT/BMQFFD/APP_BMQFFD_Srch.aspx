<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="APP_BMQFFD_Srch.aspx.cs" Inherits="BF.CrmWeb.APP.BMQFFD.APP_BMQFFD_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script src="APP_BMQFFD_Srch.js"></script>
    <script>
        vPageMsgID = '<%=CM_LMSHGL_BMQFFD%>';
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_LMSHGL_BMQFFD_CX%>');
    </script>

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
            <div class="bffld_left">活动名称</div>
            <div class="bffld_right">
                <input id="TB_NAME" type="text" tabindex="2" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">编码券名称</div>
            <div class="bffld_right">
                <input id="TB_BMQMC" type="text" />
                <input id="HF_BMQID" type="hidden" />
                <input id="zHF_BMQID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">渠道</div>
            <div class="bffld_right">
                <select id="DDL_CHANNELID">
                    <option></option>
                    <option value="0">微信</option>
                    <option value="1">APP</option>
                    <option value="2">后台</option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">促销主题</div>
            <div class="bffld_right">
                <input id="TB_CXZT" type="text" tabindex="1" />
                <input id="HF_CXID" type="hidden" />
                <input id="zHF_CXID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">单据状态</div>
            <div class="bffld_right">
                <input type="checkbox" name="CB_STATUS" value="0" class="magic-checkbox" id="C_BC" />
                <label for="C_BC">已保存</label>
                <input type="checkbox" name="CB_STATUS" value="1" class="magic-checkbox" id="C_SH" />
                <label for="C_SH">已审核</label>
                <input type="checkbox" name="CB_STATUS" value="2" class="magic-checkbox" id="C_QD" />
                <label for="C_QD">已启动</label>
                <input type="checkbox" name="CB_STATUS" value="-1" class="magic-checkbox" id="C_ZZ" />
                <label for="C_ZZ">已终止</label>
                <input type="hidden" id="HF_STATUS" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right">
                <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">券开始日期</div>
            <div class="bffld_right">
                <input id="TB_QKSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">券结束日期</div>
            <div class="bffld_right">
                <input id="TB_QJSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
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
            <div class="bffld_left">执行人</div>
            <div class="bffld_right">
                <input id="TB_ZXRMC" type="text" />
                <input id="HF_ZXR" type="hidden" />
                <input id="zHF_ZXR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">执行日期</div>
            <div class="bffld_right">
                <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">启动人</div>
            <div class="bffld_right">
                <input id="TB_DQRMC" type="text" />
                <input id="HF_QDR" type="hidden" />
                <input id="zHF_QDR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">启动日期</div>
            <div class="bffld_right">
                <input id="TB_QDSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_QDSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">终止人</div>
            <div class="bffld_right">
                <input id="TB_ZZRMC" type="text" />
                <input id="HF_ZZR" type="hidden" />
                <input id="zHF_ZZR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">终止日期</div>
            <div class="bffld_right">
                <input id="TB_ZZSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZZSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>




    <%=V_SearchBodyEnd %>
</body>
</html>
