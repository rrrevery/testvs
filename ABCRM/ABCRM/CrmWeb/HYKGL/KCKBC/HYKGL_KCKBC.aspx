<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_KCKBC.aspx.cs" Inherits="BF.CrmWeb.HYKGL.KCKBC.HYKGL_KCKBC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <style type="text/css">
        button {
            margin: 0px 5px 5px 0px;
            border-radius: 5px;
            border: currentColor;
            border-image: none;
            width: 82px;
            height: 28px;
            font-size: 15px;
            background-color: rgb(60, 148, 210);
        }
    </style>
    <script>
        vPageMsgID = '<%=CM_HYKGL_KCKBC%>';        
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <%=V_WriteCardJS%>
    <script src="HYKGL_KCKBC.js"></script>
    <script src="../../CrmLib/CrmLib_FillBGDD.js"></script>
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
        <div class="bffld" style="display: none;">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input type="text" id="TB_CZKHM" />
                <input type="hidden" id="HF_HYID" />
                <input type="hidden" id="HF_CDNR" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <label id="LB_HYKNAME"></label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">状态</div>
            <div class="bffld_right">
                <label id="LB_STATUS"></label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld" style="display: none;">
            <div class="bffld_left">操作地点</div>
            <div class="bffld_right">
                <input type="text" id="TB_BGDDMC" />
                <input type="hidden" id="HF_BGDDDM" />
                <%--<input type="hidden" id="zHF_BGDDDM" />--%>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">面值金额</div>
            <div class="bffld_right">
                <label id="LB_YCJE"></label>
            </div>
        </div>
    </div>

    <%=V_InputBodyEnd %>
    <div id="menuContent" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
</body>
</html>
