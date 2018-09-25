<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKGS.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKGL_HYKGS" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
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
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="HYKGL_HYKGS.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" />
                <input id="HF_HYID" type="hidden" />
                <input id="HF_BJ_CHILD" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">操作地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">姓名</div>
            <div class="bffld_right">
                <label id="LB_HYNAME" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">积分</div>
            <div class="bffld_right">
                <label id="LB_JF" style="text-align: left;" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">金额</div>
            <div class="bffld_right">
                <label id="LB_JE" style="text-align: left;" runat="server" />
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
            <div class="bffld_left">发行单位</div>
            <div class="bffld_right">
                <label id="LB_FXDWMC" runat="server" style="text-align: left;"></label>

                <%-- <input id="TB_FXDWMC" runat="server" readonly="true" />--%>
                <input id="HF_FXDWDM" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡原状态</div>
            <div class="bffld_right">
                <label id="LB_YZT" style="text-align: left;" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡新状态</div>
            <div class="bffld_right">
                <label id="LB_XZT" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>


    <div class="bfrow" id="A" style="display:none">
        <div class="bffld">
            <div class="bffld_left">
                <input type="button" value="查子卡" id="btn_HYKHM" />
            </div>
            <div class="bffld_right">
                <input id="TB_HYKHM_C" type="text" />
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" class="long" type="text" />
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
