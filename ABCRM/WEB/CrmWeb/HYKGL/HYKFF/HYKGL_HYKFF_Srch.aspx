﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKFF_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKFF.HYKGL_HYKFF_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_JFKFF_CX%>';
    </script>
    <script src="HYKGL_HYKFF_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" /><%--默认宽度150?高度25?--%>
                <input type="hidden" id="HF_FS" value="0" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld" style="display: none;">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDDM" type="text" tabindex="3" />
                <input id="HF_BGDDDM" type="hidden" tabindex="3" />
                <input id="zHF_BGDDDM" type="hidden" tabindex="3" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="2" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">操作门店</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">发放人</div>
            <div class="bffld_right">
                <input id="TB_DJRMC" type="text" />
                <input id="HF_DJR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">发放时间</div>
            <div class="bffld_right">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
