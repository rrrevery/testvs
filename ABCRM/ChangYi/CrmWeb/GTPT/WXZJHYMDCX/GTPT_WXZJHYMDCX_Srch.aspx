<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXZJHYMDCX_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXZJHYMDCX.GTPT_WXZJHYMDCX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search%>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXZJHYMDCX%>';
    </script>
    <script src="GTPT_WXZJHYMDCX_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">抽奖类型</div>
            <div class="bffld_right">
                <select id="DDL_CJLX">
                    <option value=""></option>
                    <option value="1">抢红包</option>
                    <option value="2">刮刮卡</option>
                    <option value="3">抽奖</option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">抽奖时间</div>
            <div class="bffld_right">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>

    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员姓名</div>
            <div class="bffld_right">
                <input id="TB_HYMC" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">联系电话</div>
            <div class="bffld_right">
                <input id="TB_SJHM" type="text" />
            </div>
        </div>
    </div>


    <%=V_SearchBodyEnd %>
</body>
</html>
