<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKGS_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKGS.HYKGL_HYKGS_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vHF = IsNullValue(GetUrlParam("hf"),"0");
        vCZK = IsNullValue(GetUrlParam("czk"),"0");
        vCaption = vCZK == "0" ? "会员卡" : "面值卡";
        if(vCZK=="0")
        {
            if (vHF == "0") {
                vPageMsgID = <%=CM_HYKGL_HYKGS%>;
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKGS_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKGS_SH%>);
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKGS_CX%>);
            }
            else {
                vPageMsgID = <%=CM_HYKGL_HYKGSHF%>;
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKGSHF_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKGSHF_SH%>);
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKGSHF_CX%>);
            }
        }
        else
        {
            if (vHF == "0") {
                vPageMsgID = <%=CM_MZKGL_MZKGS%>;
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKGS_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKGS_SH%>);
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKGS_CX%>);
            }
            else {
                vPageMsgID = <%=CM_MZKGL_MZKGSHF%>;
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKGSHF_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKGSHF_SH%>);
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKGSHF_CX%>);
            }
        }
    </script>
    <script src="HYKGL_HYKGS_Srch.js"></script>
    <style type="text/css">
        input[type='text'] {
            text-align: left;
        }
    </style>

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
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" tabindex="2" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="4" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">会员姓名</div>
            <div class="bffld_right">
                <input id="TB_HYNAME" type="text" tabindex="1" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">操作地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="zHF_BGDDDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" type="text" tabindex="6" />
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
</body>
</html>
