<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_HYQYDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.HYQYDY.GTPT_HYQYDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXHYQY%>';
    </script>
         <script src="../../CrmLib/CrmLib_BillWX.js"></script>
     <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../../Js/jquery.form.js"></script>
    <script src="GTPT_HYQYDY.js"></script>
    <script src='../../../Js/KindEditor/kindeditor.js'></script>
    <script src="../../../Js/KindEditor/lang/zh-CN.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">名称</div>
            <div class="bffld_right">
                <input id="TB_NAME" type="text" />
                <input id="HF_JLBH" type="hidden" />

            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">描述</div>
            <div class="bffld_right">
                <input id="TB_HEAD" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">上传LOGO图片 </div>
            <div class="bffld_right">
                <input id="TB_LOGO" type="text" tabindex="2" />
            </div>
        </div>
      <%--  <div class="bffld">
            <form id="form1" method="post" name="form1" enctype="multipart/form-data">
                <span class="img_span">
                    <input type="file" name="file1" id="files1" />
                </span>
                <input type="button" value="点击上传" id="upload1" />
            </form>
        </div>--%>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">上传主图片 </div>
            <div class="bffld_right">
                <input id="TB_IMG" type="text" tabindex="2" />
            </div>
        </div>
      <%--  <div class="bffld">
            <form id="form2" method="post" name="form2" enctype="multipart/form-data">
                <span class="img_span">
                    <input type="file" name="file" id="files" />
                </span>
                <input type="button" value="点击上传" id="upload" />
            </form>
        </div>--%>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">权益内容</div>
            <div class="bffld_right">
                <textarea id="TA_CONTENT"></textarea>
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
    <script>
        KindEditor.ready(function (K) {
            window.editor = K.create('#TA_CONTENT', { uploadJson: "../../CrmLib/CrmLib.ashx?func=upload&folder=sample1" });
        });
    </script>
</body>
</html>
