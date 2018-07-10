<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_FQDYD_Srch.aspx.cs" Inherits="BF.CrmWeb.HYXF.FQDYD.HYXF_FQDYD_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>        
        var lx = IsNullValue(GetUrlParam("lx"), "0");//券类型（BJ_TS:0一般券;1礼品券;2抽奖券;3积分券;4促销积分券;5积分抵现券;）
        var djlx = IsNullValue(GetUrlParam("djlx"), "0");//优惠券定义里的发券类型（FQLX:0:按商品;2:按支付送;3:开卡礼";）
        vPageMsgID = '<%=CM_YHQGL_YHQFFDYD%>'
        switch (djlx+lx) {
            case "00":
                vCaption = "优惠券发放定义单";
                vPageMsgID = '<%=CM_YHQGL_YHQFFDYD%>';
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_CX%>);
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_SH%>);
                break;
            case "01": 
                vCaption = "促销礼品发放定义单"; 
                vPageMsgID = '<%=CM_YHQGL_YHQFFDYD_LP%>';
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_LP_CX%>);
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_LP_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_LP_SH%>);
                break;
            case "02": 
                vCaption = "促销抽奖发放定义单"; 
                vPageMsgID = '<%=CM_YHQGL_YHQFFDYD_CJ%>';
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_CJ_CX%>);
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_CJ_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_CJ_SH%>);
                break;
            case "03": 
                vCaption = "赠送积分定义单"; 
                vPageMsgID = '<%=CM_YHQGL_YHQFFDYD_JF%>';
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_JF_CX%>);
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_JF_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_YHQGL_YHQFFDYD_JF_SH%>);
                
                break;
            case "04": vCaption = "促销积分定义单"; break;

            case "20": vCaption = "优惠券发放定义单支付送"; break;
            case "21": vCaption = "促销礼品发放定义单支付送"; break;
            case "22": vCaption = "促销抽奖发放定义单支付送"; break;
            case "23": vCaption = "赠送积分定义单支付送"; break;
            case "24": vCaption = "促销积分定义单支付送"; break;

            case "33": vCaption = "赠送积分定义单首刷礼"; break;
        }
    </script>
    <script src="HYXF_FQDYD_Srch.js"></script>
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
        <div class="bffld">
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input id="TB_YHQMC" type="text" />
                <input id="HF_YHQID" type="hidden" />
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
            <div class="bffld_left">单据状态</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="djzt" id="R_ZB" value="-1" />
                <label for="R_ZB">全部</label>
                <input class="magic-radio" type="radio" name="djzt" id="R_WZX" value="0" />
                <label for="R_WZX">未审核</label>

                <input class="magic-radio" type="radio" name="djzt" id="R_ZX" value="1" />
                <label for="R_ZX">已审核</label>
                <input class="magic-radio" type="radio" name="djzt" id="R_BC" value="2" />
                <label for="R_BC">已启动</label>
                <input class="magic-radio" type="radio" name="djzt" id="R_ZZ" value="3" />
                <label for="R_ZZ">已终止</label>
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

        <div class="bffld">
            <div class="bffld_left">审核日期</div>
            <div class="bffld_right twodate">
                <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <%--    <div class="bfrow">
    </div>--%>
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
