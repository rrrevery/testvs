<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YHQGL_YXRL_Srch.aspx.cs" Inherits="BF.CrmWeb.YHQGL.YXRL.YHQGL_YXRL_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_YHQGL_CXHDZT%>';
    </script>
    <link href="../../../Css/fullcalendar.css" rel="stylesheet" />
    <script src="../../../Js/moment.min.js"></script>
    <script src="../../../Js/fullcalendar.js"></script>
    <script src="YHQGL_YXRL_Srch.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <style type="text/css">
        body {
            margin: 40px 10px;
            background: #ffffff;
        }

        .fc-day-grid-event .fc-time {
            display: none;
        }

        .fc-time-grid-event .fc-time {
            display: none;
        }

        /*#calendar {
            max-width: 850px;
            margin: 0 auto;
            background: #ffffff;
            width: 960px;
            margin: 20px auto 10px auto;
        }*/
    </style>

</head>
<body>
    <div>
        <div id="TopPanel" class="topbox">
            <div id="location" style="width: 400px; height: 40px; display: block; float: left; line-height: 40px;">
                <div id="switchspace" style="width: 50px; height: 40px; display: block; float: left">
                </div>
            </div>
            <div id="btn-toolbar" style="float: right;display:none">
                <div id="more" style="float: right; height: 40px; line-height: 40px; margin-right: 8px; padding-left: 20px;">
                    <i class="fa fa-list-ul fa-lg" aria-hidden="true" style="color: rgb(140,151,157)"></i>
                </div>
            </div>
        </div>
        <div id="MainPanel" class="bfbox">
            <div id='calendar'></div>
            <div style="display:none">
                <table id='list'></table>
            </div> 
        </div>        
    </div>
</body>
</html>
