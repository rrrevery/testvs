<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGl_HYDADR.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYDADR.HYKGl_HYDADR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../../Js/plupload.full.min.js"></script>
    <script src="../../../Js/zh_CN.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../CrmLib/CrmLib_BaseImport.js"></script>
    <script>
        vPageMsgID = <%=CM_HYKGL_HYDADR%>;
        bCanEdit = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYDADR_LR%>);
        //  bCanExec = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKGS_SH%>);
        //  bCanSrch = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKGS_CX%>);
    </script>
    <script src="HYKGl_HYDADR.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">文件导入</div>
            <div class="bffld_right">
                <input id="fileCover" type="text" />
                <button id="B_Import" class="bfbtn btn_upload "><%--<i class="fa fa-upload" aria-hidden="true"></i>--%></button>
            </div>
        </div>
    </div>
    <div id="GKDAPanel">
        <div class="clear"></div>
        <div id="zGKDA" class="common_menu_tit slide_down_title">
            <span>顾客档案信息</span>
        </div>

        <div id="zGKDA_Hidden" class="maininput bfborder_bottom">
            <table id="list"></table>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
