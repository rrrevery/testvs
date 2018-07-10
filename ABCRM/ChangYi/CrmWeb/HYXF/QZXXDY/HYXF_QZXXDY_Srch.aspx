<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_QZXXDY_Srch.aspx.cs" Inherits="BF.CrmWeb.HYXF.QZXXDY.HYXF_QZXXDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = '<%=CM_HYXF_QZXXDY%>';
    </script>
    <script src="HYXF_QZXXDY_Srch.js"></script>

</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">圈子ID</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">圈子类型</div>
            <div class="bffld_right">
                <select id="S_QZLX">
                    <option></option>
                </select>
                
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">圈子名称</div>
            <div class="bffld_right">
                <input id="TB_QZMC" type="text" tabindex="2" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">成员人数</div>
            <div class="bffld_right">
                <input id="TB_QZRS" type="text" tabindex="3" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" tabindex="3" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
