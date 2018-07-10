<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKLQ.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKLQ.HYKGL_HYKLQ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script type="text/javascript">
        vCZK = IsNullValue(GetUrlParam("czk"), "0");
        vDJLX = GetUrlParam("djlx");
        if (vCZK=="0"){
            if (vDJLX == "0") {
                vPageMsgID =  <%=CM_HYKGL_HYKLQ%>;
                bCanEdit = CheckMenuPermit(iDJR,  <%=CM_HYKGL_HYKLQ_LR%>);
                bCanExec = CheckMenuPermit(iDJR,  <%=CM_HYKGL_HYKLQ_SH%>);
                bCanSrch = CheckMenuPermit(iDJR,  <%=CM_HYKGL_HYKLQ_CX%>);
            }
            if (vDJLX == "1") {
                vPageMsgID = <%=CM_HYKGL_HYKDB%>;
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKDB_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKDB_SH%>);
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKDB_CX%>);
            }
            if (vDJLX == "2") {
                vPageMsgID = <%=CM_HYKGL_HYKTL%>;
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKTL_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKTL_SH%>);
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKTL_CX%>);
            }

        }
        else{
            if (vDJLX == "0") {
                vPageMsgID =  <%=CM_MZKGL_MZKLQ%>;
                bCanEdit = CheckMenuPermit(iDJR,  <%=CM_MZKGL_MZKLQ_LR%>);
                bCanExec = CheckMenuPermit(iDJR,  <%=CM_MZKGL_MZKLQ_SH%>);
                bCanSrch = CheckMenuPermit(iDJR,  <%=CM_MZKGL_MZKLQ_CX%>);
            }
            if (vDJLX == "1") {
                vPageMsgID = <%=CM_MZKGL_MZKLQ%>;
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKDB_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKDB_SH%>);
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKDB_CX%>);
            }
            if (vDJLX=="2")
            {
                vPageMsgID = <%=CM_MZKGL_MZKTL%>;
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKTL_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKTL_SH%>);
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKTL_CX%>);
            }

        }
    </script>
    <script src="HYKGL_HYKLQ.js"></script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld" id="sqd">
            <div class="bffld_left">申请单选择</div>
            <div class="bffld_right">
                <input id="TB_SQD" type="text" />
                <input id="HF_SQD" type="hidden" />
                <input id="zHF_SQD" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">拨出地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC_BC" type="text" />
                <input id="HF_BGDDDM_BC" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">拨入地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC_BR" type="text" />
                <input id="HF_BGDDDM_BR" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">领取人</div>
            <div class="bffld_right">
                <input id="TB_LQRMC" type="text" tabindex="2" />
                <input id="HF_LQR" type="hidden" />
                <input id="zHF_LQR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">领取数量</div>
            <div class="bffld_right">
                <label id="LB_SL"></label>
            </div>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div style="float: left; width: 33%; text-align: center" id="sqdxx">
        <div id="zMP4" class="common_menu_tit slide_down_title bfborder_top">
            <span>申请单信息</span>
        </div>
        <div id="zMP4_Hidden" class="maininput">
            <div class="bfrow" style="width: 33%">
                <div class="bffld">
                    <div class="bffld_left">&nbsp;</div>
                    <div class="bffld_right">
                        &nbsp;
                    </div>
                </div>
            </div>
            <div class="bfrow" style="width: 33%">
                <div class="bffld">
                    <div class="bffld_left">&nbsp;</div>
                    <div class="bffld_right">
                        &nbsp;
                    </div>
                </div>
            </div>
            <div class="bfrow bfrow_table">
                <table id="list_sqd" style="border: thin"></table>
            </div>

        </div>
    </div>

    <div style="float: left; width: 100%" id="kdxx">
        <div id="zMP5" class="common_menu_tit slide_down_title bfborder_top">
            <span>卡段信息</span>
        </div>
        <div id="zMP5_Hidden" class="maininput">
            <div class="bfrow" id="hyktype">
                <div class="bffld">
                    <div class="bffld_left">卡类型</div>
                    <div class="bffld_right">
                        <input id="TB_HYKNAME" type="text" />
                        <input id="HF_HYKTYPE" type="hidden" />
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">&nbsp;</div>
                    <div class="bffld_right">
                        <button id="AddItem" type='button' class="bfbut bfblue">添加卡段</button>
                        <button id="DelItem" type='button' class="bfbut bfblue">删除卡段</button>
                    </div>
                </div>
            </div>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">开始卡号</div>
                    <div class="bffld_right">
                        <input id="TB_CZKHM_BEGIN" type="text" />
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">结束卡号</div>
                    <div class="bffld_right">
                        <input id="TB_CZKHM_END" type="text" />
                    </div>
                </div>
            </div>
            <%--<div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">领取数量</div>
                    <div class="bffld_right">
                        <input id="TB_SL" type="text" />
                    </div>
                </div>
            </div>--%>
            <div class="bfrow bfrow_table">
                <table id="list" style="border: thin"></table>
            </div>
            <%--                <div id="tb" class="item_toolbar">
                    <span style="float: left">卡段列表</span>
                    <button id="AddItem" type='button' class="item_addtoolbar">添加卡段</button>
                    <button id="DelItem" type='button' class="item_deltoolbar">删除卡段</button>
                </div>--%>
        </div>
    </div>


    <%=V_InputBodyEnd %>
    <%--<div id="menuContent_BC" class="menuContent">
        <ul id="TreeBGDD_BC" class="ztree"></ul>
    </div>
    <div id="menuContent_BR" class="menuContent">
        <ul id="TreeBGDD_BR" class="ztree"></ul>
    </div>
    <div id="menuContentHYKTYPE" class="menuContent">
        <ul id="TreeHYKTYPE" class="ztree"></ul>--%>
    </div>
</body>
</html>
