<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKHK_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKHK.HYKGL_HYKHK_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%=V_Head_Search %>
    <script>
        var vMZK = IsNullValue(GetUrlParam("mzk"),"0");
        if(vMZK=="0")
        {
            vPageMsgID = <%=CM_HYKGL_HYKHK%>
            bCanEdit = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKHK_LR%>);
            bCanExec = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKHK_SH%>);
            bCanSrch = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKHK_CX%>);
            vCaption =  "会员卡换卡";

        }
        if(vMZK=="1")
        {
            vPageMsgID = <%=CM_MZKGL_MZKHK%>;
            bCanEdit = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKHK_LR%>);        
            bCanExec = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKHK_SH%>);
            bCanSrch = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKHK_CX%>);
            vCaption =  "面值卡换卡" ;
        }

    </script>
    <script src="HYKGL_HYKHK_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" tabindex="1" />
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld" style="display: none">
            <div class="bffld_left">操作门店</div>
            <div class="bffld_right">
                <input type="text" id="TB_MDMC" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">换卡原因</div>
            <div class="bffld_right">
                <select id="DDL_HKYY" name="hkyy">
                    <option value="" selected="selected">全部</option>
                    <option value="1">卡遗失换卡（收费）</option>
<%--                    <option value="2">会员升级换卡(免费)</option>--%>
                    <option value="3">电子会员换实体卡(免费)</option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">姓名</div>
            <div class="bffld_right">
                <input id="TB_HYNAME" type="text" tabindex="4" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">旧卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKHM_OLD" type="text" tabindex="2" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">新卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKHM_NEW" type="text" tabindex="3" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">操作地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="zHF_BGDDDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="4" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" type="text" tabindex="6" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">证件号码</div>
            <div class="bffld_right">
                <input id="TB_SFZBH" type="text" tabindex="6" />
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
            <div class="bffld_right">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
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
            <div class="bffld_right">
                <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>

    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
