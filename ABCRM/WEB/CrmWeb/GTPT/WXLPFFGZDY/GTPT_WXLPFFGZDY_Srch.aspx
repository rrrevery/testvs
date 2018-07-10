<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXLPFFGZDY_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXLPFFGZDY.GTPT_WXLPFFGZDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
            <title></title>

    <%=V_Head_Search %>
    <script>
        vPageMsgID = <%=CM_GTPT_WXLBFFGZ%>
      
    </script>
    <script src="GTPT_WXLPFFGZDY_Srch.js?ts=1"></script>
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
        <div class="bffld">
        
                <div class="bffld_left">规则名称</div>
                <div class="bffld_right">
                    <input id="TB_GZMC" type="text" />

                </div>
           
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">规则类型</div>
            <div class="bffld_right">
                <select id="DDL_GZLX">
                    <option></option>
                    <option value="2">抽奖</option>
                    <option value="1">抢红包</option>
                    <option value="3">刮刮卡</option>

                </select>
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
    <%=V_SearchBodyEnd %>

</body>
</html>
