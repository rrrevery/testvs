<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXFL.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXFL.GTPT_WXFL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXPPFL%>';
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXFL.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
         <div class="bffld">
            <div class="bffld_left">分类名称</div>
            <div class="bffld_right">
                <input type="text" id="TB_FLMC" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">序号</div>
            <div class="bffld_right">
                <input type="text" id="TB_INX" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">分类图片</div>
            <div class="bffld_right">
                <input id="TB_IMG" type="text" tabindex="2" />
            </div>
        </div>
      <%-- <div class="bffld">
            <form id="form2" method="post" name="form2" enctype="multipart/form-data">

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
