<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKSJ.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKSJ.HYKGL_HYKSJ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        sj = GetUrlParam("sj");
        if (sj == 1) {
            vPageMsgID = '<%=CM_HYKGL_HYSJHKCL%>';
            bCanEdit = '<%=CM_HYKGL_HYJJHKCL_LR%>';
            bCanExec = '<%=CM_HYKGL_HYJJHKCL_SH%>';
        }
        else {
            vPageMsgID = '<%=CM_HYKGL_HYJJHKCL%>';
            bCanEdit = '<%=CM_HYKGL_HYJJHKCL_LR%>';
            bCanExec = '<%=CM_HYKGL_HYJJHKCL_SH%>';
        }
    </script>
    <script src="HYKGL_HYKSJ.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">
                原卡号<%--<input type="button" value="原卡号" id="btn_HYKHM_OLD" />--%>
            </div>
            <div class="bffld_right">
                <input id="TB_HYKHM_OLD" type="text" />
                <input id="HF_YJKRQ" type="hidden" />
                <input id="HF_HYID" type="hidden" />
                <input id="HF_FXDW" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <label id="LB_BGDDMC" style="text-align: left;" runat="server" />
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
            <div class="bffld_left">原卡类型</div>
            <div class="bffld_right">
                <label id="LB_HYKNAMEOLD" style="text-align: left;" runat="server" />
                <input id="HF_HYKTYPEOLD" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">未处理积分</div>
            <div class="bffld_right">
                <label id="LB_WCLJF" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">本期积分</div>
            <div class="bffld_right">
                <label id="LB_BQJF" style="text-align: left;" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">消费金额</div>
            <div class="bffld_right">
                <label id="LB_BQXFJE" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">新卡号</div>
            <div class="bffld_right">
                <div class="dv_sub_right">
                    <input id="TB_HYKHM_NEW" type="text" />
                </div>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">新卡类型</div>
            <div class="bffld_right">
                <label id="LB_HYKNAMENEW" style="text-align: left;" runat="server" />
                <input id="HF_HYKTYPENEW" type="hidden" />
                <input id="HF_BJ_XFJE" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" >升级积分</div>
            <div class="bffld_right">
                <label id="LB_SJJF" style="text-align: left;" runat="server" />
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
    <%=V_InputBodyEnd %>
</body>
</html>
