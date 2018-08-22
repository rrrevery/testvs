<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXZXHDDY_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXZXHDDY.GTPT_WXZXHDDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search%>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXZXHDDY%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXZXHDDY%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXZXHDDY%>');
    </script>
         <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXZXHDDY_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">活动编号</div>
            <div class="bffld_right">
                <input id="TB_HDID" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">活动名称</div>
            <div class="bffld_right">
                <input id="TB_HDMC" type="text" tabindex="2" />

            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">活动简介</div>
            <div class="bffld_right">
                <input id="TB_HDJJ" type="text" tabindex="3" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">活动时间</div>
            <div class="bffld_right">
                <input id="TB_HDSJ" type="text" tabindex="3" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right">
                <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input type="checkbox" name="CB_TY" value="0" />否
                <input type="checkbox" name="CB_TY" value="1" />是
                <input id="DH_HDTY" type="hidden" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
