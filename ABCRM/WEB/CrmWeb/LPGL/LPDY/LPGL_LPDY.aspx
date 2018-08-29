<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LPGL_LPDY.aspx.cs" Inherits="BF.CrmWeb.LPGL.LPDY.LPGL_LPDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%=V_Head_Input %>
    <script>
        vPageMsgID = <%=CM_LPGL_JFFHLPDY%>;
    </script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="LPGL_LPDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow" style="display: none">
        <div class="bffld">
        </div>

        <div class="bffld">
            <div id="jlbh">
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">礼品代码</div>
            <div class="bffld_right">

                <input id='TB_LPDM' type='text' readonly='readonly' style='background-color: bisque;' />

            </div>
        </div>

        <div class="bffld">
            <div class="bffld_left">礼品类型</div>
            <div class="bffld_right">
                <select id="DDL_LPLX">
                    <option value="0" id="a">普通礼品</option>
                    <option value="1" id="b">纸券</option>
                </select>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">礼品名称</div>
            <div class="bffld_right">
                <input id="TB_LPMC" runat="server" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">礼品规格</div>
            <div class="bffld_right">
                <input id="TB_LPGG" runat="server" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow" style="display: none;">
        <div class="bffld">
            <div class="bffld_left">礼品材质</div>
            <div class="bffld_right">
                <select id="S_LPCZ">
                    <option></option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">礼品颜色</div>
            <div class="bffld_right">
                <select id="S_LPYS">
                    <option></option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow" style="display: none;">
        <div class="bffld">
            <div class="bffld_left">礼品款式</div>
            <div class="bffld_right">
                <select id="S_LPKS">
                    <option></option>
                </select>
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">礼品单价</div>
            <div class="bffld_right">
                <input id="TB_LPDJ" runat="server" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">礼品进价</div>
            <div class="bffld_right">
                <input id="TB_LPJJ" runat="server" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">扣减积分</div>
            <div class="bffld_right">
                <input id="TB_LPJF" runat="server" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">礼品分类</div>
            <div class="bffld_right">
                <input id="TB_LPFLMC" type="text" tabindex="3" />
                <input id="HF_LPFLID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow" style="display: none">
        <div class="bffld">
            <div class="bffld_left">
                商户商标
            </div>
            <div class="bffld_right">
                <input id="TB_SHSBMC" runat="server" type="text" />
                <input id="HF_SHSBID" type="hidden" />
                <input id="zHF_SHSBID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">礼品国际码</div>
            <div class="bffld_right">
                <input id="TB_LPGJBM" runat="server" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">不限制库存</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="CK_BJ_WKC" value="" />
                <label for="CK_BJ_WKC"></label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">停用</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="CK_BJ_DEL" value="" />
                <label for="CK_BJ_DEL"></label>
            </div>
        </div>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>

