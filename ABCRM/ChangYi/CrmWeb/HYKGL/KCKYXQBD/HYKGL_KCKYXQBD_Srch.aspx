<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_KCKYXQBD_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.KCKYXQBD.HYKGL_KCKYXQBD_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vCZK = GetUrlParam("czk");
        if (vCZK == "0") {
            vPageMsgID =  <%=CM_HYKGL_KCKYXQBG%>;
            bCanEdit = CheckMenuPermit(iDJR,  <%=CM_HYKGL_KCKYXQBG_LR%>);
            bCanExec = CheckMenuPermit(iDJR,  <%=CM_HYKGL_KCKYXQBG_SH%>);
            bCanSrch = CheckMenuPermit(iDJR,  <%=CM_HYKGL_KCKYXQBG_CX%>);
        }
        if (vCZK == "1") {
            vPageMsgID = <%=CM_MZKGL_KCKYXQBG%>;
            bCanEdit = CheckMenuPermit(iDJR, <%=CM_MZKGL_KCKYXQBG_LR%>);
            bCanExec = CheckMenuPermit(iDJR, <%=CM_MZKGL_KCKYXQBG_SH%>);
            bCanSrch = CheckMenuPermit(iDJR, <%=CM_MZKGL_KCKYXQBG_CX%>);
        }                
    </script>
    <script src="HYKGL_KCKYXQBD_Srch.js"></script>
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
                <input id="TB_HYKNAME" type="text" tabindex="1" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">包含卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" tabindex="1" />
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
            <div class="bffld_left">新有效期</div>
            <div class="bffld_right">
                <input id="TB_XYXQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_XYXQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记人</div>
            <div class="bffld_right">
                <input id="TB_DJRMC" type="text" />
                <input id="HF_DJR" type="hidden" />
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
            <div class="bffld_left">审核人 </div>
            <div class="bffld_right">
                <input id="TB_ZXRMC" type="text" />
                <input id="HF_ZXR" type="hidden" />
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
