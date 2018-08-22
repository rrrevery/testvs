<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXLSSCSC.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXLSSCSC.GTPT_WXLSSCSC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script type="text/javascript">
        vPageMsgID = '<%=CM_GTPT_WXLSSCSC%>';

      
    </script>
    <script src="GTPT_WXLSSCSC.js"></script>
    <script src="../../../Js/jquery.form.js"></script>



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
        <%=V_InputBodyEnd %>

</body>
</html>
