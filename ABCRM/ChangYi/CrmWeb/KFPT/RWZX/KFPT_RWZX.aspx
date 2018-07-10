<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_RWZX.aspx.cs" Inherits="BF.CrmWeb.KFPT.RWZX.KFPT_RWZX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_KFPT_RWZX%>';
    </script>
    <script src="KFPT_RWZX.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">客服经理</div>
            <div class="bffld_right">
                <label id="LB_PERSON_NAME" style="text-align: left;" runat="server" />
                <input id="TB_RWDX" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">任务主题</div>
            <div class="bffld_right">
                <label id="LB_RWZT" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">任务 </div>
            <div class="bffld_right">
                <textarea name="rw" cols="84" rows="4" id="TT_RW"></textarea>
            </div>
        </div>
    </div>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">任务完成情况 </div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="rwwczt" id="R_WWC" value="0" />
                <label for="R_WWC">未完成</label>
                <input class="magic-radio" type="radio" name="rwwczt" id="R_YWC" value="1" />
                <label for="R_YWC">已完成</label>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">完成情况说明 </div>
            <div class="bffld_right">
                <textarea name="wcqk" cols="84" rows="4" id="TT_WCQK"></textarea>
            </div>
        </div>
    </div>

    <div class="bfrow" style="display: none">
        <div class="bffld">
            <div class="bffld_left">会员卡号 </div>
            <div class="bffld_right">
                <label id="LB_HYKNO" style="text-align: left;" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">会员姓名</div>
            <div class="bffld_right">
                <label id="LB_HYNAME" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>
    <div class='bfrow'>
        <div class='bffld' id='DJR'>
            <div class='bffld_left' id='djr1'>发布人</div>
            <div class='bffld_right'>
                <label id='LB_DJRMC' class='djr'></label>
                <input id="HF_DJR" type="hidden" />
                <label id='LB_DJSJ' class='djsj'></label>
            </div>
        </div>
        <div class='bffld' id='ZXR'>
            <div class='bffld_left' id='zxr1'>执行人</div>
            <div class='bffld_right'>
                <label id='LB_ZXRMC' class='djr'></label>
                <input id="HF_ZXR" type="hidden" />
                <label id='LB_ZXRQ' class='djsj'></label>
            </div>
        </div>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>
