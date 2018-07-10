<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_CXMBJZDYD.aspx.cs" Inherits="BF.CrmWeb.HYXF.CXMBJZDYD.HYXF_CXMBJZDYD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_CXD %>
    <script type="text/javascript">
        var mjjq = IsNullValue(GetUrlParam("mjjq"), "0");
        switch (mjjq) {
            case "0": 
                vCaption = "促销满减定义单";
                vPageMsgID = '<%=CM_YHQGL_CXMBJZDYD%>';
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_YHQGL_CXMBJZDYD_CX%>);
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_YHQGL_CXMBJZDYD_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_YHQGL_CXMBJZDYD_SH%>);                
                bCanStart = CheckMenuPermit(iDJR, <%=CM_YHQGL_CXMBJZDYD_QD%>);
                bCanStop = CheckMenuPermit(iDJR, <%=CM_YHQGL_CXMBJZDYD_ZZ%>);
                break;
            case "1": 
                vCaption = "促销满抵定义单";
                vPageMsgID = '<%=CM_YHQGL_CXMDDYD%>';
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_YHQGL_CXMDDYD_CX%>);
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_YHQGL_CXMDDYD_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_YHQGL_CXMDDYD_SH%>);                
                bCanStart = CheckMenuPermit(iDJR, <%=CM_YHQGL_CXMDDYD_QD%>);
                bCanStop = CheckMenuPermit(iDJR, <%=CM_YHQGL_CXMDDYD_ZZ%>);
                break;
        }        
    </script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="HYXF_CXMBJZDYD.js"></script>
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
                        <div class="bffld_left">累计方式</div>
                        <div class="bffld_right">
                            <input class="magic-radio" type="radio" name="LJFS" id="CK_DB" value="0" />
                            <label for="CK_DB">单笔</label>
                            <input class="magic-radio" type="radio" name="LJFS" id="CK_DJ" value="3" />
                            <label for="CK_DJ">单件</label>
                            <input class="magic-radio" type="radio" name="LJFS" id="CK_DPP" value="6" />
                            <label for="CK_DPP">单笔单品牌</label>
                            <input class="magic-radio" type="radio" name="LJFS" id="CK_DHT" value="6" />
                            <label for="CK_DHT">单笔单合同</label>
                            <%--<input type="hidden" id="HF_XFLJMJFS" />--%>
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
