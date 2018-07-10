<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXLCDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXLCDY.GTPT_WXLCDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script type="text/javascript">
        vPageMsgID = '<%=CM_GTPT_WXLCDY%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXLCDY%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXLCDY%>');
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXLCDY.js"></script>
    <script src="../GTPTLib/Plupload/jquery.form.js"></script>

</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input id="TB_WXMDMC" type="text" />
                <input id="HF_WXMDID" type="hidden" />
                <input id="zHF_WXMDID" type="hidden" />
            </div>
        </div>

    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">序号</div>
            <div class="bffld_right">
                <input id="TB_INX" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">楼层名称</div>
            <div class="bffld_right">
                <input id="TB_NAME" type="text" />
            </div>
        </div>
    </div>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">上传楼层图片</div>
            <div class="bffld_right">
                <input id="TB_IMG" type="text" tabindex="2" />
            </div>
        </div>
       <%-- <div class="bffld">
            <form id="form1" method="post" name="form1" enctype="multipart/form-data">

                <span class="img_span">
                    <input type="file" name="file" id="files" />
                </span>
                <input type="button" value="点击上传" id="upload" />
            </form>
        </div>--%>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
