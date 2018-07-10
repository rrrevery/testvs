<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_KCKXXYZ.aspx.cs" Inherits="BF.CrmWeb.HYKGL.KCKYK.HYKGL_KCKYK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
            vPageMsgID = '<%=CM_HYKGL_KCKXXYZ%>';
    </script>
    <script src="HYKGL_KCKXXYZ.js"></script>
</head>
<body>
    <object
        id="rwcard"
        classid="clsid:936CB8A6-052B-4ECA-9625-B8CF4CE51B5F"
        codebase="../../CrmLib/RWCard/BFCRM_RWCard.inf"
        width="0"
        height="0">
    </object>

    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">请刷卡...</div>
            <div class="bffld_right">
                <input id="TB_CDNR" no_control="true" type="password" tabindex="0" />
            </div>
        </div>
        <div class="bffld" id="jlbh">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">通过卡数量</div>
            <div class="bffld_right">
                <label id="LB_SUCNUM">0</label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">错误卡数量</div>
            <div class="bffld_right">
                <label id="LB_FAILNUM">0</label>
            </div>
        </div>
    </div>

    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>
    <div id="tb" class="item_toolbar">
        <span style="float: left">验证列表</span>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>
