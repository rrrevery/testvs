<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectLMSH.aspx.cs" Inherits="BF.CrmWeb.CrmLib.SelectLMSH.SelectLMSH" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="../CrmLib_SimSelect.js"></script>
    <script>
        vPageMsgID = '<%=CM_HYKGL_LMSHDY%>';
    </script>
    <script src="SelectLMSH.js"></script>
</head>
<body>
    <div id="panelwrap">
        <div class="center_content">
            <div id="right_wrap">
                <div id="right_content">
                    <ul id="tabsmenu" class="tabsmenu">
                        <li class="active"><a href="#tab1">联盟商户</a></li>
                    </ul>
                    <div id="tab1" class="tabcontent">
                        <div id="btn-toolbar">
                        </div>
                        <div class="form_s">
                            <div class="form_row_s">
                                <div class="div1_s">
                                    <div class="dv_sub_s">
                                        <input id="TB_JLBH" type="text" tabindex="1" placeholder="记录编号" style="width:195px"/>
                                    </div>
                                </div>
                                <div class="div2_s">
                                    <div class="dv_sub_s">
                                        <input id="TB_SHMC" type="text" tabindex="1" placeholder="商户名称" style="width:195px" />
                                    </div>
                                </div>
                            </div>

                            <div class="form_row_s">
                                <table id="list"></table>
                                <div id="pager"></div>
                            </div>
                        </div>
                        <div class="clear"></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</body>
</html>
