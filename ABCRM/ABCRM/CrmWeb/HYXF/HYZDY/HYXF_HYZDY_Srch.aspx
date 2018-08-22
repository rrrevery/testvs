<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_HYZDY_Srch.aspx.cs" Inherits="BF.CrmWeb.HYXF.HYZDY.HYXF_HYZDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        var status = GetUrlParam("status");
        if (status == 0) {
            vPageMsgID = '<%=CM_HYXF_HYZDY%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYZDY_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYZDY_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYZDY_CX%>');
        }
        else {
            vPageMsgID = '<%=CM_HYXF_HYZDY_DT%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYZDY_DT_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYZDY_DT_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYZDY_DT_CX%>');
        }
    </script>
    <script src="HYXF_HYZDY_Srch.js"></script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
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
            <div class="bffld_left">客群组类型</div>
            <div class="bffld_right" style="white-space: nowrap;">
                <input type="text" id="TB_HYZLXMC" />
                <input type="hidden" id="HF_HYZLXID" />
            </div>

        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">组名称</div>
            <div class="bffld_right">
                <input id="TB_GRPMC" type="text" tabindex="2" />
            </div>
            <%--<div class="bffld_left">规则方式</div>
                                    <div class="bffld_right">
                                        <input type="checkbox" name="CB_GZFS" value="0" />单张卡
                                        <input type="checkbox" name="CB_GZFS" value="1" />组合规则
                                        <input type="hidden" id="HF_GZFS" />
                                    </div>--%>
        </div>
        <div class="bffld">
            <div class="bffld_left">组用途</div>
            <div class="bffld_right">
                <select id="S_GRPYT">
                    <option></option>
                    <option value="0">促销</option>
                    <option value="1">短信</option>
                    <option value="2">分析</option>
                    <option value="3">其它</option>
                </select>
            </div>

        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">生日方式</div>
            <div class="bffld_right" style="white-space: nowrap;">
                <input type="checkbox" name="CB_SRFS" value="" checked="checked" />全部
                <input type="checkbox" name="CB_SRFS" value="-1" />无
                <input type="checkbox" name="CB_SRFS" value="0" />生日当日
                <input type="checkbox" name="CB_SRFS" value="1" />生日当月
                <input type="checkbox" name="CB_SRFS" value="2" />有效期内生日
                <input type="hidden" id="HF_SRFS" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">客群组状态</div>
            <div class="bffld_right" style="white-space: nowrap">
                <input type="checkbox" name="CB_STATUS" value="" checked="checked" />全部
                <input type="checkbox" name="CB_STATUS" value="0" />未审核
                <input type="checkbox" name="CB_STATUS" value="2" />审核
                <input type="checkbox" name="CB_STATUS" value="-1" />到期
                <input type="checkbox" name="CB_STATUS" value="-2" />作废
                <input type="hidden" id="HF_STATUS" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始时间</div>
            <div class="bffld_right twodate">
                <input id="TB_KSSJ1" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" />
                <span class="Wdate_span">至</span>
                <input id="TB_KSSJ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束时间</div>
            <div class="bffld_right twodate">
                <input id="TB_JSSJ1" type="text" class="Wdate" tabindex="2" onfocus="WdatePicker({skin:'whyGreen'})" />
                <span class="Wdate_span">至</span>
                <input id="TB_JSSJ2" type="text" class="Wdate" tabindex="2" onfocus="WdatePicker({skin:'whyGreen'})" />
            </div>
        </div>
    </div>
    <div class="bfrow" style="display: none">
        <div class="bffld" id="YXQ">
            <div class="bffld_left">更新截止日期</div>
            <div class="bffld_right">
                <input id="TB_YXQ" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" />
            </div>
        </div>
        <div class="bffld" id="GXZQ">
            <div class="bffld_left" style="white-space: nowrap;">更新周期(天)</div>
            <div class="bffld_right">
                <input type="text" id="TB_GXZQ" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">客群门店</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>

        </div>
        <div class="bffld">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">允许修改</div>
            <div class="bffld_right" style="white-space: nowrap">
                <input type="checkbox" name="CB_BJ_YXXG" value="" checked="checked" />全部
                <input type="checkbox" name="CB_BJ_YXXG" value="0" />禁止
                <input type="checkbox" name="CB_BJ_YXXG" value="1" />允许                                        
                <input type="hidden" id="HF_BJ_YXXG" />
            </div>
        </div>
        <div class="bffld" style="display: none">
            <div class="bffld_left" style="white-space: nowrap;">动/静态标记</div>
            <div class="bffld_right" style="white-space: nowrap">
                <input type="checkbox" name="CB_BJ_DJT" value="" disabled="disabled" />全部
                <input type="checkbox" name="CB_BJ_DJT" value="0" disabled="disabled" />静态
                <input type="checkbox" name="CB_BJ_DJT" value="1" disabled="disabled" />动态                                     
                <input type="hidden" id="HF_BJ_DJT" />
            </div>
        </div>
        <div class="bffld" id="WXHY">
            <div class="bffld_left">微信会员</div>
            <div class="bffld_right">
                <input type="checkbox" id="BJ_WXHY" />
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">最后修改人</div>
            <div class="bffld_right">
                <input id="TB_XGRMC" type="text" />
                <input id="HF_XGR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">组描述</div>
            <div class="bffld_right">
                <input type="text" id="TB_GRPMS" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">修改时间</div>
            <div class="bffld_right twodate">
                <input id="TB_XGRQ1" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'} )" />
                <span class="Wdate_span">至</span>
                <input id="TB_XGRQ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" />
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
    <%=V_SearchBodyEnd %>
</body>
</html>
