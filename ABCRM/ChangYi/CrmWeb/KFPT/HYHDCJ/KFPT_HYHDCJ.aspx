<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_HYHDCJ.aspx.cs" Inherits="BF.CrmWeb.KFPT.HYHDCJ.KFPT_HYHDCJ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script type="text/javascript">
        vPageMsgID = '<%=CM_KFPT_HYHDCJ%>';
    </script>
    <script src="KFPT_HYHDCJ.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">活动名称</div>
            <div class="bffld_right">
                <label id="LB_HDMC" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <label id="LB_HYKNO" style="text-align: left;" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">姓名</div>
            <div class="bffld_right">
                <label id="LB_NAME" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">联系电话</div>
            <div class="bffld_right">
                <label id="LB_LXDH" style="text-align: left;" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">证件号码</div>
            <div class="bffld_right">
                <label id="LB_ZJHM" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">报名人数</div>
            <div class="bffld_right">
                <label id="LB_BMRS" style="text-align: left;" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">参加人数</div>
            <div class="bffld_right">
                <input id="TB_CJRS" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">参加备注</div>
            <div class="bffld_right">
                <input id="TB_CJBZ" type="text" />
            </div>
        </div>
    </div>

    <div class='bfrow'>
        <div class='bffld' id='DJR'>
            <div class='bffld_left' id='djr1'>报名登记人</div>
            <div class='bffld_right'>
                <label id='LB_BMDJRMC' class='djr'></label>
                <input id="HF_BMDJR" type="hidden" />
                <label id='LB_BMSJ' class='djsj'></label>
            </div>
        </div>
        <div class='bffld' id='ZXR'>
            <div class='bffld_left' id='zxr1'>参加登记人</div>
            <div class='bffld_right'>
                <label id='LB_CJDJRMC' class='djr'></label>
                <input id="HF_CJDJR" type="hidden" />
                <label id='LB_CJSJ' class='djsj'></label>
            </div>
        </div>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>
