<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXTPSC.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXTPSC.GTPT_WXTPSC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
        <%=V_Head_InputList %>
    <script>vPageMsgID = '<%=CM_GTPT_WXKBKTPSC%>';</script>

    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXTPSC.js"></script>
    <script src="../GTPTLib/jquery.form.js"></script>
</head>
<body>
      <%=V_InputListBegin %>
                                <div class="bfrow" style="display:none" >
                                    <div class="bffld" id="jlbh">
                                    </div>
                                    <div class="bffld">
                                    </div>
                                </div>

                                <div class="bfrow">
                                    <div class="bffld">
                                        <div class="bffld_left">图片名称</div>
                                        <div class="bffld_right">
                                            <input id="TB_NAME" type="text" tabindex="1" />
                                            <input id="HF_ID" type="hidden" />
                                        </div>
                                    </div>
                                </div>

     <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">图片上传</div>
            <div class="bffld_right">
                    <input id="TB_IMG" type="text" />
                  
            </div>
        </div>
    </div>


                       <%=V_InputListEnd %>

</body>
</html>

