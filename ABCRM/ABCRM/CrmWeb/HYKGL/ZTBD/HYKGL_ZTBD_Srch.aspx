<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_ZTBD_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.ZTBD.HYKGL_ZTBD_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vCZK = IsNullValue(GetUrlParam("czk"), 0);
        if (vCZK == "0") {
            vPageMsgID = '<%= CM_HYKGL_HYKZTBD%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYKGL_HYKZTBD_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYKGL_HYKZTBD_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_HYKGL_HYKZTBD_CX%>');
        }
        else {
            vPageMsgID = '<%= CM_HYKGL_MZKZTBD%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYKGL_MZKZTBD_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYKGL_MZKZTBD_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_HYKGL_MZKZTBD_CX%>');
        }
    </script>
    <script src="HYKGL_ZTBD_Srch.js"></script>
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
            <div class="bffld_left">操作地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="zHF_BGDDDM" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">包含卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" />
            </div>
        </div>
        <div class="bffld" style="display: none">
            <div class="bffld_left">操作门店</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">工本费</div>
            <div class="bffld_right">
                <input id="TB_GBF" type="text" tabindex="2" name="s" onblur="checkvalue(this)" />
                <script>
                    function checkvalue(obj) {
                        if (!/^[+|-]?\d+\.?\d*$/.test(obj.value) && obj.value != '') {
                            alert('请输入数字！');
                            obj.select();
                        }
                    }
                </script>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">新状态：</div>
            <div class="bffld_right">
                <select id="DDL_NEW_STATUS">
                    <option></option>
                    <option value="0">发售卡</option>
                    <option value="1">已消费卡</option>
                     <option value="-4">停用卡</option>

                </select>
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
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" type="text" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
