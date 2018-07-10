<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKHK_Srch.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKHK.MZKGL_MZKHK_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = <%=CM_MZKGL_MZKHK%>;
        bCanEdit = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKHK_LR%>);        
        bCanExec = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKHK_SH%>);
        bCanSrch = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKHK_CX%>);
    </script>
    <script src="MZKGL_MZKHK_Srch.js"></script>
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
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">旧卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKHM_OLD" type="text" tabindex="2" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">新卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKHM_NEW" type="text" tabindex="3" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="zHF_BGDDDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="4" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" type="text" tabindex="6" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
