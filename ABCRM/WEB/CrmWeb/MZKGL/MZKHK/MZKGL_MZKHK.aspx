<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKHK.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKHK.MZKGL_MZKHK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_MZKGL_MZKHK%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKHK_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKHK_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_MZKGL_MZKHK_CX%>');
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="MZKGL_MZKHK.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">原卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKHM_OLD" type="text" onblur="GetMZKXX();" />
                <input id="HF_HYID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <label id="LB_HYKNAME" style="text-align: left;" runat="server" />
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">
                金额
            </div>
            <div class="bffld_right">
                <label id="LB_JE" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">新卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKHM_NEW" type="text" onblur="GetKCKXX()" />
                <input type="button" id="btn_HYKHM_NEW" class="bfbtn btn_search" style="display:none" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">操作地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" readonly="readonly" style="background-color: bisque;" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">工本费</div>
            <div class="bffld_right">
                <input id="TB_GBF" name="TB_GBF" type="text" tabindex="1" value="0"/>
                <%--<span class="Currency"><i class="fa fa-jpy fa-lg" aria-hidden="true"></i></span>--%>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY"  type="text" />
            </div>
        </div>
    </div>


    <%=V_InputBodyEnd %>
</body>
</html>
