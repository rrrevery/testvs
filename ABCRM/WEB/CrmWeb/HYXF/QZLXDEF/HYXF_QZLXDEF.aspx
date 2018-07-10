<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_QZLXDEF.aspx.cs" Inherits="BF.CrmWeb.HYXF.QZLXDEF.HYXF_QZLXDEF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = <%=CM_HYXF_QZLXDEF%>;
    </script>
    <script src="HYXF_QZLXDEF.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">门店</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">圈子类型名称</div>
            <div class="bffld_right">
                <input id="TB_QZLXMC" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">圈子成员人数</div>
            <div class="bffld_right">
                <input id="TB_QZCYRS" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">状态</div>
            <div class="bffld_right">
                <input type="checkbox" id="NOTY" name="CB_STATUS" value="0"  class="magic-checkbox" />
                <label for="NOTY">有效</label>
                <input type="checkbox" id="TY" name="CB_STATUS" value="-1"  class="magic-checkbox"/>
                <label for="TY">无效</label>
                <input type="hidden" id="HF_STATUS" value="0" />
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
