<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_KCKYXQBD.aspx.cs" Inherits="BF.CrmWeb.HYKGL.KCKYXQBD.HYKGL_KCKYXQBD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vCZK = GetUrlParam("czk");
        if (vCZK == "0") {
            vPageMsgID =  <%=CM_HYKGL_KCKYXQBG%>;
            bCanEdit = CheckMenuPermit(iDJR,  <%=CM_HYKGL_KCKYXQBG_LR%>);
            bCanExec = CheckMenuPermit(iDJR,  <%=CM_HYKGL_KCKYXQBG_SH%>);
            bCanSrch = CheckMenuPermit(iDJR,  <%=CM_HYKGL_KCKYXQBG_CX%>);                                                    
        }
        if (vCZK == "1") {
            vPageMsgID = <%=CM_MZKGL_KCKYXQBG%>;
            bCanEdit = CheckMenuPermit(iDJR, <%=CM_MZKGL_KCKYXQBG_LR%>);
            bCanExec = CheckMenuPermit(iDJR, <%=CM_MZKGL_KCKYXQBG_SH%>);
            bCanSrch = CheckMenuPermit(iDJR, <%=CM_MZKGL_KCKYXQBG_CX%>);                        
        }     
    </script>
    <script src="HYKGL_KCKYXQBD.js"></script>
    <script src="../../CrmLib/CrmLib_FillHYKTYPE.js"></script>
    <script src="../../CrmLib/CrmLib_FillBGDD.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" tabindex="2" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="1" />
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">新有效期</div>
            <div class="bffld_right">
                <input class="Wdate" type="text" onfocus="WdatePicker()" id="TB_XYXQ" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" type="text" />
            </div>
        </div>
    </div>


    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">卡号列表</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加卡</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除卡</button>
    </div>

    <%=V_InputBodyEnd %>
        <div id="menuContentHYKTYPE" class="menuContent">
        <ul id="TreeHYKTYPE" class="ztree"></ul>
    </div>
        <div id="menuContent" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
</body>
</html>
