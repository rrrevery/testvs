<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmArt_ArtCLDX.aspx.cs" Inherits="BF.CrmWeb.CrmArt.ArtCLDX.CrmArt_ArtCLDX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_WebArt%>
    <script>
        vBrandPageId = '<%=CM_CRMGL_SPSBCX%>';
        vContractPageId = '<%=CM_CRMGL_SHHTCX%>';
        vGoodsPageId = '<%=CM_CRMGL_SHSPCX%>';
    </script>
    <script src="../../../Js/datagrid-dnd.js"></script>
    <script src="CrmArt_ArtCLDX.js"></script>
</head>
<body style="height: auto; overflow-y: hidden">
    <div id="MainPanel" class="bfbox zero_padding">
        <%=V_ArtToolBar%>

        <div id="tt" class="easyui-tabs" style="width: 100%; height: 500px;">
            <div title="部门" style="padding: 20px; display: none;">
                <div id="TreePanel" class="zTreeArt">
                    <ul id="treeBM" class="ztree"></ul>
                </div>
            </div>
            <div title="合同" data-options="closable:false" style="padding: 0px; display: none;">
                <div class="bfrow">
                    <div class="bffld_art">
                        <div class="bffld_left">合同号</div>
                        <div class="bffld_right">
                            <input id="TB_HTH" type="text" />
                        </div>
                    </div>
                    <div class="bffld_art">
                        <div class="bffld_left">供货商名称</div>
                        <div class="bffld_right">
                            <input id="TB_GHSMC" type="text" />
                        </div>
                    </div>
                </div>
                <table id="list_contract"></table>
            </div>

            <div title="商品分类" data-options="closable:false" style="padding: 20px; display: none;">
                <div id="TreePanelFL" class="zTreeArt">
                    <ul id="treeFL" class="ztree"></ul>
                </div>
            </div>

            <div title="商品商标" data-options="closable:false" style="display: none;">
                <div class="bfrow">
                    <div class="bffld_art">
                        <div class="bffld_left">商标名称</div>
                        <div class="bffld_right">
                            <input id="TB_SBMC" type="text" />
                        </div>
                    </div>
                    <div class="bffld_art">
                        <div class="bffld_left">拼音码</div>
                        <div class="bffld_right">
                            <input id="TB_SBPYM" type="text" />
                        </div>
                    </div>
                </div>
                <table id="list_brand"></table>
            </div>

            <%--<div title="客群组" data-options="closable:false" style="padding: 20px; display: none;">
            </div>--%>

            <div title="商品" data-options="closable:false" style="padding: 0px; display: none;">
                <div class="bfrow">
                    <div class="bffld_art">
                        <div class="bffld_left">商品代码</div>
                        <div class="bffld_right">
                            <input id="TB_SPDM" type="text" />
                        </div>
                    </div>
                    <div class="bffld_art">
                        <div class="bffld_left">商品名称</div>
                        <div class="bffld_right">
                            <input id="TB_SPMC" type="text" />
                        </div>
                    </div>
                </div>
                <table id="list_goods"></table>
            </div>

        </div>
    </div>
</body>
</html>
