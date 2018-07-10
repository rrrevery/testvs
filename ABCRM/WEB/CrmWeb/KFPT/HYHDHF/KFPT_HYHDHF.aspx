<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_HYHDHF.aspx.cs" Inherits="BF.CrmWeb.KFPT.HYHDHF.KFPT_HYHDHF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script type="text/javascript">
        vPageMsgID = '<%=CM_KFPT_HYHDHF%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_KFPT_HYHDHF_LR%>');
    </script>
    <script src="KFPT_HYHDHF.js"></script>
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
                <label id="LB_CJRS" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">回访备注</div>
            <div class="bffld_right">
                <input type="text" id="TB_HFBZ" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">回访结果</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="hfjg" id="R_FCMY" value="1" checked="checked" />
                <label for="R_FCMY">非常满意</label>
                <input class="magic-radio" type="radio" name="hfjg" id="R_MY" value="2" />
                <label for="R_MY">满意</label>
                <input class="magic-radio" type="radio" name="hfjg" id="R_YB" value="3" />
                <label for="R_YB">一般</label>
                <input class="magic-radio" type="radio" name="hfjg" id="R_BMY" value="4" />
                <label for="R_BMY">不满意</label>
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
    <div class='bfrow'>
        <div class='bffld'>
            <div class='bffld_left'>回访登记人</div>
            <div class='bffld_right'>
                <label id="LB_HFDJRMC" class='djr'></label>
                <input id="HF_HFDJR" type="hidden" />
                <label id='LB_HFSJ' class='djsj'></label>
            </div>
        </div>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>
