<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_GKZYDALR_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.GKZYDALR.HYKGL_GKZYDALR_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>

    <script>
        vPageMsgID = '<%=CM_HYKGL_GKZYDALR%>'
    </script>
    <script src="HYKGL_GKZYDALR_Srch.js"></script>
</head>

<body>
    <%=V_SearchBodyBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">顾客ID</div>
            <div class="bffld_right">
                <input id="TB_GKID" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">身份证号</div>
            <div class="bffld_right">
                <input id="TB_SFZBH" type="text" />
            </div>

        </div>
        <%--                                    <div class="bffld">
                                        <div class="bffld_left">手机号码</div>
                                        <div class="bffld_right">
                                            <input id="TB_SJHM" type="text" runat="server" />
                                        </div>
                                    </div>--%>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">更新人</div>
            <div class="bffld_right">
                <input id="TB_GXRMC" type="text" />
                <input id="HF_GXR" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">更新时间</div>
            <div class="bffld_right">
                <input id="TB_GXSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_GXSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
