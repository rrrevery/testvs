<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYMMXG_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYMMXG.HYKGL_HYMMXG_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        var TYPE = GetUrlParam("iTYPE");
        if (TYPE == 0) {
            vPageMsgID = '<%=CM_HYKGL_XGPASSWORD %>';
            vCaption = "会员密码修改";
        }
        else {
            vPageMsgID = '<%=CM_HYKGL_MMCZ %>';
            vCaption = "会员密码重置";
        }
    </script>
    <script src="HYKGL_HYMMXG_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" name="" />
            </div>
        </div>
        <div class="bffld">
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
                                    <div class="bffld_left">审核人</div>
                                    <div class="bffld_right">
                                        <input id="TB_ZXRMC" type="text" />
                                        <input id="HF_ZXR" type="hidden" />
                                    </div>
                                </div>
                                <div class="bffld">
                                </div>
                            </div>

                             <div class="bfrow">
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
