<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKLQ_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKLQ.HYKGL_HYKLQ_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vCZK = IsNullValue(GetUrlParam("czk"), "0");
        vDJLX = GetUrlParam("djlx");
        if (vCZK=="0"){
            if (vDJLX == "0") {
                vPageMsgID =  <%=CM_HYKGL_HYKLQ%>;
                bCanEdit = CheckMenuPermit(iDJR,  <%=CM_HYKGL_HYKLQ_LR%>);
                bCanExec = CheckMenuPermit(iDJR,  <%=CM_HYKGL_HYKLQ_SH%>);
                bCanSrch = CheckMenuPermit(iDJR,  <%=CM_HYKGL_HYKLQ_CX%>);
            }
            if (vDJLX == "1") {
                vPageMsgID = <%=CM_HYKGL_HYKDB%>;
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKDB_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKDB_SH%>);
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKDB_CX%>);
            }
            if (vDJLX == "2") {
                vPageMsgID = <%=CM_HYKGL_HYKTL%>;
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKTL_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKTL_SH%>);
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKTL_CX%>);
            }

        }
        else{
            if (vDJLX == "0") {
                vPageMsgID =  <%=CM_MZKGL_MZKLQ%>;
                bCanEdit = CheckMenuPermit(iDJR,  <%=CM_MZKGL_MZKLQ_LR%>);
                bCanExec = CheckMenuPermit(iDJR,  <%=CM_MZKGL_MZKLQ_SH%>);
                bCanSrch = CheckMenuPermit(iDJR,  <%=CM_MZKGL_MZKLQ_CX%>);
            }
            if (vDJLX == "1") {
                vPageMsgID = <%=CM_MZKGL_MZKLQ%>;
                        bCanEdit = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKDB_LR%>);
                        bCanExec = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKDB_SH%>);
                        bCanSrch = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKDB_CX%>);
                    }
                    if(vDJLX=="2")
                    {
                        vPageMsgID = <%=CM_MZKGL_MZKTL%>;
            bCanEdit = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKTL_LR%>);
            bCanExec = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKTL_SH%>);
            bCanSrch = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKTL_CX%>);
        }

    }
    </script>
    <script src="HYKGL_HYKLQ_Srch.js"></script>
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
            <div class="bffld_left">包含卡号</div>
            <div class="bffld_right">
                <input id="TB_CZKHM" type="text" tabindex="8" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">拨出地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDDM_BC" type="text" tabindex="3" />
                <input id="HF_BGDDDM_BC" type="hidden" />
                <input id="zHF_BGDDDM_BC" type="hidden" />
            </div>

        </div>
        <div class="bffld">
            <div class="bffld_left">拨入地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDDM_BR" type="text" tabindex="3" />
                <input id="HF_BGDDDM_BR" type="hidden" />
                <input id="zHF_BGDDDM_BR" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">领用数量</div>
            <div class="bffld_right">
                <input id="TB_HYKSL" type="text" tabindex="8" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">领取人</div>
            <div class="bffld_right">
                <input id="TB_LQRMC" type="text" />
                <input id="HF_LQR" type="hidden" />
                <input id="zHF_LQR" type="hidden" />
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

    <div class="bfrow" style="display: none">
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
