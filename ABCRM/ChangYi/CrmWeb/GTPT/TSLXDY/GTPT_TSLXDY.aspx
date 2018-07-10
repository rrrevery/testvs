<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_TSLXDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.TSLXDY.GTPT_TSLXDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script>
        vPageMsgID = <%=CM_GTPT_TSLXDY%>;
    </script>
    <script src="GTPT_TSLXDY.js"></script>

</head>
<body> 
  <%=V_InputListBegin %>

                            <div class="bfrow" id ="A">
                                <div class="bffld">
                                    <div id="jlbh"></div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">类型名称 </div>
                                    <div class="bffld_right">
                                        <input id="TB_LXMC" type="text" tabindex="2" />
                                    </div>
                                </div>
                            </div>

      <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_BZ" type="text" />
            </div>
        </div>
    </div>
                         
                            
        <%=V_InputListEnd %>

</body>
</html>
