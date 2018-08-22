<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_KHDALR.aspx.cs" Inherits="BF.CrmWeb.MZKGL.KHDALR.MZKGL_KHDALR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_MZKDKHGL_DKHZLLR%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_MZKDKHGL_DKHZLLR_LR%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_MZKDKHGL_DKHZLLR_CX%>');
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="MZKGL_KHDALR.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">客户代码</div>
            <div class="bffld_right">
                <input id="TB_KHDM" type="text" readonly="true" class="cs" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">客户姓名</div>
            <div class="bffld_right">
                <input id="TB_KHXM" type="text" />
                <input id="HF_PYM" type="hidden"  />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">客户网址</div>
            <div class="bffld_right">
                <input type="text" id="TB_KHWZ" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">客户地址</div>
            <div class="bffld_right">
                <input id="TB_KHDZ" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="text-wrap: none">联系人邮箱</div>
            <div class="bffld_right">
                <input id="TB_LXRYX" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">联系人</div>
            <div class="bffld_right">
                <input id="TB_LXRMC" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="text-wrap: none">联系人手机</div>
            <div class="bffld_right">
                <input id="TB_LXRSJ" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" style="text-wrap: none">联系人邮编</div>
            <div class="bffld_right">
                <input id="TB_LXRYB" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="text-wrap: none">联系人电话</div>
            <div class="bffld_right">
                <input id="TB_LXRDH" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" style="text-wrap: none">联系人证件</div>
            <div class="bffld_right">
                <select id="DDL_ZJLX">
                    <option></option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="text-wrap: none">证件号码</div>
            <div class="bffld_right">
                <input id="TB_LXRZJHM" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">联系人地址</div>
            <div class="bffld_right">
                <input id="TB_LXRTXDZ" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">备注</div>
            <div class="bffld_right">
                <input id="TB_ZY" type="text" />
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
