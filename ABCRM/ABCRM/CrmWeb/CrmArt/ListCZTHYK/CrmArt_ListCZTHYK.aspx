<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmArt_ListCZTHYK.aspx.cs" Inherits="BF.CrmWeb.CrmArt.ListCZTHYK.CrmArt_ListCZTHYK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_WebArtList%>
    <script>
        vPageMsgID = <%=CM_HYKGL_HYKCX%>;
    </script>
    <script src="CrmArt_ListCZTHYK.js"></script>

    <style>
        .cztleft {
            float: left;
            width: 35%;
            box-sizing: border-box;
            padding-right: 0px;
            margin-right: 0px;
        }

        .cztright {
            float: left;
            width: 65%;
            box-sizing: border-box;
            padding-left: 0px;
            border-left: 1px solid #dce1e4;
        }

        .bffld {
            width: 90%;
            margin: 14px 0;
        }

            .bffld .bffld_right {
                width: 60%;
            }
    </style>
</head>
<body style="height: auto; overflow-y: hidden">

    <%=V_ArtToolBar%>
    <div class="cztleft">
        <div id="MainPanel" class="bfbox zero_padding">
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">身份证</div>
                    <div class="bffld_right">
                        <input id="TB_SFZBH" type="text" />
                    </div>
                </div>
            </div>

            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">卡号</div>
                    <div class="bffld_right">
                        <input id="TB_HYKNO" type="text" />
                    </div>
                </div>
            </div>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">原卡号</div>
                    <div class="bffld_right">
                        <input id="TB_HYKNO_OLD" type="text" />
                    </div>
                </div>
            </div>

            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">姓名</div>
                    <div class="bffld_right">
                        <input id="TB_NAME" type="text" />
                    </div>
                </div>
            </div>

            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">手机号</div>
                    <div class="bffld_right">
                        <input id="TB_SJHM" type="text" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="cztright">
        <table id="list"></table>
    </div>

</body>
</html>
