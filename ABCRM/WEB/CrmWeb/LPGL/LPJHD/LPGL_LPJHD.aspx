<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LPGL_LPJHD.aspx.cs" Inherits="BF.CrmWeb.LPGL.LPJHD.LPGL_LPJHD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        var DJLX = GetUrlParam("iDJLX");
        if (DJLX==0){
            vPageMsgID = <%=CM_LPGL_JFFHLPJHD%>;
            bCanEdit = CheckMenuPermit(iDJR,  <%=CM_LPGL_JFFHLPJHD_LR%>);
            bCanExec = CheckMenuPermit(iDJR,  <%=CM_LPGL_JFFHLPJHD_SH%>);
            bCanSrch = CheckMenuPermit(iDJR,  <%=CM_LPGL_JFFHLPJHD_CX%>);
        }
        else if (DJLX==1){
            vPageMsgID = <%=CM_LPGL_JFFHLPTHD%>;
            bCanEdit = CheckMenuPermit(iDJR,  <%=CM_LPGL_JFFHLPTHD_LR%>);
            bCanExec = CheckMenuPermit(iDJR,  <%=CM_LPGL_JFFHLPTHD_SH%>);
            bCanSrch = CheckMenuPermit(iDJR,  <%=CM_LPGL_JFFHLPTHD_CX%>);

        }
    </script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="LPGL_LPJHD.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">供货商</div>
            <div class="bffld_right">
                <input id="TB_GHSMC" type="text" />
                <input id="HF_GHSID" type="hidden" />
                <input id="zHF_GHSID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap" id="BGDDSTR"></div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">总数量</div>
            <div class="bffld_right">
                <label id="LB_ZSL" style="text-align: left">0</label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" id="ChangeTitle"></div>
            <div class="bffld_right">
                <label id="LB_JJJE" style="text-align: left">0</label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">总金额</div>
            <div class="bffld_right">
                <label id="LB_ZJE" style="text-align: left">0</label>
            </div>
        </div>
    </div>
    <div class="bfrow" style="display: none;">
        <div class="bffld">
            <div class="bffld_left" id="JHDWSTR"></div>
            <div class="bffld_right">
                <input id="TB_JHDWMC" type="text" />
                <input id="HF_JHDWID" type="hidden" />
                <input id="zHF_JHDWID" type="hidden" />
            </div>
        </div>
        <div class="bffld" id="DV_JHFS">
            <div class="bffld_left">进货方式</div>
            <div class="bffld_right">
                <select id="DDL_JHFS">
                    <option></option>
                    <option value="0">统采</option>
                    <option value="1">自采</option>
                </select>
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
        <span style="float: left">礼品列表</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加礼品</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除礼品</button>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>
