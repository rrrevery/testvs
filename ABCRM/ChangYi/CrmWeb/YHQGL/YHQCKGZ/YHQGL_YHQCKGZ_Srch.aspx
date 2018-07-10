<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_YHQCKGZ_Srch.aspx.cs" Inherits="BF.CrmWeb.YHQGL.YHQCKGZ.YHQGL_YHQCKGZ_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_YHQGL_YHQYCZZ%>';
    </script>
    <script src="YHQGL_YHQCKGZ_Srch.js"></script>
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
            <%--                                    <div class="bffld_left">姓名</div>
                                    <div class="bffld_right">
                                        <input id="TB_HYNAME" type="text" tabindex="4" />
                                    </div>--%>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记人</div>
            <div class="bffld_right">
                <input id="TB_DJRMC" type="text" />
                <input id="HF_DJR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <%--                                    <div class="bffld_left">审核人</div>
                                    <div class="bffld_right">
                                        <input id="TB_ZXRMC" type="text" />
                                        <input id="HF_ZXR" type="hidden" />
                                    </div>--%>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记时间</div>
            <div class="bffld_right">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_DJSJ2\')}'} )" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" />
            </div>
        </div>
    </div>

    <%--                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">审核日期</div>
                                    <div class="bffld_right">
                                        <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_ZXRQ2\')}'} )" />
                                    </div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">----------</div>
                                    <div class="bffld_right">
                                        <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" />
                                    </div>
                                </div>
                            </div>--%>
    <%=V_SearchBodyEnd %>
</body>
</html>
