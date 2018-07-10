<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_JFDYD.aspx.cs" Inherits="BF.CrmWeb.HYXF.JFDYD.HYXF_JFDYD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_CXD %>
    <script type="text/javascript">
        vPageMsgID = <%=CM_HYXF_JFGZ%>;
        bCanEdit = CheckMenuPermit(iDJR, <%=CM_HYXF_JFGZ_LR%>);
        bCanExec = CheckMenuPermit(iDJR, <%=CM_HYXF_JFGZ_SH%>);
        bCanStart = CheckMenuPermit(iDJR, <%=CM_HYXF_JFGZ_QD%>);
        bCanStop = CheckMenuPermit(iDJR, <%=CM_HYXF_JFGZ_ZZ%>);
    </script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="HYXF_JFDYD.js"></script>
</head>
<body>
    <%=V_NoneBodyBegin %>
    <div class="bfbox">
        <div class="common_menu_tit">
            <span id='bftitle'></span>
        </div>
        <div class="maininput2">
            <div id='TreePanel' class='bfblock_left2'>
                <%--<div class="bfrow">
                    <div class="bffld_l4">
                        <div class="bffld_right">
                            <select id="S_SH" class="easyui-combobox" style="color: #a9a9a9">
                            </select>
                        </div>
                    </div>
                </div>
                <div class="bfrow">
                    <div class="bffld_l4">
                        <div class="bffld_right">
                            <input id="TB_BM" type="text" placeholder="选择部门" />
                            <input id="HF_SHDM" type="hidden" />
                            <input id="HF_BMDM" type="hidden" />
                        </div>
                    </div>
                </div>
                <div class="bfrow">
                    <div class="bffld_l4">
                        <div class="bffld_right">
                            <input class="magic-radio" type="radio" name="YXD" id="CK_PT" value="0" onclick="SearchDJList()" checked="checked"/>
                            <label for="CK_PT">普通单</label>
                            <input class="magic-radio" type="radio" name="YXD" id="CK_YX" value="1" onclick="SearchDJList()" />
                            <label for="CK_YX">优先单</label>
                        </div>
                    </div>
                </div>
                <div class="bfrow">
                    <div class="bffld_l4">
                        <div class="bffld_left">部门已有单据</div>
                    </div>
                </div>
                <table id='list_dj'></table>--%>
            </div>
            <div id='MainPanel' class='bfblock_right2'>
                <div class="bfrow">
                    <div class="bffld" id="jlbh">
                    </div>
                    <%--<div class="bffld">
                        <div class="bffld_left">商户部门</div>
                        <div class="bffld_right">
                            <label id="LB_SHBM" style="text-align: left;" runat="server" />
                        </div>
                    </div>--%>
                </div>
                <%--<div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">开始日期</div>
                        <div class="bffld_right">
                            <input id="TB_RQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left">结束日期</div>
                        <div class="bffld_right">
                            <input id="TB_RQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                        </div>
                    </div>
                </div>--%>
                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">卡类型</div>
                        <div class="bffld_right">
                            <input id="TB_HYKNAME" type="text" tabindex="1" />
                            <input id="HF_HYKTYPE" type="hidden" />
                            <input id="zHF_HYKTYPE" type="hidden" />
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left">特定积分</div>
                        <div class="bffld_right">
                            <input class="magic-checkbox" type="checkbox" id="CK_BJ_JFBS" value="1" />
                            <label for="CK_BJ_JFBS"></label>
                        </div>
                    </div>
                </div>

                <div id="tt" class="easyui-tabs" data-options="tools:'#tab-tools'" style="width: 100%; height: 39px">
                </div>
                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">分单日期</div>
                        <div class="bffld_right twodate">
                            <input id="TB_FDRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                            <span class="Wdate_span">至</span>
                            <input id="TB_FDRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                        </div>
                    </div>
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
