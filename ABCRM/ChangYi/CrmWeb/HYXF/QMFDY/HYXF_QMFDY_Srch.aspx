<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_QMFDY_Srch.aspx.cs" Inherits="BF.CrmWeb.HYXF.QMFDY.HYXF_QMFDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_HYXF_HYKYQMJFGZ%>';
    </script>
    <script src="HYXF_QMFDY_Srch.js"></script>
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
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="1" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">

        <div class="bffld">
            <div class="bffld_left">未处理积分/元</div>
            <div class="bffld_right">
                <input id="TB_JF_J" type="text" tabindex="3" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">是否停用</div>
            <div class="bffld_right">
                <input type="checkbox" id="NOTY" name="CB_BJ_TY" value="0" onclick="$('#TY').prop('checked', false);" class="magic-checkbox" />
                <label for="NOTY">否</label>
                <input type="checkbox" id="TY" name="CB_BJ_TY" value="1" onclick="$('#NOTY').prop('checked', false);" class="magic-checkbox"/>
                <label for="TY">是</label>
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
