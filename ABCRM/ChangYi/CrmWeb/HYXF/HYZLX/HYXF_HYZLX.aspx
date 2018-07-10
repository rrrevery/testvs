<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_HYZLX.aspx.cs" Inherits="BF.CrmWeb.HYXF.HYZLX.HYXF_HYZLX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID=<%=CM_HYXF_HYZDY%>;
    </script>
    <script src="HYXF_HYZLX.js"></script>
</head>
<body>
    <div>
        <div id="TopPanel" class="topbox" >
            <div id="location">
                <div id="switchspace">
                </div>
            </div>
            <div id="btn-toolbar" style="float: right;display:none">
                <div id="more" style="float: right; height: 40px; line-height: 40px; margin-right: 8px; padding-left: 20px;">
                    <i class="fa fa-list-ul fa-lg" aria-hidden="true" style="color: rgb(140,151,157)"></i>
                </div>
            </div>
        </div>
        <div class="bfbox">
            <div class="common_menu_tit" style="display:none">
                <span id='bftitle'></span>
            </div>
            <div class="maininput2">
                <div id='TreePanel' class='bfblock_left2'>
                    <ul id='TreeLeft' class='ztree' style='margin-top: 0;'></ul>
                </div>
                <div id='MainPanel' class='bfblock_right2'>
                    <div class="bffld_l">
                        <div class="bffld_left" >客群组状态</div>
                        <div class="bffld_right" >
                            <input type="checkbox" name="CB_STATUS" value="0" class="magic-checkbox" id="WSH" />
                            <label for="WSH">未审核</label>
                            <input type="checkbox" name="CB_STATUS" value="2" class="magic-checkbox" id="YSH"/>
                            <label for="YSH">审核</label>
                            <input type="checkbox" name="CB_STATUS" value="-1" class="magic-checkbox" id="YDQ"/>
                            <label for="YDQ">到期</label>
                            <input type="checkbox" name="CB_STATUS" value="-2" class="magic-checkbox" id="ZF"/>
                            <label for="ZF">作废</label>
                            <input type="hidden" id="HF_STATUS" />
                        </div>
                    </div>
                    <table id='list'></table>
                    <div class="clear"></div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
