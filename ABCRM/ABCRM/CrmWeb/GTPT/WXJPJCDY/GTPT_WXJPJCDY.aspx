<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXJPJCDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXJPJCDY.GTPT_WXJPJCDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXJPJCDY%>';
    </script>
    <script src="GTPT_WXJPJCDY.js"></script>
</head>
<body>
      <%=V_InputListBegin %>

                            <div class="bfrow" id="A">
                                <div class="bffld">
                                    <div id="jlbh"></div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">级次名称 </div>
                                    <div class="bffld_right">
                                        <input id="TB_MC" type="text" tabindex="2" />
                                    </div>
                                </div>
                            </div>      
            <%=V_InputListEnd %>

</body>
</html>
