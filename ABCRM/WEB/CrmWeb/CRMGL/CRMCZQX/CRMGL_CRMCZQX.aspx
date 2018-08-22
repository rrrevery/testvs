<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_CRMCZQX.aspx.cs" Inherits="BF.CrmWeb.CRMGL.CRMCZQX.CRMGL_CRMCZQX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%=V_Head_None %>
    <script src="../../CrmLib/CrmLib_FillCheckTree.js"></script>
    <script>
        var lx = GetUrlParam("czy");
        //if (lx == 1) {
        vPageMsgID = ' <%=CM_CRMGL_CRMQXDY%>';
        //}
        //if (lx == 0) {

        //}
    </script>
    <script src="CRMGL_CRMCZQX.js"></script>
</head>
<body style="overflow: auto;">
    <%=V_NoneBodyBegin %>
    <div id="MainPanel" class="bfbox" style="margin-top: 0px">
        <%--        <div class="common_menu_tit">
            <span>操作权限定义</span>
        </div>--%>
        <div class="maininput2">
            <div class="bfrow" style="display: none;">
                <div class="bffld">
                    <div class="bffld_left">操作员</div>
                    <div class="bffld_right">
                        <input id="TB_DJRMC" type="text" />
                        <input id="HF_DJR" type="hidden" />
                        <input id="zHF_DJR" type="hidden" />
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">操作员组</div>
                    <div class="bffld_right">
                        <input id="TB_HYZ" type="text" />
                        <%--                                <input id="Hidden1" type="hidden" />
                                <input id="Hidden2" type="hidden" />--%>
                    </div>
                </div>
            </div>
            <div class="bfrow">
                <button id="All" tabindex="100" style="height: 28px; width: 70px;" type='button' class='button'>全选</button>
                <button id="None" tabindex="100" style="height: 28px; width: 70px;" type='button' class='button'>全取消</button>
            </div>
            <div class="easyui-tabs" id="tabs" style="width: 100%; height: 250px">
                <div title="卡类型权限" id="klx" style="padding: 10px">
                    <ul id="TreeKLXList" class="ztree" style="margin-top: 0;"></ul>
                </div>
                <div title="保管地点权限" id="bgdd" style="padding: 10px">
                    <ul id="TreeBGDD" class="ztree" style="margin-top: 0;"></ul>
                </div>
                <div title="商户部门权限" id="shbm" style="padding: 10px">
                    <ul id="TreeSHBM" class="ztree" style="margin-top: 0;"></ul>
                </div>
                <div title="门店权限" id="md" style="padding: 10px">
                    <ul id="TreeMDList" class="ztree" style="margin-top: 0;"></ul>
                </div>
                <div title="发行单位权限" id="fxdw" style="padding: 10px">
                    <ul id="TreeFXDW" class="ztree" style="margin-top: 0;"></ul>
                </div>
            </div>
            <%--<ul id="tabsmenu" class="tabsmenu">
                    <li class="active"><a href="#tab1">卡类型权限</a></li>
                    <li><a href="#tab2">保管地点权限</a></li>
                    <li><a href="#tab3">商户部门权限</a></li>
                    <li><a href="#tab4">门店权限</a></li>
                    <li><a href="#tab5">发行单位权限</a></li>
                </ul>
                <div id="tab1" class="tabcontent">

                    <div class="clear"></div>
                </div>
                <div class="clear"></div>
                <div id="tab2" class="tabcontent">
                </div>
                <div id="tab3" class="tabcontent">
                    <div class="clear"></div>
                </div>
                <div id="tab4" class="tabcontent">
                    <div class="clear"></div>
                </div>
                <div id="tab5" class="tabcontent">

                    <div class="clear"></div>
                </div>--%>
        </div>
    </div>
</body>
</html>
