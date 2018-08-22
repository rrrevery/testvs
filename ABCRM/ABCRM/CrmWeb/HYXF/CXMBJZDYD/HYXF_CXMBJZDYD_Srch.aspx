<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_CXMBJZDYD_Srch.aspx.cs" Inherits="BF.CrmWeb.HYXF.CXMBJZDYD.HYXF_CXMBJZDYD_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        var mjjq = IsNullValue(GetUrlParam("mjjq"), "0");
        switch (mjjq) {
            case "0": 
                vCaption = "促销满减定义单";
                vPageMsgID = '<%=CM_YHQGL_CXMBJZDYD%>';
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_YHQGL_CXMBJZDYD_CX%>);
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_YHQGL_CXMBJZDYD_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_YHQGL_CXMBJZDYD_SH%>);                
                break;
            case "1": 
                vCaption = "促销满抵定义单";
                vPageMsgID = '<%=CM_YHQGL_CXMDDYD%>';
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_YHQGL_CXMDDYD_CX%>);
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_YHQGL_CXMDDYD_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_YHQGL_CXMDDYD_SH%>);                
                break;
        }        
    </script>
    <script src="HYXF_CXMBJZDYD_Srch.js"></script>
    <script src="../../CrmLib/CrmLib_FillSHBM.js"></script>
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
            <div class="bffld_left">商户</div>
            <div class="bffld_right" onchange="SHChange()">
                <select id="S_SH" class="easyui-combobox" style="color: #a9a9a9">
                    <option></option>

                </select>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">部门</div>
            <div class="bffld_right">
                <input id="TB_BMMC" type="text" />
                <input id="HF_BMDM" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right twodate">
                <input id="TB_RQ11" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_RQ12" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right twodate">
                <input id="TB_RQ21" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_RQ22" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记人</div>
            <div class="bffld_right">
                <input id="TB_DJRMC" type="text" />
                <input id="HF_DJR" type="hidden" />
                <input id="zHF_DJR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">登记时间</div>
            <div class="bffld_right twodate">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">审核人</div>
            <div class="bffld_right">
                <input id="TB_ZXRMC" type="text" />
                <input id="HF_ZXR" type="hidden" />
                <input id="zHF_ZXR" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">审核日期</div>
            <div class="bffld_right twodate">
                <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">启动人</div>
            <div class="bffld_right">
                <input id="TB_QDRMC" type="text" />
                <input id="HF_QDR" type="hidden" />
                <input id="zHF_QDR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">启动时间</div>
            <div class="bffld_right twodate">
                <input id="TB_QDSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_QDSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">终止人</div>
            <div class="bffld_right">
                <input id="TB_ZZRMC" type="text" />
                <input id="HF_ZZR" type="hidden" />
                <input id="zHF_ZZR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">终止日期</div>
            <div class="bffld_right twodate">
                <input id="TB_ZZRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZZRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
    <div id="menuContent" class="menuContent">
        <ul id="TreeSHBM" class="ztree"></ul>
    </div>
</body>
</html>
