<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_SCYJTWSC.aspx.cs" Inherits="BF.CrmWeb.GTPT.SCYJTWSC.GTPT_SCYJTWSC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script type="text/javascript">
        vPageMsgID = '<%=CM_GTPT_WXSCYJTWSC%>';

      
    </script>

    <script src="GTPT_SCYJTWSC.js"></script>
    <script src="../../../Js/jquery.form.js"></script>
    <script src='../../../Js/KindEditor/kindeditor.js'></script>
    <script src="../../../Js/KindEditor/lang/zh-CN.js"></script>

</head>
<body>
    
      <%=V_InputBodyBegin %>
     <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
        </div>
    </div>
       <div class="bfrow">


 

         <div class="bffld">
            <form id="form1" method="post" name="form1" enctype="multipart/form-data">
                <span class="img_span">
                    <input type="file" name="file" id="files" />
                </span>
                <input type="button" value="点击上传" id="upload" />
            </form>
        </div>


    </div>

     <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">文字内容</div>
            <div class="bffld_right">
                <textarea id="TB_WZCONTENT"></textarea>
            </div>
        </div>
    </div>
        <%=V_InputBodyEnd %>

      <script>
          KindEditor.ready(function (K) {
              window.editor = K.create('#TB_WZCONTENT');
          });
    </script>

</body>
</html>
