<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXTSJL.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXTSJL.GTPT_WXTSJL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetHYXX.js"></script>
    <script src="GTPT_WXTSJL.js"></script>
    <script>
        vPageMsgID = <%=CM_GTPT_TSCL%>
        bCanEdit = CheckMenuPermit(iDJR, <%=CM_GTPT_TSCL_CL %>);
        bCanExec = CheckMenuPermit(iDJR, <%=CM_GTPT_TSCL_HF%>);
        bCanSrch = CheckMenuPermit(iDJR, <%=CM_GTPT_TSCL_CX%>);
    </script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">单据状态</div>
            <div class="bffld_right">
                <label id="LB_STATUS" style="text-align: left;" runat="server" />
                <input id="HF_STATUS" type="hidden" />
                <input id="HF_OPENID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">投诉类型</div>
            <div class="bffld_right">
                <label id="LB_TSLX" style="text-align: left;" runat="server" />
                <input id="HF_TSLX" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">投诉日期</div>
            <div class="bffld_right">
                <label id="LB_TSRQ" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">
                顾客姓名
            </div>
            <div class="bffld_right">
                <label id="LB_GKXM" style="text-align: left;" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">
                联系电话
            </div>
            <div class="bffld_right">
                <label id="LB_LXDH" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">
                门店名称
            </div>
            <div class="bffld_right">

                <label id="LB_MDMC" style="text-align: left;" runat="server" />
                <input id="HF_MDID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <label id="LB_HYK_NO" style="text-align: left;" runat="server" />
                <input id="HF_HYID" type="hidden" />
            </div>

        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">投诉内容</div>
            <div class="bffld_right">
                 <label id="LB_TSNR" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">处理意见</div>
            <div class="bffld_right">
                <input id="TB_CLYJ" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow" style="display:none">
        <div class="bffld_l">
            <div class="bffld_left">反馈信息</div>
            <div class="bffld_right">
                <input id="TB_FKXX" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow" id="hfjg">
        <div class="bffld">
            <div class="bffld_left">
                回访结果
            </div>
            <div class="bffld_right">
                <label id="LB_HFJG" style="text-align: left;" runat="server" />
            </div>
        </div>

        <div class="bffld">
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
