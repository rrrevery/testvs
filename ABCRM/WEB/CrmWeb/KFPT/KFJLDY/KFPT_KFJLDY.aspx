<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_KFJLDY.aspx.cs" Inherits="BF.CrmWeb.KFPT.KFJLDY.KFPT_KFJLDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script type="text/javascript">
        vPageMsgID = '<%=CM_KFPT_KFJLDY%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_KFPT_KFJLDY%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_KFPT_KFJLDY%>');
    </script>
    <script src="KFPT_KFJLDY.js"></script>
    <script src="../../../Js/plupload.full.min.js"></script>
    <script src="../../../Js/zh_CN.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">客服经理</div>
            <div class="bffld_right">
                <input id="HF_KFJL" type="hidden" />
                <input id="TB_KFJLMC" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l" style="width: 100%; height: 60px;">
            <div class="bffld_left" style="width: 8.3%">备注</div>
            <div class="bffld_right">
                <textarea id="TB_BZ" cols="20" rows="3" style="width: 66%; border: 1px #90A9B7 solid; border-radius: 4px; font-size: inherit;"></textarea>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld" style="width: 100%; margin-left: 8.3%">
            所导入的excel文件数据自第二行起，第二列应为'会员卡号', 第三列应为'姓名', 第四列应为'电话'！
        </div>
    </div>

    <div class="bfrow" style="margin-left: 8.3%">
        <button id="AddItem" type='button' class='button'>添加卡</button>
        <button id="DelItem" type='button' class='button'>删除卡</button>
        <button id="ExcelImport" type='button' class='button'>从Excel导入</button>
    </div>
    <div class="bfrow">
        <div class="bffld_left" style="width: 58%; margin-left: 8.3%">
            <table id="list"></table>
            <div id="pager"></div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
