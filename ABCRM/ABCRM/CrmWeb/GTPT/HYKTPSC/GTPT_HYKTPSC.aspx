<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_HYKTPSC.aspx.cs" Inherits="BF.CrmWeb.GTPT.HYKTPSC.GTPT_HYKTPSC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>vPageMsgID = '<%=CM_GTPT_WXHYKTPSC%>';</script>
    <script src="GTPT_HYKTPSC.js"></script>
    <script src="../../../Js/jquery.form.js"></script>

</head>
<body>
    <%=V_InputListBegin %>
    <div class="bfrow" style="display: none">
        <div class="bffld" id="jlbh">
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


    </div>
<%--    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">图片上传</div>
            <div class="bffld_right">
                <form id="form1" method="post" name="form1" enctype="multipart/form-data">
                    <input id="TB_IMG" type="text" />
                    <input type="file" name="file" id="files" style="display: none" />
                    <button id="upload" class="bfbtn btn_upload " onclick="$('input[id=files]').click();"></button>
                </form>
            </div>
        </div>
    </div>--%>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">上传图片 </div>
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


    <%=V_InputListEnd %>
</body>
</html>
