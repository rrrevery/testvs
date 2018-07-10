<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKKCBB_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKKCBB.HYKGL_HYKKCBB_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_HYKKCBB%>';
    </script>
    <script src="HYKGL_HYKKCBB_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow" style="display: none">
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" />
            </div>
        </div>
        <div class="bffld">
            <%--                                    <div class="bffld_left">建卡编号</div>
                                    <div class="bffld_right">
                                        <input id="TB_JKBH" type="text" />
                                    </div>--%>
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
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="zHF_BGDDDM" type="hidden" />
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
        <div class="bffld">
            <div class="bffld_left">保管人</div>
            <div class="bffld_right">
                <input id="TB_BGRMC" type="text" />
                <input id="HF_BGR" type="hidden" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
