<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_YHQQK.aspx.cs" Inherits="BF.CrmWeb.HYKGL.YHQQK.HYKGL_YHQQK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_YHQZHQKCL%>';
    </script>
    <script src="HYKGL_YHQQK.js"></script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" onblur="GetHYXX();" />
                <input id="HF_HYID" type="hidden" />
                <input type="button" id="btn_HYKHM" class="bfbtn btn_search" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型 </div>
            <div class="bffld_right">
                <label id="LB_HYKNAME" runat="server" />
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">券账户</div>
            <div class="bffld_right">
                <label id="LB_YHQMC" runat="server" style="text-align: left"></label>
                <input id="HF_YHQID" type="hidden" />
                <input type="button" class="bfbtn btn_query" id="BTN_YHQZH" />
            </div>

        </div>
        <div class="bffld">
            <div class="bffld_left">促销活动</div>
            <div class="bffld_right">
                <label runat="server" style="text-align: left" id="LB_CXHD"></label>
                <input id="HF_CXHD" type="hidden" />
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">门店范围代码</div>
            <div class="bffld_right">
                <label id="LB_MDFWDM" style="text-align: left"></label>
                <input id="HF_YHQZH" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">门店范围</div>
            <div class="bffld_right">
                <label id="LB_MDFWMC" style="text-align: left"></label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">原金额</div>
            <div class="bffld_right">
                <label id="LB_YJE" style="text-align: left"></label>
            </div>

        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期  </div>
            <div class="bffld_right">
                <%--<input id="TB_JSRQ" type="text" class="Wdate" readonly="true" />--%>
                <label id="LB_JSRQ" style="text-align: left"></label>
            </div>
        </div>
    </div>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">取款金额</div>
            <div class="bffld_right">
                <input id="TB_QKJE" type="text" onblur="PDYE();" />
            </div>

        </div>
        <div class="bffld">
            <div class="bffld_left">操作地点 </div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
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
    <input type="hidden" id="HF_CZYMDID" />
    <%=V_InputBodyEnd %>
</body>
</html>
