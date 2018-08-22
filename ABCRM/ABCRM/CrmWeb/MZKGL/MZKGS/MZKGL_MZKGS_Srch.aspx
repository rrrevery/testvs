<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKGS_Srch.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKGS.MZKGL_MZKGS_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vHF = GetUrlParam("hf");
        if (vHF == 0) {
            vPageMsgID = '<%=CM_MZKGL_MZKGS%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKGS_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKGS_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKGS_CX%>');
        }
        else {
            vPageMsgID = '<%=CM_MZKGL_MZKGSHF%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKGSHF_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKGSHF_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKGSHF_CX%>');
        }
    </script>
    <script src="MZKGL_MZKGS_Srch.js"></script>
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
            <div class="bffld_l">
                <div class="bffld_left">摘要</div>
                <div class="bffld_right">
                    <input id="TB_ZY" type="text" tabindex="6" />
                </div>
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
