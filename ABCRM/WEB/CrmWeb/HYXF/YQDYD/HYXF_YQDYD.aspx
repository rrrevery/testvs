<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_YQDYD.aspx.cs" Inherits="BF.CrmWeb.HYXF.YQDYD.HYXF_YQDYD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_CXD %>
    <script type="text/javascript">
        vPageMsgID = <%=CM_YHQGL_YHQSYDYD%>
        bCanEdit = CheckMenuPermit(iDJR, "<%=CM_YHQGL_YHQSYDYD_LR%>");
        bCanExec = CheckMenuPermit(iDJR, "<%=CM_YHQGL_YHQSYDYD_SH%>");
        bCanStart = CheckMenuPermit(iDJR, " <%=CM_YHQGL_YHQSYDYD_QD%>");
        bCanStop = CheckMenuPermit(iDJR, "<%=CM_YHQGL_YHQSYDYD_ZZ%>");
    </script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="HYXF_YQDYD.js"></script>
</head>
<body>
    <%=V_NoneBodyBegin %>
    <div class="bfbox">
        <div class="common_menu_tit">
            <span id='bftitle'></span>
        </div>
        <div class="maininput2">
            <div id='TreePanel' class='bfblock_left2'>
            </div>

            <div id='MainPanel' class='bfblock_right2'>
                <div class="bfrow">
                    <div class="bffld" id="jlbh">
                    </div>

                </div>

                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">促销活动</div>
                        <div class="bffld_right">
                            <input id="TB_CXZT" type="text" tabindex="1" />
                            <input id="HF_CXID" type="hidden" />
                            <input id="zHF_CXID" type="hidden" />
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left">返券标记</div>
                        <div class="bffld_right">
                            <input class="magic-checkbox" type="checkbox" id="CK_BJ_TD" value="1" />
                            <label for="CK_BJ_TD"></label>
                        </div>
                    </div>
                </div>

                <div id="tt" class="easyui-tabs" data-options="tools:'#tab-tools'" style="width: 100%; height: 39px">
                </div>
                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left" id="DV_CXSD" no_control="true">促销时段</div>
                        <div class="bffld_right">
                            <label runat="server" style="text-align: left; border-bottom: none" id="LB_CXSDSTR">请选择促销时段</label>
                            <input id="HF_CXSD" type="hidden" />
                        </div>
                    </div>
                </div>
                <div id="status-bar" style="top: 5px"></div>
            </div>
        </div>
    </div>
    <%=V_NoneBodyEnd %>
</body>
</html>
