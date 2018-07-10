<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXCDDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXCDDY.GTPT_WXCDDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Tree %>    
    <script>
        vPageMsgID = '<%=CM_GTPT_WXCDDY%>';
    </script>
    <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXCDDY.js"></script>
</head>
<body>
    <%=V_TreeBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">功能类型</div>
            <div class="bffld_right">
                <input type="radio" value="0" name="RD_GNLX" checked="checked" class="magic-radio" id="YJCD" />
                <label for="YJCD">一级菜单</label>
                <input type="radio" value="1" name="RD_GNLX" class="magic-radio" id="TSMS" />
                <label for="TSMS">推送模式</label>
                <input type="radio" value="2" name="RD_GNLX" class="magic-radio" id="WYMS" />
                <label for="WYMS">网页模式(必填URL)</label>
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">&nbsp;&nbsp;&nbsp;</div>
            <div class="bffld_right">
                <font color='red'>注：发布菜单后手机端有24小时的延时更新,实时更新请取消关注再重新关注!</font>
            </div>
        </div>
    </div>
    <div class="bfrow" id="nbdm">
        <div class="bffld">
            <div class="bffld_left">内部代码</div>
            <div class="bffld_right">
                <select id="DDL_NBDM" class="easyui-combobox">
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow" id="url">
        <div class="bffld">
            <div class="bffld_left">URL</div>
            <div class="bffld_right">
                <input type="text" id="TB_URL" />
            </div>
        </div>
    </div>
    <div class="bfrow" id="urlNote">
        <div class="bffld">
            <div class="bffld_left">&nbsp;&nbsp;&nbsp;</div>
            <div class="bffld_right">
                <font color='red'>注：URL的格式为:http://www.baidu.com 以http://或https://开头</font>
            </div>
        </div>
    </div>
    <div class="bfrow" id="ask">
        <div class="bffld">
            <div class="bffld_left">问题/关键字</div>
            <div class="bffld_right">
                <select id="DDL_WT" class="easyui-combobox">
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow" style="display: none">
        <div class="bffld" id="mjbj">
        </div>
    </div>
    <%=V_TreeBodyEnd %>
</body>
</html>
